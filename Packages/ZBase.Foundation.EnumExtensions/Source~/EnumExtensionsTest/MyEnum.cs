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
