using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Text;
using ZBase.Foundation.SourceGen;

namespace ZBase.Foundation.EnumExtensions
{
    public partial class EnumDeclaration
    {
        private const string DISPLAY_ATTRIBUTE = "ZBase.Foundation.EnumExtensions.DisplayAttribute";

        public string EnumName { get; private set; }

        public string ExtensionsName { get; private set; }

        public bool ParentIsNamespace { get; private set; }

        public string FullyQualifiedName { get; private set; }

        public string UnderlyingTypeName { get; private set; }

        public EquatableArray<(string Key, EnumValueOption Value)> Members { get; private set; }

        public Accessibility Accessibility { get; private set; }

        public bool HasFlags { get; private set; }

        public bool ReferenceUnityCollections { get; private set; }

        public string FixedStringTypeName { get; private set; }

        public bool IsDisplayAttributeUsed { get; private set; }

        public EnumDeclaration(
              INamedTypeSymbol symbol
            , bool hasFlags
            , bool parentIsNamespace
            , string extensionsName
            , Accessibility accessibility
        )
        {
            EnumName = symbol.Name;
            ExtensionsName = extensionsName;
            ParentIsNamespace = parentIsNamespace;
            FullyQualifiedName = symbol.ToFullName();
            UnderlyingTypeName = symbol.EnumUnderlyingType.ToString();
            Accessibility = accessibility;
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
                    || field.ConstantValue is null
                )
                {
                    continue;
                }

                string displayName = null;

                foreach (var attribute in member.GetAttributes())
                {
                    var attributeName = attribute.AttributeClass?.Name ?? string.Empty;

                    if (attributeName == nameof(System.ObsoleteAttribute))
                    {
                        goto CONTINUE;
                    }

                    if (attributeName == "DisplayAttribute"
                        && attribute.AttributeClass.ToDisplayString() == DISPLAY_ATTRIBUTE
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
                {
                    if (displayName is not null)
                    {
                        isDisplayNameTheFirstPresence = displayNames.Add(displayName);
                    }

                    var nameByteCount = GetByteCount(member.Name);
                    var displayNameByteCount = GetByteCount(displayName);
                    var byteCount = Math.Max(nameByteCount, displayNameByteCount);
                    maxByteCount = Math.Max(maxByteCount, byteCount);

                    members.Add((member.Name, new EnumValueOption(displayName, isDisplayNameTheFirstPresence)));
                    continue;
                }

                CONTINUE:
                {
                    continue;
                }
            }

            Members = new EquatableArray<(string Key, EnumValueOption Value)>(members.ToArray());
            IsDisplayAttributeUsed = displayNames.Count > 0;

            if (ReferenceUnityCollections)
            {
                FixedStringTypeName = maxByteCount switch {
                    <= 32 => "global::Unity.Collections.FixedString32Bytes",
                    <= 64 => "global::Unity.Collections.FixedString64Bytes",
                    <= 128 => "global::Unity.Collections.FixedString128Bytes",
                    <= 512 => "global::Unity.Collections.FixedString512Bytes",
                    _ => "global::Unity.Collections.FixedString4096Bytes",
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
