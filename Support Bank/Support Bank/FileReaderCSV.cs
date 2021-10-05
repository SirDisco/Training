using System;
using System.IO;

namespace Support_Bank
{
    public class FileReaderCSV : SupportBankFileReader
    {
        public void CreateAllAccounts(StreamReader fileReader)
        {
            // Reset FileReader to beginning
            fileReader.BaseStream.Position = 0;
            fileReader.DiscardBufferedData();
            
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

        public void ParseTransactions(StreamReader fileReader)
        {
            // Reset FileReader to beginning
            fileReader.BaseStream.Position = 0;
            fileReader.DiscardBufferedData();
            
            // Skip line containing column titles
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
    }
}