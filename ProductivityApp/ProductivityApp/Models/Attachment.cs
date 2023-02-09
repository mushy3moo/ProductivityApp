using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ProductivityApp.Models
{
    public class Attachment
    {
        public string FileName { get; set; }
        public byte[] FileContent { get; set; }

        public Attachment(string fileName, byte[] fileContent)
        {
           FileName = fileName;
           FileContent = fileContent;
        }
    }
}
