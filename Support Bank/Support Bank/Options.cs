using System.Text.Json.Serialization;
using CommandLine;

namespace Support_Bank
{
    [Verb("list", HelpText = "Lists information along with transactions")]
    class ListInfo
    {
        [Option('u', "user", Required = false, HelpText = "Specifies a user to get info for, empty for all users")]
        public string user { get; set; }
    }
    
    [Verb("simple", HelpText = "Lists information excluding transactions")]
    class ListSimpleInfo
    {
        [Option('u', "user", Required = false, HelpText = "Specifies a user to get info for, empty for all users")]
        public string user { get; set; }
    }
}