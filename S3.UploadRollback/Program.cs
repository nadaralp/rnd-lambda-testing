using S3.Services;
using System;
using System.IO;
using System.Threading.Tasks;

namespace S3.UploadRollback
{
    internal class Program
    {
        private static async Task Main(string[] args)
        {
            try
            {
                IS3Serivce s3Service = new S3Service();
                Console.WriteLine("Enter the bucket name you want to create: ");
                string bucketName = Console.ReadLine();

                // creating bucket if not existent
                //await s3Service.CreateBucketIfNotExistsAync(bucketName);
                string directoryPath = @"D:\Test Projects\Lambda-testing\Lambda\S3.UploadRollback\DemoProject\";
                string[] filesInDirectory = Directory.GetFiles(directoryPath);

                foreach (string file in filesInDirectory)
                {
                    await s3Service.UploadFileToBucketAsync(file, "nadar-alpenidze-testttt");
                }

                // tasks
                //1. create bucket with versioning.
                //2. Create bucket with ACL permissions attached.
                //3. upload object with public ACL permission and see that you can look at it from anonymous.
                //4. Enable versioning for bucket on creation.
                //5  create roll back methods for all bucket files.
                //6. glacier object?
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
        }
    }
}