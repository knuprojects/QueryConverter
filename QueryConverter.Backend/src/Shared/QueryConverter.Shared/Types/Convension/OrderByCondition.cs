using QueryConverter.Types.Shared.Enums;
using System.Runtime.CompilerServices;

namespace QueryConverter.Shared.Types.Convension
{
    public class OrderByCondition
    {
        private string _ascendingValue = string.Empty;
        private string _descendingValue = string.Empty;

        public string Column { get; set; } = String.Empty;
        public LiteralType Type { get; set; }
        public OperatorType Operator { get; set; }

        public string Value
        {
            set
            {
                string trimmedValue = value.Trim('\'');

                switch (Operator)
                {
                    case OperatorType.Ascending:
                        _ascendingValue = trimmedValue;
                        break;
                    case OperatorType.Descending:
                        _descendingValue = trimmedValue;
                        break;
                }
            }
        }

        public string DescdendingValue
        {
            get
            {
                return _descendingValue;
            }
        }

        public string AscendingValue
        {
            get
            {
                return _ascendingValue;
            }
        }

        public static OperatorType ToOperatorType(string operatorType)
        {
            return operatorType switch
            {
                "asc" => OperatorType.Ascending,
                "desc" => OperatorType.Descending,
                _ => OperatorType.Unknown
            };
        
        }

        public static string FromOperatorType(OperatorType operatorType)
        {
            return operatorType switch
            {
                OperatorType.Ascending => "asc",
                OperatorType.Descending => "desc",
                _ => operatorType.ToString()
            };
        }
    }
}
