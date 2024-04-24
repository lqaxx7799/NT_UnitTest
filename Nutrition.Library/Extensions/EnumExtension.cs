namespace Nutrition.Library;

public static class EnumExtension
{
    public static T? GetAttribute<T>(this Enum @enum) where T : class
    {
        var enumType = @enum.GetType();
        var name = Enum.GetName(enumType, @enum);
        if (name is null) return null;
        return enumType.GetField(name)?.GetCustomAttributes(false).OfType<T>().SingleOrDefault();
    }

    public static bool HasAttribute<T>(this Enum @enum) where T : class
    {
        var enumType = @enum.GetType();
        var name = Enum.GetName(enumType, @enum);
        if (name is null) return false;
        return enumType.GetField(name)?.GetCustomAttributes(false).OfType<T>().Any() == true;
    }
}
