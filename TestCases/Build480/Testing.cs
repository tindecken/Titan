using NUnit.Framework;
using NUnit.Framework.Internal;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Titan.Framework;
using Titan.Framework.CustomAttributes;
using Titan.Framework.WrapperFactory;
using Titan.Keywords.CustomerSite;
using Titan.SQLiteDB;

namespace Titan.TestCases.Build480
{
    [TestFixture]
    class Testing : SetupAndTearDown
    {
        IWebDriver driver;
        private Framework.Logger logger = new Framework.Logger();
        SQLiteUtils sqlUtil = new SQLiteUtils();
        static int RunId;
        [OneTimeSetUp]
        public void ClassSetup() {
            Console.WriteLine("CLASS SETUP");
            RunId = SQLiteUtils.LastRunId_Plus_1();
            TestExecutionContext.CurrentContext.CurrentTest.Properties.Add("RunId", RunId);
        }

        [OneTimeTearDown]
        public void ClassTearDown()
        {
            WebDriverFactory.CloseAllDrivers();
        }


        [TestCase(12, 3, ExpectedResult = 4)]
        [TestCase(12, 2, ExpectedResult = 5)]
        [TestCase(12, 4, ExpectedResult = 3)]
        public int DivideTest(int n, int d)
        {
            return n / d;
        }

        [Test, Timeout(100000)]
        [Category("ProjectPath")]
        [Author("Tindecken")]
        [Property("TestCaseType", "Normal")]
        [Property("TestCaseId", "TestCaseId")]
        [Property("TestGroupId", "TestGroupId")]
        [Property("TestSuiteId", "TestSuiteId")]
        [Property("RunOwner", "Tindecken")]
        public void TestLog4NetSQL()
        {
            //WebDriverFactory.InitBrowser("Chrome");
            //Login login = new Login(WebDriverFactory.Driver);
            //login.LoginCustomer("f", "c");
            logger.Info($"HEEEEEEEEEEEEEEEEEEEE");
        }

        [Test]
        [Category("ProjectPath")]
        [Author("Tindecken")]
        [TestCaseId("GetSQLiteDBVersion ID")]
        [TestGroupId("TestGroupId1")]
        [TestSuiteId("TestSuiteId1")]
        [RunOwner("Tindecken1")]
        [TestCaseType("Look")]
        public void GetSQLiteDBVersion() {
            WebDriverFactory.InitBrowser("Chrome2");
            Login login = new Login(WebDriverFactory.Driver2);
            string version = sqlUtil.GetVersion();
            logger.Info($"SQLite Version: {version}");
            login.LoginAdmin("f", "c");
        }
    }
}
