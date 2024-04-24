using Nutrition.Business;
using Nutrition.Library;

namespace Nutrition.APIs;

public class ConversionUtilities
{
    public static double Convert(double value, string fromUnit, string toUnit)
    {
        var fromUnitEnum = GetSystemUnit(fromUnit);
        var toUnitEnum = GetSystemUnit(toUnit);
        if (fromUnitEnum is null || toUnitEnum is null)
        {
            throw new ArgumentException("Unit not supported");
        }
        var fromUnitType = fromUnitEnum.GetAttribute<UnitTypeAttribute>()?.Type;
        var toUnitType = toUnitEnum.GetAttribute<UnitTypeAttribute>()?.Type;

        if (fromUnitType is null || toUnitEnum is null || fromUnitType != toUnitType)
        {
            throw new ArgumentException("Mismatch unit type");
        }

        var fromUnitRate = fromUnitEnum.GetAttribute<ConversionRateAttribute>()?.Rate;
        var toUnitRate = toUnitEnum.GetAttribute<ConversionRateAttribute>()?.Rate;
        if (fromUnitRate is null || toUnitRate is null || toUnitRate == 0)
        {
            throw new ArgumentException("Cannot convert");
        }

        return value * fromUnitRate.Value / toUnitRate.Value;
    }

    public static SystemUnit? GetSystemUnit(string name)
    {
        var normalizedName = name.ToLower();
        var systemUnits = Enum.GetValues<SystemUnit>();
        foreach (var systemUnit in systemUnits)
        {
            var nameVariation = systemUnit.GetAttribute<UnitVariationAttribute>();
            if (nameVariation?.NameVariations.Contains(normalizedName) == true)
            {
                return systemUnit;
            }
        }
        return null;
    }

    public static SystemUnit? GetDefaultUnit(string unitType)
    {
        var normalizedUnitType = unitType.ToLower();
        var systemUnits = Enum.GetValues<SystemUnit>();
        foreach (var systemUnit in systemUnits)
        {
            var currentUnitType = systemUnit.GetAttribute<UnitTypeAttribute>();
            if (currentUnitType?.Type.ToLower() != normalizedUnitType)
            {
                continue;
            }

            if (systemUnit.HasAttribute<DefaultUnitAttribute>())
            {
                return systemUnit;
            }
        }
        return null;
    }
}

