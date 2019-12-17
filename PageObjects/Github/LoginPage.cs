using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace Titan.PageObjects.Github
{
    public class LoginPage
    {
        private readonly IWebDriver driver;
        public LoginPage(IWebDriver driver)
        {
            this.driver = driver;
        }
        public IWebElement WEinputName => driver.FindElement(By.Id("login_field"));

        public IWebElement WEinputPassword => driver.FindElement(By.Id("password"));

        public IWebElement WEbtnSignIn => driver.FindElement(By.XPath("//input[@value='Sign in']"));
    }
}
