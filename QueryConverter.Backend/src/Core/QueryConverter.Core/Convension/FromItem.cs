namespace QueryConverter.Core.Convension
{
    public class FromItem
    {
        public string Index { get; set; }
        public bool IsComplete
        {
            get
            {
                return Index != null;
            }
        }
    }
}
