using System;
using Unity.Collections;

namespace ZBase.Foundation.EnumExtensions
{
    public interface IHasLength
    {
        int Length { get; }
    }

    public interface IToString
    {
        string ToStringFast();

        string ToDisplayStringFast();
    }

    public interface IToFixedString32Bytes
    {
        FixedString32Bytes ToFixedStringFast();

        FixedString32Bytes ToFixedDisplayStringFast();
    }
    
    public interface IToFixedString64Bytes
    {
        FixedString64Bytes ToFixedStringFast();

        FixedString64Bytes ToFixedDisplayStringFast();
    }
    
    public interface IToFixedString128Bytes
    {
        FixedString128Bytes ToFixedStringFast();

        FixedString128Bytes ToFixedDisplayStringFast();
    }
    
    public interface IToFixedString512Bytes
    {
        FixedString512Bytes ToFixedStringFast();

        FixedString512Bytes ToFixedDisplayStringFast();
    }
    
    public interface IToFixedString4096Bytes
    {
        FixedString4096Bytes ToFixedStringFast();

        FixedString4096Bytes ToFixedDisplayStringFast();
    }

    public interface IToUnderlyingValue<T> where T : unmanaged
    {
        T ToUnderlyingValue();
    }

    public interface ITryFormat
    {
        bool TryFormat(
              Span<char> destination
            , out int charsWritten
            , ReadOnlySpan<char> format = default
            , IFormatProvider provider = null
        );
    }

    public interface IIsDefined
    {
        bool IsDefined();

        bool IsDefinedIn(string name);

        bool IsDefinedIn(string name, bool allowMatchingMetadataAttribute);
    }

    public interface IFindIndex
    {
        int FindIndex();
    }

    public interface IEnumExtensions<TEnum, TUnderlyingValue>
        : IHasLength
        , IToString
        , IToUnderlyingValue<TUnderlyingValue>
        , ITryFormat
        , IIsDefined
        , IFindIndex
        where TEnum : struct, Enum
        where TUnderlyingValue : unmanaged
    { }

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
    [AttributeUsage(AttributeTargets.Interface | AttributeTargets.Struct | AttributeTargets.Class, AllowMultiple = false)]
    public class GeneratedEnumExtensionsForAttribute : Attribute
    {
        public Type EnumType { get; }

        public Type InterfaceType { get; }

        public Type ExtensionsType { get; }

        public Type WrapperType { get; }

        public GeneratedEnumExtensionsForAttribute(
              Type enumType
            , Type interfaceType
            , Type extensionsType
            , Type wrapperType
        )
        {
            EnumType = enumType;
            InterfaceType = interfaceType;
            ExtensionsType = extensionsType;
            WrapperType = wrapperType;
        }
    }
}