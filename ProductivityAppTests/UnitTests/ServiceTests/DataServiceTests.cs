using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit;
using NUnit.Framework;
using ProductivityApp.Models;
using ProductivityApp.Services;
using NUnit.Framework.Internal;
using Moq;
using ProductivityAppTests.UnitTests.Fakes;

namespace ProductivityAppTests.UnitTests.ServiceTests
{
    [TestFixture]
    public class DataServiceTests
    {
        [Test]
        public async Task AddItemAsync_ReturnsTrue_WhenDataStoreIsSuccessful()
        {
            var milestone = new MilestoneModel() { };
            var mockDataStore = new Mock<IDataStore<MilestoneModel>>();
            mockDataStore.Setup(d => d.SaveItemAsync(milestone)).Returns(Task.FromResult(true));

            var dataService = new DataService<MilestoneModel>(mockDataStore.Object);
            var result = await dataService.AddItemAsync(milestone);

            Assert.That(result, Is.True);
        }
        
        [Test]
        public async Task AddItemAsync_ReturnsFalse_WhenDataStoreFails()
        {
            var milestone = new MilestoneModel() { };
            var mockDataStore = new Mock<IDataStore<MilestoneModel>>();
            mockDataStore.Setup(d => d.SaveItemAsync(milestone)).Returns(Task.FromResult(false));

            var dataService = new DataService<MilestoneModel>(mockDataStore.Object);
            var result = await dataService.AddItemAsync(milestone);

            Assert.That(result, Is.False);
        }

        [Test]
        public async Task AddItemsAsync_ReturnsTrue_WhenDataStoreIsSuccessful()
        {
            var milestoneList = new List<MilestoneModel>();
            var mockDataStore = new Mock<IDataStore<MilestoneModel>>();
            mockDataStore.Setup(d => d.SaveAllItemsAsync(milestoneList)).Returns(Task.FromResult(true));

            var dataService = new DataService<MilestoneModel>(mockDataStore.Object);
            var result = await dataService.AddItemsAsync(milestoneList);

            Assert.That(result, Is.True);
        }

        [Test]
        public async Task AddItemsAsync_ReturnsFalse_WhenDataStoreFails()
        {
            var milestoneList = new List<MilestoneModel>();
            var mockDataStore = new Mock<IDataStore<MilestoneModel>>();
            mockDataStore.Setup(d => d.SaveAllItemsAsync(milestoneList)).Returns(Task.FromResult(false));

            var dataService = new DataService<MilestoneModel>(mockDataStore.Object);
            var result = await dataService.AddItemsAsync(milestoneList);

            Assert.That(result, Is.False);
        }

        [Test]
        public async Task GetItemAsync_ReturnsItem_WhenItmeIdIsValid()
        {
            var expectedItem = new MilestoneModel 
            { 
                Id = Guid.NewGuid().ToString()
            };
            var milestoneList = new List<MilestoneModel>()
            {
                expectedItem
            };
            var mockDataStore = new Mock<IDataStore<MilestoneModel>>();
            mockDataStore.Setup(d => d.LoadAllItemsAsync()).Returns(Task.FromResult<IEnumerable<MilestoneModel>>(milestoneList));

            var dataService = new DataService<MilestoneModel>(mockDataStore.Object);
            var result = await dataService.GetItemAsync(expectedItem);

            Assert.That(result, Is.EqualTo(expectedItem));
        }

        [Test]
        public async Task GetItemAsync_ReturnsNull_WhenIdIsEmpty()
        {
            var milestoneList = new List<MilestoneModel>()
            {
                new MilestoneModel() { Id = Guid.NewGuid().ToString() }
            };
            var mockDataStore = new Mock<IDataStore<MilestoneModel>>();
            mockDataStore.Setup(d => d.LoadAllItemsAsync()).Returns(Task.FromResult<IEnumerable<MilestoneModel>>(milestoneList));

            var dataService = new DataService<MilestoneModel>(mockDataStore.Object);
            var result = await dataService.GetItemAsync(new MilestoneModel());

            Assert.That(result, Is.Null);
        }

        [Test]
        public async Task GetAllItemsAsync_ReturnsItems_Successfully()
        {
            var expectedMilestones = new List<MilestoneModel>()
            {
                new MilestoneModel { Id = Guid.NewGuid().ToString() }
            };
            var mockDataStore = new Mock<IDataStore<MilestoneModel>>();
            mockDataStore.Setup(d => d.LoadAllItemsAsync()).Returns(Task.FromResult<IEnumerable<MilestoneModel>>(expectedMilestones));

            var dataService = new DataService<MilestoneModel>(mockDataStore.Object);
            var resultData = await dataService.GetAllItemsAsync();

            Assert.That(resultData, Is.EqualTo(expectedMilestones));
        }

        [Test]
        public async Task UpdateItemAsync_CanUpdateItem_WhenIdIsValid()
        {
            var id = Guid.NewGuid().ToString();
            var expectedData = new MilestoneModel()
            {
                Id = id,
                Label = "Updated"
            };
            var milestoneList = new List<MilestoneModel>()
            {
                new MilestoneModel() { Id = id }
            };
            var fakeDataStore = new FakeDataStore<MilestoneModel>(milestoneList);

            var dataService = new DataService<MilestoneModel>(fakeDataStore);
            var result = await dataService.UpdateItemAsync(expectedData);
            var resultData = await dataService.GetItemByIdAsync(id);

            Assert.Multiple(() =>
            {
                Assert.That(result, Is.True);
                Assert.That(resultData, Is.EqualTo(expectedData));
            });
        }

        [Test]
        public async Task UpdateItemAsync_ReturnsFalse_WhenIdIsInvalid()
        {
            var milestone = new MilestoneModel { Id = "Incorrect Id" };
            var milestoneList = new List<MilestoneModel>()
            {
                new MilestoneModel() { Id = Guid.NewGuid().ToString() }
            };
            var fakeDataStore = new FakeDataStore<MilestoneModel>(milestoneList);

            var dataService = new DataService<MilestoneModel>(fakeDataStore);
            var result = await dataService.UpdateItemAsync(milestone);
            var resultData = await fakeDataStore.LoadAllItemsAsync();

            Assert.Multiple(() =>
            {
                Assert.That(result, Is.False);
                Assert.That(resultData.Count, Is.EqualTo(milestoneList.Count));
            });
        }

        [Test]
        public async Task DeleteItemAsync_CanDeleteItem_WhenIdIsValid()
        {
            var milestone = new MilestoneModel() { Id = Guid.NewGuid().ToString() };
            var milestoneList = new List<MilestoneModel>()
            {
                milestone
            };
            var fakeDataStore = new FakeDataStore<MilestoneModel>(milestoneList);

            var dataService = new DataService<MilestoneModel>(fakeDataStore);
            var result = await dataService.DeleteItemAsync(milestone);
            var resultData = await fakeDataStore.LoadAllItemsAsync();

            Assert.Multiple(() =>
            {
                Assert.That(result, Is.True);
                Assert.That(resultData, Is.Empty);
            });
        }

        [Test]
        public async Task DeleteItemAsync_ReturnsFalse_WhenIdIsEmpty()
        {
            var milestoneList = new List<MilestoneModel>()
            {
                new MilestoneModel() { Id = Guid.NewGuid().ToString() }
            };
            var fakeDataStore = new FakeDataStore<MilestoneModel>(milestoneList);

            var dataService = new DataService<MilestoneModel>(fakeDataStore);
            var result = await dataService.DeleteItemAsync(new MilestoneModel());
            var resultData = await fakeDataStore.LoadAllItemsAsync();

            Assert.Multiple(() =>
            {
                Assert.That(result, Is.False);
                Assert.That(resultData, Is.Not.Empty);
            });
        }

        //[Test]

    }
}
