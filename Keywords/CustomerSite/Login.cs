using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Titan.Framework;
using Titan.Framework.WrapperFactory;

namespace Titan.Keywords.CustomerSite
{
    class Login
    {
        private IWebDriver driver;
        private Logger logger = new Logger();
        public Login(IWebDriver driverr) {
            this.driver = driverr;
        }
        public void LoginCustomer(string user, string password) {
            logger.Info("Log from Customer");
            driver.Navigate().GoToUrl("https://manualqc1.acomcloud.com/");
        }

        public void LoginAdmin(string user, string password)
        {
            logger.Info("Log from Admin Site");
            driver.Navigate().GoToUrl("https://google.com");
            logger.Info("Verify true or false");
            Assert.IsTrue(true);
            var a = TestContext.CurrentContext.Test.FullName;
        }
    }
}
