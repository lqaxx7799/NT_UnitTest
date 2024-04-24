namespace Nutrition.Business;

public class ConversionRateAttribute : Attribute
{
    public double Rate { get; set; }
    public ConversionRateAttribute(double rate)
    {
        Rate = rate;
    }
}
