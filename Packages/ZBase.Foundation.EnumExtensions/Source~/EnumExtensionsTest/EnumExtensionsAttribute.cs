using System;

namespace ZBase.Foundation.EnumExtensions
{
    /// <summary>
    /// Add to enums that want to be extended
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
}