using TinyFx.Extensions.IP2Country.Entities;
using System.Collections.Generic;

namespace TinyFx.Extensions.IP2Country.Datasources
{
    public interface IIP2CountryDataSource
    {
        IEnumerable<IIPRangeCountry> Read();
    }
}
