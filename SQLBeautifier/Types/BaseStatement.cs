using Microsoft.SqlServer.Management.SqlParser.SqlCodeDom;

namespace SQLBeautifier.Types
{
    public abstract class BaseStatement : ISqlObject
    {
        public int Tabs { get; set; }
        public abstract (string, string) SqlText(SqlCodeObject sqlObject);
    
        public string InsertTab(string s)
        {
            return new string(' ', Tabs * 4) + s;
        }
    }
}
