using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TinyFx.Extensions.IDGenerator.Common
{
    internal interface IWorkerIdProvider : IDisposable
    {
        Task<int> GetNextWorkId();
        Task Active();
    }
}
