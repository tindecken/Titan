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

namespace Titan.Framework
{
    class SetupAndTearDown
    {
        private Logger logger = new Logger();
        [SetUp]
        public void Setup()
        {
            Console.WriteLine("Create Record into SQLiteDB");
            string RunId = TestExecutionContext.CurrentContext.CurrentTest.Parent.Properties.Get("RunId").ToString();
            TestExecutionContext.CurrentContext.CurrentTest.Properties.Set("RunId", RunId);
            var TestCaseId = TestContext.CurrentContext.Test.Properties.Get("TestCaseId").ToString();
            DBResultMapping initTestRecord = new DBResultMapping
            {
                RunId = int.Parse(RunId),
                BuildName = TestContext.CurrentContext.Test.ClassName.Split('.')[2],
                TestCaseId = TestContext.CurrentContext.Test.Properties.Get("TestCaseId").ToString(),
                TestCaseName = TestContext.CurrentContext.Test.Name,
                TestCaseType = TestContext.CurrentContext.Test.Properties.Get("TestCaseType").ToString(),
                TestCaseOwner = TestContext.CurrentContext.Test.Properties.Get("Author").ToString(),
                TestCaseStatus = TestContext.CurrentContext.Result.Outcome.Status.ToString(),
                RunOwner = TestContext.CurrentContext.Test.Properties.Get("RunOwner").ToString(),
                RunMachine = Environment.MachineName,
                StartTime = DateTime.UtcNow,
                IsInQueue = false,
                TestGroupId = TestContext.CurrentContext.Test.Properties.Get("TestGroupId").ToString(),
                TestGroupName = TestContext.CurrentContext.Test.Properties.Get("Category").ToString(),
                TestSuiteId = TestContext.CurrentContext.Test.Properties.Get("TestSuiteId").ToString(),
                TestSuiteName = TestContext.CurrentContext.Test.ClassName.Split('.')[3],
                RunFailedMessage = "",
                RunFailedImage = null,
                ManualAnalyze = "",
                Issue = "",
                Comments = "",
            };

            SQLiteUtils.InitRecord(initTestRecord);


        }
        [TearDown]
        public void TearDown()
        {
            Console.WriteLine("-------- Teardown --------");
            switch (TestContext.CurrentContext.Result.Outcome.Status)
            {
                case TestStatus.Passed:
                    logger.Pass();
                    break;
                case TestStatus.Failed:
                    logger.Fail();
                    break;
            }



            //WebDriverFactory.CloseAllDrivers();

            //Process[] chromeDriverProcesses = Process.GetProcessesByName("chromedriver");

            //foreach (var chromeDriverProcess in chromeDriverProcesses)
            //{
            //    chromeDriverProcess.Kill();
            //}
        }
    }
}
