using System.ComponentModel;

namespace CharacterSheetCreator.Shared.Utilities;

public static class Extensions
{
    public static string ToDescriptionString(this Enum val)
    {
        var customAttributes = (DescriptionAttribute[])val.GetType().GetField(val.ToString())
            .GetCustomAttributes(typeof(DescriptionAttribute), false);
        return customAttributes.Length == 0 ? val.ToString().ToLower() : customAttributes[0].Description;
    }
}