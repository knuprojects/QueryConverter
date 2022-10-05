using QueryConverter.Types.Shared.Enums;

namespace QueryConverter.Core.Convension
{
    public class WhereCondition
    {
        private List<string> _inValues = new();

        private List<string> _betweenValues = new();

        private string _singularValue = string.Empty;
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

                //var result = Operator switch
                //{
                //    var x when
                //        x == OperatorType.Equal ||
                //        x == OperatorType.GreaterThan ||
                //        x == OperatorType.GreaterThanOrEquals ||
                //        x == OperatorType.LessThan ||
                //        x == OperatorType.LessThanOrEquals => _singularValue = trimmedValue,
                //    OperatorType.In => _inValues.Add(trimmedValue),
                //    OperatorType.Between => _betweenValues.Add(trimmedValue),
                //    OperatorType.Like => _singularValue = trimmedValue
                //};

                switch (Operator)
                {
                    case OperatorType.Equal:
                    case OperatorType.GreaterThan:
                    case OperatorType.GreaterThanOrEquals:
                    case OperatorType.LessThan:
                    case OperatorType.LessThanOrEquals:
                        _singularValue = trimmedValue;
                        break;
                    case OperatorType.In:
                        _inValues.Add(trimmedValue);
                        break;
                    case OperatorType.Between:
                        _betweenValues.Add(trimmedValue);
                        break;
                    case OperatorType.Like:
                        _singularValue = trimmedValue;
                        break;
                    case OperatorType.Ascending:
                        _ascendingValue = trimmedValue;
                        break;
                    case OperatorType.Descending:
                        _ascendingValue = trimmedValue;
                        break;

                }
            }
        }

        public List<string> InValues
        {
            get
            {
                return _inValues;
            }
        }

        public List<string> BetweenValues
        {
            get
            {
                return _betweenValues;
            }
        }

        public string SingularValue
        {
            get
            {
                return _singularValue;
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

        public bool IsComplete
        {
            get
            {
                return Column != null && (SingularValue != null || BetweenValues.Count > 0 || InValues.Count > 0);
            }
        }

        public static OperatorType ToOperatorType(string operatorType)
        {
            return operatorType.ToLower() switch
            {
                "=" => OperatorType.Equal,
                ">" => OperatorType.GreaterThan,
                ">=" => OperatorType.GreaterThanOrEquals,
                "<" => OperatorType.LessThan,
                "<=" => OperatorType.LessThanOrEquals,
                "in" => OperatorType.In,
                "between" => OperatorType.Between,
                "like" => OperatorType.Like,
                "asc" => OperatorType.Ascending,
                "desc" => OperatorType.Descending,
                _ => OperatorType.Unknown
            };
        }

        public static string FromOperatorType(OperatorType operatorType)
        {
            return operatorType switch
            {
                OperatorType.Equal => "=",
                OperatorType.GreaterThan => "gt",
                OperatorType.GreaterThanOrEquals => "gte",
                OperatorType.LessThan => "lt",
                OperatorType.LessThanOrEquals => "lte",
                OperatorType.Ascending => "asc",
                OperatorType.Descending => "desc",
                _ => operatorType.ToString()
            };
        }
    }
}
