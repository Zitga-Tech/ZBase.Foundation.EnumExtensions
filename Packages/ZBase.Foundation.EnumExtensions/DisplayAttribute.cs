using System;

namespace ZBase.Foundation.EnumExtensions
{
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