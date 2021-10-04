using System;
using System.IO;
using Newtonsoft.Json.Linq;

namespace Support_Bank
{
    public class FileHelper
    {
        // CSV format
        static public void CreateAllAccountsCSV(StreamReader fileReader)
        {
            // Skip line containing column titles
            fileReader.ReadLine();
            
            string line;
            while ((line = fileReader.ReadLine()) != null)
            {
                string[] columns = line.Split(",");
                
                // Indices
                // 0: date, 1: sender, 2: recipient, 3: narrative, 4: amount
                Program.AddToDictionary(columns[1]);
                Program.AddToDictionary(columns[2]);
            }
        }

        static public void ParseTransactionsCSV(StreamReader fileReader)
        {
            fileReader.ReadLine();
            
            string line;
            while ((line = fileReader.ReadLine()) != null)
            {
                string[] columns = line.Split(",");
                var sender = Program.GetAccount(columns[1]);
                var recipient = Program.GetAccount(columns[2]);
                
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
                
                Program.AddToTransactions(transaction);
                
                sender.HandleOutgoing(ref transaction);
                recipient.HandleIncoming(ref transaction);
            }
        }
        
        // JSON format
        static public void CreateAllAccountsJSON(StreamReader fileReader)
        {
            var wholeFile = fileReader.ReadToEnd();
            var json = JArray.Parse(wholeFile);

            foreach (var transaction in json)
            {
                Program.AddToDictionary(transaction["FromAccount"].ToString());
                Program.AddToDictionary(transaction["ToAccount"].ToString());
            }
        }
        
        static public void ParseTransactionsJSON(StreamReader fileReader)
        {
            var wholeFile = fileReader.ReadToEnd();
            var json = JArray.Parse(wholeFile);
            
            foreach (var tr in json)
            {
                var sender = Program.GetAccount(tr["FromAccount"].ToString());
                var recipient = Program.GetAccount(tr["ToAccount"].ToString());
                
                // Check amount formatting is correct
                if (!float.TryParse(tr["Amount"].ToString(), out var amount))
                {
                    Console.WriteLine("Failed to parse amount for transaction (check formatting)");
                    continue;
                }
                
                var narrative = tr["Narrative"].ToString();
                
                // Check date formatting is correct
                if (!DateTime.TryParse(tr["Date"].ToString(), out var date))
                {
                    Console.WriteLine("Failed to parse date for transaction (check formatting)");
                    continue;
                }

                var transaction = new Transaction(ref sender, ref recipient, amount, narrative, date);
                
                Program.AddToTransactions(transaction);
                
                sender.HandleOutgoing(ref transaction);
                recipient.HandleIncoming(ref transaction);
            }
        }
    }
}