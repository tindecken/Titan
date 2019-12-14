using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Titan.Framework.CustomAttributes
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class TestCaseTypeAttribute : PropertyAttribute
    {
        public TestCaseTypeAttribute(string value) : base(value) { }
    }
}
