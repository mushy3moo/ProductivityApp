using System;
using System.Threading.Tasks;
using Autofac.Extras.Moq;
using NUnit.Framework;
using ProductivityApp.Models;
using ProductivityApp.Services;
using ProductivityApp.ViewModels;

namespace ProductivityAppTests.UnitTests
{
    public class EditMilestonePageTests
    {
        [Test]
        public async Task LoadMilestoneIdSetsVariablesCorrectly()
        {
            using(var mock = AutoMock.GetLoose())
            {
                var dataStore = mock.Create<MilestoneDataStore>();
                var expectedMilestone = new Milestone()
                {
                    Id = Guid.NewGuid().ToString(),
                    Label = "Test Label",
                    Description = "Test Description",
                    Deadline = DateTime.Now
                };
                await dataStore.AddItemAsync(expectedMilestone);

                var viewModel = new EditMilestoneViewModel(dataStore)
                {
                    MilestoneId = expectedMilestone.Id
                };

                Assert.Multiple(() =>
                {
                    Assert.That(viewModel.Id, Is.EqualTo(expectedMilestone.Id));
                    Assert.That(viewModel.Label, Is.EqualTo(expectedMilestone.Label));
                    Assert.That(viewModel.Description, Is.EqualTo(expectedMilestone.Description));
                    Assert.That(viewModel.Deadline, Is.EqualTo(expectedMilestone.Deadline));
                });
            }
        }
    }
}
