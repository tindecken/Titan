using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Titan.Framework.CustomAttributes
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class TestSuiteIdAttribute : PropertyAttribute
    {
        public TestSuiteIdAttribute(string value) : base(value) { }
    }
}
