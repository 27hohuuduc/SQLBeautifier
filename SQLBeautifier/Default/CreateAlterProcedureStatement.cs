using Microsoft.SqlServer.Management.SqlParser.SqlCodeDom;
using SQLBeautifier.Types;

namespace SQLBeautifier.Default
{
    public class CreateAlterProcedureStatement : BaseStatement
    {
        public override (string, string) SqlText(SqlCodeObject sqlObject)
        {
            return (string.Empty, string.Empty);
        }
    }
}
