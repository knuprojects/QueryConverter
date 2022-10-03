namespace QueryConverter.Shared.Types.Convension;

public class SelectItem
{
    public string Column { get; set; }
    public bool IsComplete
    {
        get
        {
            return Column != null;
        }
    }
}
