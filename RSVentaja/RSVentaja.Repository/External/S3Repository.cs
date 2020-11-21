using Amazon;
using Amazon.S3;
using Amazon.S3.Transfer;
using RSVentaja.Domain.Entity;
using RSVentaja.Domain.Repository;
using RSVentaja.Domain.ValueObjects;
using System.Threading.Tasks;

namespace RSVentaja.Repository.External
{
    public class S3Repository : IS3Repository
    {
        private AmazonS3Client _amazonS3Client;
        private readonly S3Configuration _s3Configuration;

        public S3Repository(S3Configuration s3Configuration)
        {
            _s3Configuration = s3Configuration;
            _amazonS3Client = new AmazonS3Client(_s3Configuration.AwsAccessKeyId, _s3Configuration.AwsSecretAccessKey, RegionEndpoint.SAEast1);
        }

        public async Task StoreFile(string id, File file)
        {
            var fileTransferUtility =
                    new TransferUtility(_amazonS3Client);
            System.IO.Stream stream = new System.IO.MemoryStream(file.FileData);
            var fileTransferUtilityRequest = new TransferUtilityUploadRequest
            {
                BucketName = _s3Configuration.BucketName,
                InputStream = stream,
                StorageClass = S3StorageClass.OneZoneInfrequentAccess,
                Key = $"{id}.pdf",
                CannedACL = S3CannedACL.PublicRead
            };
            await fileTransferUtility.UploadAsync(fileTransferUtilityRequest);
        }
    }
}
