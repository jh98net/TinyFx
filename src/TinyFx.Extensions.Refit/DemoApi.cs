using Newtonsoft.Json;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TinyFx.Extensions.Refit
{

    internal class DemoClient
    {
        private async Task Init()
        {
            // 全局
            JsonConvert.DefaultSettings = () => new JsonSerializerSettings { };
        }
        private async Task Get()
        {
            var client = RestService.For<DemoApi>("", new RefitSettings 
            {
                ContentSerializer = new SystemTextJsonContentSerializer(new System.Text.Json.JsonSerializerOptions
                { 

                })
            });
        }
    }
    internal interface DemoApi
    {
        // /group/4/users?search.order=desc&search.Limit=10&ages=10%2C20%2C30
        // ages=10,20,30
        [Get("/group/{id}/users")]
        Task<List<string>> GroupListWithAttribute(
            [AliasAs("id")] int groupId, 
            [Query(".", "search")] User param,
            [Query(CollectionFormat.Csv)] int[] ages
            );

        // Body数据：
        //  1) stream => streamContent
        //  2) string => string [Body(BodySerializationMethod.Json)] => stringContent
        //  3) [Body(BodySerializationMethod.UrlEncoded)] => urlEncode
        //  4) other => json
        [Post("/users/new")]
        Task CreateUser([Body] User user);
    }
    internal class User
    {
        [AliasAs("order")]
        public string SortOrder { get; set; }

        public int Limit { get; set; }
    }
}
