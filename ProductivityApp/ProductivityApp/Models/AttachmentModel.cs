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
        public string FileName { get; set; }
        public byte[] FileContent { get; set; }
        public string FileTypeIcon { get; set; }
    }
}
