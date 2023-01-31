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
            var milestonesDataStore = new MilestoneDataStore();

            var result = await milestonesDataStore.AddItemAsync(expectedMilestone);

            Assert.Multiple(() =>
            {
                Assert.That(result, Is.True);
                Assert.That(milestonesDataStore.milestones, Does.Contain(expectedMilestone));
            });
        }

        [Test]
        public async Task AddItemsAsync_AddsMultipleMilestoneToDataStore()
        {
            var expectedMilestones = new List<Milestone> { 
                new Milestone {Id = Guid.NewGuid().ToString(), Label = "Test Milestone 1" },
                new Milestone {Id = Guid.NewGuid().ToString(), Label = "Test Milestone 2" },
                new Milestone {Id = Guid.NewGuid().ToString(), Label = "Test Milestone 3" }

            };
            var milestonesDataStore = new MilestoneDataStore();

            var result = await milestonesDataStore.AddItemsAsync(expectedMilestones);

            Assert.Multiple(() =>
            {
                Assert.That(result, Is.True);
                Assert.That(milestonesDataStore.milestones, Does.Contain(expectedMilestones[0]));
                Assert.That(milestonesDataStore.milestones, Does.Contain(expectedMilestones[1]));
                Assert.That(milestonesDataStore.milestones, Does.Contain(expectedMilestones[2]));
            });
            
        }

        [Test]
        public async Task GetItemAsync_ReturnsCorrectMilestone()
        {
            var id = Guid.NewGuid().ToString();
            var milestone = new Milestone { Id = id, Description = "Initial milestone" };
            var milestonesDataStore = new MilestoneDataStore();
            await milestonesDataStore.AddItemAsync(milestone);

            var result = await milestonesDataStore.GetItemAsync(id);

            Assert.That(result, Is.EqualTo(milestone));
        }

        [Test]
        public async Task GetItemsAsync_ReturnsMilestoneList()
        {
            var milestoneList = new List<Milestone>
                {
                    new Milestone { Id = "1", Label = "Milestone 1" },
                    new Milestone { Id = "2", Label = "Milestone 2" },
                    new Milestone { Id = "3", Label = "Milestone 3" }
                };
            var milestonesDataStore = new MilestoneDataStore();
            await milestonesDataStore.AddItemsAsync(milestoneList);

            var data = await milestonesDataStore.GetItemsAsync();
            var result = data.ToList();

            Assert.That(result, Is.EqualTo(milestoneList));
        }

        [Test]
        public async Task UpdateItemAsync_UpdatesCorrectMilestone()
        {
            var id = Guid.NewGuid().ToString();
            var resultLable = "Updated milestone";
            var milestone = new Milestone { Id = id, Label = "Test Milestone" };
            var updatedMilestone = new Milestone { Id = id, Label = resultLable };
            var milestonesDataStore = new MilestoneDataStore();
            await milestonesDataStore.AddItemAsync(milestone);

            var result = await milestonesDataStore.UpdateItemAsync(updatedMilestone);

            Assert.Multiple(() =>
            {
                Assert.That(result, Is.True);
                Assert.That(milestonesDataStore.GetItemAsync(id).Result.Label, Is.EqualTo(resultLable));
            });
        }

        [Test]
        public async Task DeleteItemAsync_RemovesMilestoneFromDataStore()
        {
            var milestonesDataStore = new MilestoneDataStore();
            var id = "2";
            var milestoneList = new List<Milestone>
                {
                    new Milestone { Id = "1", Label = "Milestone 1" },
                    new Milestone { Id = id, Label = "Milestone 2" },
                    new Milestone { Id = "3", Label = "Milestone 3" }
                };
            await milestonesDataStore.AddItemsAsync(milestoneList);

            var deleteResult = await milestonesDataStore.DeleteItemAsync(id);
            var getItemResult = await milestonesDataStore.GetItemAsync(id);

            Assert.Multiple(() =>
            {
                Assert.That(deleteResult, Is.True);
                Assert.That(getItemResult, Is.Null);
                Assert.That(milestonesDataStore.milestones, Is.Not.EqualTo(milestoneList));
            });
        }

        [Test]
        public async Task SaveItemsLocalCreatesJson()
        {
            var pathUWP = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "ProductivityApp");
            var expectedPath = Path.Combine(pathUWP, "data", "milestones.json");
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
        }

        [TestCase("")]
        [TestCase("C:\\Users\\Marcellino\\AppData\\Local\\ProductivityApp\\data\\incorrect.json")]
        public void LoadItemsLocalPassesEmptyListIfNoFileFound(string path)
        {
            var dataStore = new MilestoneDataStore(path);

            var milestones = dataStore.LoadItemsLocal();
            Assert.That(milestones, Is.Empty);
        }


        [Test]
        public void LoadItemsLocalLoadsJsonCorrectly()
        {
            var pathUWP = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "ProductivityApp");
            var fullPath = Path.Combine(pathUWP, "data", "milestones.json");
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
            File.WriteAllText(fullPath, json);
            var dataStore = new MilestoneDataStore(pathUWP);

            var resultList = dataStore.LoadItemsLocal();
            var result = resultList.FirstOrDefault();
            Assert.That(result.Id, Is.EqualTo(expectedMilestone.Id));    
            File.Delete(fullPath);
        }
    }
}
