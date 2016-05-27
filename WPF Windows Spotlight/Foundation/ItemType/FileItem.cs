﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF_Windows_Spotlight.Foundation;

namespace WPF_Windows_Spotlight.Foundation.ItemType
{
    public class FileItem : Item
    {
        private readonly FolderOrFile _folderOrFile;

        public FileItem(FolderOrFile folderOrFile)
            : base(folderOrFile.Name)
        {
            _folderOrFile = folderOrFile;
        }

        public string FullName
        {
            get { return _folderOrFile.FullName; }
        }

        public List<KeyValuePair<string, string>> GetProperty()
        {
            var propertys = new List<KeyValuePair<string, string>>();
            propertys.Add(new KeyValuePair<string, string>("Created", _folderOrFile.CreationDate));
            propertys.Add(new KeyValuePair<string, string>("Modified", _folderOrFile.LastWriteDate));
            propertys.Add(new KeyValuePair<string, string>("Accessed", _folderOrFile.LastAccessDate));
            return propertys;
        }

        public override void Open()
        {
            try
            {
                if (_folderOrFile != null)
                {
                    System.Diagnostics.Process.Start(_folderOrFile.FullName);
                    var filePriority = new FilePriority();
                    filePriority.PriorityUp(_folderOrFile);
                }
            }
            catch (Win32Exception e)
            {
                throw new Exception("Can't open this file or folder");
            }
        }
    }
}
