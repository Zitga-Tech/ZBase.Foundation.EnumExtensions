using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Text;
using ZBase.Foundation.SourceGen;

namespace ZBase.Foundation.EnumExtensions
{
    public partial class EnumDeclaration
    {
        public string ExtensionsName { get; }
        
        public string ExtensionsWrapperName { get; }

        public bool ParentIsNamespace { get;  }

        public string Name { get; }
        
        public string FullyQualifiedName { get; }

        public string UnderlyingTypeName { get; }

        public List<EnumMemberDeclaration> Members { get; }

        public Accessibility Accessibility { get; }

        public bool HasFlags { get; }

        public bool ReferenceUnityCollections { get; }

        public string FixedStringTypeName { get; }

        public string FixedStringTypeFullyQualifiedName { get; }

        public bool IsDisplayAttributeUsed { get; }

        public bool OnlyNames { get; }

        public bool NoDocumentation { get; }

        public EnumDeclaration(
              INamedTypeSymbol symbol
            , bool hasFlags
            , bool parentIsNamespace
            , string extensionsName
            , Accessibility accessibility
        )
        {
            ExtensionsName = extensionsName;
            ExtensionsWrapperName = $"{extensionsName}Wrapper";
            ParentIsNamespace = parentIsNamespace;
            Name = symbol.Name;
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

                        case "DescriptionAttribute":
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
                FixedStringTypeName = (maxByteCount + 3) switch {
                    <= 32 => "FixedString32Bytes",
                    <= 64 => "FixedString64Bytes",
                    <= 128 => "FixedString128Bytes",
                    <= 512 => "FixedString512Bytes",
                    _ => "FixedString4096Bytes",
                };

                FixedStringTypeFullyQualifiedName = $"global::Unity.Collections.{FixedStringTypeName}";
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
