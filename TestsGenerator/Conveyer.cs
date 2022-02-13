using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using TestsGeneratorLib;
using System;
using System.Threading;

namespace TestsGenerator
{
    public class Conveyer
    {
        public async Task startConveyer(string[] srcFiles, string dstPath)
        {
            Directory.CreateDirectory(dstPath);

            var getSrcFiles = new TransformBlock<string, string>//считывает название файла, вернет информацию из файла
            (
                async path => //получает один аргумент
                {
                    using (var reader = new StreamReader(path)) //очищаем память
                    {
                        Thread.Sleep(1000);
                        Console.WriteLine("1");
                        return await reader.ReadToEndAsync();
                    }
                },
                new ExecutionDataflowBlockOptions { MaxDegreeOfParallelism = 4 }
            );

            var generateTests = new TransformManyBlock<string, KeyValuePair<string, string>>//принял от прошлого блока код, отправил готовые тесты
            (
                async sourceCode =>
                {
                    var fileInfo = await Task.Run(() => new CodeParser().GetFileElement(sourceCode)); //пропарсил файл
                    Console.WriteLine("2");
                    return await Task.Run(() => new Generator().GenerateTests(fileInfo));//готовые тесты
                },
                new ExecutionDataflowBlockOptions { MaxDegreeOfParallelism = 4}
            );

            var writeTests = new ActionBlock<KeyValuePair<string, string>>//(цель) запись файлов в директорию
            (
                 async fileNameCodePair =>
                 {
                     using (var writer = new StreamWriter(dstPath + '\\' + fileNameCodePair.Key + ".cs"))
                     {
                         Console.WriteLine("3");
                         await writer.WriteAsync(fileNameCodePair.Value);
                     }
                 },
                new ExecutionDataflowBlockOptions { MaxDegreeOfParallelism = 4 }
            );


            getSrcFiles.LinkTo(generateTests, new DataflowLinkOptions { PropagateCompletion = true }); //указали переход на следующий блок пайплайна
            generateTests.LinkTo(writeTests, new DataflowLinkOptions { PropagateCompletion = true });
            foreach (string srcFile in srcFiles)
            {
                getSrcFiles.Post(srcFile); //передаем файлы
            }

            getSrcFiles.Complete(); //ожидание выполнения getSrcFiles
            await writeTests.Completion;
        }
    }
}