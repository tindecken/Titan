using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;


namespace Titan.PageObjects.Github
{
    public class HomePageObject
    {
        private IWebDriver driver;
        public HomePageObject(IWebDriver driver) {
            this.driver = driver;
        }
        public IWebElement WEAvatar => driver.FindElement(By.XPath("//a[@href='/login']"));
    }
}
