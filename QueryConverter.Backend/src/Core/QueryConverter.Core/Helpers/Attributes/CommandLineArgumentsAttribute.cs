namespace QueryConverter.Core.Helpers.Attributes
{
    /// <summary>
    /// Indicates that a type represents command line arguments.
    /// adding this attribute is optional, and can be sued to override
    /// default behavior for case sensitivity.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
    public class CommandLineArgumentsAttribute : Attribute
    {
        public CommandLineArgumentsAttribute() { }

        /// <summary>
        /// Should argument name parsing respect case
        /// </summary>
        public bool CaseSensitive
        {
            get { return this.caseSensitive; }
            set { this.caseSensitive = value; }
        }

        private bool caseSensitive = true;
    }
}
