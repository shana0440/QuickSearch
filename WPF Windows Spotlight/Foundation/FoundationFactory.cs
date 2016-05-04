﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_Windows_Spotlight.Foundation
{
    public class FoundationFactory
    {
        private List<string> _foundations;

        public FoundationFactory()
        {
            _foundations = new List<string>();
            _foundations.Add("calculator");
        }

        public IFoundation CreateFoundation(string foundationName, string arg)
        {
            IFoundation foundation;
            switch (foundationName)
	        {
                case "calculator":
                    foundation = new Calculator(arg);
                    break;
		        default:
                    foundation = null;
                    break;
	        }
            return foundation;
        }

        public List<string> GetFoundations()
        {
            return _foundations;
        }
    }
}
