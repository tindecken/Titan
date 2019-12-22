using NUnit.Framework;
using NUnit.Framework.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Titan.Framework.WrapperFactory;
using Titan.SQLiteDB;
using NUnit.Framework.Interfaces;
using System.Diagnostics;
using System.Collections;

namespace Titan.Framework
{
    class SetupAndTearDown
    {
        private Logger logger = new Logger();
        [SetUp]
        public void Setup()
        {
            string RunId = TestExecutionContext.CurrentContext.CurrentTest.Parent.Properties.Get("RunId").ToString();
            TestExecutionContext.CurrentContext.CurrentTest.Properties.Set("RunId", RunId);
            var TestCaseId = TestContext.CurrentContext.Test.Properties.Get("TestCaseId").ToString();
            DBResultMapping initTestRecord = new DBResultMapping
            {
                RunId = int.Parse(RunId),
                BuildName = TestContext.CurrentContext.Test.ClassName.Split('.')[2],
                TestCaseId = TestContext.CurrentContext.Test.Properties.Get("TestCaseId").ToString(),
                TestCaseName = TestContext.CurrentContext.Test.Name,
                TestCaseDescription = TestContext.CurrentContext.Test.Properties.Get("Description").ToString(),
                TestCaseType = TestContext.CurrentContext.Test.Properties.Get("TestCaseType").ToString(),
                TestCaseOwner = TestContext.CurrentContext.Test.Properties.Get("Author").ToString(),
                TestCaseStatus = TestContext.CurrentContext.Result.Outcome.Status.ToString(),
                RunOwner = TestContext.CurrentContext.Test.Properties.Get("RunOwner").ToString(),
                IsDebug = (bool)TestContext.CurrentContext.Test.Properties.Get("IsDebug"),
                RunMachine = Environment.MachineName,
                StartTime = DateTime.UtcNow,
                IsInQueue = false,
                Category = TestContext.CurrentContext.Test.Properties.Get("Category").ToString(),
                TestSuiteName = TestContext.CurrentContext.Test.ClassName.Split('.')[3],
                RunFailedMessage = "",
                RunFailedImage = null,
                ManualAnalyze = "",
                Issue = "",
                Comments = "",
            };

            //TestCase Info
            initTestRecord.RunLog = $"{DateTime.UtcNow} - [Setup]: -------- Setup --------";
            initTestRecord.RunLog += "\nTest Case attributes:";
            IList<string> lstPros = TestContext.CurrentContext.Test.Properties.Keys.ToList<string>();
            foreach (var key in lstPros)
            {
                if (!key.Contains("Driver")) //Driver attribute is multiple, so need to get list of it before action
                {
                    initTestRecord.RunLog += $"\n[{key}]: {TestContext.CurrentContext.Test.Properties.Get(key)}";
                }
                else {
                    IList lstDriver = (IList)TestContext.CurrentContext.Test.Properties["Driver"];
                    foreach (string driver in lstDriver) {
                        initTestRecord.RunLog += $"\n[{key}]: {driver}";
                    }
                }
                
            }
            if (initTestRecord.IsDebug) initTestRecord.RunLog += "\n[Warning] TestCase is used DEBUG mode, so, the orthers can't init new WebDriver, it means can't run, and you must close manually Web Browser and webdriver process in TaskManager.";
            initTestRecord.RunLog += "\n";
            SQLiteUtils.InitRecord(initTestRecord);
        }
        [TearDown]
        public void TearDown()
        {
            logger.Info($"-------- Teardown --------");
            switch (TestContext.CurrentContext.Result.Outcome.Status)
            {
                case TestStatus.Passed:
                    logger.Pass();
                    break;
                case TestStatus.Failed:
                    logger.Fail();
                    break;
            }
            bool isDebug = (bool)TestContext.CurrentContext.Test.Properties.Get("IsDebug");
            if (isDebug) return;
            IList lstDriverName = (IList)TestContext.CurrentContext.Test.Properties["Driver"];
            foreach (string dv in lstDriverName)
            {
                logger.Info("Driver: " + dv);
                switch (dv[dv.Length - 1])
                {
                    case '1':
                        WebDriverFactory.Driver1.Quit();
                        WebDriverFactory.Driver1 = null;
                        break;
                    case '2':
                        WebDriverFactory.Driver2.Quit();
                        WebDriverFactory.Driver2 = null;
                        break;
                    case '3':
                        WebDriverFactory.Driver3.Quit();
                        WebDriverFactory.Driver3 = null;
                        break;

                }

            }
        }
    }
}
