using NUnit.Framework;
using ProductivityApp.Models;
using System;

namespace ProductivityAppTests.UnitTests
{
    public class MilestoneTests
    {
        [Test]
        public void NewMIlestoneSetsIsCompleteToFalse()
        {
            var milestone = new Milestone();
            var result = milestone.IsCompleted;
            
            Assert.That(result, Is.False);
        }       
        
        [Test]
        public void NewMIlestoneSetsCreatedOnToDateTimeNow()
        {
            var expected = DateTime.Now;

            var milestone = new Milestone();
            var actual = milestone.CreatedOn;

            Assert.That((actual - expected).TotalSeconds, Is.LessThan(1));
        }
    }
}
