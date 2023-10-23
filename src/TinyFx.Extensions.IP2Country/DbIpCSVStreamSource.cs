using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using TinyFx.Extensions.IP2Country.DataSources.CSVFile;
using TinyFx.Extensions.IP2Country.DbIp;
using TinyFx.Extensions.IP2Country.Entities;

namespace TinyFx.Extensions.IP2Country
{
    public class DbIpCSVStreamSource : IP2CountryCSVStreamSource<DbIpIPRangeCountry>
    {
        public DbIpCSVStreamSource(Stream stream)
            : base(stream, new DbIpCSVRecordParser()) { }

        public override IEnumerable<IIPRangeCountry> Read() => ReadStream(Stream, Parser);
    }
}
