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
using System.Threading;
using Titan.Keywords;

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
            WebDriverFactory.InitBrowser("Chrome");
            Common common = new Common(WebDriverFactory.Driver);
            HomePage homePage = new HomePage(WebDriverFactory.Driver);
            LoginPage loginPage = new LoginPage(WebDriverFactory.Driver);
            common.GoToUrl("https://github.com");
            homePage.WEbtnLogin.Click();
            loginPage.WEinputName.SendKeys("tindecken");
            loginPage.WEinputPassword.SendKeys("..");
            loginPage.WEbtnSignIn.Click();
            Thread.Sleep(5000);
            Assert.IsTrue(false);
        }
    }
}
