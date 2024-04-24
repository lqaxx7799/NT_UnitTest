namespace Nutrition.Business;

public enum SystemUnit
{
    [UnitType("Weight")]
    [DefaultUnit]
    [UnitSymbol("g")]
    [ConversionRate(1)]
    [UnitVariation("gr", "g", "gram")]
    Gram = 1,
    [UnitType("Weight")]
    [UnitSymbol("kg")]
    [ConversionRate(1000)]
    [UnitVariation("kg", "kilogram")]
    Kilogram,
    [UnitType("Weight")]
    [UnitSymbol("oz")]
    [ConversionRate(28.35)]
    [UnitVariation("oz", "ounce")]
    Ounce,
    [UnitType("Volume")]
    [DefaultUnit]
    [UnitSymbol("ml")]
    [ConversionRate(1)]
    [UnitVariation("ml", "milliliter", "millilitre")]
    Milliliter,
    [UnitType("Volume")]
    [UnitSymbol("l")]
    [ConversionRate(1000)]
    [UnitVariation("l", "liter", "litre")]
    Liter,
    [UnitType("Volume")]
    [UnitSymbol("fl. oz")]
    [ConversionRate(28.41)]
    [UnitVariation("fl. oz", "oz. fl", "fl oz", "oz fl", "fluid ounce")]
    FluidOunce,
}
