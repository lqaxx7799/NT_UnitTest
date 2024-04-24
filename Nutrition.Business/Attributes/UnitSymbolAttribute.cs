namespace Nutrition.Business;

public class UnitSymbolAttribute : Attribute
{
    public string Symbol { get; set; } = default!;
    public UnitSymbolAttribute(string symbol) 
    {
        Symbol = symbol;
    }
}
