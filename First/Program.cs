using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Linq;
using VkNet;
using VkNet.Enums.Filters;
using VkNet.Model;
using VkNet.Model.RequestParams;
using System.Collections.Generic;
using System.IO;

namespace DataMiningTestt
{
    class Program
    {
        static void Main(string[] args)
        {
            var api = new VkApi();
            //autthorization
            api.Authorize(new ApiAuthParams
            {
                ApplicationId = 7479662,
                Login = "79196861086",
                Password = "Lublyucs16",
                Settings = Settings.All,   
            });
            Console.WriteLine(1);
            Console.WriteLine(api.Token);

            var listid = new List<long>();
            //getting id from your friends
            var users = api.Friends.Get(new VkNet.Model.RequestParams.FriendsGetParams
            {
                UserId = 146993560, //your id
                Fields = ProfileFields.Uid
            });
            //adding id's with excepting a locked acc's
            foreach (var e in users)
            {
                if(e.IsClosed == true)   
                listid.Add(e.Id);
            }
            var result = new List<long>();
            foreach(var e in listid)
            {
                var groupes = api.Groups.Get(new GroupsGetParams()
                {
                    UserId = e,
                    Extended = true,
                    Filter = GroupsFilters.Publics,
                    Fields = GroupsFields.All
                }).
                ToList();
                var interest = groupes.Where(e => e.Name.Contains("Аниме") || // your themes
                e.Name.Contains("Dota") ||
                e.Name.Contains("Програм") ||
                e.Name.Contains("Кни") ||
                e.Name.Contains("Сериал"));
                if (interest.Count() != 0)
                    result.Add(e);
            }

            var write = new StreamWriter("C:/DataMining/First/Result.txt");

            foreach (var e in result)
                write.WriteLine(e);

            write.Close();
            Console.ReadLine();

        }
    }
}
