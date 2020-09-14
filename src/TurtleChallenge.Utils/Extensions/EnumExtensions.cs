using System;
using System.ComponentModel;
using System.Reflection;

namespace TurtleChallenge.Utils.Extensions
{
    public static class EnumExtensions
    {
        /// <summary>
        /// Get value of attribute Description from Enum values
        /// </summary>
        /// <param name="value">An Enum with Description attribute on values</param>
        /// <returns>
        /// If the value belongs to the Enum and contains a Description attribute, returns the value of the attribute.
        /// If the value belongs to the Enum and does not contain a Description attribute, returns the value name.
        /// If the value does not belong to the Enum, returns empty.
        /// </returns>
        public static string GetDescription(this Enum value)
        {
            Type type = value.GetType();
            string? name = Enum.GetName(type, value);
            if (string.IsNullOrEmpty(name))
            {
                return string.Empty;
            }

            FieldInfo? fieldInfo = type.GetField(name);
            if (fieldInfo != null && Attribute.GetCustomAttribute(fieldInfo, typeof(DescriptionAttribute)) is DescriptionAttribute descriptionAttribute)
            {
                return descriptionAttribute.Description;
            }

            return name;
        }
    }
}
