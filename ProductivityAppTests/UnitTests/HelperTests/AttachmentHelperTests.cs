using NUnit.Framework;
using ProductivityApp.Helpers;
using ProductivityApp.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductivityAppTests
{
    [TestFixture]
    public class AttachmentHelperTests
    {
        [Test]
        public async Task CreateAttachmentAsync_ReturnsNull_WhenFilePath_DoesNotExist()
        {
            var incorrectFilePath = "not/A_filepath";

            using (var attachmentHelper = new AttachmentHelper())
            {
                var result = await attachmentHelper.CreateAttachmentAsync(incorrectFilePath);

                Assert.That(result, Is.Null);
            }
        }
        
        [Test]
        public async Task CreateAttachmentAsync_ReturnsValidAttachment_WhenFilePath_Exists()
        {
            var testDirectoy = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
            var expectedFilePath = Path.Combine(testDirectoy, "UnitTests", "Data", "icon_attachment.png");

            using (var attachmentHelper = new AttachmentHelper())
            {
                var result = await attachmentHelper.CreateAttachmentAsync(expectedFilePath);

                Assert.Multiple(() =>
                {
                    Assert.That(result, Is.TypeOf<AttachmentModel>());
                    Assert.That(result.Id, Is.Not.Null);
                    Assert.That(result.FileContent, Is.Not.Null);
                    Assert.That(result.FileName, Is.EqualTo(expectedFilePath));
                    Assert.That(result.FileTypeIcon, Is.Not.Null);
                });
            }
        }

        [TestCase(null)]
        [TestCase("  ")]
        public void SetIconImageFromMime_Returns_WhenStringIs_NullOrWhiteSpace(string contentType)
        {
            var expectedResult = "icon_none.png";

            using (var attachmentHelper = new AttachmentHelper())
            {
                var result = attachmentHelper.SetIconImageFromMime(contentType);

                Assert.That(result, Is.EqualTo(expectedResult));
            }
        }
    }
}