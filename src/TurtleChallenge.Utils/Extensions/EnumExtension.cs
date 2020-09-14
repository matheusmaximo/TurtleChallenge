﻿using System;
using System.ComponentModel;
using System.Reflection;

namespace TurtleChallenge.Utils.Extensions
{
    public static class EnumExtension
    {
        public static string GetDescription(this Enum value)
        {
            Type type = value.GetType();
            string name = Enum.GetName(type, value);
            if (name != null)
            {
                FieldInfo field = type.GetField(name);
                if (field != null && Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) is DescriptionAttribute attr)
                {
                    return attr.Description;
                }

                return name;
            }
            return string.Empty;
        }
    }
}
