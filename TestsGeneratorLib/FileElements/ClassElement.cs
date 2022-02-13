using System.Collections.Generic;

namespace TestsGeneratorLib.FileElements
{
    public class ClassElement
    {
        public List<MethodElement> Methods { get; private set; }
        public string ClassName { get; private set; }

        public ClassElement(string className, List<MethodElement> methods)
        {
            ClassName = className;
            Methods = methods;
        }
    }
}