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
}