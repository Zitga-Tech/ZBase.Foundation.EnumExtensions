using ZBase.Foundation.SourceGen;

namespace ZBase.Foundation.EnumExtensions
{
    partial class EnumDeclaration
    {
        private const string AGGRESSIVE_INLINING = "[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]";
        private const string GENERATED_CODE = "[global::System.CodeDom.Compiler.GeneratedCode(\"ZBase.Foundation.EnumExtensions.EnumExtensionsGenerator\", \"1.0.2\")]";
        private const string EXCLUDE_COVERAGE = "[global::System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]";

        public string WriteCode()
        {
            var typeName = $"{Syntax.Identifier.Text}Extensions";
            var scopePrinter = new SyntaxNodeScopePrinter(Printer.DefaultLarge, Syntax.Parent);
            var p = scopePrinter.printer;

            p = p.IncreasedIndent();
            {
                p.PrintLine("using System;");

                if (ReferenceUnityCollections)
                {
                    p.PrintLine("using Unity.Collections;");
                }

                p.PrintEndLine();

                p.PrintLine(GENERATED_CODE).PrintLine(EXCLUDE_COVERAGE);
                p.PrintBeginLine();
                {
                    if (IsPublic)
                        p.Print("public ");
                    else
                        p.Print("internal ");

                    p.Print("static partial class ").Print(typeName);
                }
                p.PrintEndLine();

                p.OpenScope();
                {
                    p.PrintLine("/// <summary>");
                    p.PrintLine("/// The number of members in the enum.");
                    p.PrintLine("/// This is a non-distinct count of defined names.");
                    p.PrintLine("/// </summary>");
                    p.PrintLine(GENERATED_CODE);
                    p.PrintLine($"public const int Length = {Names.Count};");

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
                    p.PrintLine($"public static string ToStringFast(this {FullyQualifiedName} value)");
                    p = p.IncreasedIndent();
                    p.PrintLine("=> DisplayNames.Get(value);");
                    p = p.DecreasedIndent();

                    p.PrintEndLine();

                    if (ReferenceUnityCollections)
                    {
                        p.PrintLine("/// <summary>");
                        p.PrintLine($"/// Returns the fixed string representation of the <see cref=\"{FullyQualifiedName}\"/> value.");
                        p.PrintLine("/// If the attribute is decorated with a <c>[Display]</c> attribute, then");
                        p.PrintLine("/// uses the provided value. Otherwise uses the name of the member, equivalent to");
                        p.PrintLine("/// calling <c>ToString()</c> on <paramref name=\"value\"/>.");
                        p.PrintLine("/// </summary>");
                        p.PrintLine("/// <param name=\"value\">The value to retrieve the string value for</param>");
                        p.PrintLine("/// <returns>The fixed string representation of the value</returns>");
                        p.PrintLine(AGGRESSIVE_INLINING).PrintLine(GENERATED_CODE).PrintLine(EXCLUDE_COVERAGE);
                        p.PrintLine($"public static {FixedStringTypeName} ToFixedStringFast(this {FullyQualifiedName} value)");
                        p = p.IncreasedIndent();
                        p.PrintLine("=> FixedDisplayNames.Get(value);");
                        p = p.DecreasedIndent();

                        p.PrintEndLine();
                    }

                    p.PrintLine(GENERATED_CODE).PrintLine(EXCLUDE_COVERAGE);
                    p.PrintLine("public static bool TryFormat(");
                    p = p.IncreasedIndent();
                    {
                        p.PrintLine($"  this {FullyQualifiedName} value");
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
                            p.PrintLine("var span = value.ToStringFast().AsSpan();");

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

                        p.PrintLine("if (value.ToUnderlyingValue().TryFormat(destination, out var chars, format, provider))");
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
                    p.PrintLine($"public static bool IsDefined(this {FullyQualifiedName} value)");
                    p = p.IncreasedIndent();
                    {
                        p.PrintLine("=> value switch");
                        p.OpenScope();
                        {
                            foreach (var (key, _) in Names)
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
                    p.PrintLine("public static bool IsDefined(string name)");
                    p = p.IncreasedIndent();
                    p.PrintLine("=> IsDefined(name, allowMatchingMetadataAttribute: false);");
                    p = p.DecreasedIndent();

                    p.PrintEndLine();

                    p.PrintLine("/// <summary>");
                    p.PrintLine("/// Returns a boolean telling whether an enum with the given name exists in the enumeration,");
                    p.PrintLine("/// or if a member decorated with a <c>[Display]</c> attribute");
                    p.PrintLine("/// with the required name exists.");
                    p.PrintLine("/// </summary>");
                    p.PrintLine("/// <param name=\"name\">The name to check if it's defined</param>");
                    p.PrintLine("/// <param name=\"allowMatchingMetadataAttribute\">If <c>true</c>, considers the value of metadata attributes,otherwise ignores them</param>");
                    p.PrintLine("/// <returns><c>true</c> if a member with the name exists in the enumeration, or a member is decorated");
                    p.PrintLine("/// with a <c>[Display]</c> attribute with the name, <c>false</c> otherwise</returns>");
                    p.PrintLine(GENERATED_CODE).PrintLine(EXCLUDE_COVERAGE);
                    p.PrintLine("public static bool IsDefined(string name, bool allowMatchingMetadataAttribute)");
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
                                    foreach (var (key, value) in Names)
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
                            foreach (var (key, _) in Names)
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
                    p.PrintLine($"public static {UnderlyingTypeName} ToUnderlyingValue(this {FullyQualifiedName} value)");
                    p = p.IncreasedIndent();
                    p.PrintLine($"=> ({UnderlyingTypeName})value;");
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
                    p.PrintLine("/// <returns><c>true</c> if the value parameter was converted successfully; otherwise, <c>false</c>.</returns>");
                    p.PrintLine(AGGRESSIVE_INLINING).PrintLine(GENERATED_CODE).PrintLine(EXCLUDE_COVERAGE);
                    p.PrintLine($"public static bool TryParse(string name, out {FullyQualifiedName} value)");
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
                    p.PrintLine($"public static bool TryParse(string name, out {FullyQualifiedName} value, bool ignoreCase)");
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
                    p.PrintLine($"public static bool TryParse(string name, out {FullyQualifiedName} value, bool ignoreCase, bool allowMatchingMetadataAttribute)");
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
                                        foreach (var (key, value) in Names)
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
                                        foreach (var (key, value) in Names)
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
                                foreach (var (key, _) in Names)
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
                                foreach (var (key, _) in Names)
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
                            p.PrintLine("fs.Append(value);");
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
                            foreach (var (key, _) in Names)
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
                            p.PrintLine($"public static global::Unity.Collections.NativeArray<{FullyQualifiedName}>.ReadOnly AsNativeArray(global::Unity.Collections.Allocator allocator)");
                            p = p.IncreasedIndent();
                            p.PrintLine($"=> new global::Unity.Collections.NativeArray<{FullyQualifiedName}> (s_values, allocator).AsReadOnly();");
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
                            foreach (var (key, _) in Names)
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
                            p.PrintLine($"public static global::Unity.Collections.NativeArray<{UnderlyingTypeName}>.ReadOnly AsNativeArray(global::Unity.Collections.Allocator allocator)");
                            p = p.IncreasedIndent();
                            p.PrintLine($"=> new global::Unity.Collections.NativeArray<{UnderlyingTypeName}> (s_values, allocator).AsReadOnly();");
                            p = p.DecreasedIndent();
                        }
                    }
                    p.CloseScope();

                    p.PrintEndLine();

                    p.PrintLine(GENERATED_CODE).PrintLine(EXCLUDE_COVERAGE);
                    p.PrintLine("public static class Names");
                    p.OpenScope();
                    {
                        foreach (var (key, _) in Names)
                        {
                            p.PrintLine(GENERATED_CODE);
                            p.PrintLine($"public const string {key} = nameof({FullyQualifiedName}.{key});");
                            p.PrintEndLine();
                        }

                        p.PrintLine($"private static readonly string[] s_names = new string[]");
                        p.OpenScope();
                        {
                            foreach (var (key, _) in Names)
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
                            foreach (var (key, _) in Names)
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
                        foreach (var (key, value) in Names)
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
                            foreach (var (key, _) in Names)
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
                            foreach (var (key, _) in Names)
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
                            foreach (var (key, _) in Names)
                            {
                                p.PrintLine(GENERATED_CODE).PrintLine(EXCLUDE_COVERAGE);
                                p.PrintLine($"public static {FixedStringTypeName} {key}");
                                p.OpenScope();
                                {
                                    p.PrintLine("get");
                                    p.OpenScope();
                                    {
                                        p.PrintLine($"var fs = new {FixedStringTypeName}();");
                                        p.PrintLine($"fs.Append(({FixedStringTypeName})nameof({FullyQualifiedName}.{key}));");
                                        p.PrintLine("return fs;");
                                    }
                                    p.CloseScope();
                                }
                                p.CloseScope();
                                p.PrintEndLine();
                            }

                            p.PrintLine(GENERATED_CODE).PrintLine(EXCLUDE_COVERAGE);
                            p.PrintLine($"public static global::Unity.Collections.NativeArray<{FixedStringTypeName}> AsNativeArray(global::Unity.Collections.Allocator allocator)");
                            p.OpenScope();
                            {
                                p.PrintLine($"var names = new global::Unity.Collections.NativeArray<{FixedStringTypeName}> ({typeName}.Length, allocator, NativeArrayOptions.UninitializedMemory);");

                                var index = 0;

                                foreach (var (key, _) in Names)
                                {
                                    p.PrintLine($"names[{index}] = {key};");
                                    index++;

                                    if (index >= Names.Count)
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
                                foreach (var (key, _) in Names)
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
                            foreach (var (key, value) in Names)
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
                                            p.PrintLine($"fs.Append(({FixedStringTypeName})\"{value.DisplayName}\");");
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
                                            p.PrintLine($"fs.Append(({FixedStringTypeName})nameof({FullyQualifiedName}.{key}));");
                                            p.PrintLine("return fs;");
                                        }
                                        p.CloseScope();
                                    }
                                    p.CloseScope();
                                }

                                p.PrintEndLine();
                            }

                            p.PrintLine(GENERATED_CODE).PrintLine(EXCLUDE_COVERAGE);
                            p.PrintLine($"public static global::Unity.Collections.NativeArray<{FixedStringTypeName}> AsNativeArray(global::Unity.Collections.Allocator allocator)");
                            p.OpenScope();
                            {
                                p.PrintLine($"var names = new global::Unity.Collections.NativeArray<{FixedStringTypeName}> ({typeName}.Length, allocator, NativeArrayOptions.UninitializedMemory);");

                                var index = 0;

                                foreach (var (key, _) in Names)
                                {
                                    p.PrintLine($"names[{index}] = {key};");
                                    index++;

                                    if (index >= Names.Count)
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
                                foreach (var (key, _) in Names)
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
