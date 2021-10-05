using System;
using System.IO;
using System.Xml;

namespace Support_Bank
{
    public class FileReaderXML : SupportBankFileReader
    {
        public void CreateAllAccounts(StreamReader fileReader)
        {
            // Reset FileReader to beginning
            fileReader.BaseStream.Position = 0;
            fileReader.DiscardBufferedData();

            XmlReaderSettings settings = new XmlReaderSettings();

            using (XmlReader reader = XmlReader.Create(fileReader, settings))
            {
                while (reader.Read())
                {
                    switch (reader.NodeType)
                    {
                        case XmlNodeType.Element:
                            if (reader.Name == "From" || reader.Name == "To")
                                Program.AddToDictionary(reader.ReadElementContentAsString());
                            break;
                        default:
                                break;
                    }
                }
            }
        }

        public void ParseTransactions(StreamReader fileReader)
        {
            // Reset FileReader to beginning
            fileReader.BaseStream.Position = 0;
            fileReader.DiscardBufferedData();

            XmlReaderSettings settings = new XmlReaderSettings();

            using (XmlReader reader = XmlReader.Create(fileReader, settings))
            {
                while (reader.Read())
                {
                    switch (reader.NodeType)
                    {
                        case XmlNodeType.Element:
                            if (reader.Name == "From" || reader.Name == "To")
                                Program.AddToDictionary(reader.ReadElementContentAsString());
                            break;
                        default:
                            break;
                    }
                }
            }
        }
    }
}