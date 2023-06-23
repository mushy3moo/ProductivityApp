using NUnit.Framework;
using ProductivityApp.Models;
using ProductivityApp.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductivityAppTests.UnitTests
{
    public class AttachmentServiceTests
    {
        [Test]
        public async Task AddItemAsync_AddsAttachment_ToDataStore()
        {
            var expectedAttachment = new AttachmentModel
            {
                FileName = "icon_attachment.png"
            };
            var dataStore = new AttachmentService();

            var result = await dataStore.AddItemAsync(expectedAttachment);
            var data = await dataStore.GetItemsAsync();
            var resultAttachment = data.FirstOrDefault();

            Assert.Multiple(() =>
            {
                Assert.That(result, Is.True);
                Assert.That(resultAttachment.FileName, Does.Contain(expectedAttachment.FileName));
            });
        }

        [TestCase("")]
        [TestCase("  ")]
        [Ignore("Test needs to be moved")]
        public void AddItemAsync_ThrowsDirectoryNotFoundException_WhenPassedIncorrectPath(string incorrectPath)
        {
            var expectedMessage = "The specified file does not exist at path: ";
            var dataStore = new AttachmentService();
            var attachment = new AttachmentModel { FileName = incorrectPath };

            var resultExecption = Assert.ThrowsAsync<FileNotFoundException>(() => dataStore.AddItemAsync(attachment));
            var result = resultExecption.Message.Contains(expectedMessage);
            Assert.That(result, Is.True);
        }

        [Test]
        public async Task GetItemAsync_ReturnsNoCount_WhenEmpty()
        {
            var dataStore = new AttachmentService();

            var result = await dataStore.GetItemsAsync();

            Assert.That(result.Count, Is.EqualTo(0));
        }

        [Test]
        public async Task GetIconImage_ReturnsIconNone_WhenNoMatch()
        {
            Assert.Fail();
        }
    }
}
