using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Titan.SQLiteDB
{
    public class DBResultMapping
    {
        public int RunId { get; set; }
        public string BuildName { get; set; }
        public string TestCaseId { get; set; }
        public string TestCaseName { get; set; }
        public string TestCaseType { get; set; }
        public string TestCaseOwner { get; set; }
        public string TestCaseStatus { get; set; }
        public string RunOwner { get; set; }
        public string RunMachine { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public Boolean IsInQueue { get; set; }
        public string TestGroupId { get; set; }
        public string TestGroupName { get; set; }
        public string TestSuiteId { get; set; }
        public string TestSuiteName { get; set; }
        public string RunLog { get; set; }
        public string RunFailedMessage { get; set; }
        public byte[] RunFailedImage { get; set; }
        public string ManualAnalyze { get; set; }
        public string Issue { get; set; }
        public string Comments { get; set; }
    }
}
