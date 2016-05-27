﻿using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;
using System.Xml;
using System.Xml.Resolvers;

namespace WPF_Windows_Spotlight.Foundation
{
    public class FilePriority
    {
        private const string PriorityFile = "filePriority.xml";

        public FilePriority()
        {
            
        }

        public int PriorityUp(FolderOrFile folderOrFile)
        {
            var xml = new XmlDocument();
            var openCount = 1;
            if (File.Exists(PriorityFile))
            {
                xml.Load(PriorityFile);
                var node = xml.SelectSingleNode("History/File[@FullName='" + folderOrFile.FullName + "']");
                if (node != null)
                {
                    var count = Int32.Parse(node.Attributes["Count"].InnerText) + 1;
                    node.Attributes["Count"].InnerText = count.ToString();
                    openCount = count;
                }
                else
                {
                    var history = xml.SelectSingleNode("History");
                    history.AppendChild(CreateFileXml(xml, folderOrFile));
                    xml.AppendChild(history);
                }
            }
            else
            {
                var history = xml.CreateElement("History");
                history.AppendChild(CreateFileXml(xml, folderOrFile));
                xml.AppendChild(history);
            }
            xml.Save(PriorityFile);
            return openCount;
        }

        public bool InPriorityFile(string filename)
        {
            var xml = new XmlDocument();
            xml.Load(PriorityFile);
            var query = String.Format("History/File[contains(@Name,'{0}')]", filename);
            var node = xml.SelectSingleNode(query);
            return node != null;
        }

        private XmlNode CreateFileXml(XmlDocument xml, FolderOrFile folderOrFile)
        {
            var ele = xml.CreateElement("File");
            ele.SetAttribute("FullName", folderOrFile.FullName);
            ele.SetAttribute("Name", folderOrFile.Name);
            ele.SetAttribute("Count", "1");
            return ele;
        }
    }

    public class FileData
    {
        public string FullName { get; set; }
        public int Count { get; set; }
    }
}
