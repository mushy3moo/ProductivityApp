using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac.Extras.Moq;
using NUnit.Framework;
using ProductivityApp.Models;
using ProductivityApp.Services;
using ProductivityApp.ViewModels;

namespace ProductivityAppTests.UnitTests
{
    public class MilestonesPageTests
    {
        [Test]
        public async Task LoadMilestonesCommandCanLoadMilestone()
        {

            using (var mock = AutoMock.GetLoose())
            {
                var dataStore = mock.Create<MilestoneService>();
                var expectedMilestone = new Milestone()
                {
                    Id = Guid.NewGuid().ToString(),
                    Label = "Test Label",
                    Description = "Test Description",
                    Deadline = DateTime.Now
                };
                await dataStore.AddItemAsync(expectedMilestone);
                var viewModel = new MilestonesViewModel(dataStore);

                viewModel.LoadMilestonesCommand.Execute(null);

                var milestone = viewModel.Milestones.FirstOrDefault();
                Assert.That(milestone, Is.EqualTo(expectedMilestone));
            }
        }

        [Test]
        public async Task LoadMilestonesCommandCanLoadMulitpleMilestones()
        {

            using (var mock = AutoMock.GetLoose())
            {
                var dataStore = mock.Create<MilestoneService>();
                var expectedMilestones = new List<Milestone>
                {
                    new Milestone
                    {
                        Id = Guid.NewGuid().ToString(),
                        Label = "Test Label",
                        Description = "Test Description",
                        Deadline = DateTime.Now
                    },
                    new Milestone
                    {
                        Id = Guid.NewGuid().ToString(),
                        Label = "Test Label 2",
                        Description = "Test Description 2",
                        Deadline = DateTime.Now
                    }
                };
                await dataStore.AddItemsAsync(expectedMilestones);
                var viewModel = new MilestonesViewModel(dataStore);

                viewModel.LoadMilestonesCommand.Execute(null);

                var milestones = viewModel.Milestones;
                Assert.That(milestones, Does.Contain(expectedMilestones.FirstOrDefault()));
                Assert.That(milestones, Does.Contain(expectedMilestones.LastOrDefault()));
            }
        }
    }
}
