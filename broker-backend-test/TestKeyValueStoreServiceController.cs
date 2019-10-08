using backend.Controllers;
using backend.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace broker_backend_test
{
    [TestClass]
    public class TestKeyValueStoreServiceController
    {
        /// <summary>
        /// test post method
        /// </summary>
        /// <param name="Service">new service</param>
        [TestMethod]
        public void PostKeyValueStoreService_ShouldReturnNewKeyValueStoreService()
        {
            KeyValueStoreService Service = new KeyValueStoreService()
            {
                HasDBMS = true,
                HasReplication = true
            };
            var controller = new KeyValueStoreServiceController();
            var result = controller.PostKeyValueStoreServices(Service) as KeyValueStoreService;
            Assert.AreEqual(Service.Id, 1);
        }
        /// <summary>
        /// test delete method
        /// </summary>
        /// <param name="id"></param>
        [TestMethod]
        public void DeleteKeyValueStoreService_ShouldReturnTrue(int id)
        {
            var controller = new KeyValueStoreServiceController();
            var result = controller.DeleteKeyValueStoreServices(id);
            Assert.AreEqual(true, result);
        }
    }
}
