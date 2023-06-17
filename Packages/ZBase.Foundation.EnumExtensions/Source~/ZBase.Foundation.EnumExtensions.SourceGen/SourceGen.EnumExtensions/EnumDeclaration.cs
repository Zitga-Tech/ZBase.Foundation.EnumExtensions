using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Text;
using ZBase.Foundation.SourceGen;

namespace ZBase.Foundation.EnumExtensions
{
    public partial class EnumDeclaration
    {
        private const string DISPLAY_ATTRIBUTE = "System.ComponentModel.DataAnnotations.DisplayAttribute";
        private const string DESCRIPTION_ATTRIBUTE = "System.ComponentModel.DescriptionAttribute";
        private const string ENUM_DISPLAY_ATTRIBUTE = "ZBase.Foundation.EnumExtensions.DisplayAttribute";

        public EnumDeclarationSyntax Syntax { get; private set; }

        public string FullyQualifiedName { get; private set; }

        public string UnderlyingTypeName { get; private set; }

        public EquatableArray<(string Key, EnumValueOption Value)> Names { get; private set; }

        public bool IsPublic { get; private set; }

        public bool HasFlags { get; private set; }

        public bool ReferenceUnityCollections { get; private set; }

        public string FixedStringTypeName { get; private set; }

        public bool IsDisplayAttributeUsed { get; private set; }

        public EnumDeclaration(
              EnumDeclarationSyntax syntax
            , INamedTypeSymbol symbol
            , bool hasFlags
        )
        {
            Syntax = syntax;
            FullyQualifiedName = symbol.ToFullName();
            UnderlyingTypeName = symbol.EnumUnderlyingType.ToString();
            IsPublic = symbol.DeclaredAccessibility == Accessibility.Public;
            HasFlags = hasFlags;

            foreach (var assembly in symbol.ContainingModule.ReferencedAssemblySymbols)
            {
                if (assembly.ToDisplayString().StartsWith("Unity.Collections,"))
                {
                    ReferenceUnityCollections = true;
                    break;
                }
            }

            var enumMembers = symbol.GetMembers();
            var members = new List<(string, EnumValueOption)>(enumMembers.Length);
            var displayNames = new HashSet<string>();
            var isDisplayNameTheFirstPresence = false;
            var maxByteCount = 0;

            foreach (var member in enumMembers)
            {
                if (member is not IFieldSymbol field
                    || field.ConstantValue is null)
                {
                    continue;
                }

                string displayName = null;

                foreach (var attribute in member.GetAttributes())
                {
                    if (attribute.AttributeClass?.Name == "DisplayAttribute")
                    {
                        var attribName = attribute.AttributeClass.ToDisplayString();

                        if (attribName == DISPLAY_ATTRIBUTE || attribName == ENUM_DISPLAY_ATTRIBUTE)
                        {
                            foreach (var namedArgument in attribute.NamedArguments)
                            {
                                if (namedArgument.Key == "Name" && namedArgument.Value.Value?.ToString() is { } dn)
                                {
                                    // found display attribute, all done
                                    displayName = dn;
                                    goto ADD_DISPLAY_NAME;
                                }
                            }
                        }
                    }

                    if (attribute.AttributeClass?.Name == "DescriptionAttribute"
                        && attribute.AttributeClass.ToDisplayString() == DESCRIPTION_ATTRIBUTE
                        && attribute.ConstructorArguments.Length == 1
                    )
                    {
                        if (attribute.ConstructorArguments[0].Value?.ToString() is { } dn)
                        {
                            // found display attribute, all done
                            displayName = dn;
                            goto ADD_DISPLAY_NAME;
                        }
                    }
                }

                ADD_DISPLAY_NAME:
                if (displayName is not null)
                {
                    isDisplayNameTheFirstPresence = displayNames.Add(displayName);
                }

                var nameByteCount = GetByteCount(member.Name);
                var displayNameByteCount = GetByteCount(displayName);
                var byteCount = Math.Max(nameByteCount, displayNameByteCount);
                maxByteCount = Math.Max(maxByteCount, byteCount);

                members.Add((member.Name, new EnumValueOption(displayName, isDisplayNameTheFirstPresence)));
            }

            Names = new EquatableArray<(string Key, EnumValueOption Value)>(members.ToArray());
            IsDisplayAttributeUsed = displayNames.Count > 0;

            if (ReferenceUnityCollections)
            {
                FixedStringTypeName = maxByteCount switch {
                    <= 32 => "global::Unity.Collections.FixedString32Bytes",
                    <= 64 => "global::Unity.Collections.FixedString64Bytes",
                    <= 128 => "global::Unity.Collections.FixedString128Bytes",
                    <= 512 => "global::Unity.Collections.FixedString512Bytes",
                    <= 4096 => "global::Unity.Collections.FixedString4096Bytes",
                    _ => string.Empty,
                };
            }
        }

        private static int GetByteCount(string value)
        {
            if (value == null)
                return 0;

            return Encoding.UTF8.GetByteCount(value);
        }
    }
}
