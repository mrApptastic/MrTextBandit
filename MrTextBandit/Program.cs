using System;
using System.IO;

namespace MrTextBandit
{
    class Program
    {
        static string settings = @"c:\temp\Settings.txt";
        static string input = "";
        static string output = "";
        static string startText = @"START";
        static string endText = @"END";
        static string prefix = "<----- ";
        static string infix = " ------ ";
        static string suffix = " ----->";
        static string beginStartTag = prefix + startText + infix;
        static string beginEndTag = infix + startText + suffix + Environment.NewLine + Environment.NewLine;
        static string doneStartTag = Environment.NewLine + Environment.NewLine + prefix + endText + infix;
        static string doneEndTag = infix + endText + suffix + Environment.NewLine + Environment.NewLine;
        static bool exit = false;
        static void Main(string[] args)
        {
            while(!exit)
            {
                GetPaths();
                Console.WriteLine("1. Merge. 2. Split" + Environment.NewLine + Environment.NewLine);
                string ib = Console.ReadLine();
                int.TryParse(ib, out int bo);
                switch (bo)
                {
                    case 1: MergeFiles(); break;
                    case 2: SplitFiles(); break;
                    default: exit = true; break;
                }
            }

        }

        static void GetPaths()
        {
            string[] lilleib = File.ReadAllLines(settings);
            input = lilleib[0];
            output = lilleib[1];
        }

        static void MergeFiles()
        {
            string[] lulu = Directory.GetDirectories(input);

            foreach (var pippi in lulu)
            {
                string fileOut = "";
                string baseString = pippi.Substring(pippi.LastIndexOf(@"\")).Replace(@"\", "");

                string[] ib = Directory.GetFiles(input + baseString);

                foreach (var bo in ib)
                {

                    string bobby = bo.Substring(bo.LastIndexOf(@"\")).Replace(@"\", "");

                    if (bo.Substring(bo.LastIndexOf('.') + 1) == "txt")
                    {
                        var tom = File.ReadAllText(bo);
                        fileOut += beginStartTag + bobby + beginEndTag;
                        fileOut += tom;
                        fileOut += doneStartTag + bobby + doneEndTag;
                    }
                }

                Directory.CreateDirectory(output + "/versioner/");
                string version = output + "/versioner/" + baseString + "-" + DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss") + ".txt";
                string outString = output + baseString + ".txt";
                File.WriteAllText(version, fileOut, System.Text.Encoding.UTF8);
                File.WriteAllText(outString, fileOut, System.Text.Encoding.UTF8);
            }            
        }

        static void SplitFiles()
        {
            string[] luna = Directory.GetFiles(output);

            foreach (var didi in luna)
            {
                string baseString = didi.Substring(didi.LastIndexOf(@"\")).Replace(@"\", "").Split('.')[0];

                var bibi = File.ReadAllText(output + baseString + ".txt");

                string[] ib = bibi.Split(doneEndTag);

                foreach (var bo in ib)
                {
                    if (bo.Length > 0 && bo.Contains(".txt"))
                    {
                        string title = bo.Substring(bo.IndexOf(doneStartTag) + doneStartTag.Length);
                        title = title.Substring(0, title.IndexOf(".txt") + 4);
                        string body = bo.Substring(bo.IndexOf(beginEndTag) + beginEndTag.Length, (bo.IndexOf(doneStartTag)) - (bo.IndexOf(beginEndTag) + beginEndTag.Length));

                        string path = input + baseString + "/" + title;

                        File.WriteAllText(path, body, System.Text.Encoding.UTF8);
                    }

                }
            }
        }
    }
}
