using System;
using System.Collections.Generic;
using System.IO;
using CommandLine;

namespace Support_Bank
{
    class Program
    {
        static public void AddToDictionary(string name)
        {
            if (!m_Accounts.ContainsKey(name))
                m_Accounts[name] = new Account(name);
        }

        static public void AddToTransactions(Transaction transaction)
        {
            m_AllTransactions.Add(transaction);
        }

        static public Account GetAccount(string name)
        {
            return m_Accounts[name];
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
            string path = "Transactions2012.xml";
            var file = new StreamReader(path);

            SupportBankFileReader fileReader = null;

            if (path.EndsWith(".csv"))
                fileReader = new FileReaderCSV();
            else if (path.EndsWith(".json"))
                fileReader = new FileReaderJSON();
            else if (path.EndsWith(".xml"))
                fileReader = new FileReaderXML();
            else
                return;

            fileReader.CreateAllAccounts(file);
            fileReader.ParseTransactions(file);

            HandleCLIArguments(args);
        }

        private static Dictionary<string, Account> m_Accounts;
        private static List<Transaction> m_AllTransactions;
    }
}