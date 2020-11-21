using System;
using System.Collections.Generic;
using System.Text;

namespace RSVentaja.Domain.Entity
{
    public class S3Configuration
    {
        public string BucketName { get; set; }
        public string AwsAccessKeyId { get; set; }
        public string AwsSecretAccessKey { get; set; }
    }
}
