using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using TinyFx.IO;
using TinyFx.Logging;

namespace TinyFx.Reflection
{
    /// <summary>
    /// 反射辅助方法类
    /// </summary>
    public static class ReflectionUtil
    {
        #region AssemblyInfo.cs
        /// <summary>
        /// 获取AssemblyInfo.cs中定义的Product
        /// </summary>
        /// <param name="asm"></param>
        /// <returns></returns>
        public static AssemblyProductAttribute GetAssemblyProduct(Assembly asm)
            => asm.GetCustomAttribute<AssemblyProductAttribute>();

        /// <summary>
        /// 获取AssemblyInfo.cs中定义的版本信息
        /// </summary>
        /// <param name="asm"></param>
        /// <returns></returns>
        public static Version GetAssemblyVersion(Assembly asm)
            => asm.GetName().Version;

        /// <summary>
        /// 获取指定Assembly的GUID
        /// </summary>
        /// <param name="asm"></param>
        /// <returns></returns>
        public static string GetAssemblyGuidString(Assembly asm)
        {
            string ret = null;
            GuidAttribute[] attrs = (GuidAttribute[])asm.GetCustomAttributes(typeof(GuidAttribute), false);
            if (attrs != null && attrs.Length > 0)
                ret = attrs[0].Value;
            return ret;
        }
        #endregion

        #region PrimitiveType & SimpleType & MapToJsType
        /// <summary>
        /// 类型是否是基元类型
        /// https://msdn.microsoft.com/en-us/library/system.type.isprimitive.aspx
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool IsPrimitiveType(Type type)
            => SimpleTypeNames.PrimitiveTypes.Contains(type.FullName);

        /// <summary>
        /// 简单类型：基元类型 + TimeSpan + DateTime + Guid + Decimal + String + Byte[] + 任何可从字符串转入的对象（暂未加入判断）
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool IsSimpleType(Type type)
            => SimpleTypeNames.PrimitiveTypes.Contains(type.FullName) || SimpleTypeNames.SimpleTypes.Contains(type.FullName);

        private static Type[] valueTypes = new Type[] {typeof(byte),typeof(sbyte),typeof(short),typeof(ushort),
            typeof(int),typeof(uint),typeof(long),typeof(ulong),typeof(float),typeof(double),typeof(decimal),
            typeof(bool),typeof(string),typeof(char),typeof(Guid),typeof(DateTime),typeof(DateTimeOffset),
            typeof(TimeSpan),typeof(TimeOnly),typeof(DateOnly),typeof(DBNull)};
        public static bool IsEntityType(this Type type, out Type underlyingType)
        {
            underlyingType = type;
            if (valueTypes.Contains(type) || type.FullName == "System.Data.Linq.Binary")
                return false;
            underlyingType = Nullable.GetUnderlyingType(type) ?? type;
            if (valueTypes.Contains(underlyingType) || underlyingType.FullName == "System.Data.Linq.Binary" || underlyingType.IsEnum)
                return false;
            if (type.IsArray)
            {
                var elementType = type.GetElementType();
                return elementType!.IsEntityType(out underlyingType);
            }
            if (type.IsGenericType)
            {
                if (type.FullName.StartsWith("System.ValueTuple`") && type.GenericTypeArguments.Length == 1)
                    return false;
                if (typeof(IEnumerable).IsAssignableFrom(type))
                {
                    if (typeof(IDictionary).IsAssignableFrom(type))
                        return true;
                    foreach (var elementType in type.GenericTypeArguments)
                    {
                        if (elementType.IsEntityType(out underlyingType))
                            return true;
                    }
                    return false;
                }
            }
            return true;
        }

        // .Net简单类型 => JS类型 映射缓存
        private static readonly Dictionary<string, string> _jsTypeMapCache = new Dictionary<string, string>() {
            { "System.Sbyte", "Number" },
            { "System.Byte", "Number"},
            { "System.Int16", "Number"},
            { "System.UInt16", "Number"},
            { "System.Int32", "Number"},
            { "System.UInt32", "Number"},
            { "System.Int64", "Number"},
            { "System.UInt64", "Number"},
            { "System.Single", "Number"},
            { "System.Double", "Number"},
            { "System.Decimal", "Number"},
            { "System.Boolean", "Boolean"},
            { "System.Char", "String"},
            { "System.String", "String"},
            { "System.DateTime", "String"},
            { "System.TimeSpan", "String"},
            { "System.Enum", "String"},
            { "System.Guid", "String"},
            { "System.Object", "Object"}
        };

        /// <summary>
        /// 获得.NET类型映射的JS类型
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string MapToJsType(Type type)
        {
            string ret = null;
            string key = type.FullName;
            if (_jsTypeMapCache.ContainsKey(key))
                return _jsTypeMapCache[key];
            if (typeof(ICollection).IsAssignableFrom(type) || typeof(IEnumerable<>).IsAssignableFrom(type))
                return "Array";
            return ret;
        }
        #endregion

        #region CreateInstance
        internal delegate T ObjectActivator<out T>(params object[] args);
        public static Delegate GetActivator<T>(ConstructorInfo ctor)
        {
            Type type = ctor.DeclaringType;
            ParameterInfo[] paramsInfo = ctor.GetParameters();

            //create a single param of type object[]
            ParameterExpression param =
                Expression.Parameter(typeof(object[]), "args");

            var argsExp =
                new Expression[paramsInfo.Length];

            //pick each arg from the params array 
            //and create a typed expression of them
            for (int i = 0; i < paramsInfo.Length; i++)
            {
                Expression index = Expression.Constant(i);
                Type paramType = paramsInfo[i].ParameterType;

                Expression paramAccessorExp =
                    Expression.ArrayIndex(param, index);

                Expression paramCastExp =
                    Expression.Convert(paramAccessorExp, paramType);

                argsExp[i] = paramCastExp;
            }

            //make a NewExpression that calls the
            //ctor with the args we just created
            NewExpression newExp = Expression.New(ctor, argsExp);

            //create a lambda with the New
            //Expression as body and our param object[] as arg
            LambdaExpression lambda =
                Expression.Lambda(typeof(ObjectActivator<T>), newExp, param);

            //compile it
            return lambda.Compile();
        }
        // 构造函数缓存
        private static readonly ConcurrentDictionary<string, Delegate> ObjectActivators = new ConcurrentDictionary<string, Delegate>();
        public static object CreateInstance(Type type, params object[] args)
        {
            if (!ObjectActivators.TryGetValue(type.FullName, out Delegate activator))
            {
                var constructors = type.GetConstructors();
                if (constructors.Count() == 1)
                    activator = GetActivator<object>(constructors.First());
                ObjectActivators.AddOrUpdate(type.FullName, activator, (k, v) => v);
            }
            return (activator == null)
                ? Activator.CreateInstance(type, args)
                : ((ObjectActivator<object>)activator)(args);
        }
        /// <summary>
        /// 根据创建类实例(只有一个构造函数时使用lambda表达式)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T CreateInstance<T>(params object[] args)
            => (T)CreateInstance(typeof(T), args);
        /// <summary>
        /// 根据类型名称创建实例
        /// </summary>
        /// <param name="typeName"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static object CreateInstance(string typeName, params object[] args)
            => CreateInstance(Type.GetType(typeName), args);
        #endregion

        #region InvokeMethod
        public static object InvokeMethod(string typeName, string methodName, params object[] args)
        {
            var type = Type.GetType(typeName);
            var method = type.GetMethod(methodName);
            var obj = Activator.CreateInstance(type);
            return method.Invoke(obj, args);
        }
        public static object InvokeStaticMethod(string typeName, string methodName, params object[] args)
        {
            var type = Type.GetType(typeName);
            var method = type.GetMethod(methodName);
            return method.Invoke(null, args);
        }
        #endregion

        #region GetPropertyValue
        //private static readonly ConcurrentDictionary<PropertyInfo, MethodInfo> _propertyGetterCache = new ConcurrentDictionary<PropertyInfo, MethodInfo>();
        private static readonly ConcurrentDictionary<int, Func<object, object>> _propertyValueGetterCache = new();
        /// <summary>
        /// 通过反射获取对象属性值
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="property"></param>
        /// <returns></returns>
        public static object GetPropertyValue(this object obj, PropertyInfo property)
        {
            return obj.GetPropertyValue(property.Name);

            //if (!_propertyGetterCache.TryGetValue(property, out MethodInfo ret))
            //{
            //    ret = property.GetGetMethod();
            //    _propertyGetterCache.TryAdd(property, ret);
            //}
            //return ret.Invoke(obj, null);
        }

        private static readonly ConcurrentDictionary<string, MethodInfo> _propertyNameGetterCache = new ConcurrentDictionary<string, MethodInfo>();
        /// <summary>
        /// 通过反射获取对象属性值
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public static object GetPropertyValue(this object obj, string propertyName)
        {
            var entityType = obj.GetType();
            var hashKey = HashCode.Combine(entityType, propertyName);
            if (!_propertyValueGetterCache.TryGetValue(hashKey, out var memberGetter))
            {
                var objExpr = Expression.Parameter(typeof(object), "entity");
                var typedObjExpr = Expression.Convert(objExpr, entityType);
                var bodyExpr = Expression.Convert(Expression.PropertyOrField(typedObjExpr, propertyName), typeof(object));
                memberGetter = Expression.Lambda<Func<object, object>>(bodyExpr, objExpr).Compile();
                _propertyValueGetterCache.TryAdd(hashKey, memberGetter);
            }
            return memberGetter.Invoke(obj);

            //var key = $"{obj.GetType().FullName}:{propertyName}";
            //if (!_propertyNameGetterCache.TryGetValue(key, out MethodInfo ret))
            //{
            //    var property = obj.GetType().GetProperty(propertyName);
            //    ret = property.GetGetMethod();
            //    _propertyNameGetterCache.TryAdd(key, ret);
            //}
            //return ret.Invoke(obj, null);
        }

        /// <summary>
        /// 通过反射获取对象属性值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public static T GetPropertyValue<T>(this object obj, string propertyName)
            => TinyFxUtil.ConvertTo<T>(GetPropertyValue(obj, propertyName));
        #endregion

        #region SetPropertyValue
        private static readonly ConcurrentDictionary<PropertyInfo, MethodInfo> _propertySetterCache = new ConcurrentDictionary<PropertyInfo, MethodInfo>();
        /// <summary>
        /// 通过反射设置对象属性值
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="property"></param>
        /// <param name="value"></param>
        public static void SetPropertyValue(this object obj, PropertyInfo property, object value)
        {
            obj.SetPropertyValue(property.Name, value);
            //if (!_propertySetterCache.TryGetValue(property, out MethodInfo ret))
            //{
            //    ret = property.GetSetMethod();
            //    _propertySetterCache.TryAdd(property, ret);
            //}
            //ret.Invoke(obj, new object[] { value });
        }
        //private static readonly ConcurrentDictionary<string, MethodInfo> _propertyNameSetterCache = new ConcurrentDictionary<string, MethodInfo>();
        private static readonly ConcurrentDictionary<int, Action<object, object>> _propertyValueSetterCache = new();
        /// <summary>
        /// 通过反射设置对象属性值
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="propertyName"></param>
        /// <param name="value"></param>
        public static void SetPropertyValue(this object obj, string propertyName, object value)
        {
            var entityType = obj.GetType();
            var valueType = value.GetType();
            var hashKey = HashCode.Combine(entityType, propertyName, valueType);
            if (!_propertyValueSetterCache.TryGetValue(hashKey, out var valueSetter))
            {
                var objExpr = Expression.Parameter(typeof(object), "entity");
                var valueExpr = Expression.Parameter(typeof(object), "value");
                var typedObjExpr = Expression.Convert(objExpr, entityType);
                var propertyInfo = entityType.GetProperty(propertyName);
                Expression typedValueExpr = null;
                MethodInfo methodInfo = null;
                if (valueType != propertyInfo.PropertyType)
                {
                    methodInfo = typeof(Convert).GetMethod(nameof(Convert.ChangeType), new Type[] { typeof(object), typeof(Type) });
                    typedValueExpr = Expression.Call(methodInfo, valueExpr, Expression.Constant(propertyInfo.PropertyType));
                    typedValueExpr = Expression.Convert(typedValueExpr, propertyInfo.PropertyType);
                }
                else typedValueExpr = Expression.Convert(valueExpr, propertyInfo.PropertyType);
                methodInfo = propertyInfo.GetSetMethod();
                var bodyExpr = Expression.Call(typedObjExpr, methodInfo, typedValueExpr);
                valueSetter = Expression.Lambda<Action<object, object>>(bodyExpr, objExpr, valueExpr).Compile();
                _propertyValueSetterCache.TryAdd(hashKey, valueSetter);
            }
            valueSetter.Invoke(obj, value);
        }
        #endregion

        #region GetProperties
        private static readonly Dictionary<Type, Dictionary<string, PropertyInfo>> _propertyCache = new Dictionary<Type, Dictionary<string, PropertyInfo>>();
        public static Dictionary<string, PropertyInfo> GetPropertyDic<T>()
             where T : new()
        {
            var type = typeof(T);
            if (!_propertyCache.TryGetValue(type, out Dictionary<string, PropertyInfo> ret))
            {
                ret = new Dictionary<string, PropertyInfo>();
                foreach (var p in type.GetProperties())
                {
                    ret.Add(p.Name, p);
                }
                _propertyCache.Add(type, ret);
            }
            return ret;
        }
        #endregion

        /// <summary>
        /// 获取文本类型的嵌入资源
        /// </summary>
        /// <param name="assembly"></param>
        /// <param name="name"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static string GetManifestResourceString(Assembly assembly, string name, Encoding encoding = null)
        {
            var stream = assembly.GetManifestResourceStream(name);
            using (var reader = new StreamReader(stream, encoding ?? Encoding.UTF8))
                return reader.ReadToEnd();
        }
        /// <summary>
        /// 保存嵌入资源文件到指定目录
        /// </summary>
        /// <param name="assembly"></param>
        /// <param name="name"></param>
        /// <param name="path"></param>
        /// <param name="overwrite"></param>
        public static void SaveManifestResourceFileToDir(Assembly assembly, string name, string path, bool overwrite = false)
        {
            var names = name.Split('.');
            var file = $"{names[names.Length - 2]}.{names[names.Length - 1]}";
            var target = Path.Combine(path, file);
            SaveManifestResourceFileToFile(assembly, name, target, overwrite);
        }
        public static void SaveManifestResourceFileToFile(Assembly assembly, string name, string target, bool overwrite = false)
        {
            if (File.Exists(target) && !overwrite)
                return;
            var stream = assembly.GetManifestResourceStream(name);
            IOUtil.WriteStreamToFile(stream, target);
        }
        /// <summary>
        /// 判断类型type是否继承自某泛型类
        /// </summary>
        /// <param name="type"></param>
        /// <param name="generic"></param>
        /// <returns></returns>
        public static bool IsSubclassOfGeneric(this Type type, Type generic)
        {
            if (type == null) throw new ArgumentNullException(nameof(type));
            if (generic == null) throw new ArgumentNullException(nameof(generic));
            // 测试接口。
            var isTheRawGenericType = type.GetInterfaces().Any(IsTheRawGenericType);
            if (isTheRawGenericType) return true;
            // 测试类型。
            while (type != null && type != typeof(object))
            {
                isTheRawGenericType = IsTheRawGenericType(type);
                if (isTheRawGenericType) return true;
                type = type.BaseType;
            }

            // 没有找到任何匹配的接口或类型。
            return false;

            // 测试某个类型是否是指定的原始接口。
            bool IsTheRawGenericType(Type test)
                => generic == (test.IsGenericType ? test.GetGenericTypeDefinition() : test);
        }

        /// <summary>
        /// 是否是C#内置类型
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool IsBulitinType(Type type)
        {
            return type.FullName.StartsWith("System.");
            //return (type == typeof(object) || Type.GetTypeCode(type) != TypeCode.Object);
        }

        public static List<Type> GetAssemblyTypes(string asm, bool ignoreError, string msg = null)
        {
            if (!string.IsNullOrEmpty(asm) && asm.EndsWith(".dll"))
            {
                var file = asm;
                if (!File.Exists(file))
                    file = Path.Combine(AppContext.BaseDirectory, asm);
                if (File.Exists(file))
                    return Assembly.LoadFrom(file).GetTypes().ToList();
            }
            msg ??= $"加载Assembly获取Types失败。name:{asm}";
            if (!ignoreError)
                throw new Exception(msg);
            LogUtil.Warning(msg);
            return new List<Type>();
        }
    }
}
