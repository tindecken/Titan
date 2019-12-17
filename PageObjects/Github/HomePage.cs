using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using System;
using System.Collections.Generic;
using System.Text;


namespace Titan.PageObjects.Github
{
    public class HomePage
    {
        private IWebDriver driver;
        [FindsBy(How = How.XPath, Using = "//a[@href='/login']")]
        public IWebDriver WEbtnLogin { get; set; }
    }
}
