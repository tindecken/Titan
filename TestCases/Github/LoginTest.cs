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
        }

        [Test]
        [Description("Login to github with valid information")]
        [Category("ProjectPath")]
        [Author("Tindecken")]
        [TestCaseId("GetSQLiteDBVersion ID")]
        [RunOwner("Tindecken1")]
        [TestCaseType("Look")]
        [Driver("Chrome1")]
        public void LoginGitHubWithValidInformation()
        {
            WebDriverFactory.InitBrowser("Chrome1");
            CommonKeyword common = new CommonKeyword(WebDriverFactory.Driver1);
            LaunchingPage launchingPage = new LaunchingPage(WebDriverFactory.Driver1);
            LoginPage loginPage = new LoginPage(WebDriverFactory.Driver1);
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
        [Driver("Chrome1")]
        public void LoginGitHubWithInValidInformation()
        {
            WebDriverFactory.InitBrowser("Chrome1");
            CommonKeyword common = new CommonKeyword(WebDriverFactory.Driver1);
            LaunchingPage launchingPage = new LaunchingPage(WebDriverFactory.Driver1);
            LoginPage loginPage = new LoginPage(WebDriverFactory.Driver1);
            common.GoToUrl("https://github.com");
            launchingPage.GotoLoginPage();
            loginPage.LoginGithub("tindeckenn", "1@Rivaldo", "", "Incorrect username or password.");
        }

        [Test]
        [Description("Login two times github with valid information")]
        [Category("ProjectPath")]
        [Author("Tindecken")]
        [TestCaseId("Login2TimesGitHubWithValidInformation ID")]
        [RunOwner("Tindecken1")]
        [TestCaseType("Look at me")]
        [Driver("Chrome1")]
        [Driver("Chrome2")]
        [IsDebug(true)]
        public void Login2TimesGitHubWithValidInformation()
        {
            WebDriverFactory.InitBrowser("Chrome1");
            WebDriverFactory.InitBrowser("Chrome2");
            CommonKeyword common = new CommonKeyword(WebDriverFactory.Driver1);
            LaunchingPage launchingPage = new LaunchingPage(WebDriverFactory.Driver1);
            LoginPage loginPage = new LoginPage(WebDriverFactory.Driver1);
            common.GoToUrl("https://github.com");
            launchingPage.GotoLoginPage();
            loginPage.LoginGithub("tindecken", "1@Rivaldo", "", "");

            CommonKeyword common2 = new CommonKeyword(WebDriverFactory.Driver2);
            LaunchingPage launchingPage2 = new LaunchingPage(WebDriverFactory.Driver2);
            LoginPage loginPage2 = new LoginPage(WebDriverFactory.Driver2);
            common2.GoToUrl("https://github.com");
            launchingPage2.GotoLoginPage();
            loginPage2.LoginGithub("tindecken", "1@Rivaldo", "", "");
        }
    }
}
