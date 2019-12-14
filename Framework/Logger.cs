using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using log4net;
using log4net.Config;
using NUnit.Framework;
using OpenQA.Selenium;
using Titan.Framework.WrapperFactory;
using Titan.SQLiteDB;

namespace Titan.Framework
{
    public class Logger
    {
        private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private IWebDriver driver;
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
            dBResultMapping.EndTime = DateTime.UtcNow;
            dBResultMapping.RunFailedMessage = $"{TestContext.CurrentContext.Result.Message} {TestContext.CurrentContext.Result.StackTrace}";
            Bitmap bmp = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
            byte[] errImgAsByte;
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.CopyFromScreen(0, 0, 0, 0, Screen.PrimaryScreen.Bounds.Size);
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
