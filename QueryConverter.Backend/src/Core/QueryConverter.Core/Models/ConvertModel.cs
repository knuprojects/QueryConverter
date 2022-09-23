namespace QueryConverter.Core.Models
{
    public class ConvertModel
    {
        public string SQLQuery { get; set; } = null!;
        public string ElasticQuery { get; set; } = null!;
    }
}
