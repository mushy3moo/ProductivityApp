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

namespace ProductivityAppTests.UnitTests
{
    [TestFixture]
    public class DataServiceTests
    {
        [Test]
        public async Task AddItemAsync_ReturnsTrue_WhenDataStoreIsSuccessful()
        {
            var milestone = new MilestoneModel() { };
            var mockDataStore = new Mock<IDataStore<MilestoneModel>>();
            mockDataStore.Setup(d => d.SaveItemAsync(It.IsAny<MilestoneModel>())).Returns(Task.FromResult(true));
            var dataService = new DataService<MilestoneModel>(mockDataStore.Object);

            var result = await dataService.AddItemAsync(milestone);

            Assert.That(result, Is.True);
        }
        
        [Test]
        public async Task AddItemAsync_ReturnsFalse_WhenDataStoreFails()
        {
            var milestone = new MilestoneModel() { };
            var mockDataStore = new Mock<IDataStore<MilestoneModel>>();
            mockDataStore.Setup(d => d.SaveItemAsync(It.IsAny<MilestoneModel>())).Returns(Task.FromResult(false));
            var dataService = new DataService<MilestoneModel>(mockDataStore.Object);

            var result = await dataService.AddItemAsync(milestone);

            Assert.That(result, Is.False);
        }

        [Test]
        public async Task AddItemsAsync_ReturnsTrue_WhenDataStoreIsSuccessful()
        {
            var milestoneList = new List<MilestoneModel>();
            var mockDataStore = new Mock<IDataStore<MilestoneModel>>();
            mockDataStore.Setup(d => d.SaveAllItemsAsync(It.IsAny<List<MilestoneModel>>())).Returns(Task.FromResult(true));
            var dataService = new DataService<MilestoneModel>(mockDataStore.Object);

            var result = await dataService.AddItemsAsync(milestoneList);

            Assert.That(result, Is.True);
        }

        [Test]
        public async Task AddItemsAsync_ReturnsFalse_WhenDataStoreFails()
        {
            var milestoneList = new List<MilestoneModel>();
            var mockDataStore = new Mock<IDataStore<MilestoneModel>>();
            mockDataStore.Setup(d => d.SaveAllItemsAsync(It.IsAny<List<MilestoneModel>>())).Returns(Task.FromResult(false));
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
        public async Task GetItemAsync_ReturnsNull_WhenItmeIdIsInvalid()
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
        public async Task GetItemByIdAsync_ReturnsItem_WhenIdIsValid()
        {
            var id = Guid.NewGuid().ToString();
            var expectedItem = new MilestoneModel { Id = id };
            var milestoneList = new List<MilestoneModel>()
            {
                expectedItem
            };
            var mockDataStore = new Mock<IDataStore<MilestoneModel>>();
            mockDataStore.Setup(d => d.LoadAllItemsAsync()).Returns(Task.FromResult<IEnumerable<MilestoneModel>>(milestoneList));
            var dataService = new DataService<MilestoneModel>(mockDataStore.Object);

            var resultItem = await dataService.GetItemByIdAsync(id);

            Assert.That(resultItem, Is.EqualTo(expectedItem));
        }

        [Test]
        public async Task GetItemByIdAsync_ReturnsNull_WhenIdIsInvalid()
        {
            var invalidId = "";
            var milestoneList = new List<MilestoneModel>()
            {
                new MilestoneModel { Id = Guid.NewGuid().ToString() }
            };
            var mockDataStore = new Mock<IDataStore<MilestoneModel>>();
            mockDataStore.Setup(d => d.LoadAllItemsAsync()).Returns(Task.FromResult<IEnumerable<MilestoneModel>>(milestoneList));
            var dataService = new DataService<MilestoneModel>(mockDataStore.Object);

            var resultItem = await dataService.GetItemByIdAsync(invalidId);

            Assert.That(resultItem, Is.Null);
        }

        [Test]
        public async Task GetAllItemsAsync_ReturnsItems_WhenDataStoreIsSuccessful()
        {
            var expectedMilestones = new List<MilestoneModel>()
            {
                new MilestoneModel { Id = Guid.NewGuid().ToString() }
            };
            var mockDataStore = new Mock<IDataStore<MilestoneModel>>();
            mockDataStore.Setup(d => d.LoadAllItemsAsync()).Returns(Task.FromResult<IEnumerable<MilestoneModel>>(expectedMilestones));
            var dataService = new DataService<MilestoneModel>(mockDataStore.Object);

            var resultItems = await dataService.GetAllItemsAsync();

            Assert.That(resultItems, Is.EqualTo(expectedMilestones));
        }

        [Test]
        public async Task UpdateItemAsync_ReturnsTrue_When()
        {

        }
    }
}
