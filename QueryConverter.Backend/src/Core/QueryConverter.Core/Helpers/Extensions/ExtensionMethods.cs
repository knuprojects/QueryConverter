using Newtonsoft.Json;
using QueryConverter.Core.Enums;
using QueryConverter.Core.Helpers.Attributes;
using System.Diagnostics;
using System.Reflection;
using System.Text;

namespace QueryConverter.Core.Helpers.Extensions;

public static class ExtensionMethods
{
    public static string PrettyJson(this string jsonString)
    {
        dynamic parsedJson = JsonConvert.DeserializeObject(jsonString);
        return JsonConvert.SerializeObject(parsedJson, Formatting.Indented);
    }

    public static int IndexOf(this StringBuilder text, char value, int startIndex)
    {
        for (int index = startIndex; index < text.Length; index++)
        {
            if (text[index] == value)
                return index;
        }

        return -1;
    }

    public static int LastIndexOf(this StringBuilder text, char value, int startIndex)
    {
        for (int index = Math.Min(startIndex, text.Length - 1); index >= 0; index--)
        {
            if (text[index] == value)
                return index;
        }

        return -1;
    }

    public static ArgumentAttribute GetAttribute(FieldInfo field)
    {
        return (ArgumentAttribute)GetAttribute(field, typeof(ArgumentAttribute));
    }

    public static CommandLineArgumentsAttribute GetAttribute(Type type)
    {
        return (CommandLineArgumentsAttribute)GetAttribute(type, typeof(CommandLineArgumentsAttribute));
    }

    public static object GetAttribute(MemberInfo member, Type attributeType)
    {
        object[] attributes = member.GetCustomAttributes(attributeType, false);
        if (attributes.Length == 1)
            return attributes[0];

        Debug.Assert(attributes.Length == 0);
        return null;
    }

    public static void AddNewLine(string newLine, StringBuilder builder, ref int currentColumn)
    {
        builder.Append(newLine);
        currentColumn = 0;
    }

    public static string LongName(ArgumentAttribute attribute, FieldInfo field)
    {
        return (attribute == null || attribute.DefaultLongName) ? field.Name : attribute.LongName;
    }

    public static string ShortName(ArgumentAttribute attribute, FieldInfo field)
    {
        if (attribute is DefaultArgumentAttribute)
            return null;
        if (!ExplicitShortName(attribute))
            return LongName(attribute, field).Substring(0, 1);
        return attribute.ShortName;
    }

    public static string HelpText(ArgumentAttribute attribute, FieldInfo field)
    {
        if (attribute == null)
            return null;
        else
            return attribute.HelpText;
    }

    public static bool HasHelpText(ArgumentAttribute attribute)
    {
        return (attribute != null && attribute.HasHelpText);
    }

    public static bool ExplicitShortName(ArgumentAttribute attribute)
    {
        return (attribute != null && !attribute.DefaultShortName);
    }

    public static object DefaultValue(ArgumentAttribute attribute, FieldInfo field)
    {
        return (attribute == null || !attribute.HasDefaultValue) ? null : attribute.DefaultValue;
    }

    public static Type ElementType(FieldInfo field)
    {
        if (IsCollectionType(field.FieldType))
            return field.FieldType.GetElementType();
        else
            return null;
    }

    public static ArgumentType Flags(ArgumentAttribute attribute, FieldInfo field)
    {
        if (attribute != null)
            return attribute.Type;
        else if (IsCollectionType(field.FieldType))
            return ArgumentType.MultipleUnique;
        else
            return ArgumentType.AtMostOnce;
    }

    public static bool IsCollectionType(Type type)
    {
        return type.IsArray;
    }

    public static bool IsValidElementType(Type type)
    {
        return type != null && (
            type == typeof(int) ||
            type == typeof(uint) ||
            type == typeof(string) ||
            type == typeof(bool) ||
            type.IsEnum);
    }
}
