namespace SQLBeautifier.Types
{
    internal class StatementNotFound : Exception
    {
        static new readonly string Message = "This statement is not defined"; 
        public StatementNotFound() : base(Message) { }
    }
}
