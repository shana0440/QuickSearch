﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.CSharp;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Reflection;
using System.Text.RegularExpressions;

namespace WPF_Windows_Spotlight.Foundation
{
    public class Calculator : IFoundation
    {
        private string _expression;
        private string _lastResult;
        private string _transformWord;
        private string _pattern = @"(\d+\.*\d*)|(\+)|(\-)|(\*)|(\/)";
        private string _powPattern = @"\(.*\)\^\(.*\)";

        public Calculator(string expression = "")
        {
            _expression = expression;
        }

        public string Expression
        {
            set { _expression = value + ";"; }
        }

        public void transformWord (string input)
        {
            Regex regex = new Regex(_pattern);
            MatchCollection matches = regex.Matches(input);
            foreach (Match m in matches)
            {
                Console.WriteLine(m);
            }
        }

        public void replaceSqrt ()
        {
            string replace = _expression.Replace("sqrt", "Math.sqrt");
            Console.WriteLine(replace);
        }

        public string GetResult()
        {
            try
            {
                transformWord(_expression);
                string result = Eval(_expression).ToString();
                _lastResult = result;
                return _lastResult;
            }
            catch (Exception e)
            {
                return _lastResult;
            }
        }

        public void DoWork(object sender, DoWorkEventArgs e)
        {
            Item item = new Item();
            item.Title = GetResult();
            e.Result = item;
        }

        private static object Eval(string sCSCode)
        {
            CSharpCodeProvider c = new CSharpCodeProvider();
            ICodeCompiler icc = c.CreateCompiler();
            CompilerParameters cp = new CompilerParameters();

            cp.ReferencedAssemblies.Add("system.dll");
            cp.ReferencedAssemblies.Add("system.xml.dll");
            cp.ReferencedAssemblies.Add("system.data.dll");
            cp.ReferencedAssemblies.Add("system.windows.forms.dll");
            cp.ReferencedAssemblies.Add("system.drawing.dll");

            cp.CompilerOptions = "/t:library";
            cp.GenerateInMemory = true;

            StringBuilder sb = new StringBuilder("");
            sb.Append("using System;\n");
            sb.Append("using System.Xml;\n");
            sb.Append("using System.Data;\n");
            sb.Append("using System.Data.SqlClient;\n");
            sb.Append("using System.Windows.Forms;\n");
            sb.Append("using System.Drawing;\n");

            sb.Append("namespace CSCodeEvaler{ \n");
            sb.Append("public class CSCodeEvaler{ \n");
            sb.Append("public object EvalCode(){\n");
            sb.Append("return " + sCSCode + "; \n");
            sb.Append("} \n");
            sb.Append("} \n");
            sb.Append("}\n");

            CompilerResults cr = icc.CompileAssemblyFromSource(cp, sb.ToString());
            if (cr.Errors.Count > 0)
            {
                Console.WriteLine("ERROR: " + cr.Errors[0].ErrorText,
                   "Error evaluating cs code");
                return null;
            }

            System.Reflection.Assembly a = cr.CompiledAssembly;
            object o = a.CreateInstance("CSCodeEvaler.CSCodeEvaler");

            Type t = o.GetType();
            MethodInfo mi = t.GetMethod("EvalCode");

            object s = mi.Invoke(o, null);
            return s;
        }
    }
}
