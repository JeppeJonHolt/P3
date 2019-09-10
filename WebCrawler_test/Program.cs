using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using System.Net.Http;


namespace WebCrawler_test
{
    class Program
    {
        static void Main(string[] args)
        {
            Crawler();
            Console.WriteLine("end of yet");
            Console.ReadLine();
        }

        private static async Task Crawler()
        {
            //var url = "https://www.NotgoogleYeet.dk/";
            var url = "https://etilbudsavis.dk/stores/26acLXL?fbclid=IwAR3PI3-o9FHC63Y5pY8EiqB0P_R84KUba4ys3MzTqu3GZXq8mg7mTgczB9g";
            //var url = "https://www.google.dk/";
            HttpClient HttpClient = new HttpClient();
            var html = await HttpClient.GetStringAsync(url);
            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(html);

            var yeet = htmlDocument.DocumentNode.SelectNodes("//div[@class]");


            Console.WriteLine(yeet.Count);
            var response = HttpClient.GetAsync(url).Result;

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var divs = htmlDocument.DocumentNode.Descendants("div")
                    .Where(node => node.GetAttributeValue("class", "")
                    .Equals("fb_reset")).ToList();
                

                
                foreach (var div in divs)
                {

                    var Item = new Item
                    {
                        ItemName = div.Descendants("a").FirstOrDefault().ChildAttributes("title").FirstOrDefault().Value,
                        ItemPrice = div.Descendants("a").FirstOrDefault().ChildAttributes("title").FirstOrDefault().Value
                    };

                }
            }
            else
            {
                Console.WriteLine("Connection failed");
            }



        }
    }


    public class Item
    {
        public string ItemName { get; set; }
        public string ItemPrice { get; set; }

        public Item()
        {
        }
    }

}
