using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Titan.Framework;
using System.Data.SQLite;

namespace Titan.SQLiteDB
{
    public class SQLiteUtils
    {
        private static string cs = $@"URI=file:{ProjectConstant.sSQLiteDB}";
        public string GetVersion()
        {
            string version;
            string stm = "SELECT SQLITE_VERSION()";

            var con = new SQLiteConnection(cs);
            con.Open();
            var cmd = new SQLiteCommand(stm, con);
            version = cmd.ExecuteScalar().ToString();

            return version;
        }
        public static void Fail(DBResultMapping testRecord, Byte[] errImageAsByte)
        {
            SQLiteConnection con = new SQLiteConnection(cs);
            SQLiteCommand cmd = con.CreateCommand();
            cmd.CommandText = String.Format("UPDATE run SET TestCaseStatus = @TestCaseStatus, EndTime = @EndTime, RunLog = RunLog || @FailedMessage, RunFailedMessage = @RunFailedMessage, RunFailedImage = @RunFailedImage WHERE RunId = @RunId AND TestCaseName = @TestCaseName");
            SQLiteParameter param = new SQLiteParameter("@RunFailedImage", System.Data.DbType.Binary);
            param.Value = errImageAsByte;
            cmd.Parameters.Add(param);
            cmd.Parameters.AddWithValue("@RunId", testRecord.RunId);
            cmd.Parameters.AddWithValue("@TestCaseName", testRecord.TestCaseName);
            cmd.Parameters.AddWithValue("@TestCaseStatus", testRecord.TestCaseStatus);
            cmd.Parameters.AddWithValue("@EndTime", testRecord.EndTime);
            cmd.Parameters.AddWithValue("@FailedMessage", $"{testRecord.RunFailedMessage} \n {DateTime.UtcNow}: FAILED");
            cmd.Parameters.AddWithValue("@RunFailedMessage", testRecord.RunFailedMessage);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }
        public static int LastRunId_Plus_1() {
            int result;
            SQLiteConnection con = new SQLiteConnection(cs);
            SQLiteCommand cmd = con.CreateCommand();
            cmd.CommandText = String.Format("SELECT RunId FROM run ORDER BY RunId DESC LIMIT 1");
            con.Open();
            object exeScalarReult = cmd.ExecuteScalar();
            result = (exeScalarReult == DBNull.Value) ? 1 : Convert.ToInt32(exeScalarReult) + 1;
            con.Close();
            return result;
        }

        public static void InitRecord(DBResultMapping testRecord)
        {
            SQLiteConnection con = new SQLiteConnection(cs);
            con.Open();
            SQLiteCommand cmd = con.CreateCommand();
            cmd.CommandText = String.Format("INSERT INTO run (RunId, BuildName, TestCaseId, TestCaseName, TestCaseDescription, TestCaseType, TestCaseOwner, TestCaseStatus, RunOwner, RunMachine, " +
                "StartTime, IsInQueue, Category, TestSuiteName, RunLog, RunFailedMessage, RunFailedImage, ManualAnalyze, Issue, Comments)" +
                " VALUES (@RunId, @BuildName, @TestCaseId, @TestCaseName, @TestCaseDescription, @TestCaseType, @TestCaseOwner, @TestCaseStatus, @RunOwner, @RunMachine, " +
                "@StartTime, @IsInQueue, @Category, @TestSuiteName, @RunLog, @RunFailedMessage, @RunFailedImage, @ManualAnalyze, @Issue, @Comments)");
            cmd.Parameters.AddWithValue("@RunId", testRecord.RunId);
            cmd.Parameters.AddWithValue("@BuildName", testRecord.BuildName);
            cmd.Parameters.AddWithValue("@TestCaseId", testRecord.TestCaseId);
            cmd.Parameters.AddWithValue("@TestCaseName", testRecord.TestCaseName);
            cmd.Parameters.AddWithValue("@TestCaseDescription", testRecord.TestCaseDescription);
            cmd.Parameters.AddWithValue("@TestCaseType", testRecord.TestCaseType);
            cmd.Parameters.AddWithValue("@TestCaseOwner", testRecord.TestCaseOwner);
            cmd.Parameters.AddWithValue("@TestCaseStatus", testRecord.TestCaseStatus);
            cmd.Parameters.AddWithValue("@RunOwner", testRecord.RunOwner);
            cmd.Parameters.AddWithValue("@RunMachine", testRecord.RunMachine);
            cmd.Parameters.AddWithValue("@StartTime", testRecord.StartTime);
            cmd.Parameters.AddWithValue("@IsInQueue", testRecord.IsInQueue);
            cmd.Parameters.AddWithValue("@Category", testRecord.Category);
            cmd.Parameters.AddWithValue("@TestSuiteName", testRecord.TestSuiteName);
            cmd.Parameters.AddWithValue("@RunLog", "");
            cmd.Parameters.AddWithValue("@RunFailedMessage", testRecord.RunFailedMessage);
            cmd.Parameters.AddWithValue("@RunFailedImage", testRecord.RunFailedImage);
            cmd.Parameters.AddWithValue("@ManualAnalyze", testRecord.ManualAnalyze);
            cmd.Parameters.AddWithValue("@Issue", testRecord.Issue);
            cmd.Parameters.AddWithValue("@Comments", testRecord.Comments);
            cmd.Prepare();
            object result = cmd.ExecuteNonQuery();
            con.Close();
        }

        public static void Pass(DBResultMapping testRecord)
        {
            SQLiteConnection con = new SQLiteConnection(cs);
            con.Open();
            SQLiteCommand cmd = con.CreateCommand();
            cmd.CommandText = String.Format("UPDATE run SET TestCaseStatus = @TestCaseStatus, EndTime = @EndTime, RunLog = RunLog || @PassedMessage || char(10) WHERE RunId = @RunId AND TestCaseName = @TestCaseName");
            cmd.Parameters.AddWithValue("@RunId", testRecord.RunId);
            cmd.Parameters.AddWithValue("@TestCaseName", testRecord.TestCaseName);
            cmd.Parameters.AddWithValue("@TestCaseStatus", testRecord.TestCaseStatus);
            cmd.Parameters.AddWithValue("@EndTime", testRecord.EndTime);
            cmd.Parameters.AddWithValue("@PassedMessage", $"{DateTime.UtcNow}: PASSED");
            cmd.Prepare();
            object result = cmd.ExecuteNonQuery();
            con.Close();
        }
        public static void Info(DBResultMapping testRecord)
        {
            SQLiteConnection con = new SQLiteConnection(cs);
            con.Open();
            SQLiteCommand cmd = con.CreateCommand();
            cmd.CommandText = String.Format("UPDATE run SET TestCaseStatus = @TestCaseStatus, RunLog = RunLog || @RunLog || char(10) WHERE RunId = @RunId AND TestCaseName = @TestCaseName");
            cmd.Parameters.AddWithValue("@RunId", testRecord.RunId);
            cmd.Parameters.AddWithValue("@TestCaseName", testRecord.TestCaseName);
            cmd.Parameters.AddWithValue("@TestCaseStatus", testRecord.TestCaseStatus);
            cmd.Parameters.AddWithValue("@RunLog", testRecord.RunLog);
            cmd.Prepare();
            object result = cmd.ExecuteNonQuery();
            con.Close();
        }
    }
}
