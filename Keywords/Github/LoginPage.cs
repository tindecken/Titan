using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;
using Titan.PageObjects.Github;
using Titan.Framework;

namespace Titan.Keywords.Github
{
    public class LoginPage
    {
        private Logger logger = new Logger();
        private IWebDriver driver;
        private LoginPageObject loginObject;
        public LoginPage(IWebDriver driver)
        {
            this.driver = driver;
            loginObject = new LoginPageObject(driver);
        }

        public void LoginGithub(string sUserName, string sPassword, string sExpectedLogin, string sErrorMessage, string sOptional = null)
        {
            loginObject.WEinputName.SendKeys(sUserName);
            loginObject.WEinputPassword.SendKeys(sPassword);
            loginObject.WEbtnSignIn.Click();
            switch (sExpectedLogin.ToLower())
            {
                case "success":
                    break;
                case "error":
                    break;
                default:
                    break;
            }
            
            if (!string.IsNullOrEmpty(sErrorMessage)) {
                logger.Info($"Assert error message");
                Assert.AreEqual(sErrorMessage, loginObject.sLoginErrorMessage);
            }
            logger.Info("Passed");
        }
    }
}
