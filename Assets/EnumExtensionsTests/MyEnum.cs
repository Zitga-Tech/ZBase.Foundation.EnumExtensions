using UnityEngine;
using ZBase.Foundation.EnumExtensions;

namespace EnumExtensionsTests
{
    [EnumExtensions]
    public enum MyEnum { A, B, C }

    partial class MyEnumExtensions { }

    [EnumExtensionsFor(typeof(LogType))]
    static partial class LogTypeExtensions { }
}
