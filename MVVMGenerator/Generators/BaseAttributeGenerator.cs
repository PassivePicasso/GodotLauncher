using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MVVMGenerator.Generators
{
    internal static class GenDebugger
    {
        private static bool launchedRequest = false;
        public static bool LaunchRequested
        {
            get
            {
                if (launchedRequest) return true;

                launchedRequest = true;
                return false;
            }
        }
    }
    public abstract class BaseAttributeGenerator<Symbol, Attribute> : ISourceGenerator
        where Symbol : ISymbol
        where Attribute : System.Attribute
    {
        protected static readonly string AttributeName = typeof(Attribute).Name;
        protected static readonly Func<string, string, string> appendLines = (a, b) => $"{a}\r\n{b}";
        protected static readonly Func<string, string, string> appendInterfaces = (a, b) => $"{a}, {b}";
        Func<AttributeData, bool> SymbolAttribute
        {
            get
            {
                return a =>
                {
                    bool v = a.AttributeClass.Name == AttributeName;
                    return v;
                };
            }
        }

        Func<ISymbol, bool> SymbolContainsAttribute => p => p.GetAttributes().Any(SymbolAttribute);
        public void Initialize(GeneratorInitializationContext context)
        {
#if DEBUG
            //if (GenDebugger.LaunchRequested) return;
            //if (System.Diagnostics.Debugger.IsAttached) return;
            //System.Diagnostics.Debugger.Launch();
#endif
        }

        public void Execute(GeneratorExecutionContext context)
        {
            var compilation = context.Compilation;
            var customExportClassSymbols = context.Compilation.SyntaxTrees
                .SelectMany(tree =>
                    tree.GetRoot()
                    .DescendantNodes()
                    .OfType<ClassDeclarationSyntax>()
                    .Select(classDec => compilation.GetSemanticModel(classDec.SyntaxTree).GetDeclaredSymbol(classDec))
                    .Where((classSymbol) => classSymbol.GetMembers().Any(SymbolContainsAttribute))
                    );

            foreach (var classSymbol in customExportClassSymbols)
            {
                var generatedCode = GeneratePartialClass(classSymbol);
                if (string.IsNullOrEmpty(generatedCode)) continue;

                var sourceText = SourceText.From(generatedCode, Encoding.UTF8);
                context.AddSource($"{classSymbol.Name}.{GetType().Name}.cs", sourceText);
            }
        }

        private string GeneratePartialClass(INamedTypeSymbol classSymbol)
        {
            var namespaceName = classSymbol.ContainingNamespace.ToDisplayString();
            List<string> usings/*    */= new();
            List<string> interfaces/**/= new();
            List<string> nestedClasses = new();
            List<string> iImpls/*    */= new();
            List<string> properties/**/= new();
            List<string> fields/*    */= new();

            var symbols = classSymbol.GetMembers()
                .Where(p => p.GetAttributes().Any(SymbolAttribute))
                .ToArray();
            if (symbols.Length == 0) return string.Empty;

            foreach (var tSymbol in symbols.OfType<Symbol>())
            {
                AddUsings/*              */(usings,/*    */tSymbol);
                AddInterfaces/*          */(interfaces,/**/tSymbol);
                AddNestedClasses/*       */(nestedClasses, tSymbol);
                AddInterfaceImplementations(iImpls,/*    */tSymbol);
                AddFields/*              */(fields,/*    */tSymbol);
                AddProperties/*          */(properties,/**/tSymbol);
            }
            string classUsing = $"using {classSymbol.ContainingNamespace};";
            usings.RemoveAll(usng => usng.Contains(classUsing));
            usings.Sort();

            string derivationSeparator = (interfaces.Any() ? " : " : string.Empty);
            string renderedInterfaceList = RenderInterfaces(interfaces);
            string renderedUsings/*    */= Render(usings);
            string renderedNestedClasses = Render(nestedClasses);
            string renderedIntImpl/*   */= Render(iImpls);
            string renderedFields/*    */= Render(fields);
            string renderedProperties/**/= Render(properties);

            return $$"""
                    {{renderedUsings}}
                                                            
                    namespace {{namespaceName}}
                    {
                        public partial class {{classSymbol.Name}}{{derivationSeparator}}{{renderedInterfaceList}}
                        {
                    {{renderedNestedClasses}}
                    {{renderedIntImpl}}
                    {{renderedFields}}
                    {{renderedProperties}}
                        }
                    }
                    """;
        }

        string Render(IEnumerable<string> strings) => strings.Any()
                                                    ? strings.Distinct().Aggregate(appendLines)
                                                    : string.Empty;
        string RenderInterfaces(IEnumerable<string> interfaces) => interfaces.Any()
                                                                 ? interfaces.Distinct().Aggregate(appendInterfaces)
                                                                 : string.Empty;

        protected virtual void AddUsings(List<string> definitions, Symbol symbol) { }
        protected virtual void AddInterfaces(List<string> definitions, Symbol symbol) { }
        protected virtual void AddNestedClasses(List<string> definitions, Symbol symbol) { }
        protected virtual void AddInterfaceImplementations(List<string> definitions, Symbol symbol) { }
        protected virtual void AddFields(List<string> definitions, Symbol symbol) { }
        protected virtual void AddProperties(List<string> definitions, Symbol symbol) { }
        protected virtual void AddConstructorInitializers(List<string> definitions, Symbol symbol) { }
    }
}
