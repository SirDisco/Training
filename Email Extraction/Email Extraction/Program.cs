using System;
using System.IO;

namespace Email_Extraction
{
    class Program
    {
        static bool NaiveApproach(string email)
        {
            for (int i = 0; i < email.Length; i++)
                if (email.Substring(i, 13) == "@softwire.com")
                    return true;

            return false;
        }
        
        static void Main(string[] args)
        {
            string emailFile = "emails.??";

            int numberOfSoftwireEmails = 0;
            
            using (StreamReader reader = File.OpenText(emailFile))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                    numberOfSoftwireEmails += (NaiveApproach(line) ? 1 : 0);
            }
            
            Console.WriteLine(numberOfSoftwireEmails);
        }
    }
}