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
            string[] BasicPaths = { @"C:\WorkSpace\XML\继电保护高级技师\", @"C:\WorkSpace\XML\继电保护基础知识" };
            foreach (var BasicPath in BasicPaths)
            {
                using (StreamWriter sw = new StreamWriter(Path.Combine(BasicPath, "输出.txt")))
                {
                    var QuestionNumber = 0;
                    foreach (var file in Directory.GetFiles(BasicPath))
                    {
                        if (file.EndsWith("txt"))
                        {
                            continue;
                        }
                        var doc = new XmlDocument();
                        doc.Load(Path.Combine(BasicPath, file));
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
                                QuestionNumber++;
                                sw.Write(QuestionNumber.ToString());
                                sw.Write(". ");
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
                                    sw.WriteLine(TitleAnswer);
                                    Console.WriteLine(TitleAnswer);
                                }


                                var TrueAnswer = "";
                                foreach (XmlNode e in PerQuestion.ParentNode.ChildNodes)
                                {
                                    TrueAnswer += e.Attributes["text"].Value;
                                }

                                sw.WriteLine();
                                sw.WriteLine(TrueAnswer);
                                sw.WriteLine();
                                sw.WriteLine();
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
            

            
        }
    }
}