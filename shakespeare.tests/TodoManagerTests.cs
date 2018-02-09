using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using shakespeare.core;
using shakespeare.core.dtos;

namespace shakespeare.tests
{
    [TestClass]
    public class TodoManagerTests
    {
        private ITodoManager m_todoManager;

        [TestInitialize]
        public void Initialize()
        {
            m_todoManager = new TodoManager();
        }

        [TestMethod]
        public void CreateItem_SavesTodoItem()
        {
            m_todoManager.SaveItem("something");
            var result = m_todoManager.GetItems();
            Assert.IsNotNull(result);
            IList<TodoItem> items = result.ToList();
            
            Assert.AreEqual(1, items.Count);
            Assert.AreEqual("something", items[0].Description);
            Assert.AreEqual(false, items[0].Deleted);
            Assert.AreEqual(false, items[0].Checked);
            Assert.AreEqual(items[0].LastUpdatedAt, items[0].CreatedAt);
        }
    }
}
