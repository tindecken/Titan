using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;


namespace Titan.PageObjects.Github
{
    public class HomePage
    {
        private IWebDriver driver;
        public HomePage(IWebDriver driver) {
            this.driver = driver;
        }
        public IWebElement WEbtnLogin => driver.FindElement(By.XPath("//a[@href='/login']"));
    }
}
