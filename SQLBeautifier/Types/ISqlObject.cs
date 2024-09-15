using Microsoft.SqlServer.Management.SqlParser.SqlCodeDom;
using System.Text;

namespace SQLBeautifier.Types
{
    public interface ISqlObject
    {
        public (string, string) SqlText(SqlCodeObject sqlObject);
    }
}
