using System.Collections.Generic;
using System.Reflection;

namespace TestsGeneratorLib.FileElements
{
    public class MethodElement
    {
        public string MethodName { get; private set; }

        public MethodElement(string name)
        {
            MethodName = name;
        }
    }
}