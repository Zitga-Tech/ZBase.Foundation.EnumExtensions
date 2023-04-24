using System;

namespace ZBase.Foundation.EnumExtensions
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    public sealed partial class DisplayAttribute : Attribute
    {
        public string Name { get; set; }

        public DisplayAttribute() { }
    }
}