using AngleSharp;
using AngleSharp.Html.Parser;
using System.Text.RegularExpressions;
using System.Xml;

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
            var doc = new XmlDocument();
            doc.Load(@"C:\WorkSpace\XML\继电保护高级技师\单选题第一组.xml");
            //var document = parser.ParseDocument(File.ReadAllText(@"C:\WorkSpace\XML\继电保护高级技师\单选题第一组.xml"));
            var root = doc.DocumentElement;
            var nodes = root.SelectNodes("//node[@class=\"android.widget.ListView\"]");
            foreach (XmlNode PerQuestion in nodes)
            {
                //check is Per Question
                if (PerQuestion.HasChildNodes && (
                      PerQuestion.ChildNodes[0].HasChildNodes && 
                      !string.IsNullOrWhiteSpace(PerQuestion.ChildNodes[0].ChildNodes[0].Attributes["text"].Value) ||
                        !string.IsNullOrWhiteSpace(PerQuestion.ChildNodes[0].Attributes["text"].Value)
                      )
                    )
                {
                    foreach (XmlNode Content in PerQuestion.ChildNodes)
                    {
                        var TitleAnswer = Content.Attributes["text"].Value.Trim();
                        if (Content.HasChildNodes)
                        {
                            foreach (XmlNode e in Content.ChildNodes)
                            {
                                TitleAnswer += e.Attributes["text"].Value.Trim();
                                TitleAnswer += " ";
                                if (e.HasChildNodes)
                                {
                                    foreach (XmlNode f in e.ChildNodes)
                                    {
                                        TitleAnswer += f.Attributes["text"].Value.Trim();
                                        TitleAnswer += " ";
                                    }
                                }
                            }
                        }
                        Console.WriteLine(TitleAnswer); 
                    }
                    

                    var TrueAnswer = "";
                    foreach (XmlNode e in PerQuestion.ParentNode.ChildNodes)
                    {
                        TrueAnswer += e.Attributes["text"].Value;
                    }
                    

                    //Console.WriteLine(title);
                    Console.WriteLine();
                    Console.WriteLine(TrueAnswer);
                    Console.WriteLine();
                    Console.WriteLine();
                }
                //Console.WriteLine(node.ChildNodes.Attributes["text"].Value);
                //Console.WriteLine();
                //var titles = node.SelectNodes("/node[@index=\"0\"]");
                //if (titles.Count > 0)
                //Console.WriteLine(titles[0].InnerText);
            }
        }
    }
}