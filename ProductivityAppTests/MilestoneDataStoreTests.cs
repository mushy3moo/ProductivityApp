using Xunit;
using System;
using System.Collections.Generic;
using ProductivityApp.Models;
using ProductivityApp.Services;
using System.Linq;

namespace ProductivityAppTests
{
    public class MilestoneDataStoreTests
    {
        [Fact]
        public async void AddItemAsync_AddsMilestoneToDataStore()
        {
            // Arrange
            var expectedMilestone = new Milestone { Id = Guid.NewGuid().ToString(), Label = "Test Milestone" };
            var milestonesDataStore = new MilestoneDataStore();

            // Act
            var result = await milestonesDataStore.AddItemAsync(expectedMilestone);

            // Assert
            Assert.True(result);
            Assert.Contains(expectedMilestone, milestonesDataStore.milestones);
        }

        [Fact]
        public async void AddItemsAsync_AddsMultipleMilestoneToDataStore()
        {
            // Arrange
            var expectedMilestones = new List<Milestone> { 
                new Milestone {Id = Guid.NewGuid().ToString(), Label = "Test Milestone 1" },
                new Milestone {Id = Guid.NewGuid().ToString(), Label = "Test Milestone 2" },
                new Milestone {Id = Guid.NewGuid().ToString(), Label = "Test Milestone 3" }

            };
            var milestonesDataStore = new MilestoneDataStore();

            // Act
            var result = await milestonesDataStore.AddItemsAsync(expectedMilestones);

            // Assert
            Assert.True(result);
            Assert.Contains(expectedMilestones[0], milestonesDataStore.milestones);
            Assert.Contains(expectedMilestones[1], milestonesDataStore.milestones);
            Assert.Contains(expectedMilestones[2], milestonesDataStore.milestones);
        }

        [Fact]
        public async void GetItemAsync_ReturnsCorrectMilestone()
        {
            // Arrange
            var id = Guid.NewGuid().ToString();
            var milestone = new Milestone { Id = id, Description = "Initial milestone" };
            var milestonesDataStore = new MilestoneDataStore();
            await milestonesDataStore.AddItemAsync(milestone);

            // Act
            var result = await milestonesDataStore.GetItemAsync(id);

            // Assert
            Assert.Equal(milestone, result);
        }

        [Fact]
        public async void GetItemsAsync_ReturnsMilestoneList()
        {
            // Arrange
            var milestoneList = new List<Milestone>
                {
                    new Milestone { Id = "1", Label = "Milestone 1" },
                    new Milestone { Id = "2", Label = "Milestone 2" },
                    new Milestone { Id = "3", Label = "Milestone 3" }
                };
            var milestonesDataStore = new MilestoneDataStore();
            await milestonesDataStore.AddItemsAsync(milestoneList);

            // Act
            var result = await milestonesDataStore.GetItemsAsync();
            result = result.ToList();

            // Assert
            Assert.Equal(milestoneList, result);
        }

        [Fact]
        public async void UpdateItemAsync_UpdatesCorrectMilestone()
        {
            // Arrange
            var id = Guid.NewGuid().ToString();
            var resultLable = "Updated milestone";
            var milestone = new Milestone { Id = id, Label = "Test Milestone" };
            var updatedMilestone = new Milestone { Id = id, Label = resultLable };
            var milestonesDataStore = new MilestoneDataStore();
            await milestonesDataStore.AddItemAsync(milestone);

            // Act
            var result = await milestonesDataStore.UpdateItemAsync(updatedMilestone);

            // Assert
            Assert.True(result);
            Assert.Equal(resultLable, milestonesDataStore.GetItemAsync(id).Result.Label);
        }

        [Fact]
        public async void DeleteItemAsync_RemovesMilestoneFromDataStore()
        {
            // Arrange
            var id = "2";
            var milestoneList = new List<Milestone>
                {
                    new Milestone { Id = "1", Label = "Milestone 1" },
                    new Milestone { Id = id, Label = "Milestone 2" },
                    new Milestone { Id = "3", Label = "Milestone 3" }
                };
            var milestonesDataStore = new MilestoneDataStore();
            await milestonesDataStore.AddItemsAsync(milestoneList);

            // Act
            var deleteResult = await milestonesDataStore.DeleteItemAsync(id);
            var getItemResult = await milestonesDataStore.GetItemAsync(id);

            // Assert
            Assert.True(deleteResult);
            Assert.Null(getItemResult);
            Assert.NotEqual(milestoneList, milestonesDataStore.milestones);
        }
    }
}
