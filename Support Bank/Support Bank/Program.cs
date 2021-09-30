using System;
using System.Collections.Generic;
using System.IO;

namespace Support_Bank
{
    class Program
    {
        static void AddToDictionary(string name)
        {
            if (!m_Accounts.ContainsKey(name))
                m_Accounts[name] = new Account();
        }
        
        static void Main(string[] args)
        {
            m_Accounts = new Dictionary<string, Account>();
            
            // TODO: Check file is readable
            string path = "Transactions2014.csv";
            var file = new StreamReader(path);
            
            // Skip line containing column titles
            file.ReadLine();
            
            string line;
            while ((line = file.ReadLine()) != null)
            {
                string[] columns = line.Split(",");
                
                // Indices
                // 0: date, 1: sender, 2: recipient, 3: narrative, 4: amount
                AddToDictionary(columns[1]);
                AddToDictionary(columns[2]);
            }
        }

        private static Dictionary<string, Account> m_Accounts;
    }
}