using Newtonsoft.Json;
using ProductivityApp.Models;
using System;
using System.Collections.Generic;
using System.IO;
using Xamarin.Forms;
using Xamarin.UITest;
using Xamarin.UITest.Queries;
using Query = System.Func<Xamarin.UITest.Queries.AppQuery, Xamarin.UITest.Queries.AppQuery>;

namespace ProductivityAppTests.UiTests.Pages
{
    public class MilestonesPageHelper : BasePageHelper
    {
        private readonly string addButton;
        private readonly Query title;
        private readonly Query milestoneElements;
        private readonly int milestoneEnumCount;
        public enum ClassIndex 
            {
                title = 0,
                description = 1,
                deadline = 2
            };

        public MilestonesPageHelper()
        {
            title = c => c.Marked("Milestones");
            addButton = "Add";
            milestoneElements = c => c.ClassFull("LabelAppCompatRenderer");
            milestoneEnumCount = ClassIndex.GetNames(typeof(ClassIndex)).Length;
        }

        protected override PlatformQuery Trait => new PlatformQuery
        {
            Android = title,
            iOS = title
        };

        public void SelectAddButton(TimeSpan? timeout = default)
        { 
            SelectElement(addButton, timeout);
        }

        public AppResult[] GetMilestone(int milestoneNum, TimeSpan? timeout = default)
        {
            var allMilestones = GetElements(milestoneElements, timeout);
            var indexMod = (milestoneNum * milestoneEnumCount);

            var milestone = new AppResult[milestoneEnumCount];
            milestone[(int)ClassIndex.title] = allMilestones[(int)ClassIndex.title + indexMod];
            milestone[(int)ClassIndex.description] = allMilestones[(int)ClassIndex.description + indexMod];
            milestone[(int)ClassIndex.deadline] = allMilestones[(int)ClassIndex.deadline + indexMod];

            return milestone;
        }

        public void CreateMilestone(Milestone milestone, Platform platform)
        {
            var platformPath = "";

            if (platform == Platform.Android)
            {
                platformPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            }
            if (platform == Platform.iOS)
            {
                platformPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "..", "Library", "ApplicationSupport", "ProductivityApp");
            }
            var localDataPath = Path.Combine(platformPath, "data", "milestones.json");

            var json = JsonConvert.SerializeObject(new List<Milestone> { milestone });
            File.WriteAllText(localDataPath, json);
        }

        public void CreateMilestone(List<Milestone> milestones, int platform)
        {
            string platformPath;
            if (platform == (int)Platform.Android)
            {
                platformPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            }
            else
            {
                platformPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "..", "Library", "ApplicationSupport", "ProductivityApp");
            }
            string localDataPath = Path.Combine(platformPath, "data", "milestones.json");

            var json = JsonConvert.SerializeObject(milestones);
            File.WriteAllText(localDataPath, json);
        }
    }
}
