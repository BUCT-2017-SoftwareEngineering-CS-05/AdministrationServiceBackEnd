using AdministrationServiceBackEnd.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AdministrationServiceBackEnd.Services
{
    public static class Backup
    {
        private static MuseumContext _context = new MuseumContext();
        private const string path = "./Backups";
        public static IEnumerable<Log> GetAllLogs()
        {
            return _context.Log;
        }
        public static IEnumerable<Log> GetLogsByUsername(string username)
        {
            var logs = new List<Log>();
            foreach (var log in _context.Log)
                if (log.Username == username)
                    logs.Add(log);
            return logs.AsEnumerable<Log>();
        }
        public static bool Save()
        {
            DateTime now_time = DateTime.Now;
            //string path = "./Backups";
            string host = "cdb-3lehih0k.cd.tencentcdb.com";
            string port = "10090";
            string user = "CS1705museum";
            string pwd = "CS1705museum";
            string dbname = "museum_copy";
            string str = "cd " + path + "\nmysqldump -h\"" + host + "\" -P\"" + port + "\" -u\""
                + user + "\" -p\"" + pwd + "\" --set-gtid-purged=OFF "
                + dbname +" > " + now_time.ToFileTime().ToString() + ".sql";
            Console.WriteLine(str);

            System.Diagnostics.Process p = new System.Diagnostics.Process();
            p.StartInfo.FileName = "cmd.exe";
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardInput = true;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.RedirectStandardError = true;
            p.StartInfo.CreateNoWindow = true;
            p.Start();

            p.StandardInput.WriteLine(str + "&exit");

            p.StandardInput.AutoFlush = true;
            string output = p.StandardOutput.ReadToEnd();

            p.WaitForExit();
            p.Close();

            Console.WriteLine(output);
            return true;
        }
        public static bool Load(string filename, bool save = true)
        {
            string[] files = Directory.GetFiles(path, "*.sql");
            foreach (string file in files)
                if (filename == file[^22..^4])
                    return false;
            if (save) Save(); // 恢复数据前，先备份一次，防止数据丢失

            //string path = "./Backups";
            string host = "cdb-3lehih0k.cd.tencentcdb.com";
            string port = "10090";
            string user = "CS1705museum";
            string pwd = "CS1705museum";
            string dbname = "museum_copy"; // 用copy做demo，防止惨案

            string str = "cd " + path + "\nmysql --local-infile -h " + host + " -P " + port + " -u "
                + user + " -p" + pwd + "\n";
            string str1 = "drop database "+dbname+";\ncreate database "+dbname+";\n"
                + "use "+dbname+"\nsource " + filename + ".sql\nexit\n";

            Console.WriteLine(str);

            System.Diagnostics.Process p = new System.Diagnostics.Process();
            p.StartInfo.FileName = "cmd.exe";
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardInput = true;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.RedirectStandardError = true;
            p.StartInfo.CreateNoWindow = true;
            p.Start();

            p.StandardInput.WriteLine(str);
            p.StandardInput.WriteLine(str1 + "&exit");
            p.StandardInput.AutoFlush = true;
            p.StandardInput.Close();
            string output = p.StandardOutput.ReadToEnd();
            p.WaitForExit();
            p.Close();

            Console.WriteLine(output);
            return true;
        }
        
        public static DateTime GetDateByFilename(string filename)
        {
            Console.WriteLine("filename = " + filename);
            return DateTime.FromFileTime(long.Parse(filename));
        }
        public static string GetFilenameByDate(DateTime t)
        {
            return t.ToFileTime().ToString();
        }
        public static List<NameandTime> GetAllBackups()
        {
            //string path = "C:\\Program Files\\MySQL\\MySQL Server 8.0\\bin\\backups";
            string[] files = Directory.GetFiles(path, "*.sql");
            List<NameandTime> dateTimes = new List<NameandTime>();
            foreach (string file in files)
                dateTimes.Add(new NameandTime(file[^22..^4],GetDateByFilename(file[^22..^4])));
            return dateTimes;
        }
        public static bool DeleteBackupByDate(DateTime dt)
        {
            //string path = "C:\\Program Files\\MySQL\\MySQL Server 8.0\\bin\\backups";
            string[] files = Directory.GetFiles(path, "*.sql");
            foreach (string file in files)
            {
                if (dt == GetDateByFilename(file[^22..^4]))
                {
                    File.Delete(file);
                    return true;
                }
            }
            return false;
        }
        public static bool DeleteBackupByFname(string fname)
        {
            string[] files = Directory.GetFiles(path, "*.sql");
            foreach (string file in files)
            {
                if (fname == file[^22..^4])
                {
                    File.Delete(file);
                    return true;
                }
            }
            return false;
        }
        public class NameandTime 
        {
            public NameandTime(string _fname,DateTime _time)
            {
                fname = _fname;
                time = _time;
            }
            public string fname { get; set; }
            public DateTime time { get; set; }
        }
    }
}
