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
using WebHome.Helper;
using WebHome.Models.DataEntity;
using WebHome.Models.Locale;
using WebHome.Models.Resource;
using WebHome.Models.MIG3_1.C0401;
using Newtonsoft.Json;
using Utility;

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
            //test4();
            //test5();
            //test6();
            //test7();
            //test8();
            //test9();

            System.Diagnostics.Debugger.Launch();
            //test10();

            JObject json = new JObject();
            dynamic obj = json;
            obj.A = "aaa";
            obj.B = 100;
            obj.C = new JObject();
            obj.E = JObject.FromObject(new { D = "TEST", V = "The Value" });

            Console.WriteLine(((object)obj).JsonStringify());
            obj.E.V = "Hello,World!!";
            json["E"]["D"] = "KKK";
            Console.WriteLine(((object)obj).JsonStringify());
            json["E"]["D"] = 15000;
            Console.WriteLine(((object)obj).JsonStringify());
            json["E"]["S"] = JArray.FromObject(new String[] { "AAA", "BBB" });
            Console.WriteLine(((object)obj).JsonStringify());

            Console.ReadKey();
        }

        private static void test10()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load("G:\\temp\\data.xml");
            Invoice item = doc.ConvertTo<Invoice>();
            var data = JsonConvert.SerializeObject(doc);
            Console.WriteLine(data);
        }

        private static void test9()
        {
            String[] contractNo = new string[] {
                "CPA201608132130",
                "CPA201608132135",
                "CPB201608132152",
                "CPA201608132157",
                "CPB201608132165",
                "CPA201608132170",
                "CPA201608152182",
                "CPA201608152186",
                "CPA201608162202",
                "CPA201608162206",
                "CPA201608162207",
                "CPA201608162208",
                "CPA201608162211",
                "CPA201608162214",
                "CPA201608162216",
                "CPA201608162223",
                "CPA201608162227",
                "CPA201608162228",
                "CPA201608222254",
                "CPA201608222255",
                "CPA201608232267",
                "CPA201608262277",
                "CPA201608262282",
                "CPA201608292295",
                "CPA201608292301",
                "CPA201608292307",
                "CPA201608312322",
                "CPA201609012324",
                "CPA201609012326",
                "CPA201609012331",
                "CPA201609012332",
                "CPA201609012336",
                "CPB201609012338",
                "CPA201609012345",
                "CPA201609012350",
                "CPA201609012353",
                "CPA201609022356",
                "CPA201609022361",
                "CPA201609022362",
                "CPA201609072393",
                "CPA201609092410",
                "CPA201609132426",
                "CPA201609132427",
                "CPA201609132431",
                "CPA201609132432",
                "CPA201609142438",
                "CPB201609182463",
                "CPA201610113541",
                "CPA201610133544",
                "CPA201610143550",
                "CPA201610183569",
                "CPA201610183575",
                "CPA201610183579",
                "CPA201611143701",
                "CPB201612053830",
                "CPA201612153867",
                "CPA201612203900",
                "CPA201701165072",
                "CPA201701175078",
                "CPA201702085211",
                "CPA201702095217",
                "CPA201702095224",
                "CPA201702105232",
                "CPA201702185297",
                "CPA201702235324",
                "CPA201703016347",
                "CPA201703076376",
                "CPA201703096397",
                "CPA201703176438",
                "CPB201703226465",
                "CPB201704186597",
                "CPA201704196606",
                "CPA201704246636",
                "CPA201704246637",
                "CPA201704256642",
                "CPA201705187777",
                "CPA201705197781",
                "CPB201705197788",
                "CPA201706017890",
                "CPA201706017894",
                "CPA201706037902",
                "CPA201708288687",
                "CPA201710140005",
                "CPA201801130648",
                "CPA201801290752",
                "CPA201806271361"
            };

            using (ModelSource<UserProfile> models = new ModelSource<UserProfile>())
            {
                var dummyInvoice = models.GetTable<InvoiceItem>().Where(i => i.No == "--").FirstOrDefault();
                DateTime finalDate = new DateTime(2019, 1, 31);
                foreach (var c in contractNo)
                {
                    var item = models.GetTable<CourseContract>()
                        .Where(s => s.ContractNo == c && s.SequenceNo == 0)
                        .Where(s => !s.ContractPayment.Any(p => p.Payment.TransactionType == (int)Naming.PaymentTransactionType.合約終止沖銷))
                        .FirstOrDefault();
                    if (item != null)
                    {
                        var paymentItem = new Payment
                        {
                            InvoiceID = dummyInvoice.InvoiceID,
                            Status = (int)Naming.CourseContractStatus.已生效,
                            ContractPayment = new ContractPayment
                            {
                                ContractID = item.ContractID
                            },
                            PaymentTransaction = new PaymentTransaction
                            {
                                BranchID = item.CourseContractExtension.BranchID
                            },
                            PaymentAudit = new PaymentAudit { },
                            PayoffAmount = 0,
                            PayoffDate = item.ValidTo,
                            Remark = "終止退款",
                            HandlerID = item.AgentID,
                            PaymentType = "現金",
                            TransactionType = (int)Naming.PaymentTransactionType.合約終止沖銷
                        };

                        //paymentItem.VoidPayment = new VoidPayment
                        //{
                        //    Remark = "終止退款",
                        //    Status = (int)Naming.CourseContractStatus.已生效,
                        //    VoidDate = item.ValidTo
                        //};
                        models.GetTable<Payment>().InsertOnSubmit(paymentItem);
                        models.SubmitChanges();
                        Console.WriteLine($"{c}...done");
                    }
                }
            }
        }

        private static void test8()
        {
            using (ModelSource<UserProfile> models = new ModelSource<UserProfile>())
            {
                DateTime dateFrom = DateTime.Today.FirstDayOfMonth();
                DateTime dateTo = dateFrom.AddMonths(1);

                var items = models.PromptRegisterLessonContract()
                        .Where(c => c.Status >= (int)Naming.CourseContractStatus.已生效)
                        .Where(c => c.FitnessConsultant == 2086).FilterByToPay(models)
                        .Where(c => !c.PayoffDue.HasValue
                                    || (c.PayoffDue >= dateFrom && c.PayoffDue < dateTo));
            }
        }

        private static void test7()
        {
            using (ModelSource<UserProfile> models = new ModelSource<UserProfile>())
            {
                var items = models.GetTable<ContractInstallment>().Where(t => t.Installments > 1);
                foreach (var item in items)
                {
                    var contracts = item.CourseContract.Where(c => c.Status > (int)Naming.CourseContractStatus.草稿).OrderBy(c => c.ContractID);
                    if (contracts.Count() > 0)
                    {
                        DateTime payoffDue = contracts.First().ContractDate.Value.AddMonths(1).FirstDayOfMonth();
                        foreach (var c in contracts)
                        {
                            c.PayoffDue = payoffDue.AddDays(-1);
                            payoffDue = payoffDue.AddMonths(1);
                        }
                        models.SubmitChanges();
                    }
                }

            }
        }

        private static void test6()
        {
            using (ModelSource<UserProfile> models = new ModelSource<UserProfile>())
            {
                var items = models.GetTable<ContractTrustTrack>().Where(t => t.TrustType == Naming.TrustType.S.ToString());
                foreach (var t in items)
                {
                    var contract = t.CourseContract;
                    var remained = contract.RemainedLessonCount();
                    t.ReturnAmount = contract.TotalPaidAmount() - (contract.Lessons - remained)
                        * contract.LessonPriceType.ListPrice
                        * contract.CourseContractType.GroupingMemberCount
                        * contract.CourseContractType.GroupingLessonDiscount.PercentageOfDiscount / 100;
                    models.SubmitChanges();
                }
            }
        }

        class KeyData
        {
            public int Idx { get; set; }
            public String[] Data { get; set; }
        }

        class KeyData2
        {
            public int Idx { get; set; }
            public int Count { get; set; }
        }

        private static void test5()
        {
            var a = new List<int> { 1, 2, 3, 4, 5 };
            var b = new List<KeyData> {
                new KeyData { Idx = 1, Data = new String[] {"aaa","AAA" } },
                new KeyData { Idx = 3, Data = new String[] {"ccc","CCC" } },
                new KeyData { Idx = 5, Data = new String[] {"eee","EEE" } },
                new KeyData { Idx = 3, Data = new String[] {"fff","FFF" } }
            };

            var c = a.GroupJoin(b, o => o, i => i.Idx, (o, i) => new { Key = o, D = i.Select(dd => dd.Data) });

            var d = new List<KeyData2> {
                new KeyData2 { Idx=1,Count=10},
                new KeyData2 { Idx=4,Count=40},
                new KeyData2 { Idx=5,Count=50}
            };
            var f = a.GroupJoin(d, o => o, i => i.Idx, (o, i) => i.Select(v => v.Count).FirstOrDefault());
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
