using NUnit.Framework;
using NUnit.Framework.Internal;
using System;
using System.Collections.Generic;
using System.Text;
using Titan.Framework;
using Titan.Framework.CustomAttributes;
using Titan.Framework.WrapperFactory;
using Titan.PageObjects.Github;
using Titan.SQLiteDB;
using SeleniumExtras.PageObjects;

namespace Titan.TestCases.Github
{
    [TestFixture]
    class LoginTest : SetupAndTearDown
    {
        private Framework.Logger logger = new Framework.Logger();
        SQLiteUtils sqlUtil = new SQLiteUtils();
        static int RunId;
        [OneTimeSetUp]
        public void ClassSetup()
        {
            Console.WriteLine("CLASS SETUP");
            RunId = SQLiteUtils.LastRunId_Plus_1();
            TestExecutionContext.CurrentContext.CurrentTest.Properties.Add("RunId", RunId);
        }

        [OneTimeTearDown]
        public void ClassTearDown()
        {
            WebDriverFactory.CloseAllDrivers();
        }

        [Test]
        [Category("ProjectPath")]
        [Author("Tindecken")]
        [TestCaseId("GetSQLiteDBVersion ID")]
        [TestGroupId("TestGroupId1")]
        [TestSuiteId("TestSuiteId1")]
        [RunOwner("Tindecken1")]
        [TestCaseType("Look")]
        public void LoginGitHubWithValidInformation()
        {
            WebDriverFactory.Driver.Url = "https://github.com";
            HomePage homePage = new HomePage();
            homePage.WEbtnLogin.
        }
    }
}
