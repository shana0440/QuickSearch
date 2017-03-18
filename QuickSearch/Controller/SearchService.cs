﻿using System.Collections.Generic;
using System.ComponentModel;
using QuickSearch.Models.Calculator;
using QuickSearch.Models;
using QuickSearch.Models.ResultItemsFactory;
using System.Collections.ObjectModel;
using QuickSearch.Models.FileSystem;
using System;
using QuickSearch.Models.Dictionary;
using QuickSearch.Models.CurrencyConverter;
using QuickSearch.Models.SearchEngine;
using System.Threading;
using System.Windows;

namespace QuickSearch.Controller
{
    public class SearchService
    {
        public delegate void SearchOverEventHandler();
        SearchOverEventHandler _searchOverEvent;
        AsyncObservableCollection<IResultItem> _resultList = new AsyncObservableCollection<IResultItem>();
        public int SelectedIndex { get; internal set; }

        Thread _searchThread;
        SearchThread _searchThreadObject;

        public ObservableCollection<IResultItem> ResultList
        {
            get
            {
                return _resultList;
            }
        }

        public SearchService()
        {
            _searchThreadObject = _searchThreadObject ?? new SearchThread();
            _searchThreadObject.SubscribeSearchOverEvent(() =>
            {
                _searchThread.Join();
                Console.WriteLine("SearchThread Search Over");
                Console.WriteLine("Search Keyword: {0}", _searchThreadObject.Keyword);
                Console.WriteLine("Get Reslut Items amount: {0}", _searchThreadObject.ResultList.Count);
                Console.WriteLine("====================================");
                foreach (var item in _searchThreadObject.ResultList)
                {
                    _resultList.Add(item);
                }
                _searchOverEvent();
            });

            _searchThread = _searchThread ?? new Thread(_searchThreadObject.Search);
        }

        public void Search(string keyword)
        {
            if (!_searchThread.IsAlive)
            {
                if (_searchThread.ThreadState == ThreadState.Stopped || _searchThread.ThreadState == ThreadState.Aborted)
                {
                    _searchThread = new Thread(_searchThreadObject.Search);
                }
                _searchThread.Start();
                _resultList.Clear();
                SelectedIndex = 0;
            }

            _searchThreadObject.Wait500ms();
            _searchThreadObject.Keyword = keyword;
        }


        public void SubscribeSearchOverEvent(SearchOverEventHandler handler)
        {
            _searchOverEvent = handler;
        }

        internal void OpenSelectedItemResource()
        {
            _resultList[SelectedIndex].OpenResource();
        }

        public void OpenItemResource(int selectedIndex)
        {
            _resultList[selectedIndex].OpenResource();
        }

        public void SelectItem(int selectedIndex)
        {
            if (selectedIndex >= 0 && selectedIndex < _resultList.Count)
            {
                foreach (var item in _resultList)
                {
                    item.IsSelected = false;
                }
                SelectedIndex = selectedIndex;
                _resultList[SelectedIndex].IsSelected = true;
            }
        }

        public void AbortSearchThread()
        {
            _searchThread.Abort();
            GC.Collect();
        }
    }
}
