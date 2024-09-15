using Microsoft.SqlServer.Management.SqlParser.Parser;
using Microsoft.SqlServer.Management.SqlParser.SqlCodeDom;
using SQLBeautifier.Default;
using SQLBeautifier.Types;
using System.Text;
using CustomDictionary = System.Collections.Generic.Dictionary<string, SQLBeautifier.Types.ISqlObject>;

namespace SQLBeautifier
{
    public class Factory
    {
        public static readonly CustomDictionary defaultDefinitions = new()
        {
            {"SqlCreateProcedureStatement", new CreateAlterProcedureStatement() },
            {"SqlCompoundStatement", new BlockStatement() }
        };

        private readonly CustomDictionary _definitions;

        public CustomDictionary GetDefinitiond() { return _definitions; }

        public List<string> Errors { get; }
        public List<string> Warnings { get; }

        public Factory()
        {
            _definitions = defaultDefinitions;
            Errors = [];
            Warnings = [];
        }

        public Factory(CustomDictionary list): this()
        {

        }

        public List<string> Format(string sql)
        {
            var parseResult = Parser.Parse(sql);

            if (parseResult.Errors.Any())
            {
                throw new Exception("Do something else.");
            }
            else
            {
                var script = parseResult.Script;
                var result = new List<string>();
                //Batch
                foreach(var batch in script.Batches)
                {
                    result.Add(ToSqlText(batch.Statements));
                }

                return result;
            }
        }

        private string ToSqlText(IEnumerable<SqlCodeObject> sqlObjects)
        {
            var sb = new StringBuilder();
            foreach (var sqlObject in sqlObjects)
            {
                var typeName = sqlObject.GetType().Name;
                ISqlObject definition;

                //Check dictionary
                var value = _definitions.GetValueOrDefault(typeName);
                if (value != null)
                    definition = value;
                else
                {
                    sb.AppendLine(sqlObject.Sql);
                    Warnings.Add($"\"{typeName}\" isn't defined.");
                    continue;
                }

                //Handle each SQL object individually
                var baseType = definition.GetType().BaseType;
                if (baseType != null && baseType.Name.Equals(nameof(BaseStatement)))
                    ((BaseStatement)definition).Tabs = 1;
                (var start,var end) = definition.SqlText(sqlObject);
                var middle = string.Empty;
                if (sqlObject.Children.Any())
                {
                    middle = ToSqlText(sqlObject.Children);
                }

                sb.AppendLine(start);
                sb.AppendLine(middle);
                sb.AppendLine(end);

            }

            return sb.ToString();
        }
    }
}
