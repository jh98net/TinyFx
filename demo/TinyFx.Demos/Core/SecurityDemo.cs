using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyFx.Security;

namespace TinyFx.Demos.Core
{
    internal class SecurityDemo : DemoBase
    {
        public override void Execute()
        {
            var pwd = "admin";
            var salt = SecurityUtil.GetPasswordSalt();
            var epwd = SecurityUtil.EncryptPassword(pwd, salt);
            Console.WriteLine(SecurityUtil.ValidatePassword(pwd, epwd, salt));
        }
    }
}
