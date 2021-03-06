﻿using System.Collections.Generic;
using QuickSearch.Models.ResultItemsFactory;
using System.Collections.ObjectModel;
using System;
using System.Threading;

namespace QuickSearch.Controller
{
    public class SearchService
    {
        public delegate void SearchOverEventHandler();
        SearchOverEventHandler _searchOverEvent;
        AsyncObservableCollection<IResultItem> _resultList = new AsyncObservableCollection<IResultItem>();
        public int SelectedIndex { get; internal set; }
        
        SearchThread _searchThreadObject = new SearchThread();
        SearchTimeout _searchTimeout = new SearchTimeout(250);

        public ObservableCollection<IResultItem> ResultList
        {
            get { return _resultList; }
        }

        public SearchService()
        {
            _searchThreadObject.EachWorkerSearchOverEvnet += (List<IResultItem> list) =>
            {
                foreach (var item in list)
                {
                    _resultList.Add(item);
                }
                try
                {
                    DecideBestResult(_resultList);
                }
                catch (InvalidOperationException)
                {
                    // do not thing
                }
            };

            _searchThreadObject.SearchOverEvent += () =>
            {
                DecideBestResult(_resultList);
                _searchOverEvent();
            };

            _searchTimeout.SearchEvent = () =>
            {
                _resultList.Clear();
                SelectedIndex = 0;
                _searchThreadObject.Search();
            };
        }

        void DecideBestResult(AsyncObservableCollection<IResultItem> results)
        {
            if(results.Count <= 0) return;
            IResultItem bestSolution = null;
            foreach (var item in results)
            {
                if (bestSolution == null || bestSolution.Priority < item.Priority)
                {
                    bestSolution = item;
                }
            }
            if (results[0].GroupName == "最佳搜尋結果")
            {
                results[0].GroupName = results[0].OriginGroupName;
                Console.WriteLine("Origin Index: {0}", results[0].OriginIndex);
                results.Move(0, results[0].OriginIndex);
            }
            bestSolution.GroupName = "最佳搜尋結果";
            bestSolution.OriginIndex = results.IndexOf(bestSolution);
            results.Remove(bestSolution);
            results.Insert(0, bestSolution);
        }

        public void Search(string keyword)
        {
            _searchTimeout.Restart();
            _searchThreadObject.SetKeyword(keyword);
        }


        public void SubscribeSearchOverEvent(SearchOverEventHandler handler)
        {
            _searchOverEvent = handler;
        }

        internal void OpenSelectedItemResource()
        {
            if (SelectedIndex < _resultList.Count)
            {
                _resultList[SelectedIndex].OpenResource();
            }
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

        public void StopSearching()
        {
            _searchTimeout.Stop();
            GC.Collect();
        }
    }
}
