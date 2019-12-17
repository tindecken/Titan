using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace Titan.PageObjects.Github
{
    public class LoginPage
    {
        private IWebDriver driver;
        [FindsBy(How = How.Id, Using = "login_field")]
        public IWebDriver WEinputName { get; set; }

        [FindsBy(How = How.Id, Using = "password")]
        public IWebDriver WEinputPassword { get; set; }

        [FindsBy(How = How.XPath, Using = "//input[@value='Sign in']")]
        public IWebDriver WEbtnSignIn { get; set; }
    }
}
