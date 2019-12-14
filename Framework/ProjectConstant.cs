using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace Titan.Framework
{
    public class ProjectConstant
    {
        public static string sProjectName = "Titan";
        private static string sRootDLL = System.Reflection.Assembly.GetExecutingAssembly().Location;
        public static string sRootPath = Path.GetDirectoryName(sRootDLL);
        private static DirectoryInfo drInfoRoot = new DirectoryInfo(sRootPath);
        public static string sProjectPath = drInfoRoot.Parent.Parent.Parent.FullName;
        public static string sSQLiteDB = Path.Combine(sProjectPath, @"SQLiteDB", "result.db");
        public static string sLog4Net = Path.Combine(sProjectPath, "Framework", "log4net.config.xml");
        public static string sChromeDriver = Path.Combine(sProjectPath, "Framework", "WrapperFactory");
    }
}
