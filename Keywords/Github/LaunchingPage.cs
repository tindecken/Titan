using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;
using Titan.PageObjects.Github;
using System.Threading;
namespace Titan.Keywords.Github
{
    public class LaunchingPage
    {
        private IWebDriver driver;
        private LaunchingPageObject launchingObj;
        public LaunchingPage(IWebDriver driver) {
            this.driver = driver;
            launchingObj = new LaunchingPageObject(driver);
        }

        public void GotoLoginPage() {
            launchingObj.WEbtnLogin.Click();
            Thread.Sleep(3000);
        }
    }
}
