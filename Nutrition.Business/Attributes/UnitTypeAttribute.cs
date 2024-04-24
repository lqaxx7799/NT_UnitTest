namespace Nutrition.Business;

public class UnitTypeAttribute : Attribute
{
    public string Type { get; set; } = default!;

    public UnitTypeAttribute(string type)
    {
        Type = type;
    }
}
