using AngleSharp;
using AngleSharp.Html.Parser;
using System.Text.RegularExpressions;

namespace WLDXParser
{
    class Program
    {
        static string NomoralRegex(string input)
        {
            return input.Replace("\n", "").Replace(" ", "").Trim();
        }
        static void Main(string[] args)
        {
            var parser = new HtmlParser();
            var document = parser.ParseDocument(File.ReadAllText(@"C:\WorkSpace\XML\继电保护高级技师\单选题第一组.xml"));
            var list =  document.All.Where(m => m.ClassName == "android.widget.ListView");
            foreach (var item in list)
            {
                var children = item.Children;
                if (children.ToList().Count > 2)
                {
                    Console.WriteLine(children[0].GetAttribute("text"));
                    if (children[0].Children.ToList().Count > 0)
                    {
                        if (children[0].Children[0].Children.ToList().Count > 0)
                        {
                            foreach (var e in children[0].Children[0].Children.ToList())
                            {
                                Console.WriteLine(e.GetAttribute("text"));
                            }
                        } else
                        {
                            Console.WriteLine(children[0].Children[0].GetAttribute("text"));
                        }
                        
                    }
                    
                } else
                {
                    //Console.WriteLine(item.GetAttribute("text"));
                }
                
            }
            Console.WriteLine(list.ToList().Count);

        }
    }
}