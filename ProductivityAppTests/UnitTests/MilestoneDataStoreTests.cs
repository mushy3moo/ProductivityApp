using NUnit.Framework;
using System;
using System.Collections.Generic;
using ProductivityApp.Models;
using ProductivityApp.Services;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;

namespace ProductivityAppTests.UnitTests
{
    public class MilestoneDataStoreTests
    {
        [Test]
        public async Task AddItemAsync_AddsMilestoneToDataStore()
        {
            var expectedMilestone = new Milestone { Id = Guid.NewGuid().ToString(), Label = "Test Milestone" };
            var dataStore = new MilestoneDataStore();

            var result = await dataStore.AddItemAsync(expectedMilestone);
            var resultData = await dataStore.GetItemsAsync();

            Assert.Multiple(() =>
            {
                Assert.That(result, Is.True);
                Assert.That(resultData, Does.Contain(expectedMilestone));
            });
        }

        [Test]
        public async Task AddItemsAsync_AddsMultipleMilestoneToDataStore()
        {
            var expectedMilestones = new List<Milestone> 
            {
                new Milestone {Id = Guid.NewGuid().ToString(), Label = "Test Milestone 1" },
                new Milestone {Id = Guid.NewGuid().ToString(), Label = "Test Milestone 2" },
                new Milestone {Id = Guid.NewGuid().ToString(), Label = "Test Milestone 3" }
            };
            var dataStore = new MilestoneDataStore();

            var result = await dataStore.AddItemsAsync(expectedMilestones);
            var resultData = await dataStore.GetItemsAsync();

            Assert.Multiple(() =>
            {
                Assert.That(result, Is.True);
                Assert.That(resultData, Does.Contain(expectedMilestones[0]));
                Assert.That(resultData, Does.Contain(expectedMilestones[1]));
                Assert.That(resultData, Does.Contain(expectedMilestones[2]));
            });
        }

        [Test]
        public async Task GetItemAsync_ReturnsCorrectMilestone()
        {
            var id = Guid.NewGuid().ToString();
            var milestone = new Milestone { Id = id, Description = "Initial milestone" };
            var dataStore = new MilestoneDataStore();

            await dataStore.AddItemAsync(milestone);
            var result = await dataStore.GetItemAsync(id);

            Assert.That(result, Is.EqualTo(milestone));
        }

        [Test]
        public async Task GetItemsAsync_ReturnsAllMilestones()
        {
            var milestoneList = new List<Milestone>
            {
                new Milestone { Id = "1", Label = "Milestone 1" },
                new Milestone { Id = "2", Label = "Milestone 2" },
                new Milestone { Id = "3", Label = "Milestone 3" }
            };
            var dataStore = new MilestoneDataStore();

            await dataStore.AddItemsAsync(milestoneList);
            var data = await dataStore.GetItemsAsync();
            var result = data.ToList();

            Assert.That(result, Is.EqualTo(milestoneList));
        }

        [Test]
        public async Task UpdateItemAsync_UpdatesCorrectMilestone()
        {
            var id = Guid.NewGuid().ToString();
            var expectedLable = "Updated milestone";
            var milestone = new Milestone 
            { 
                Id = id,
                Label = "Test Milestone"
            };
            var updatedMilestone = new Milestone 
            { 
                Id = id,
                Label = expectedLable
            };
            var dataStore = new MilestoneDataStore();

            await dataStore.AddItemAsync(milestone);
            var result = await dataStore.UpdateItemAsync(updatedMilestone);
            var data = await dataStore.GetItemAsync(id);
            var resultLable = data.Label;

            Assert.Multiple(() =>
            {
                Assert.That(result, Is.True);
                Assert.That(resultLable, Is.EqualTo(expectedLable));
            });
        }

        [Test]
        public async Task DeleteItemAsync_RemovesMilestoneFromDataStore()
        {
            var dataStore = new MilestoneDataStore();
            var id = "2";
            var milestoneList = new List<Milestone>
            {
                new Milestone { Id = "1", Label = "Milestone 1" },
                new Milestone { Id = id, Label = "Milestone 2" },
                new Milestone { Id = "3", Label = "Milestone 3" }
            };

            await dataStore.AddItemsAsync(milestoneList);
            var deleteResult = await dataStore.DeleteItemAsync(id);
            var result = await dataStore.GetItemAsync(id);

            Assert.Multiple(() =>
            {
                Assert.That(deleteResult, Is.True);
                Assert.That(result, Is.Null);
            });
        }

        [TestCase("")]
        [TestCase("  ")]
        [TestCase(null)]
        public void MilestoneDataStoreThrowsExceptionWhenPassedStringEmptyOrWhitespace(string str)
        {
            var expectedMessage = "String argument cannot be null or white space";

            var resultExecption = Assert.Throws<ArgumentNullException>(() => new MilestoneDataStore(str));
            var result = resultExecption.Message.Contains(expectedMessage);
            Assert.That(result, Is.True);
        }

        [Test]
        public void SaveItemsLocalThrowsExpectionWhenDirectoryNotFound()
        {
            var expectedMessage = $"The specified directory does not exist: ";
            var dataStore = new MilestoneDataStore();

            var resultExecption = Assert.Throws<DirectoryNotFoundException>(() => dataStore.SaveItemsLocal());
            var result = resultExecption.Message.Contains(expectedMessage);
            Assert.That(result, Is.True);
        }

        [Test]
        public async Task SaveItemsLocalCreatesJson()
        {
            var pathUWP = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "ProductivityApp", "data");
            var expectedPath = Path.Combine(pathUWP, "milestones.json");

            Directory.CreateDirectory(pathUWP);
            var dataStore = new MilestoneDataStore(pathUWP);
            var milestone = new Milestone
            {
                Id = Guid.NewGuid().ToString(),
                Label = "Test Label",
                Description = "Test Description",
                Deadline = DateTime.UtcNow
            };

            await dataStore.AddItemAsync(milestone);
            dataStore.SaveItemsLocal();
            var result = File.Exists(expectedPath);

            Assert.That(result, Is.True);

            File.Delete(expectedPath);
            Directory.Delete(pathUWP);
            Directory.Delete(pathUWP + "\\..");
        }

        [Test]
        public void LoadItemsLocalPassesEmptyListIfNoFileFound()
        {
            var path = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            var dataStore = new MilestoneDataStore(path);

            var milestones = dataStore.LoadItemsLocal();

            Assert.That(milestones, Is.Empty);
        }

        [Test]
        public void LoadItemsLocalThrowsExpectionWhenDirectoryNotFound()
        {
            var expectedMessage = $"The specified directory does not exist: ";
            var dataStore = new MilestoneDataStore();

            var resultExecption = Assert.Throws<DirectoryNotFoundException>(() => dataStore.LoadItemsLocal());
            var result = resultExecption.Message.Contains(expectedMessage);
            Assert.That(result, Is.True);
        }

        [Test]
        public void LoadItemsLocalLoadsJsonCorrectly()
        {
            var pathUWP = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "ProductivityApp", "data");
            var fullPath = Path.Combine(pathUWP, "milestones.json");
            var expectedMilestone = new Milestone
            {
                Id = Guid.NewGuid().ToString(),
                Label = "Test Label",
                Description = "Test Description",
                Deadline = DateTime.UtcNow
            };
            var milestonesList = new List<Milestone> {
                expectedMilestone
            };

            var json = JsonConvert.SerializeObject(milestonesList);
            Directory.CreateDirectory(pathUWP);
            File.WriteAllText(fullPath, json);

            var dataStore = new MilestoneDataStore(pathUWP);
            var resultList = dataStore.LoadItemsLocal();
            var result = resultList.FirstOrDefault();

            Assert.That(result.Id, Is.EqualTo(expectedMilestone.Id));

            File.Delete(fullPath);
            Directory.Delete(pathUWP);
            Directory.Delete(pathUWP + "\\..");
        }
    }
}
