using Amazon.S3.Model;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace S3.Services
{
    public interface IS3Serivce
    {
        // Automatic private ACL bucket
        Task CreateBucketIfNotExistsAync(string bucketName);

        Task RollBackAllObjectsInBucketAsync(string bucketName);

        Task UploadObjectToBucketAsync(Stream objectStream, string bucketName, string key);

        // uploads a file from the specified path, the name of the file will be the name of the file on the OS.
        Task UploadFileToBucketAsync(string filePath, string bucketName);

        // returns all of the objects in the bucket
        Task<ICollection<S3Object>> ListObjectsAsync(string bucketName);

        Task CreateBucketAsync(string bucketName);

        Task DeleteBucketAsync(string bucketName);

        Task DeleteObjectAsync(string objectKey, string bucketName);
    }
}