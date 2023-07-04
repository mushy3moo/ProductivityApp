using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ProductivityApp.Models
{
    public class AttachmentModel
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("fileName")]
        public string FileName { get; set; }

        [JsonProperty("fileContent")]
        public byte[] FileContent { get; set; }

        [JsonProperty("fileTypeIcon")]
        public string FileTypeIcon { get; set; }
    }
}
