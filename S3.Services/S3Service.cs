using Amazon.S3;
using Amazon.S3.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace S3.Services
{
    public class S3Service : IS3Serivce
    {
        private IAmazonS3 _s3Client = new AmazonS3Client();

        public async Task CreateBucketIfNotExistsAync(string bucketName)
        {
            bool doesExists = await _s3Client.DoesS3BucketExistAsync(bucketName);
            if (!doesExists)
            {
                await CreateBucketAsync(bucketName);
            }

            throw new InvalidOperationException($"Bucket with this name: {bucketName} already exists. Failed at {typeof(S3Service).FullName}.{nameof(CreateBucketIfNotExistsAync)}");
        }

        public async Task CreateBucketAsync(string bucketName)
        {
            var bucketCreationResponse = await _s3Client.PutBucketAsync(bucketName);
            if (bucketCreationResponse.HttpStatusCode != HttpStatusCode.OK)
            {
                throw new InvalidOperationException($"Couldn't create bucket -> {bucketCreationResponse.ResponseMetadata.Metadata}");
            }
        }

        public async Task<ICollection<S3Object>> ListObjectsAsync(string bucketName)
        {
            ListObjectsResponse response = await _s3Client.ListObjectsAsync(bucketName);
            return response?.S3Objects
                ?? throw new InvalidOperationException("Response was invalid in ListObjectsAsync method");
        }

        public Task RollBackAllObjectsInBucketAsync(string bucketName)
        {
            throw new NotImplementedException();
        }

        public async Task UploadObjectToBucketAsync(Stream objectStream, string bucketName, string key)
        {
            PutObjectRequest request = new PutObjectRequest()
            {
                Key = key,
                BucketName = bucketName,
                InputStream = objectStream
            };

            PutObjectResponse putObjectResponse = await _s3Client.PutObjectAsync(request);
            if (putObjectResponse.HttpStatusCode != HttpStatusCode.OK)
            {
                throw new InvalidOperationException($"Failed to upload object in {nameof(UploadObjectToBucketAsync)} - object: {key}");
            }
        }

        public async Task UploadFileToBucketAsync(string filePath, string bucketName)
        {
            string fileName = Path.GetFileName(filePath);
            using (var sr = new StreamReader(filePath))
            {
                Stream fileStream = sr.BaseStream;
                await UploadObjectToBucketAsync(fileStream, bucketName, fileName);
            }
        }

        public async Task DeleteBucketAsync(string bucketName)
        {
            await _s3Client.DeleteBucketAsync(bucketName);
        }

        public async Task DeleteObjectAsync(string objectKey, string bucketName)
        {
            await _s3Client.DeleteObjectAsync(bucketName, objectKey);
        }
    }
}