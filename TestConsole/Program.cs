using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using WebHome.Models.DataEntity;
using WebHome.Models.Locale;
using WebHome.Models.Resource;

namespace TestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            //test1();
            //test2();
            //XmlDocument doc = new XmlDocument();
            //doc.Load("SampleData.xml");
            //var items = doc.SelectNodes("//REC[position()=1]/Detail");
            //test3();
            test4();
            Console.ReadKey();
        }

        private static void test4()
        {
            String data;
            using (WebClient client = new WebClient())
            {
                data = client.DownloadString("http://61.219.99.47/LED-CP5200.TXT");
            }
            Console.WriteLine(data);
        }

        private static void test3()
        {
            int n, a = 0, i;
            Console.WriteLine("Please enter an integer ");
            int.TryParse(Console.ReadLine(), out n);

            Console.WriteLine("The prime factors of n are below:");

            for (i = 1; i <= n; i++)
            {
                if (n % i == 0)
                {
                    a = 0;
                    for (int j = 1; j <= i; j++)
                    {
                        if (i % j == 0)
                        {
                            a++;
                        }
                    }
                    if (a == 2)
                    {
                        Console.WriteLine(i);
                    }
                }
            }
        }

        private static void test2()
        {
            SiteMenuItem item = new SiteMenuItem
            {
                Id = "a1",
                MenuItem = new SiteMenuItem[]
                {
                    new SiteMenuItem
                {
                    Id = "aa1",
                    MenuItem = new SiteMenuItem[] { new SiteMenuItem {
                        Id = "aaa1",
                        Name = "AAA1",
                        Url = "uuu1"
                    }
                    },
                    Name = "AA1",
                    Url = "uu1"
                },new SiteMenuItem {
                    Id = "b1",
                    Name = "B1",
                    Url = "u2"
                } },
                Name = "A1",
                Url = "u1"
            };

            Console.WriteLine(JsonConvert.SerializeObject(item));
        }

        private static void test1()
        {
            JsonSerializer js = new JsonSerializer();
            using (StreamReader sr = new StreamReader("wp_posts.json"))
            {
                using (JsonTextReader jr = new JsonTextReader(sr))
                {
                    JArray obj = (JArray)js.Deserialize(jr);
                    var items = obj.Where(o => ("revision" == (String)o["post_type"] || "post" == (String)o["post_type"])
                    && "open" == (String)o["ping_status"]
                    && ("publish" == (String)o["post_status"] || "inherit" == (String)o["post_status"]))
                    .GroupBy(o => o["post_title"]).Where(g => !String.IsNullOrEmpty((String)g.Key))
                    .Select(g => g.OrderByDescending(v => v["ID"]).First());

                    using (ModelSource<Document> mgr = new ModelSource<Document>())
                    {
                        var articleTable = mgr.GetTable<Article>();
                        foreach (var item in items)
                        {
                            articleTable.InsertOnSubmit(new Article
                            {
                                ArticleContent = ((String)item["post_content"]).Replace("\\r\\n", "\r\n"),
                                Title = ((String)item["post_title"]).Replace("\\r\\n", "\r\n"),
                                Document = new Document
                                {
                                    DocDate = DateTime.ParseExact((String)item["post_date"], "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.CurrentCulture),
                                    DocType = (int)Naming.DocumentTypeDefinition.Knowledge
                                }
                            });

                            mgr.SubmitChanges();

                        }
                    }

                    //var items = obj.Where(o => o["post_content"].ToString().StartsWith("撰文"))
                    //.GroupBy(o => o["post_title"]).Where(g => !String.IsNullOrEmpty((String)g.Key))
                    //.Select(g => g.OrderByDescending(v => v["ID"]).First());

                }
            }
        }


    }
}
