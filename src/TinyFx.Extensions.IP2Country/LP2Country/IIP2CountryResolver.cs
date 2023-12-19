using TinyFx.IP2Country.Entities;
using System.Net;

namespace TinyFx.IP2Country
{
    public interface IIP2CountryResolver
    {
        IIPRangeCountry Resolve(string ip);
        IIPRangeCountry Resolve(IPAddress ip);
    }
}
