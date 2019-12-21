using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Titan.Framework.WrapperFactory
{
    class WebDriverFactory
    {
        private static readonly IDictionary<string, IWebDriver> Drivers = new Dictionary<string, IWebDriver>();
        private static IWebDriver driver1;
        private static IWebDriver driver2;
        private static IWebDriver driver3;
        private static ChromeOptions chromeOptions = new ChromeOptions();
       
        public static IWebDriver Driver1
        {
            get
            {
                return driver1;
            }
            set
            {
                driver1 = value;
            }
        }

        public static IWebDriver Driver2
        {
            get
            {
                return driver2;
            }
            set
            {
                driver2 = value;
            }
        }

        public static IWebDriver Driver3
        {
            get
            {
                return driver3;
            }
            set
            {
                driver3 = value;
            }
        }

        public static void InitBrowser(string browserName)
        {
            chromeOptions.AddArgument("--window-size=1300,800");
            chromeOptions.AddExcludedArgument("enable-automation");
            chromeOptions.AddAdditionalCapability("useAutomationExtension", false);
            switch (browserName)
            {
                case "Firefox":
                    if (Driver1 == null)
                    {
                        driver1 = new FirefoxDriver();
                        Drivers.Add("Firefox", Driver1);
                    }
                    break;

                case "IE":
                    if (Driver1 == null)
                    {
                        driver1 = new InternetExplorerDriver(@"C:\PathTo\IEDriverServer");
                        Drivers.Add("IE", Driver1);
                    }
                    break;

                case "Chrome1":
                    if (Drivers.ContainsKey("Chrome1")) {
                        Drivers.Remove("Chrome1");
                    }
                    if (driver1 == null)
                    {
                        driver1 = new ChromeDriver(ProjectConstant.sChromeDriver, chromeOptions);
                        Drivers.Add("Chrome1", Driver1);
                    }
                    break;
                case "Chrome2":
                    if (Drivers.ContainsKey("Chrome2"))
                    {
                        Drivers.Remove("Chrome2");
                    }
                    if (driver2 == null)
                    {
                        driver2 = new ChromeDriver(ProjectConstant.sChromeDriver, chromeOptions);
                        Drivers.Add("Chrome2", Driver2);
                    }
                    break;
                case "Chrome3":
                    if (Drivers.ContainsKey("Chrome3"))
                    {
                        Drivers.Remove("Chrome3");
                    }
                    if (driver3 == null)
                    {
                        driver3 = new ChromeDriver(ProjectConstant.sChromeDriver, chromeOptions);
                        Drivers.Add("Chrome3", Driver3);
                    }
                    break;
            }
        }


        public static void CloseAllDrivers()
        {
            foreach (var key in Drivers.Keys)
            {
                Drivers[key].Close();
                Drivers[key].Quit();
            }
        }
    }
}
