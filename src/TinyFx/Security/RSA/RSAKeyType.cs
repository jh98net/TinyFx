#if !NETSTANDARD2_0
namespace TinyFx.Security
{
    public enum RSAKeyType
    {
        /// <summary>
        /// 常用于javascript
        /// </summary>
        Pkcs1,
        /// <summary>
        /// 常用于java
        /// </summary>
        Pkcs8,
        /// <summary>
        /// 常用语.net
        /// </summary>
        Xml
    }
}
#endif