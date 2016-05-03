﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace WPF_Windows_Spotlight
{
    public class Item : INotifyPropertyChanged
    {
        private string _title;

        public string Title
        {
            get { return _title; }
            set { 
                _title = value;
                NotifyPropertyChanged("Title");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(string property)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(property));
            }
        }
    }
}
