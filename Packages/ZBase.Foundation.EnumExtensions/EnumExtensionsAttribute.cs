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
}