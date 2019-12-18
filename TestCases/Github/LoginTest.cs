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
using Titan.Keywords.Github;

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
        [Description("Login to github with valid information")]
        [Category("ProjectPath")]
        [Author("Tindecken")]
        [TestCaseId("GetSQLiteDBVersion ID")]
        [RunOwner("Tindecken1")]
        [TestCaseType("Look")]
        public void LoginGitHubWithValidInformation()
        {
            WebDriverFactory.InitBrowser("Chrome");
            CommonKeyword common = new CommonKeyword(WebDriverFactory.Driver);
            LaunchingPage launchingPage = new LaunchingPage(WebDriverFactory.Driver);
            LoginPage loginPage = new LoginPage(WebDriverFactory.Driver);
            common.GoToUrl("https://github.com");
            launchingPage.GotoLoginPage();
            loginPage.LoginGithub("tindecken", "1@Rivaldo", "" , "");
        }

        [Test]
        [Description("Login to github with invalid information, verify error login message")]
        [Category("Github")]
        [Author("Tindecken")]
        [TestCaseId("GetSQLiteDBVersion ID")]
        [RunOwner("Tindecken1")]
        [TestCaseType("Look")]
        public void LoginGitHubWithInValidInformation()
        {
            WebDriverFactory.InitBrowser("Chrome");
            CommonKeyword common = new CommonKeyword(WebDriverFactory.Driver);
            LaunchingPage launchingPage = new LaunchingPage(WebDriverFactory.Driver);
            LoginPage loginPage = new LoginPage(WebDriverFactory.Driver);
            common.GoToUrl("https://github.com");
            launchingPage.GotoLoginPage();
            loginPage.LoginGithub("tindeckenn", "1@Rivaldo", "", "Incorrect username or password.");
        }
    }
}
