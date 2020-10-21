using RSVentaja.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace RSVentaja.Domain.Entity
{
    public class GetFileResponse
    {
        public string FileName { get; private set; }
        public string EncodedFileData { get; private set; }

        public GetFileResponse(File file)
        {
            FileName = file.FileName;
            EncodedFileData = file.Base64Data;
        }
    }
}
