using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProductivityApp.Models;
using Xamarin.Forms;

namespace ProductivityApp.Services
{
    public class AttachmentService
    {
        private readonly List<Attachment> attachments;

        public AttachmentService()
        {
            attachments = new List<Attachment>();
        }

        public async Task<bool> AddItemAsync(string filePath)
        {
            var fileName = Path.GetFileName(filePath);
            byte[] fileContent;

            using (var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                using (var reader = new BinaryReader(stream))
                {
                    fileContent = reader.ReadBytes((int)stream.Length);
                }
            }
            attachments.Add(new Attachment(fileName, fileContent));

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
    }
}
