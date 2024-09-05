using System;

#if UNITY_COLLECTIONS
using Unity.Collections;
#endif

namespace ZBase.Foundation.EnumExtensions
{
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

    public interface IHasLength
    {
        int Length { get; }
    }

    public interface IToString
    {
        string ToStringFast();

        string ToDisplayStringFast();
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

#if UNITY_COLLECTIONS
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
#endif
}
