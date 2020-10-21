using System;
using System.Collections.Generic;
using System.Text;

namespace RSVentaja.Domain.ValueObjects
{
    public class File : Verifiable
    {
        public string FileName { get; private set; }
        public byte[] FileData { get; private set; }
        public string Base64Data { get; private set; }

        public File(string fileName, byte[] fileData)
        {
            Assert(() => !string.IsNullOrWhiteSpace(fileName));
            FileName = fileName;
            FileData = fileData;
            Base64Data = Convert.ToBase64String(fileData);
        }
    }
}
