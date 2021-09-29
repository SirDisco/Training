using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;

namespace Email_Extraction
{
    class Program
    {
        static void NaiveApproach(String content)
        {
            int totalSoftwireEmails = 0;
            StringReader text = new StringReader(content);

            string line;
            while ((line = text.ReadLine()) != null)
            {
                for (int i = 0; i < line.Length - 13; i++)
                    if (line.Substring(i, 13) == "@softwire.com")
                        totalSoftwireEmails += 1;
            }
            
            Console.WriteLine("Naive Approach: " + totalSoftwireEmails.ToString() + " softwire emails");
        }

        static void RegexApproach(String content)
        {
            StringReader text = new StringReader(content);

            Regex pattern = new Regex(@"(\w+(?i)@softwire.com(?-i))\b");

            int totalSoftwireEmails = pattern.Matches(text.ReadToEnd()).Count;
            
            Console.WriteLine("Regex Approach: " + totalSoftwireEmails.ToString() + " softwire emails");
        }

        static void DictionaryApproach(String wholeText)
        {

            // Regex emailRegex = new Regex(@"[a-zA-Z0-9-_.]+(@[a-zA-Z0-9-]+)\.\w+");
            Regex emailRegex = new Regex(@"[a-zA-Z0-9-_.]+(@[a-zA-Z0-9-.]+)");

            MatchCollection domainCollection = emailRegex.Matches(wholeText);

            var domainCount = new Dictionary<string, int>();

            foreach (Match domain in domainCollection)
            {
                string domainString = domain.Groups[1].Value;

                if (!domainCount.ContainsKey(domainString))
                    domainCount[domainString] = 1;
                else
                    domainCount[domainString]++;
            }

            int counter = 1;

            int numberToDisplay = 1000;
            int threshold = 0;

            foreach (var domain in domainCount.OrderBy(key => key.Value).Reverse())
            {
                if (domain.Value >= threshold)
                {
                    Console.WriteLine($"Number of {domain.Key} emails: {domain.Value}");
                    counter++;
                }

                if (counter > numberToDisplay)
                    break;
            }
        }

        static void Main(string[] args)
        {
            // Regular Expression Exercises
            // Phone Number: ^(?:0|\+?44)\s?(?:\d\s?){10}$

            /*string emailFile = "sample.txt";

            StreamReader reader = File.OpenText(emailFile);*/

            WebClient client = new WebClient();
            Stream stream =
                client.OpenRead(
                    "https://raw.githubusercontent.com/techswitch-learners/email-extraction-csharp/master/EmailExtraction/Data/sample.txt");
            StreamReader reader = new StreamReader(stream);
            String content = reader.ReadToEnd();

            NaiveApproach(content);

            RegexApproach(content);
            
            DictionaryApproach(content);
        }
    }
}