using Unity.Collections;
using ZBase.Foundation.EnumExtensions;

namespace EnumExtensionsTests
{
    [EnumExtensions]
    public enum MyEnum { A, B, C }

    partial class MyEnumExtensions
    {
        public static void Do()
        {
            var fs = new FixedString32Bytes();
            global::Unity.Collections.FixedStringMethods.Append(ref fs, nameof(MyEnum.A));
        }
    }
}
