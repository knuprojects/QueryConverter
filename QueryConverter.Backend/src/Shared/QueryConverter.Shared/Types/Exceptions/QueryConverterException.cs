namespace QueryConverter.Shared.Types.Exceptions
{
    public class QueryConverterException : Exception
    {
        public string Code { get; set; }

        public QueryConverterException()
        {
        }

        public QueryConverterException(string code)
        {
            Code = code;
        }

        public QueryConverterException(string message, params object[] args)
            : this(string.Empty, message, args)
        {
        }

        public QueryConverterException(string code, string message, params object[] args)
            : this(null, code, message, args)
        {
        }

        public QueryConverterException(Exception innerException, string message, params object[] args)
            : this(innerException, string.Empty, message, args)
        {
        }

        public QueryConverterException(Exception innerException, string code, string message, params object[] args)
            : base(string.Format(message, args), innerException)
        {
            Code = code;
        }
    }
}
