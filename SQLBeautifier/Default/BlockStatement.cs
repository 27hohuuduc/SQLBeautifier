using Microsoft.SqlServer.Management.SqlParser.SqlCodeDom;
using SQLBeautifier.Types;

namespace SQLBeautifier.Default
{
    public class BlockStatement : BaseStatement
    {
        /// <summary>
        /// Opening phrase
        /// </summary>
        private static readonly string Start = "BEGIN";
        /// <summary>
        /// Closing phrase
        /// </summary>
        private static readonly string End = "END";

        public override (string, string) SqlText(SqlCodeObject sqlObject)
        {
            return (InsertTab(Start), InsertTab(End));
        }
    }
}
