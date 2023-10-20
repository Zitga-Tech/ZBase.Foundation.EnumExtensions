using ZBase.Foundation.SourceGen;

namespace ZBase.Foundation.EnumExtensions
{
    partial class EnumDeclaration
    {
        private const string AGGRESSIVE_INLINING = "[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]";
        private const string GENERATED_CODE = "[global::System.CodeDom.Compiler.GeneratedCode(\"ZBase.Foundation.EnumExtensions.EnumExtensionsGenerator\", \"1.2.2\")]";
        private const string EXCLUDE_COVERAGE = "[global::System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]";
        private const string UNITY_COLLECTIONS_ALLOCATOR = "global::Unity.Collections.AllocatorManager.AllocatorHandle";

        public string WriteCode()
        {
            var p = Printer.DefaultLarge;
            var @this = ParentIsNamespace ? "this " : "";

            p = p.IncreasedIndent();
            {
                p.PrintEndLine();

                p.PrintLine(GENERATED_CODE).PrintLine(EXCLUDE_COVERAGE);

                p.PrintBeginLine("[global::ZBase.Foundation.EnumExtensions.SourceGen.GeneratedEnumExtensionsFor(typeof(")
                    .Print(FullyQualifiedName)
                    .PrintEndLine("))]");

                p.PrintBeginLine();
                {
                    switch (Accessibility)
                    {
                        case Microsoft.CodeAnalysis.Accessibility.Internal:
                            p.Print("internal ");
                            break;

                        case Microsoft.CodeAnalysis.Accessibility.Private:
                            p.Print("private ");
                            break;

                        case Microsoft.CodeAnalysis.Accessibility.ProtectedAndInternal:
                            p.Print("private protected ");
                            break;

                        case Microsoft.CodeAnalysis.Accessibility.ProtectedOrInternal:
                            p.Print("protected internal ");
                            break;

                        default:
                            p.Print("public ");
                            break;
                    }

                    p.Print("static partial class ").Print(ExtensionsName);
                }
                p.PrintEndLine();

                p.OpenScope();
                {
                    p.PrintLine("/// <summary>");
                    p.PrintLine("/// The number of members in the enum.");
                    p.PrintLine("/// This is a non-distinct count of defined names.");
                    p.PrintLine("/// </summary>");
                    p.PrintLine(GENERATED_CODE);
                    p.PrintLine($"public const int Length = {Members.Count};");

                    p.PrintEndLine();

                    p.PrintLine("/// <summary>");
                    p.PrintLine($"/// Returns the string representation of the <see cref=\"{FullyQualifiedName}\"/> value.");
                    p.PrintLine("/// </summary>");
                    p.PrintLine("/// <param name=\"value\">The value to retrieve the string value for</param>");
                    p.PrintLine("/// <returns>The string representation of the value</returns>");
                    p.PrintLine(AGGRESSIVE_INLINING).PrintLine(GENERATED_CODE).PrintLine(EXCLUDE_COVERAGE);
                    p.PrintLine($"public static string ToStringFast({@this}{FullyQualifiedName} value)");
                    p = p.IncreasedIndent();
                    p.PrintLine("=> Names.Get(value);");
                    p = p.DecreasedIndent();

                    p.PrintEndLine();

                    p.PrintLine("/// <summary>");
                    p.PrintLine($"/// Returns the string representation of the <see cref=\"{FullyQualifiedName}\"/> value.");
                    p.PrintLine("/// If the attribute is decorated with a <c>[Display]</c> attribute, then");
                    p.PrintLine("/// uses the provided value. Otherwise uses the name of the member, equivalent to");
                    p.PrintLine("/// calling <c>ToString()</c> on <paramref name=\"value\"/>.");
                    p.PrintLine("/// </summary>");
                    p.PrintLine("/// <param name=\"value\">The value to retrieve the string value for</param>");
                    p.PrintLine("/// <returns>The string representation of the value</returns>");
                    p.PrintLine(AGGRESSIVE_INLINING).PrintLine(GENERATED_CODE).PrintLine(EXCLUDE_COVERAGE);
                    p.PrintLine($"public static string ToDisplayStringFast({@this}{FullyQualifiedName} value)");
                    p = p.IncreasedIndent();
                    p.PrintLine("=> DisplayNames.Get(value);");
                    p = p.DecreasedIndent();

                    p.PrintEndLine();

                    if (ReferenceUnityCollections)
                    {
                        p.PrintLine("/// <summary>");
                        p.PrintLine($"/// Returns the fixed string representation of the <see cref=\"{FullyQualifiedName}\"/> value.");
                        p.PrintLine("/// </summary>");
                        p.PrintLine("/// <param name=\"value\">The value to retrieve the string value for</param>");
                        p.PrintLine("/// <returns>The fixed string representation of the value</returns>");
                        p.PrintLine(AGGRESSIVE_INLINING).PrintLine(GENERATED_CODE).PrintLine(EXCLUDE_COVERAGE);
                        p.PrintLine($"public static {FixedStringTypeName} ToFixedStringFast({@this}{FullyQualifiedName} value)");
                        p = p.IncreasedIndent();
                        p.PrintLine("=> FixedNames.Get(value);");
                        p = p.DecreasedIndent();

                        p.PrintEndLine();

                        p.PrintLine("/// <summary>");
                        p.PrintLine($"/// Returns the fixed string representation of the <see cref=\"{FullyQualifiedName}\"/> value.");
                        p.PrintLine("/// If the attribute is decorated with a <c>[Display]</c> attribute, then");
                        p.PrintLine("/// uses the provided value. Otherwise uses the name of the member, equivalent to");
                        p.PrintLine("/// calling <c>ToString()</c> on <paramref name=\"value\"/>.");
                        p.PrintLine("/// </summary>");
                        p.PrintLine("/// <param name=\"value\">The value to retrieve the string value for</param>");
                        p.PrintLine("/// <returns>The fixed string representation of the value</returns>");
                        p.PrintLine(AGGRESSIVE_INLINING).PrintLine(GENERATED_CODE).PrintLine(EXCLUDE_COVERAGE);
                        p.PrintLine($"public static {FixedStringTypeName} ToFixedDisplayStringFast({@this}{FullyQualifiedName} value)");
                        p = p.IncreasedIndent();
                        p.PrintLine("=> FixedDisplayNames.Get(value);");
                        p = p.DecreasedIndent();

                        p.PrintEndLine();
                    }

                    p.PrintLine(GENERATED_CODE).PrintLine(EXCLUDE_COVERAGE);
                    p.PrintLine("public static bool TryFormat(");
                    p = p.IncreasedIndent();
                    {
                        p.PrintLine($"  {@this}{FullyQualifiedName} value");
                        p.PrintLine(", global::System.Span<char> destination");
                        p.PrintLine(", out int charsWritten");
                        p.PrintLine(", global::System.ReadOnlySpan<char> format = default");
                        p.PrintLine(", global::System.IFormatProvider provider = null");
                    }
                    p = p.DecreasedIndent();
                    p.PrintLine(")");
                    p.OpenScope();
                    {
                        p.PrintLine("if (IsDefined(value))");
                        p.OpenScope();
                        {
                            p.PrintLine("var span = global::System.MemoryExtensions.AsSpan(ToStringFast(value));");

                            p.PrintLine("if (span.Length == 0 || span.TryCopyTo(destination) == false)");
                            p.OpenScope();
                            {
                                p.PrintLine("charsWritten = 0;");
                                p.PrintLine(" return false;");
                            }
                            p.CloseScope();

                            p.PrintLine("charsWritten = span.Length;");
                            p.PrintLine(" return true;");
                        }
                        p.CloseScope();

                        p.PrintLine("if (ToUnderlyingValue(value).TryFormat(destination, out var chars, format, provider))");
                        p.OpenScope();
                        {
                            p.PrintLine("charsWritten = chars;");
                            p.PrintLine(" return true;");
                        }
                        p.CloseScope();

                        p.PrintLine("charsWritten = 0;");
                        p.PrintLine(" return false;");
                    }
                    p.CloseScope();

                    p.PrintEndLine();

                    p.PrintLine("/// <summary>");
                    p.PrintLine("/// Returns a boolean telling whether the given enum value exists in the enumeration.");
                    p.PrintLine("/// </summary>");
                    p.PrintLine("/// <param name=\"value\">The value to check if it's defined</param>");
                    p.PrintLine("/// <returns><c>true</c> if the value exists in the enumeration, <c>false</c> otherwise</returns>");
                    p.PrintLine(GENERATED_CODE).PrintLine(EXCLUDE_COVERAGE);
                    p.PrintLine($"public static bool IsDefined({@this}{FullyQualifiedName} value)");
                    p = p.IncreasedIndent();
                    {
                        p.PrintLine("=> value switch");
                        p.OpenScope();
                        {
                            foreach (var (key, _) in Members)
                            {
                                p.PrintLine($"{FullyQualifiedName}.{key} => true,");
                            }

                            p.PrintLine("_ => false,");
                        }
                        p.CloseScope("};");
                    }
                    p = p.DecreasedIndent();

                    p.PrintEndLine();

                    p.PrintLine("/// <summary>");
                    p.PrintLine("/// Returns a boolean telling whether an enum with the given name exists in the enumeration.");
                    p.PrintLine("/// </summary>");
                    p.PrintLine("/// <param name=\"name\">The name to check if it's defined</param>");
                    p.PrintLine("/// <returns><c>true</c> if a member with the name exists in the enumeration, <c>false</c> otherwise</returns>");
                    p.PrintLine(AGGRESSIVE_INLINING).PrintLine(GENERATED_CODE).PrintLine(EXCLUDE_COVERAGE);
                    p.PrintLine($"public static bool IsDefinedIn({@this}string name, {FullyQualifiedName} _)");
                    p = p.IncreasedIndent();
                    p.PrintLine($"=> IsDefinedIn(name, default({FullyQualifiedName}), allowMatchingMetadataAttribute: false);");
                    p = p.DecreasedIndent();

                    p.PrintEndLine();

                    p.PrintLine("/// <summary>");
                    p.PrintLine("/// Returns a boolean telling whether an enum with the given name exists in the enumeration,");
                    p.PrintLine("/// or if a member decorated with a <c>[Display]</c> attribute");
                    p.PrintLine("/// with the required name exists.");
                    p.PrintLine("/// </summary>");
                    p.PrintLine("/// <param name=\"name\">The name to check if it's defined</param>");
                    p.PrintLine("/// <param name=\"allowMatchingMetadataAttribute\">If <c>true</c>, considers the value of metadata attributes, otherwise ignores them</param>");
                    p.PrintLine("/// <returns><c>true</c> if a member with the name exists in the enumeration, or a member is decorated");
                    p.PrintLine("/// with a <c>[Display]</c> attribute with the name, <c>false</c> otherwise</returns>");
                    p.PrintLine(GENERATED_CODE).PrintLine(EXCLUDE_COVERAGE);
                    p.PrintLine($"public static bool IsDefinedIn({@this}string name, {FullyQualifiedName} _, bool allowMatchingMetadataAttribute)");
                    p.OpenScope();
                    {
                        if (IsDisplayAttributeUsed)
                        {
                            p.PrintLine("var isDefinedInDisplayAttribute = false;");
                            p.PrintEndLine();
                            p.PrintLine("if (allowMatchingMetadataAttribute)");
                            p.OpenScope();
                            {
                                p.PrintLine("isDefinedInDisplayAttribute = name switch");
                                p.OpenScope();
                                {
                                    foreach (var (key, value) in Members)
                                    {
                                        if (value.DisplayName is not null && value.IsDisplayNameTheFirstPresence)
                                        {
                                            p.PrintLine($"DisplayNames.{key} => true,");
                                        }
                                    }

                                    p.PrintLine("_ => false,");
                                }
                                p.CloseScope("};");
                            }
                            p.CloseScope();

                            p.PrintLine("if (isDefinedInDisplayAttribute)");
                            p.OpenScope();
                            {
                                p.PrintLine("return true;");
                            }
                            p.CloseScope();
                        }

                        p.PrintLine("return name switch");
                        p.OpenScope();
                        {
                            foreach (var (key, _) in Members)
                            {
                                p.PrintLine($"Names.{key} => true,");
                            }

                            p.PrintLine("_ => false,");
                        }
                        p.CloseScope("};");
                    }
                    p.CloseScope();

                    p.PrintEndLine();

                    p.PrintLine(AGGRESSIVE_INLINING).PrintLine(GENERATED_CODE).PrintLine(EXCLUDE_COVERAGE);
                    p.PrintLine($"public static {UnderlyingTypeName} ToUnderlyingValue({@this}{FullyQualifiedName} value)");
                    p = p.IncreasedIndent();
                    p.PrintLine($"=> ({UnderlyingTypeName})value;");
                    p = p.DecreasedIndent();

                    p.PrintEndLine();

                    if (HasFlags)
                    {
                        p.PrintLine("/// <summary>");
                        p.PrintLine("/// Determines whether one or more bit fields are set in the current instance.");
                        p.PrintLine("/// </summary>");
                        p.PrintLine("/// <returns><c>true</c> if the bit field or bit fields that are set in <c>flag</c> are also set in the current instance; otherwise, <c>false</c>.</returns>");
                        p.PrintLine(AGGRESSIVE_INLINING).PrintLine(GENERATED_CODE).PrintLine(EXCLUDE_COVERAGE);
                        p.PrintLine($"public static bool Contains({@this}{FullyQualifiedName} value, {FullyQualifiedName} flag)");
                        p = p.IncreasedIndent();
                        p.PrintLine($"=> (value & flag) == flag;");
                        p = p.DecreasedIndent();

                        p.PrintEndLine();

                        p.PrintLine("/// <summary>");
                        p.PrintLine("/// Unsets one or more bit fields on the current instance.");
                        p.PrintLine("/// </summary>");
                        p.PrintLine("/// <returns>A new instance without bit fields that are set in <c>flags</c>.</returns>");
                        p.PrintLine(AGGRESSIVE_INLINING).PrintLine(GENERATED_CODE).PrintLine(EXCLUDE_COVERAGE);
                        p.PrintLine($"public static {FullyQualifiedName} Unset({@this}{FullyQualifiedName} value, {FullyQualifiedName} flag)");
                        p = p.IncreasedIndent();
                        p.PrintLine($"=> value & (~flag);");
                        p = p.DecreasedIndent();

                        p.PrintEndLine();
                    }

                    p.PrintLine("/// <summary>");
                    p.PrintLine("/// Converts the string representation of the name or numeric value of");
                    p.PrintLine($"/// an <see cref=\"{FullyQualifiedName}\" /> to the equivalent instance.");
                    p.PrintLine("/// The return value indicates whether the conversion succeeded.");
                    p.PrintLine("/// </summary>");
                    p.PrintLine("/// <param name=\"name\">The case-sensitive string representation of the enumeration name or underlying value to convert</param>");
                    p.PrintLine("/// <param name=\"value\">When this method returns, contains an object of type ");
                    p.PrintLine($"/// <see cref=\"{FullyQualifiedName}\" /> whose");
                    p.PrintLine("/// value is represented by <paramref name=\"value\"/> if the parse operation succeeds.");
                    p.PrintLine("/// If the parse operation fails, contains the default value of the underlying type");
                    p.PrintLine($"/// of <see cref=\"{FullyQualifiedName}\" />. This parameter is passed uninitialized.</param>");
                    p.PrintLine("/// <returns><c>true</c> if the value parameter was converted successfully; otherwise, <c>false</c>.</returns>");
                    p.PrintLine(AGGRESSIVE_INLINING).PrintLine(GENERATED_CODE).PrintLine(EXCLUDE_COVERAGE);
                    p.PrintLine($"public static bool TryParse({@this}string name, out {FullyQualifiedName} value)");
                    p = p.IncreasedIndent();
                    p.PrintLine("=> TryParse(name, out value, false, false);");
                    p = p.DecreasedIndent();

                    p.PrintEndLine();

                    p.PrintLine("/// <summary>");
                    p.PrintLine("/// Converts the string representation of the name or numeric value of");
                    p.PrintLine($"/// an <see cref=\"{FullyQualifiedName}\" /> to the equivalent instance.");
                    p.PrintLine("/// The return value indicates whether the conversion succeeded.");
                    p.PrintLine("/// </summary>");
                    p.PrintLine("/// <param name=\"name\">The case-sensitive string representation of the enumeration name or underlying value to convert</param>");
                    p.PrintLine("/// <param name=\"value\">When this method returns, contains an object of type ");
                    p.PrintLine($"/// <see cref=\"{FullyQualifiedName}\" /> whose");
                    p.PrintLine("/// value is represented by <paramref name=\"value\"/> if the parse operation succeeds.");
                    p.PrintLine("/// If the parse operation fails, contains the default value of the underlying type");
                    p.PrintLine($"/// of <see cref=\"{FullyQualifiedName}\" />. This parameter is passed uninitialized.</param>");
                    p.PrintLine("/// <param name=\"ignoreCase\"><c>true</c> to read value in case insensitive mode; <c>false</c> to read value in case sensitive mode.</param>");
                    p.PrintLine("/// <returns><c>true</c> if the value parameter was converted successfully; otherwise, <c>false</c>.</returns>");
                    p.PrintLine(AGGRESSIVE_INLINING).PrintLine(GENERATED_CODE).PrintLine(EXCLUDE_COVERAGE);
                    p.PrintLine($"public static bool TryParse({@this}string name, out {FullyQualifiedName} value, bool ignoreCase)");
                    p = p.IncreasedIndent();
                    p.PrintLine("=> TryParse(name, out value, ignoreCase, false);");
                    p = p.DecreasedIndent();

                    p.PrintEndLine();

                    p.PrintLine("/// <summary>");
                    p.PrintLine("/// Converts the string representation of the name or numeric value of");
                    p.PrintLine($"/// an <see cref=\"{FullyQualifiedName}\" /> to the equivalent instance.");
                    p.PrintLine("/// The return value indicates whether the conversion succeeded.");
                    p.PrintLine("/// </summary>");
                    p.PrintLine("/// <param name=\"name\">The case-sensitive string representation of the enumeration name or underlying value to convert</param>");
                    p.PrintLine("/// <param name=\"value\">When this method returns, contains an object of type ");
                    p.PrintLine($"/// <see cref=\"{FullyQualifiedName}\" /> whose");
                    p.PrintLine("/// value is represented by <paramref name=\"value\"/> if the parse operation succeeds.");
                    p.PrintLine("/// If the parse operation fails, contains the default value of the underlying type");
                    p.PrintLine($"/// of <see cref=\"{FullyQualifiedName}\" />. This parameter is passed uninitialized.</param>");
                    p.PrintLine("/// <param name=\"ignoreCase\"><c>true</c> to read value in case insensitive mode; <c>false</c> to read value in case sensitive mode.</param>");
                    p.PrintLine("/// <param name=\"allowMatchingMetadataAttribute\">If <c>true</c>, considers the value included in metadata attributes such as");
                    p.PrintLine("/// <c>[Display]</c> attribute when parsing, otherwise only considers the member names.</param>");
                    p.PrintLine("/// <returns><c>true</c> if the value parameter was converted successfully; otherwise, <c>false</c>.</returns>");
                    p.PrintLine(GENERATED_CODE).PrintLine(EXCLUDE_COVERAGE);
                    p.PrintLine($"public static bool TryParse({@this}string name, out {FullyQualifiedName} value, bool ignoreCase, bool allowMatchingMetadataAttribute)");
                    p.OpenScope();
                    {
                        p.PrintLine("if (string.IsNullOrWhiteSpace(name))");
                        p.OpenScope();
                        {
                            p.PrintLine("value = default;");
                            p.PrintLine("return false;");
                        }
                        p.CloseScope();

                        if (IsDisplayAttributeUsed)
                        {
                            p.PrintLine("if (allowMatchingMetadataAttribute)");
                            p.OpenScope();
                            {
                                p.PrintLine("if (ignoreCase)");
                                p.OpenScope();
                                {
                                    p.PrintLine("switch (name)");
                                    p.OpenScope();
                                    {
                                        foreach (var (key, value) in Members)
                                        {
                                            if (value.DisplayName is not null && value.IsDisplayNameTheFirstPresence)
                                            {
                                                p.PrintLine($"case string s when s.Equals(DisplayNames.{key}, global::System.StringComparison.OrdinalIgnoreCase):");
                                                p.OpenScope();
                                                {
                                                    p.PrintLine($"value = {FullyQualifiedName}.{key};");
                                                    p.PrintLine("return true;");
                                                }
                                                p.CloseScope();
                                            }
                                        }

                                        p.PrintLine("default: break;");
                                    }
                                    p.CloseScope();
                                }
                                p.CloseScope();
                                p.PrintLine("else");
                                p.OpenScope();
                                {
                                    p.PrintLine("switch (name)");
                                    p.OpenScope();
                                    {
                                        foreach (var (key, value) in Members)
                                        {
                                            if (value.DisplayName is not null && value.IsDisplayNameTheFirstPresence)
                                            {
                                                p.PrintLine($"case DisplayNames.{key}:");
                                                p.OpenScope();
                                                {
                                                    p.PrintLine($"value = {FullyQualifiedName}.{key};");
                                                    p.PrintLine("return true;");
                                                }
                                                p.CloseScope();
                                            }
                                        }

                                        p.PrintLine("default: break;");
                                    }
                                    p.CloseScope();
                                }
                                p.CloseScope();
                            }
                            p.CloseScope();
                        }

                        p.PrintLine("if (ignoreCase)");
                        p.OpenScope();
                        {
                            p.PrintLine("switch (name)");
                            p.OpenScope();
                            {
                                foreach (var (key, _) in Members)
                                {
                                    p.PrintLine($"case string s when s.Equals(Names.{key}, global::System.StringComparison.OrdinalIgnoreCase):");
                                    p.OpenScope();
                                    {
                                        p.PrintLine($"value = {FullyQualifiedName}.{key};");
                                        p.PrintLine("return true;");
                                    }
                                    p.CloseScope();
                                }

                                p.PrintLine($"case string s when {UnderlyingTypeName}.TryParse(name, out var underlyingValue):");
                                p.OpenScope();
                                {
                                    p.PrintLine($"value = ({FullyQualifiedName})underlyingValue;");
                                    p.PrintLine("return true;");
                                }
                                p.CloseScope();

                                p.PrintLine("default:");
                                p.OpenScope();
                                {
                                    p.PrintLine("value = default;");
                                    p.PrintLine("return false;");
                                }
                                p.CloseScope();
                            }
                            p.CloseScope();
                        }
                        p.CloseScope();
                        p.PrintLine("else");
                        p.OpenScope();
                        {
                            p.PrintLine("switch (name)");
                            p.OpenScope();
                            {
                                foreach (var (key, _) in Members)
                                {
                                    p.PrintLine($"case Names.{key}:");
                                    p.OpenScope();
                                    {
                                        p.PrintLine($"value = {FullyQualifiedName}.{key};");
                                        p.PrintLine("return true;");
                                    }
                                    p.CloseScope();
                                }

                                p.PrintLine($"case string s when {UnderlyingTypeName}.TryParse(name, out var underlyingValue):");
                                p.OpenScope();
                                {
                                    p.PrintLine($"value = ({FullyQualifiedName})underlyingValue;");
                                    p.PrintLine("return true;");
                                }
                                p.CloseScope();

                                p.PrintLine("default:");
                                p.OpenScope();
                                {
                                    p.PrintLine("value = default;");
                                    p.PrintLine("return false;");
                                }
                                p.CloseScope();
                            }
                            p.CloseScope();
                        }
                        p.CloseScope();
                    }
                    p.CloseScope();

                    p.PrintEndLine();

                    if (ReferenceUnityCollections)
                    {
                        p.PrintLine(GENERATED_CODE).PrintLine(EXCLUDE_COVERAGE);
                        p.PrintLine($"private static {FixedStringTypeName} ToFixedString({UnderlyingTypeName} value)");
                        p.OpenScope();
                        {
                            p.PrintLine($"var fs = new {FixedStringTypeName}();");
                            p.PrintLine("global::Unity.Collections.FixedStringMethods.Append(ref fs, value);");
                            p.PrintLine("return fs;");
                        }
                        p.CloseScope();

                        p.PrintEndLine();
                    }

                    p.PrintLine(GENERATED_CODE).PrintLine(EXCLUDE_COVERAGE);
                    p.PrintLine("public static class Values");
                    p.OpenScope();
                    {
                        p.PrintLine($"private static readonly {FullyQualifiedName}[] s_values = new {FullyQualifiedName}[]");
                        p.OpenScope();
                        {
                            foreach (var (key, _) in Members)
                            {
                                p.PrintLine($"{FullyQualifiedName}.{key},");
                            }
                        }
                        p.CloseScope("};");

                        p.PrintEndLine();

                        p.PrintLine(AGGRESSIVE_INLINING).PrintLine(GENERATED_CODE).PrintLine(EXCLUDE_COVERAGE);
                        p.PrintLine($"public static global::System.ReadOnlyMemory<{FullyQualifiedName}> AsMemory() => s_values;");
                        
                        if (ReferenceUnityCollections)
                        {
                            p.PrintEndLine();

                            p.PrintLine(AGGRESSIVE_INLINING).PrintLine(GENERATED_CODE).PrintLine(EXCLUDE_COVERAGE);
                            p.PrintLine($"public static global::Unity.Collections.NativeArray<{FullyQualifiedName}>.ReadOnly AsNativeArray({UNITY_COLLECTIONS_ALLOCATOR} allocator)");
                            p = p.IncreasedIndent();
                            p.PrintLine($"=> global::Unity.Collections.CollectionHelper.CreateNativeArray<{FullyQualifiedName}>(s_values, allocator).AsReadOnly();");
                            p = p.DecreasedIndent();
                        }
                    }
                    p.CloseScope();

                    p.PrintEndLine();

                    p.PrintLine(GENERATED_CODE).PrintLine(EXCLUDE_COVERAGE);
                    p.PrintLine("public static class UnderlyingValues");
                    p.OpenScope();
                    {
                        p.PrintLine($"private static readonly {UnderlyingTypeName}[] s_values = new {UnderlyingTypeName}[]");
                        p.OpenScope();
                        {
                            foreach (var (key, _) in Members)
                            {
                                p.PrintLine($"ToUnderlyingValue({FullyQualifiedName}.{key}),");
                            }
                        }
                        p.CloseScope("};");

                        p.PrintEndLine();

                        p.PrintLine(AGGRESSIVE_INLINING).PrintLine(GENERATED_CODE).PrintLine(EXCLUDE_COVERAGE);
                        p.PrintLine($"public static global::System.ReadOnlyMemory<{UnderlyingTypeName}> AsMemory() => s_values;");

                        if (ReferenceUnityCollections)
                        {
                            p.PrintEndLine();

                            p.PrintLine(AGGRESSIVE_INLINING).PrintLine(GENERATED_CODE).PrintLine(EXCLUDE_COVERAGE);
                            p.PrintLine($"public static global::Unity.Collections.NativeArray<{UnderlyingTypeName}>.ReadOnly AsNativeArray({UNITY_COLLECTIONS_ALLOCATOR} allocator)");
                            p = p.IncreasedIndent();
                            p.PrintLine($"=> global::Unity.Collections.CollectionHelper.CreateNativeArray<{UnderlyingTypeName}>(s_values, allocator).AsReadOnly();");
                            p = p.DecreasedIndent();
                        }
                    }
                    p.CloseScope();

                    p.PrintEndLine();

                    p.PrintLine(GENERATED_CODE).PrintLine(EXCLUDE_COVERAGE);
                    p.PrintLine("public static class Names");
                    p.OpenScope();
                    {
                        foreach (var (key, _) in Members)
                        {
                            p.PrintLine(GENERATED_CODE);
                            p.PrintLine($"public const string {key} = nameof({FullyQualifiedName}.{key});");
                            p.PrintEndLine();
                        }

                        p.PrintLine($"private static readonly string[] s_names = new string[]");
                        p.OpenScope();
                        {
                            foreach (var (key, _) in Members)
                            {
                                p.PrintLine($"{key},");
                            }
                        }
                        p.CloseScope("};");

                        p.PrintEndLine();

                        p.PrintLine(AGGRESSIVE_INLINING).PrintLine(GENERATED_CODE).PrintLine(EXCLUDE_COVERAGE);
                        p.PrintLine("public static global::System.ReadOnlyMemory<string> AsMemory() => s_names;");

                        p.PrintEndLine();

                        p.PrintLine(GENERATED_CODE).PrintLine(EXCLUDE_COVERAGE);
                        p.PrintLine($"public static string Get({FullyQualifiedName} value)");
                        p = p.IncreasedIndent();
                        p.PrintLine("=> value switch");
                        p.OpenScope();
                        {
                            foreach (var (key, _) in Members)
                            {
                                p.PrintLine($"{FullyQualifiedName}.{key} => {key},");
                            }

                            p.PrintLine("_ => ToUnderlyingValue(value).ToString(),");
                        }
                        p.CloseScope("};");
                        p = p.DecreasedIndent();

                    }
                    p.CloseScope();

                    p.PrintEndLine();

                    p.PrintLine(GENERATED_CODE).PrintLine(EXCLUDE_COVERAGE);
                    p.PrintLine("public static class DisplayNames");
                    p.OpenScope();
                    {
                        foreach (var (key, value) in Members)
                        {
                            p.PrintLine(GENERATED_CODE);

                            if (value.DisplayName is not null && value.IsDisplayNameTheFirstPresence)
                            {
                                p.PrintLine($"public const string {key} = \"{value.DisplayName}\";");
                            }
                            else
                            {
                                p.PrintLine($"public const string {key} = Names.{key};");
                            }

                            p.PrintEndLine();
                        }

                        p.PrintLine($"private static readonly string[] s_names = new string[]");
                        p.OpenScope();
                        {
                            foreach (var (key, _) in Members)
                            {
                                p.PrintLine($"{key},");
                            }
                        }
                        p.CloseScope("};");

                        p.PrintEndLine();

                        p.PrintLine(AGGRESSIVE_INLINING).PrintLine(GENERATED_CODE).PrintLine(EXCLUDE_COVERAGE);
                        p.PrintLine("public static global::System.ReadOnlyMemory<string> AsMemory() => s_names;");

                        p.PrintEndLine();

                        p.PrintLine(GENERATED_CODE).PrintLine(EXCLUDE_COVERAGE);
                        p.PrintLine($"public static string Get({FullyQualifiedName} value)");
                        p = p.IncreasedIndent();
                        p.PrintLine("=> value switch");
                        p.OpenScope();
                        {
                            foreach (var (key, _) in Members)
                            {
                                p.PrintLine($"{FullyQualifiedName}.{key} => {key},");
                            }

                            p.PrintLine("_ => ToUnderlyingValue(value).ToString(),");
                        }
                        p.CloseScope("};");
                        p = p.DecreasedIndent();

                    }
                    p.CloseScope();

                    if (ReferenceUnityCollections)
                    {
                        p.PrintEndLine();

                        p.PrintLine(GENERATED_CODE).PrintLine(EXCLUDE_COVERAGE);
                        p.PrintLine("public static class FixedNames");
                        p.OpenScope();
                        {
                            foreach (var (key, _) in Members)
                            {
                                p.PrintLine(GENERATED_CODE).PrintLine(EXCLUDE_COVERAGE);
                                p.PrintLine($"public static {FixedStringTypeName} {key}");
                                p.OpenScope();
                                {
                                    p.PrintLine("get");
                                    p.OpenScope();
                                    {
                                        p.PrintLine($"var fs = new {FixedStringTypeName}();");
                                        p.PrintLine($"global::Unity.Collections.FixedStringMethods.Append(ref fs, ({FixedStringTypeName})Names.{key});");
                                        p.PrintLine("return fs;");
                                    }
                                    p.CloseScope();
                                }
                                p.CloseScope();
                                p.PrintEndLine();
                            }

                            p.PrintLine(GENERATED_CODE).PrintLine(EXCLUDE_COVERAGE);
                            p.PrintLine($"public static global::Unity.Collections.NativeArray<{FixedStringTypeName}> AsNativeArray({UNITY_COLLECTIONS_ALLOCATOR} allocator)");
                            p.OpenScope();
                            {
                                p.PrintLine($"var names = global::Unity.Collections.CollectionHelper.CreateNativeArray<{FixedStringTypeName}>({ExtensionsName}.Length, allocator, global::Unity.Collections.NativeArrayOptions.UninitializedMemory);");

                                var index = 0;

                                foreach (var (key, _) in Members)
                                {
                                    p.PrintLine($"names[{index}] = {key};");
                                    index++;

                                    if (index >= Members.Count)
                                    {
                                        break;
                                    }
                                }

                                p.PrintLine("return names;");
                            }
                            p.CloseScope();

                            p.PrintEndLine();

                            p.PrintLine(GENERATED_CODE).PrintLine(EXCLUDE_COVERAGE);
                            p.PrintLine($"public static {FixedStringTypeName} Get({FullyQualifiedName} value)");
                            p = p.IncreasedIndent();
                            p.PrintLine($"=> value switch");
                            p.OpenScope();
                            {
                                foreach (var (key, _) in Members)
                                {
                                    p.PrintLine($"{FullyQualifiedName}.{key} => {key},");
                                }

                                p.PrintLine($"_ => ToFixedString(ToUnderlyingValue(value)),");
                            }
                            p.CloseScope("};");
                            p = p.DecreasedIndent();
                        }
                        p.CloseScope();

                        p.PrintEndLine();

                        p.PrintLine(GENERATED_CODE).PrintLine(EXCLUDE_COVERAGE);
                        p.PrintLine("public static class FixedDisplayNames");
                        p.OpenScope();
                        {
                            foreach (var (key, value) in Members)
                            {
                                p.PrintLine(GENERATED_CODE).PrintLine(EXCLUDE_COVERAGE);

                                if (value.DisplayName is not null && value.IsDisplayNameTheFirstPresence)
                                {
                                    p.PrintLine($"public static {FixedStringTypeName} {key}");
                                    p.OpenScope();
                                    {
                                        p.PrintLine("get");
                                        p.OpenScope();
                                        {
                                            p.PrintLine($"var fs = new {FixedStringTypeName}();");
                                            p.PrintLine($"global::Unity.Collections.FixedStringMethods.Append(ref fs, ({FixedStringTypeName})\"{value.DisplayName}\");");
                                            p.PrintLine("return fs;");
                                        }
                                        p.CloseScope();
                                    }
                                    p.CloseScope();
                                }
                                else
                                {
                                    p.PrintLine($"public static {FixedStringTypeName} {key}");
                                    p.OpenScope();
                                    {
                                        p.PrintLine("get");
                                        p.OpenScope();
                                        {
                                            p.PrintLine($"var fs = new {FixedStringTypeName}();");
                                            p.PrintLine($"global::Unity.Collections.FixedStringMethods.Append(ref fs, ({FixedStringTypeName})DisplayNames.{key});");
                                            p.PrintLine("return fs;");
                                        }
                                        p.CloseScope();
                                    }
                                    p.CloseScope();
                                }

                                p.PrintEndLine();
                            }

                            p.PrintLine(GENERATED_CODE).PrintLine(EXCLUDE_COVERAGE);
                            p.PrintLine($"public static global::Unity.Collections.NativeArray<{FixedStringTypeName}> AsNativeArray({UNITY_COLLECTIONS_ALLOCATOR} allocator)");
                            p.OpenScope();
                            {
                                p.PrintLine($"var names = global::Unity.Collections.CollectionHelper.CreateNativeArray<{FixedStringTypeName}>({ExtensionsName}.Length, allocator, global::Unity.Collections.NativeArrayOptions.UninitializedMemory);");

                                var index = 0;

                                foreach (var (key, _) in Members)
                                {
                                    p.PrintLine($"names[{index}] = {key};");
                                    index++;

                                    if (index >= Members.Count)
                                    {
                                        break;
                                    }
                                }

                                p.PrintLine("return names;");
                            }
                            p.CloseScope();

                            p.PrintEndLine();

                            p.PrintLine(GENERATED_CODE).PrintLine(EXCLUDE_COVERAGE);
                            p.PrintLine($"public static {FixedStringTypeName} Get({FullyQualifiedName} value)");
                            p = p.IncreasedIndent();
                            p.PrintLine($"=> value switch");
                            p.OpenScope();
                            {
                                foreach (var (key, _) in Members)
                                {
                                    p.PrintLine($"{FullyQualifiedName}.{key} => {key},");
                                }

                                p.PrintLine($"_ => ToFixedString(ToUnderlyingValue(value)),");
                            }
                            p.CloseScope("};");
                            p = p.DecreasedIndent();
                        }
                        p.CloseScope();
                    }
                }
                p.CloseScope();
            }
            p = p.DecreasedIndent();

            return p.Result;
        }
    }
}
