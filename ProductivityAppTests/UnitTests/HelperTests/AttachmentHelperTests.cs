using NUnit.Framework;
using ProductivityApp.Helpers;
using ProductivityApp.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductivityAppTests.UnitTests.HelperTests
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
        [TestCase("image")]
        public void GetFileTypeIconFromMime_ReturnsIconNone_WhenStringIs_NullOrWhiteSpace(string contentType)
        {
            var expectedResult = "icon_none.png";

            using (var attachmentHelper = new AttachmentHelper())
            {
                var result = attachmentHelper.GetFileTypeIconFromMime(contentType);

                Assert.That(result, Is.EqualTo(expectedResult));
            }
        }

        [TestCase("image/png", "icon_image.png")]
        [TestCase("audio/mp3", "icon_audio.png")]
        [TestCase("video/mp4", "icon_video.png")]
        [TestCase("text/plain", "icon_txt.png")]
        [TestCase("text/html", "icon_html.png")]
        [TestCase("text/css", "icon_css.png")]
        [TestCase("application/javascript", "icon_java.png")]
        [TestCase("application/json", "icon_json.png")]
        [TestCase("application/xml", "icon_xml.png")]
        [TestCase("application/pdf", "icon_pdf.png")]
        [TestCase("application/msword", "icon_doc.png")]
        [TestCase("application/vnd.openxmlformats-officedocument.wordprocessingml.document", "icon_doc.png")]
        [TestCase("application/vnd.ms-excel", "icon_xls.png")]
        [TestCase("application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "icon_xls.png")]
        [TestCase("application/vnd.ms-powerpoint", "icon_ppt.png")]
        [TestCase("application/vnd.openxmlformats-officedocument.presentationml.presentation", "icon_ppt.png")]
        [TestCase("unknown/unknown", "icon_none.png")]
        public void GetFileTypeIconFromMime_ReturnsCorrectIcon_WhenStringIs_ValidMimeType(string contentType, string expectedResult)
        {
            using (var attachmentHelper = new AttachmentHelper())
            {
                var result = attachmentHelper.GetFileTypeIconFromMime(contentType);

                Assert.That(result, Is.EqualTo(expectedResult));
            }
        }
    }
}