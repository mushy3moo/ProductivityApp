using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ProductivityApp.Models
{
    public class AttachmentModel
    {
        public string FileName { get; set; }
        public byte[] FileContent { get; set; }
        public string Image { get; set; }
    }
}
