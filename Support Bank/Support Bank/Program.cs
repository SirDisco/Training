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
                
                // Check amount formatting is correct
                if (!float.TryParse(columns[4], out var amount))
                {
                    Console.WriteLine("Failed to parse amount for transaction (check formatting)");
                    continue;
                }
                
                var narrative = columns[3];
                
                // Check date formatting is correct
                if (!DateTime.TryParse(columns[0], out var date))
                {
                    Console.WriteLine("Failed to parse date for transaction (check formatting)");
                    continue;
                }

                var transaction = new Transaction(ref sender, ref recipient, amount, narrative, date);
                
                m_AllTransactions.Add(transaction);
                
                sender.HandleOutgoing(ref transaction);
                recipient.HandleIncoming(ref transaction);
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
                    foreach (var account in m_Accounts)
                    {
                        account.Value.CalculateAccountBalance();
                        account.Value.PrintAllInfo();
                    }
                }
                else
                {
                    m_Accounts[ops.user].CalculateAccountBalance();
                    m_Accounts[ops.user].PrintAllInfo();
                }
            });
            
            parsed.WithParsed<ListSimpleInfo>(ops =>
            {
                if (ops.user == null)
                {
                    foreach (var account in m_Accounts)
                    {
                        account.Value.CalculateAccountBalance();
                        account.Value.PrintSimpleInfo();
                    }
                }
                else
                {
                    m_Accounts[ops.user].CalculateAccountBalance();
                    m_Accounts[ops.user].PrintSimpleInfo();
                }
            });
        }

        static void Main(string[] args)
        {
            m_Accounts = new Dictionary<string, Account>();
            m_AllTransactions = new List<Transaction>();

            // TODO: Check file is readable
            string path = "DodgyTransactions2015.csv";
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
        private static List<Transaction> m_AllTransactions;
    }
}