using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using TestsGeneratorLib.FileElements;


namespace TestsGeneratorLib
{
    public class CodeParser
    {
        public FileElement GetFileElement(string code)
        {
            CompilationUnitSyntax root = CSharpSyntaxTree.ParseText(code).GetCompilationUnitRoot(); 
            var classes = new List<ClassElement>();
            foreach (ClassDeclarationSyntax classDeclaration in root.DescendantNodes().OfType<ClassDeclarationSyntax>())
            {
                classes.Add(GetClassElement(classDeclaration));
            }

            return new FileElement(classes);
        }

        private MethodElement GetMethodElement(MethodDeclarationSyntax method)
        {
            return new MethodElement(method.Identifier.ValueText);
        }


        private ClassElement GetClassElement(ClassDeclarationSyntax classDeclaration)
        {
            var methods = new List<MethodElement>();
            foreach (var method in classDeclaration.DescendantNodes().OfType<MethodDeclarationSyntax>().Where((methodDeclaration) => methodDeclaration.Modifiers.Any((modifier) => modifier.IsKind(SyntaxKind.PublicKeyword))))
            {
                methods.Add(GetMethodElement(method));
            }

            return new ClassElement(classDeclaration.Identifier.ValueText, methods);
        }
    }
}