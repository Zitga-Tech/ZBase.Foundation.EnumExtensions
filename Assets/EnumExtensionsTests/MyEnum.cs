using System;
using UnityEngine;
using ZBase.Foundation.EnumExtensions;

namespace EnumExtensionsTests
{
    [EnumExtensions]
    public enum MyEnum { A, B, C }

    partial class MyEnumExtensions { }

    [EnumExtensionsFor(typeof(LogType))]
    static partial class LogTypeExtensions { }

    [Flags, EnumExtensions]
    public enum MyFlags : byte
    {
        None = 0b_0000_0000,
        [Display("And")]
        A    = 0b_0000_0001,
        B    = 0b_0000_0010,
        C    = 0b_0000_0100,
    }

    static partial class MyFlagsExtensions { }
}
