using System;
using System.Collections.Generic;
using System.IO;
using CommandLine;

namespace Support_Bank
{
    class Program
    {
        static void AddToDictionary(string name)
        {
            if (!m_Accounts.ContainsKey(name))
                m_Accounts[name] = new Account(name);
        }

        static void CreateAllAccounts(StreamReader fileReader)
        {
            string line;
            while ((line = fileReader.ReadLine()) != null)
            {
                string[] columns = line.Split(",");
                
                // Indices
                // 0: date, 1: sender, 2: recipient, 3: narrative, 4: amount
                AddToDictionary(columns[1]);
                AddToDictionary(columns[2]);
            }
        }

        static void ParseTransactions(StreamReader fileReader)
        {
            string line;
            while ((line = fileReader.ReadLine()) != null)
            {
                string[] columns = line.Split(",");
                var sender = m_Accounts[columns[1]];
                var recipient = m_Accounts[columns[2]];
                var amount = float.Parse(columns[4]);
                var narrative = columns[3];
                var date = DateTime.Parse(columns[0]);
                var transaction = new Transaction(ref sender, ref recipient, amount, narrative, date);
                
            }

        }

        static void HandleCLIArguments(string[] args)
        {
            // Print correct data according to command line arguments
            var parsed = CommandLine.Parser.Default.ParseArguments<ListInfo, ListSimpleInfo>(args);
            
            parsed.WithParsed<ListInfo>(ops =>
            {
                if (ops.user == null)
                {
                    // TODO: this
                    Console.WriteLine("Print all user information (including transactions)");
                }
                else
                {
                    // TODO: this
                    Console.WriteLine($"Print information owned by user {ops.user} (including transactions)");
                }
            });
            
            parsed.WithParsed<ListSimpleInfo>(ops =>
            {
                if (ops.user == null)
                {
                    // TODO: this
                    Console.WriteLine("Print all user information (excluding transactions)");
                }
                else
                {
                    // TODO: this
                    Console.WriteLine($"Print information owned by user {ops.user} (excluding transactions)");
                }
            });
        }

        static void Main(string[] args)
        {
            m_Accounts = new Dictionary<string, Account>();

            // TODO: Check file is readable
            string path = "Transactions2014.csv";
            var file = new StreamReader(path);

            // Skip line containing column titles
            file.ReadLine();

            CreateAllAccounts(file);

            // Reset FileReader to beginning
            file.BaseStream.Position = 0;
            file.DiscardBufferedData();
            
            // Skip line containing column titles
            file.ReadLine();
            
            ParseTransactions(file);

            HandleCLIArguments(args);
        }

        private static Dictionary<string, Account> m_Accounts;
    }
}