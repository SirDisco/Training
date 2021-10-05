using System.IO;

namespace Support_Bank
{
    public interface SupportBankFileReader
    {
        public void CreateAllAccounts(StreamReader fileReader);
        public void ParseTransactions(StreamReader fileReader);
    }
}