using System;
using System.IO;
using System.IO.Compression;
using static System.Console;
namespace ConsoleApp4_Binary_
{
    class Program
    {

    static void Main(string[] args)
    {
            // здесь записываем в файл  цифры с помощью Stream writer 1млн сделаем через Random.
            // создаем файл binary и пишем рандомные числа
            FileInfo f = new FileInfo("binary.txt");
            using (BinaryWriter bw = new BinaryWriter(f.OpenWrite()))
            {
                Random r = new Random();
                for (int i = 0; i < 1000000; i++)
                {
                    bw.Write(r.Next(0, 1000));
                }
                bw.Close();
            }
            // с помощью BinaryRead читаем
            using (BinaryReader br = new BinaryReader(f.OpenRead()))
            {
                WriteLine(br.ReadInt32());
            }
            WriteLine("Данные успешно записались");
            ReadLine();

        string sourseFile = "binary.txt";
        string compressedFile = "binary.gz";
        string targetFile = "binary_new.txt";
        // здесь делаем сжатия файла
        Compress(sourseFile, compressedFile);
        // восстановления сджатого файла с новым именем numbers_new
        Decompress(compressedFile, targetFile);
    }
    public static void Compress(string sourceFile, string compressedFile)
    {
        // с using создаем потоки 
        using (FileStream sourceStream = new FileStream(sourceFile, FileMode.OpenOrCreate))
        {
            using (FileStream targetStream = File.Create(compressedFile))
            {
                using (GZipStream compressionStream = new GZipStream(targetStream, CompressionMode.Compress))
                {
                    sourceStream.CopyTo(compressionStream);
                    Console.WriteLine("Сжатие файла {0} завершено. Исходный размер: {1}  сжатый размер: {2}.",
                        sourceFile, sourceStream.Length.ToString(), targetStream.Length.ToString());
                }
            }
        }
    }
    public static void Decompress(string compressedFile, string targetFile)
    {
        using (FileStream sourceStream = new FileStream(compressedFile, FileMode.OpenOrCreate))
        {
            using (FileStream targetStream = File.Create(targetFile))
            {
                using (GZipStream decompressionStream = new GZipStream(sourceStream, CompressionMode.Decompress))
                {
                    decompressionStream.CopyTo(targetStream);
                    Console.WriteLine("Восстановлен файл: {0}", targetFile);
                }
            }
        }

    }
}
}