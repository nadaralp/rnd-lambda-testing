using System;

namespace S3.UploadProjectToS3
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("enter what command you want to execute: upload/rollback/seever");
            var command = Console.ReadLine().ToLower();
        }
    }
}