﻿using Microsoft.CodeAnalysis;
using MVVMGenerator.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MVVMGenerator.Generators
{
    [Generator]
    public class AutoNotifyGenerator : BaseAttributeGenerator<IFieldSymbol, AutoNotifyAttribute>
    {
        protected override void AddUsings(List<string> usings, IFieldSymbol fieldSymbol)
        {
            usings.Add("using System.ComponentModel;");
            usings.Add("using System.Runtime.CompilerServices;");
            //Add field type's namespace to usings
            usings.Add($"using {fieldSymbol.Type.ContainingNamespace};");
            foreach (var fieldAttribute in fieldSymbol.GetAttributes())
            {
                if (fieldAttribute.AttributeClass.Name == nameof(AutoNotifyAttribute)) continue;
                //Determine if Attribute can be applied to a Property by acquiring the AttributeUsageAttribute and checking its configuration
                var attributeClassAttributes = fieldAttribute.AttributeClass.GetAttributes();
                var usageAttributeData = attributeClassAttributes.First(aca => aca.AttributeClass.Name == nameof(AttributeUsageAttribute));
                var targets = usageAttributeData.ConstructorArguments.First(ad => ad.Type.Name == nameof(AttributeTargets));
                var result = (AttributeTargets)(int)targets.Value;
                var validOnProperty = result.HasFlag(AttributeTargets.Property);
                //Add using statement for transferred attributes
                if (validOnProperty)
                    usings.Add($"using {fieldAttribute.AttributeClass.ContainingNamespace};");
            }
        }
        protected override void AddProperties(List<string> properties, IFieldSymbol fieldSymbol)
        {
            var name = fieldSymbol.Name;
            var type = fieldSymbol.Type.Name;
            if (fieldSymbol.Type is INamedTypeSymbol namedTypeSymbol && namedTypeSymbol.IsGenericType)
            {
                type = $"{type}<";
                int argLen = namedTypeSymbol.TypeArguments.Length;
                for (int i = 0; i < argLen; i++)
                {
                    var arg = namedTypeSymbol.TypeArguments[i];
                    type = $"{type}{arg.Name}{(i < argLen - 1 ? "," : string.Empty)}";
                }
                type = $"{type}>";
            }
            if (fieldSymbol.Type.NullableAnnotation.HasFlag(NullableAnnotation.Annotated))
                type = $"{type}?";

            List<string> propertyAttributes = new();
            foreach (var fieldAttribute in fieldSymbol.GetAttributes())
            {
                if (fieldAttribute.AttributeClass.Name == nameof(AutoNotifyAttribute)) continue;

                var attributeClassAttributes = fieldAttribute.AttributeClass.GetAttributes();

                var usageAttributeData = attributeClassAttributes.First(aca => aca.AttributeClass.Name == nameof(AttributeUsageAttribute));
                var targets = usageAttributeData.ConstructorArguments.First(ad => ad.Type.Name == nameof(AttributeTargets));
                var result = (AttributeTargets)(int)targets.Value;
                var validOnProperty = result.HasFlag(AttributeTargets.Property);
                if (validOnProperty)
                    propertyAttributes.Add(fieldAttribute.AttributeClass.Name.Replace("Attribute", ""));
            }
            string propertyAttributesString = string.Empty;
            if (propertyAttributes.Any())
                propertyAttributesString = $"[{(propertyAttributes.Aggregate((a, b) => $"{a}, {b}"))}]";

            properties.Add($$"""
                            {{propertyAttributesString}}
                            public {{type}} {{name.Substring(0, 1).ToUpper()}}{{name.Substring(1)}}
                            {
                                get => {{name}};
                                set
                                {
                                    {{name}} = value;
                                    OnPropertyChanged();
                                }
                            }
                    """);
        }

        protected override void AddInterfaceImplementations(List<string> impls, IFieldSymbol fieldSymbol)
        {
            impls.Add($$"""
                            public event PropertyChangedEventHandler? PropertyChanged;
                            void OnPropertyChanged([CallerMemberName] string? propertyName = null) 
                                 => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
                   """);
        }

        protected override void AddInterfaces(List<string> interfaces, IFieldSymbol fieldSymbol)
        {
            interfaces.Add("INotifyPropertyChanged");
        }
    }

}
