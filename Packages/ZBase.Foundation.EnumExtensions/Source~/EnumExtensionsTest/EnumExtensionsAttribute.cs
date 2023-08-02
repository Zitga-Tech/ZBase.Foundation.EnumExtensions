using System;

namespace ZBase.Foundation.EnumExtensions
{
    /// <summary>
    /// Add to any enum that should be extended.
    /// </summary>
    [AttributeUsage(AttributeTargets.Enum, AllowMultiple = false)]
    public class EnumExtensionsAttribute : Attribute
    {
    }

    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    public sealed partial class DisplayAttribute : Attribute
    {
        public string Value { get; }

        public DisplayAttribute(string value)
        {
            this.Value = value;
        }
    }

    /// <summary>
    /// Add to any static class that should extend an enum.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class EnumExtensionsForAttribute : Attribute
    {
        public Type EnumType { get; }

        public EnumExtensionsForAttribute(Type enumType)
        {
            if (enumType == null)
                throw new ArgumentNullException(nameof(enumType));

            if (enumType.IsEnum == false)
                throw new InvalidOperationException($"{enumType} is not an enum.");

            EnumType = enumType;
        }
    }
}

namespace ZBase.Foundation.EnumExtensions.SourceGen
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class GeneratedEnumExtensionsForAttribute : Attribute
    {
        public Type EnumType { get; }

        public GeneratedEnumExtensionsForAttribute(Type enumType)
        {
            EnumType = enumType;
        }
    }
}