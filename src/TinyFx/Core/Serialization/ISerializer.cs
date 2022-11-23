using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TinyFx
{
    /// <summary>
    /// 序列化器
    /// </summary>
    public interface ISerializer
    {
        /// <summary>
        /// 序列化
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="item">The item.</param>
        /// <returns></returns>
        byte[] Serialize<T>(T item);

        /// <summary>
        /// 序列化
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="item">The item.</param>
        /// <returns></returns>
        Task<byte[]> SerializeAsync<T>(T item);
        /// <summary>
        /// 序列化
        /// </summary>
        /// <param name="value"></param>
        /// <param name="inputType"></param>
        /// <returns></returns>
        byte[] Serialize(object value, Type inputType);
        /// <summary>
        /// 序列化
        /// </summary>
        /// <param name="value"></param>
        /// <param name="inputType"></param>
        /// <returns></returns>
        Task<byte[]> SerializeAsync(object value, Type inputType);
        
        /// <summary>
        /// 反序列化
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="utf8Bytes">序列化对象的UTF8字节流</param>
        /// <returns>
        /// The instance of the specified Item
        /// </returns>
        T Deserialize<T>(byte[] utf8Bytes);

        /// <summary>
        /// 反序列化
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="utf8Bytes">序列化对象的UTF8字节流</param>
        /// <returns>
        /// The instance of the specified Item
        /// </returns>
        Task<T> DeserializeAsync<T>(byte[] utf8Bytes);
        /// <summary>
        /// 反序列化
        /// </summary>
        /// <param name="utf8Bytes">序列化对象的UTF8字节流</param>
        /// <param name="returnType"></param>
        /// <returns></returns>
        object Deserialize(byte[] utf8Bytes, Type returnType);
        /// <summary>
        /// 反序列化
        /// </summary>
        /// <param name="utf8Bytes">序列化对象的UTF8字节流</param>
        /// <param name="returnType"></param>
        /// <returns></returns>
        Task<object> DeserializeAsync(byte[] utf8Bytes, Type returnType);
    }

}
