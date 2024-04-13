using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Text;
using ZBase.Foundation.SourceGen;

namespace ZBase.Foundation.EnumExtensions
{
    public partial class EnumDeclaration
    {
        public string ExtensionsName { get; private set; }

        public bool ParentIsNamespace { get; private set; }

        public string FullyQualifiedName { get; private set; }

        public string UnderlyingTypeName { get; private set; }

        public List<EnumMemberDeclaration> Members { get; private set; }

        public Accessibility Accessibility { get; private set; }

        public bool HasFlags { get; private set; }

        public bool ReferenceUnityCollections { get; private set; }

        public string FixedStringTypeName { get; private set; }

        public bool IsDisplayAttributeUsed { get; private set; }

        public bool OnlyNames { get; private set; }

        public bool NoDocumentation { get; private set; }

        public EnumDeclaration(
              INamedTypeSymbol symbol
            , bool hasFlags
            , bool parentIsNamespace
            , string extensionsName
            , Accessibility accessibility
        )
        {
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
            var members = Members = new List<EnumMemberDeclaration>(enumMembers.Length);
            var maxByteCount = 0;
            var isDisplayAttributeUsed = false;

            foreach (var member in enumMembers)
            {
                if (member is not IFieldSymbol field
                    || field.ConstantValue is null
                )
                {
                    continue;
                }

                string displayName = null;

                foreach (var attribute in field.GetAttributes())
                {
                    var attributeName = attribute.AttributeClass?.Name ?? string.Empty;

                    switch (attributeName)
                    {
                        case nameof(System.ObsoleteAttribute):
                        {
                            goto CONTINUE;
                        }

                        case "DisplayAttribute":
                        case "DisplayNameAttribute":
                        case "InspectorNameAttribute":
                        {
                            if (attribute.ConstructorArguments.Length > 0)
                            {
                                var arg = attribute.ConstructorArguments[0];

                                if (arg.Kind == TypedConstantKind.Primitive && arg.Value?.ToString() is string dn)
                                {
                                    displayName = dn;
                                    goto ADD_DISPLAY_NAME;
                                }
                            }

                            if (attribute.NamedArguments.Length > 0)
                            {
                                foreach (var arg in attribute.NamedArguments)
                                {
                                    if (arg.Key == "Name"
                                        && arg.Value.Kind == TypedConstantKind.Primitive
                                        && arg.Value.Value?.ToString() is string dn
                                    )
                                    {
                                        displayName = dn;
                                        goto ADD_DISPLAY_NAME;
                                    }
                                }
                            }

                            break;
                        }
                    }
                }

                ADD_DISPLAY_NAME:
                {
                    if (displayName is not null && isDisplayAttributeUsed == false)
                    {
                        isDisplayAttributeUsed = true;
                    }

                    var nameByteCount = GetByteCount(member.Name);
                    var displayNameByteCount = GetByteCount(displayName);
                    var byteCount = Math.Max(nameByteCount, displayNameByteCount);
                    maxByteCount = Math.Max(maxByteCount, byteCount);

                    members.Add(new EnumMemberDeclaration {
                        name = member.Name,
                        displayName = displayName,
                    });
                    continue;
                }

                CONTINUE:
                {
                    continue;
                }
            }

            IsDisplayAttributeUsed = isDisplayAttributeUsed;

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

        public struct EnumMemberDeclaration
        {
            public string name;
            public string displayName;
        }
    }
}
