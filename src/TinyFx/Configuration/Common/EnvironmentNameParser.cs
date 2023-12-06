using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TinyFx.Configuration.Common
{
    internal class EnvironmentNameParser
    {
        private static Dictionary<string, EnvironmentNames> _envMapDic = new() {
            { "local", EnvironmentNames.Local},
            // dev
            { "dev", EnvironmentNames.Development},
            { "development",EnvironmentNames.Development },
            // sit
            { "testing",EnvironmentNames.Testing },
            { "sit",EnvironmentNames.Testing },
            { "test",EnvironmentNames.Testing },
            // fat
            { "fat",EnvironmentNames.QA },
            { "qa",EnvironmentNames.QA },
            // uat
            { "uat",EnvironmentNames.Staging },
            { "staging",EnvironmentNames.Staging },
            { "sim",EnvironmentNames.Staging },
            // pro
            { "pro",EnvironmentNames.Production },
            { "prod",EnvironmentNames.Production },
            { "production",EnvironmentNames.Production },
        };
        public EnvironmentNames Parse(string envString)
        {
            return !string.IsNullOrEmpty(envString) && _envMapDic.TryGetValue(envString.ToLower(), out var v)
                ? v : EnvironmentNames.Unknown;
        }
    }
}
