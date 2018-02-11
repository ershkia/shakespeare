using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using shakespeare.core;
using shakespeare.core.dtos;
using Moq;
using shakespeare.core.utilities;

namespace shakespeare.tests
{
    [TestClass]
    public class TodoManagerTests
    {
        private ITodoManager m_todoManager;
        private DateTime m_now = DateTime.UtcNow;

        [TestInitialize]
        public void Initialize()
        {
            Mock<INowProvider> nowProvider = new Mock<INowProvider>();
            nowProvider.Setup(x => x.Now).Returns(m_now);

            m_todoManager = new TodoManager(nowProvider.Object);
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
            Assert.AreEqual(items[0].CreatedAt, m_now);
        }
    }
}
