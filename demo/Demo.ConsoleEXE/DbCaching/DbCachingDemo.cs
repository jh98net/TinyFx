using Demo.ConsoleEXE.DAL;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyFx.Data.SqlSugar;
using TinyFx.DbCaching;
using TinyFx.Demos;

namespace Demo.ConsoleEXE
{
    internal class DbCachingDemo : DemoBase
    {
        public override async Task Execute()
        {
            await Run2();
        }
        private async Task Run2()
        {
            var operatorId = "own_lobby_bra";
            var a = DbCachingUtil.GetList<Ssf_promoter_comm_configEO>(it => new { it.OperatorID }, new Ssf_promoter_comm_configEO
            {
                OperatorID = operatorId
            });
            var b = DbCachingUtil.GetList<Ssf_promoter_comm_configEO>(it => it.OperatorID, "own_lobby_bra");
        }
        private async Task Run1()
        {
            var stopwatch = new Stopwatch();
            var appList = await DbUtil.CreateRepository<Ss_appEO>().GetListAsync();
            var operList = await DbUtil.CreateRepository<Ss_operator_appEO>().GetListAsync();
            foreach (var app in appList)
            {
                var i = 0;
                stopwatch.Reset();
                stopwatch.Start();
                var sAppEo = DbCacheUtil.GetApp(app.AppID);
                var provider = DbCacheUtil.GetProvider(sAppEo.ProviderID);
                foreach (var oper in operList)
                {
                    var item = DbCacheUtil.GetOperatorApp(oper.OperatorID, app.AppID);
                    if (item == null)
                        i++;
                }
                Console.WriteLine($"{stopwatch.ElapsedMilliseconds} count:{i}");
                stopwatch.Stop();
            }
        }
    }
}
