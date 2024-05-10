namespace Nutrition.Business;

public class UnitVariationAttribute : Attribute
{
    public List<string> NameVariations { get; set; } = default!;

    public UnitVariationAttribute(params string[] nameVariations)
    {
        NameVariations = [.. nameVariations];
    }
}
