using System.ComponentModel;

namespace WorkoutPlanner.Common.Extensions;

public static class EnumExtensions
{
    public static string? GetDescription<TEnum>(this TEnum enumValue)
    {
        if (enumValue is null) return null;

        var fieldInfo = enumValue.GetType().GetField(enumValue.ToString()!);

        if (fieldInfo is null) return null;

        if (fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false) is DescriptionAttribute[] attributes 
            && !attributes.Any())
        {
            return attributes.FirstOrDefault()?.Description;
        }

        return enumValue.ToString();
    }
}
