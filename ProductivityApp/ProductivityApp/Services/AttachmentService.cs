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
        private readonly List<AttachmentModel> attachments;

        public AttachmentService()
        {
            attachments = new List<AttachmentModel>();
        }

        public async Task<bool> AddItemAsync(AttachmentModel attachment)
        {
            attachments.Add(attachment);
            return await Task.FromResult(true);
        }

        public async Task<bool> AddItemsAsync(IEnumerable<AttachmentModel> attachments)
        {
            this.attachments.AddRange(attachments);
            return await Task.FromResult(true);
        }

        public async Task<AttachmentModel> GetItemAsync(string fileName)
        {
            var attachment = attachments.Find((AttachmentModel a) => a.FileName == fileName);
            return await Task.FromResult(attachment);
        }

        public async Task<IEnumerable<AttachmentModel>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(attachments);
        }

        public async Task<bool> UpdateItemAsync(string fileName, AttachmentModel attachment) 
        {
            var oldAttachment = attachments.Find((AttachmentModel a) => a.FileName == fileName);
            attachments.Remove(oldAttachment);
            attachments.Add(attachment);
            return await Task.FromResult(true);
        }


        public async Task<bool> DeleteItemAsync(string fileName)
        {
            var oldItem = attachments.Find((AttachmentModel a) => a.FileName == fileName);
            attachments.Remove(oldItem);

            return await Task.FromResult(true);
        }
    }
}
