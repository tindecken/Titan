using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using log4net;
using NUnit.Framework;
using OpenQA.Selenium;
using Titan.SQLiteDB;

namespace Titan.Framework
{
    public class Logger
    {
        private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        DBResultMapping dBResultMapping = new DBResultMapping();
        public Logger()
        {
            //GlobalContext.Properties["sqliteDB"] = ProjectConstant.sSQLiteDB;
            //XmlConfigurator.ConfigureAndWatch(new FileInfo(ProjectConstant.sLog4Net));
        }
        public void Info(String message)
        {
            GetTestCaseInfo();
            // Get call stack
            StackTrace stackTrace = new StackTrace();

            // Get calling method name
            dBResultMapping.RunLog = $"{DateTime.UtcNow} - [{stackTrace.GetFrame(1).GetMethod().Name}]: {message}";
            SQLiteUtils.Info(dBResultMapping);
        }
        public void Pass()
        {
            GetTestCaseInfo();
            dBResultMapping.EndTime = DateTime.UtcNow;
            SQLiteUtils.Pass(dBResultMapping);
        }
        public void Fail()
        {
            GetTestCaseInfo();
            // Get call stack
            StackTrace stackTrace = new StackTrace();
            dBResultMapping.EndTime = DateTime.UtcNow;
            dBResultMapping.RunFailedMessage = $"{DateTime.UtcNow} - [{stackTrace.GetFrame(1).GetMethod().Name}]: {TestContext.CurrentContext.Result.Message} {TestContext.CurrentContext.Result.StackTrace}";
            if (OperatingSystem.IsWindows())
            {
                Bitmap bmp = new Bitmap(System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width, System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height);
                byte[] errImgAsByte;
                using (Graphics g = Graphics.FromImage(bmp))
                {
                    g.CopyFromScreen(0, 0, 0, 0, System.Windows.Forms.Screen.PrimaryScreen.Bounds.Size);
                    g.SmoothingMode = SmoothingMode.AntiAlias;
                    g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    using (var stream = new MemoryStream())
                    {
                        bmp.Save(stream, ImageFormat.Png);
                        errImgAsByte = stream.ToArray();
                    }
                }
                if (errImgAsByte == null) return;
                SQLiteUtils.Fail(dBResultMapping, errImgAsByte);
            }
            else if (OperatingSystem.IsLinux()) //Use webdriver for taking screenshot
            {
                
            }
            else if (OperatingSystem.IsMacOS()) //Use webdriver for taking screenshot
            {
                
            }

        }

        public void SetTestCaseInfoToLog4Net()
        {
            //Get Build Name
            GlobalContext.Properties["BuildName"] = TestContext.CurrentContext.Test.FullName.Split('.')[2];
            GlobalContext.Properties["TestSuiteName"] = TestContext.CurrentContext.Test.ClassName;
            GlobalContext.Properties["TestCaseName"] = TestContext.CurrentContext.Test.Name;
        }

        public void GetTestCaseInfo() {
            dBResultMapping.RunId = int.Parse(TestContext.CurrentContext.Test.Properties.Get("RunId").ToString());
            dBResultMapping.TestCaseName = TestContext.CurrentContext.Test.Name.ToString();
            dBResultMapping.TestCaseStatus = TestContext.CurrentContext.Result.Outcome.Status.ToString();
        }
    }
}
