using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ProductivityApp.Models;

namespace ProductivityApp.Services
{
    public class AttachmentService
    {
        private readonly List<Attachment> attachments;

        public AttachmentService()
        {
            attachments = new List<Attachment>();
        }

        public async Task<bool> AddItemAsync(Attachment attachment)
        {
            attachments.Add(attachment);

            return await Task.FromResult(true);
        }

        public async Task<bool> AddItemAsync(string filePath)
        {
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException("The specified file does not exist at path: " + filePath);
            }

            var attachment = new Attachment
            {
                FileName = Path.GetFileName(filePath)
            };

            using (var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                using (var reader = new BinaryReader(stream))
                {
                    attachment.FileContent = reader.ReadBytes((int)stream.Length);
                }
            }
            
            attachment.Image = GetIconImage(filePath);

            attachments.Add(attachment);

            return await Task.FromResult(true);
        }

        public async Task<IEnumerable<Attachment>> GetItemsAsync()
        {
            return await Task.FromResult(attachments);
        }

        public async Task<bool> DeleteItemAsync(string fileName)
        {
            var oldItem = attachments.Where((Attachment a) => a.FileName == fileName).FirstOrDefault();
            attachments.Remove(oldItem);

            return await Task.FromResult(true);
        }

        public string GetIconImage(string filePath)
        {
            var extention = filePath.Split('.').Last();
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

        public string GetIconImageFromMime(string contentType)
        {
            var fileType = contentType.ToLower().Split('/')[0];
            string source;

            if (fileType == "image")
            {
                source = "icon_image.png";
            }
            else if (fileType == "audio")
            {
                source = "icon_audio.png";
            }
            else if (fileType == "video")
            {
                source = "icon_video.png";
            }
            else
            {
                switch (contentType.ToLower())
                {
                    case "text/plain":
                        source = "icon_txt.png";
                        break;
                    case "text/html":
                        source = "icon_html.png";
                        break;
                    case "text/css":
                        source = "icon_css.png";
                        break;
                    case "application/javascript":
                        source = "icon_java.png";
                        break;
                    case "application/json":
                        source = "icon_json.png";
                        break;
                    case "application/xml":
                        source = "icon_xml.png";
                        break;
                    case "application/pdf":
                        source = "icon_pdf.png";
                        break;
                    case "application/msword":
                    case "application/vnd.openxmlformats-officedocument.wordprocessingml.document":
                        source = "icon_doc.png";
                        break;
                    case "application/vnd.ms-excel":
                    case "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet":
                        source = "icon_xls.png";
                        break;
                    case "application/vnd.ms-powerpoint":
                    case "application/vnd.openxmlformats-officedocument.presentationml.presentation":
                        source = "icon_ppt.png";
                        break;
                    default:
                        source = "icon_none.png";
                        break;
                }
            }
            return source;
        }
    }
}
