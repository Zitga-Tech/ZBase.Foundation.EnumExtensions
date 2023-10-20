using System;
using ZBase.Foundation.EnumExtensions;

namespace EnumExtensionsTests
{
    [EnumExtensions]
    internal enum MyEnum
    {
        A, B, C
    }

    partial class MyEnumExtensions
    {

    }

    public partial class Program
    {
        public void Do()
        {
        }

        [EnumExtensions]
        private enum Enumz
        {
            X, Y, Z
        }

        partial class EnumzExtensions { }

        [EnumExtensionsFor(typeof(Enumz))]
        private static partial class EnumZZZ { }
    }

    public enum BuiltInEnum
    {
        C, D, K
    }

    [EnumExtensionsFor(typeof(BuiltInEnum))]
    internal static partial class ExtensionsForBuiltInEnum
    {

    }

    [Flags, EnumExtensions]
    public enum DirectionFlag : byte
    {
        None           = 0b_0000_0000,
        TargetLocation = 0b_0000_0001,

        [Obsolete]
        Upward         = 0b_0000_0010,
    }

    partial class DirectionFlagExtensions
    {

    }
}

namespace UsingEnumExtensions
{
    public partial class Program
    {
        [EnumExtensionsFor(typeof(EnumExtensionsTests.BuiltInEnum))]
        public static partial class ExtensionsForBuiltInEnum
        {

        }
    }
}
