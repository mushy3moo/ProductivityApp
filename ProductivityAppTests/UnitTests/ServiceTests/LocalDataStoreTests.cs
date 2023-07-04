using NUnit.Framework;
using ProductivityApp.Models;
using ProductivityApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms.Mocks;

namespace ProductivityAppTests.UnitTests.ServiceTests
{
    [TestFixture]
    public class LocalDataStoreTests
    {
        [OneTimeSetUp]
        public void SetUp() 
        {
            MockForms.Init();
        }

        [Test]
        public void LocalDataStoreTest()
        {
            var localDataStore = new LocalDataStore<AttachmentModel>();

        }
    }
}
