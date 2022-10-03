namespace QueryConverter.Shared.Types.Convension;

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
