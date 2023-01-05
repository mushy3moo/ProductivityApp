using ProductivityApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductivityApp.Services
{
    public class MilestoneDataStore : IDataStore<Milestone>
    {
        readonly List<Milestone> milestones;

        public MilestoneDataStore()
        {
            milestones = new List<Milestone>()
            {
                new Milestone { Id = Guid.NewGuid().ToString(), Title = "First item", Description="This is an item description." },
                new Milestone { Id = Guid.NewGuid().ToString(), Title = "Second item", Description="This is an item description." }
            };
        }

        public async Task<bool> AddItemAsync(Milestone item)
        {
            milestones.Add(item);

            return await System.Threading.Tasks.Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(Milestone item)
        {
            var oldItem = milestones.Where((Milestone arg) => arg.Id == item.Id).FirstOrDefault();
            milestones.Remove(oldItem);
            milestones.Add(item);

            return await System.Threading.Tasks.Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            var oldItem = milestones.Where((Milestone arg) => arg.Id == id).FirstOrDefault();
            milestones.Remove(oldItem);

            return await System.Threading.Tasks.Task.FromResult(true);
        }

        public async Task<Milestone> GetItemAsync(string id)
        {
            return await System.Threading.Tasks.Task.FromResult(milestones.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<Milestone>> GetItemsAsync(bool forceRefresh = false)
        {
            return await System.Threading.Tasks.Task.FromResult(milestones);
        }
    }
}
