﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.ComponentModel;
using QuickSearch.Models.SearchEngine;
using System.Threading;
using System.Collections.Generic;
using QuickSearch.Models.ResultItemsFactory;
using Telerik.JustMock;
using System.Net.NetworkInformation;

namespace QuickSearchTest
{
    [TestClass]
    public class SearchEngineThreadTests
    {
        object _threadResult;

        [TestMethod]
        public void TestDoWorkSuccess()
        {
            SearchEngineThread thread = new SearchEngineThread();
            BackgroundWorker worker = new BackgroundWorker();
            worker.DoWork += thread.DoWork;
            worker.RunWorkerCompleted += WorkerCompleted;
            worker.RunWorkerAsync("google");
            
            Thread.Sleep(1000);
            List<IResultItem> result = (List<IResultItem>)_threadResult;
            Assert.AreEqual(5, result.Count);
        }

        [TestMethod]
        public void TestDoWorkFail()
        {
            SearchEngineThread thread = new SearchEngineThread();
            BackgroundWorker worker = new BackgroundWorker();
            worker.DoWork += thread.DoWork;
            worker.RunWorkerCompleted += WorkerCompleted;
            worker.RunWorkerAsync("googleqweoirjwflszjksdhf;laewoijfdz;sldkfj");

            Thread.Sleep(1000);
            List<IResultItem> result = (List<IResultItem>)_threadResult;
            Assert.IsNull(result);
        }

        [TestMethod]
        public void TestNetworkAvailable()
        {
            Mock.SetupStatic(typeof(NetworkInterface), Behavior.Strict, StaticConstructor.Mocked);
            Mock.Arrange(() => NetworkInterface.GetIsNetworkAvailable()).Returns(false);

            SearchEngineThread thread = new SearchEngineThread();
            BackgroundWorker worker = new BackgroundWorker();
            DoWorkEventArgs e = new DoWorkEventArgs("google");
            thread.DoWork("0.0", e);

            Thread.Sleep(1000);
            Assert.IsNull(e.Result);
        }

        private void WorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            _threadResult = e.Result;
        }
    }
}
