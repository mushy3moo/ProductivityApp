using System;
using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;
using ProductivityApp.Models;
using ProductivityApp.Services;
using ProductivityApp.ViewModels;
using Autofac.Extras.Moq;

namespace ProductivityAppTests.UnitTests
{
    public class AddMilestoneViewModelTests
    {

        [Test]
        public async Task SaveCommandSavesMilestoneToDataStore()
        {
            var expectedLabel = "Test Label";
            var expectedDescription = "Test Description";
            var expectedDeadline = DateTime.Now;

            using(var mock = AutoMock.GetLoose())
            {
                var dataStore = mock.Create<MilestoneDataStore>();
                var viewModel = new AddMilestoneViewModel(dataStore)
                {
                    Label = expectedLabel,
                    Description = expectedDescription,
                    Deadline = expectedDeadline
                };

                viewModel.SaveCommand.Execute(null);

                var result = await dataStore.GetItemsAsync();
                var milestone = result.ToList<Milestone>().FirstOrDefault();
                Assert.Multiple(() =>
                {
                    Assert.That(milestone.Label, Is.EqualTo(expectedLabel));
                    Assert.That(milestone.Description, Is.EqualTo(expectedDescription));
                    Assert.That(milestone.Deadline, Is.EqualTo(expectedDeadline));
                });
            }
        }
    }
}
