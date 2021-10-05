using System;
using System.IO;
using Newtonsoft.Json.Linq;

namespace Support_Bank
{
    public class FileReaderJSON : SupportBankFileReader
    {
        public void CreateAllAccounts(StreamReader fileReader)
        {
            // Reset FileReader to beginning
            fileReader.BaseStream.Position = 0;
            fileReader.DiscardBufferedData();
            
            var wholeFile = fileReader.ReadToEnd();
            var json = JArray.Parse(wholeFile);

            foreach (var transaction in json)
            {
                Program.AddToDictionary(transaction["FromAccount"].ToString());
                Program.AddToDictionary(transaction["ToAccount"].ToString());
            }
        }
        
        public void ParseTransactions(StreamReader fileReader)
        {
            // Reset FileReader to beginning
            fileReader.BaseStream.Position = 0;
            fileReader.DiscardBufferedData();
            
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