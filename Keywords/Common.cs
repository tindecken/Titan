using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;

namespace Titan.Keywords
{
    public class Common
    {
        IWebDriver driver;
        public Common(IWebDriver driver)
        {
            this.driver = driver;
        }

        public void GoToUrl(string url)
        {
            driver.Navigate().GoToUrl(url);
        }
    }
}
