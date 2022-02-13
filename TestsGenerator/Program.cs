using System;
using System.IO;
using System.Threading.Tasks;
using TestsGeneratorLib;

namespace TestsGenerator
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            string[] srcFiles = new string[]
            {
                "C:\\Users\\Asus\\Visual Studio\\TestsGenerator\\Example\\Class3.cs",
                "C:\\Users\\Asus\\Visual Studio\\TestsGenerator\\Example\\Class4.cs",
                "C:\\Users\\Asus\\Visual Studio\\TestsGenerator\\Example\\Class1.cs",
                "C:\\Users\\Asus\\Visual Studio\\TestsGenerator\\Example\\Class2.cs"
            };
            string dstPath = "C:\\Users\\Asus\\Visual Studio\\TestsGenerator\\Tests";
            Conveyer conveyer = new Conveyer();
            await conveyer.startConveyer(srcFiles, dstPath);
        }
    }
}