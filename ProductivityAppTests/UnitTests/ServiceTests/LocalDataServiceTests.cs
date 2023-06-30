using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit;
using NUnit.Framework;
using Xamarin.Forms;
using Xamarin.Forms.Mocks;
using ProductivityApp.Models;
using ProductivityApp.Services;
using NUnit.Framework.Internal;
using Moq;

namespace ProductivityAppTests.UnitTests
{
    [TestFixture]
    public class LocalDataServiceTests
    {
        [OneTimeSetUp]
        public void BeforeEachTest()
        {
            MockForms.Init();
        }

        [Test]
        public async Task AddItemAsync_CanAddItem_Successfully()
        {
            var expectedMilestone = new MilestoneModel()
            {
                Id = Guid.NewGuid().ToString(),
                Label = "Expected Milestone",
                Description = "Test Description"
            };
            /*
            var dataService = new DataService<MilestoneModel>();

            await dataService.AddItemAsync(expectedMilestone);
            var resultMilestone = await dataService.GetItemByIdAsync(expectedMilestone.Id);

            Assert.That(resultMilestone, Is.EqualTo(expectedMilestone));
            */
            Assert.Fail();
        }
    }
}
