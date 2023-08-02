using System;

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
