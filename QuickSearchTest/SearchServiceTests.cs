﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuickSearch.Controller;
using System.Threading;
using System.Collections.Generic;
using QuickSearch.Models.ResultItemsFactory;
using System.Collections.ObjectModel;

namespace QuickSearchTest
{
    [TestClass]
    public class SearchServiceTests
    {
        SearchService _service;
        bool _isSearchOver = false;

        [TestInitialize]
        public void Initialize()
        {
            _service = new SearchService();
            _isSearchOver = false;
        }

        [TestMethod]
        public void TestSearch()
        {
            SearchService.SearchOverEventHandler handler = new SearchService.SearchOverEventHandler(SearchOverEvent);
            _service.SubscribeSearchOverEvent(handler);
            _service.Search("1+1");

            Thread.Sleep(10000);
            Assert.IsTrue(_isSearchOver);
        }

        void SearchOverEvent()
        {
            _isSearchOver = true;
        }

        [TestMethod]
        public void TestCancelSearch()
        {
            SearchService.SearchOverEventHandler handler = new SearchService.SearchOverEventHandler(SearchOverEvent);
            _service.SubscribeSearchOverEvent(handler);
            _service.Search("1+1");
            _service.StopSearching();

            Thread.Sleep(1000);
            Assert.IsFalse(_isSearchOver);
        }
    }
}
