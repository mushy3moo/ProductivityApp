using Autofac.Util;
using ProductivityApp.Models;
using System;
using System.IO;
using System.Threading.Tasks;

namespace ProductivityApp.Helpers
{
    public class AttachmentHelper : Disposable
    {
        public AttachmentHelper() { }

        public async Task<AttachmentModel> CreateAttachmentAsync(string fileName)
        {

            if (File.Exists(fileName))
            {
                return new AttachmentModel()
                {
                    Id = Guid.NewGuid().ToString(),
                    FileName = fileName,
                    FileContent = await ReadFileContentAsync(fileName),
                    FileTypeIcon = GetFileTypeIcon(fileName)
                };
            }
            else
            {
                return null;
            }
        }

        public string GetFileTypeIconFromMime(string contentType)
        {
            var type = string.Empty;
            var subType = string.Empty;

            if (!string.IsNullOrWhiteSpace(contentType))
            {
                contentType = contentType.ToLower();
                if (contentType.Contains("/"))
                {
                    type = contentType.Split('/')[0];
                    subType = contentType.Split('/')[1];
                }
            }

            string source = type switch
            {
                "image" => "icon_image.png",
                "audio" => "icon_audio.png",
                "video" => "icon_video.png",
                "text" => subType switch
                {
                    "plain" => "icon_txt.png",
                    "html" => "icon_html.png",
                    "css" => "icon_css.png",
                    _ => "icon_none.png",
                },
                "application" => subType switch
                {
                    "javascript" => "icon_java.png",
                    "json" => "icon_json.png",
                    "xml" => "icon_xml.png",
                    "pdf" => "icon_pdf.png",
                    "msword" => "icon_doc.png",
                    "vnd.openxmlformats-officedocument.wordprocessingml.document" => "icon_doc.png",
                    "vnd.ms-excel" => "icon_xls.png",
                    "vnd.openxmlformats-officedocument.spreadsheetml.sheet" => "icon_xls.png",
                    "vnd.ms-powerpoint" => "icon_ppt.png",
                    "vnd.openxmlformats-officedocument.presentationml.presentation" => "icon_ppt.png",
                    _ => "icon_none.png"
                },
                _ => "icon_none.png",
            };
            return source;
        }

        private async Task<byte[]> ReadFileContentAsync(string filePath)
        {
            using (var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                byte[] buffer = new byte[fileStream.Length];
                await fileStream.ReadAsync(buffer, 0, buffer.Length);
                return buffer;
            }
        }

        private string GetFileTypeIcon(string filePath)
        {
            var extention = Path.GetExtension(filePath);
            string source;

            switch (extention.ToLower())
            {
                case "jpg":
                case "jpeg":
                case "png":
                case "gif":
                case "bmp":
                    source = "icon_image.png";
                    break;
                case "mp3":
                case "wav":
                case "aac":
                case "wma":
                    source = "icon_audio.png";
                    break;
                case "mp4":
                case "avi":
                case "mov":
                case "wmv":
                    source = "icon_video.png";
                    break;
                case "txt":
                    source = "icon_txt.png";
                    break;
                case "html":
                case "htm":
                    source = "icon_html.png";
                    break;
                case "css":
                    source = "icon_css.png";
                    break;
                case "js":
                    source = "icon_java.png";
                    break;
                case "json":
                    source = "icon_json.png";
                    break;
                case "xml":
                    source = "icon_xml.png";
                    break;
                case "pdf":
                    source = "icon_pdf.png";
                    break;
                case "doc":
                case "docx":
                    source = "icon_doc.png";
                    break;
                case "xls":
                case "xlsx":
                    source = "icon_xls.png";
                    break;
                case "ppt":
                case "pptx":
                    source = "icon_ppt.png";
                    break;
                default:
                    source = "icon_none.png";
                    break;
            }
            return source;
        }
    }
}
