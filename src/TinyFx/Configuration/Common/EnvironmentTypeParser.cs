using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TinyFx.Configuration.Common
{
    internal class EnvironmentTypeParser
    {
        private static Dictionary<string, EnvironmentType> _envMapDic = new() {
            // dev
            { "dev", EnvironmentType.Development},
            { "development",EnvironmentType.Development },
            // sit
            { "testing",EnvironmentType.Testing },
            { "sit",EnvironmentType.Testing },
            { "test",EnvironmentType.Testing },
            // fat
            { "fat",EnvironmentType.QA },
            { "qa",EnvironmentType.QA },
            // uat
            { "uat",EnvironmentType.Staging },
            { "staging",EnvironmentType.Staging },
            { "sim",EnvironmentType.Staging },
            // pro
            { "pro",EnvironmentType.Production },
            { "prod",EnvironmentType.Production },
            { "production",EnvironmentType.Production },
        };
        public EnvironmentType Parse(string envString)
        {
            return !string.IsNullOrEmpty(envString) && _envMapDic.TryGetValue(envString.ToLower(), out var v)
                ? v : EnvironmentType.Unknown;
        }
    }
}
