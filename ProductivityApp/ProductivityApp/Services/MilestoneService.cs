using Newtonsoft.Json;
using ProductivityApp.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ProductivityApp.Services
{
    public class MilestoneService : IDataService<MilestoneModel>
    {
        private readonly List<MilestoneModel> milestones;
        private readonly string localDataPath;

        public MilestoneService()
        {
            milestones = new List<MilestoneModel>();
        }

        public MilestoneService(string localDataPath)
        {
            if(string.IsNullOrWhiteSpace(localDataPath))
            {
                throw new ArgumentNullException(nameof(localDataPath), "String argument cannot be null or white space");
            }

            this.localDataPath = localDataPath;
            milestones = LoadItemsLocal();
        }
        
        public async Task<bool> AddItemAsync(MilestoneModel item)
        {
            milestones.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> AddItemsAsync(List<MilestoneModel> items)
        {
            items.ForEach(item => milestones.Add(item));

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(MilestoneModel item)
        {
            var oldItem = milestones.Where((MilestoneModel arg) => arg.Id == item.Id).FirstOrDefault();
            milestones.Remove(oldItem);
            milestones.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemByIdAsync(string id)
        {
            var oldItem = milestones.Where((MilestoneModel m) => m.Id == id).FirstOrDefault();
            milestones.Remove(oldItem);

            return await Task.FromResult(true);
        }

        public async Task<MilestoneModel> GetItemByIdAsync(string id)
        {
            return await Task.FromResult(milestones.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<MilestoneModel>> GetItemsByIdAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(milestones);
        }

        public void SaveItemsLocal()
        {
            if(!Directory.Exists(localDataPath))
            {
                throw new DirectoryNotFoundException("The specified directory does not exist: " + localDataPath);
            }
            var json = JsonConvert.SerializeObject(milestones);
            var fullPath = Path.Combine(localDataPath, "milestones.json");

            File.WriteAllText(fullPath, json);
        }

        public List<MilestoneModel> LoadItemsLocal()
        {
            var localMilestones = new List<MilestoneModel>();

            if (!Directory.Exists(localDataPath))
            {
                throw new DirectoryNotFoundException("The specified directory does not exist: " + localDataPath);
            }

            try
            {
                var fullPath = Path.Combine(localDataPath, "milestones.json");
                var json = File.ReadAllText(fullPath);
                localMilestones = JsonConvert.DeserializeObject<List<MilestoneModel>>(json);
            } catch(Exception ex)
            {
                Console.WriteLine(ex.ToString()); //Replace with App Logger
            }

            return localMilestones;
        }
    }
}
