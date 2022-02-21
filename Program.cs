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
                    //foreach (var child in children)
                    //{
                    //    Console.WriteLine($"child is {NomoralRegex(child.TextContent)}");
                    //    if (string.IsNullOrEmpty(NomoralRegex(child.TextContent)))
                    //    {
                    //        var tmpStr = new List<string>();
                    //        foreach(var item2 in child.Children)
                    //        {
                    //            string text = item2.TextContent;
                    //            Console.WriteLine($"text is {NomoralRegex(text)}");
                    //            if (!string.IsNullOrEmpty(NomoralRegex(text)))
                    //            {
                    //                text = text.Trim();
                    //                tmpStr.Add(text);
                    //            } else
                    //            {
                    //                foreach (var item3 in  item2.Children)
                    //                {
                    //                    string text2 = item3.TextContent;
                    //                    if (!string.IsNullOrEmpty(NomoralRegex(text2)))
                    //                    {
                    //                        text2 = text2.Trim();
                    //                        tmpStr.Add(text2);
                    //                        Console.WriteLine($"text2 is {NomoralRegex(text2)}");
                    //                    }
                    //                }
                    //            }
                    //        }
                    //        Console.WriteLine(String.Join("", tmpStr));
                    //    }
                    //}
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