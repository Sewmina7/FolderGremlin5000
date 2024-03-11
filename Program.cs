using System;
using System.Diagnostics;
using System.IO;
using System.Threading;

namespace automator
{
    class Program
    {
        public static string cmd_path = AppDomain.CurrentDomain.BaseDirectory + "cmd.txt";
        public static string queue_dir_path = AppDomain.CurrentDomain.BaseDirectory + "queue_dir.txt";
        /// <summary>
        /// path of program that needs to run ex: C:/ProgramFiles/blender/blender.exe
        /// </summary>
        public static string file;
        /// <summary>
        /// Command ( Arguments for file ) to start the process
        /// </summary>
        public static string cmd;
        /// <summary>
        /// Directory to watch. Program will work on the new files in this folder when newly added.
        /// </summary>
        public static string queue_dir;
        public static Process p = new Process();
        static void Main(string[] args)
        {
            Console.WriteLine("AUTO FUCKING MATOR M8!"); Console.WriteLine("By Dombakara brain 3.0");
            Console.WriteLine("");
            bool isMissingFile = false;

            if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + "file.txt"))
            {
                file = File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + "file.txt");
            }
            else
            {
                File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + "file.txt", "C:/blender/");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("File setting not found, created new file. Edit it and run again");
                Console.ForegroundColor = ConsoleColor.White;
                isMissingFile = true;
            }

            if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + "command.txt"))
            {
                cmd = File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + "command.txt");
            }
            else
            {
                File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + "command.txt", "command goes brrrrrrrrrr, use {file} for file, use {output} to output, {FileName} is {File} but without file extension" + Environment.NewLine + "Ex: render {File} -output {FileName}{output}");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Command not found, created new file. Edit it and run again");
                Console.ForegroundColor = ConsoleColor.White;
                isMissingFile = true;
            }

            if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + "queue_dir.txt"))
            {
                queue_dir = File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + "queue_dir.txt");
            }
            else
            {
                File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + "queue_dir.txt", "/path/to/queue/");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Queue folder not found, created new file. Edit it and run again");
                Console.ForegroundColor = ConsoleColor.White;
                isMissingFile = true;
            }

            if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + "output.txt"))
            {
                queue_dir = File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + "output.txt");
            }
            else
            {
                File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + "output.txt", "png");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Output type not found, created new file. Edit it and run again");
                Console.ForegroundColor = ConsoleColor.White;
                isMissingFile = true;
            }

            if (isMissingFile) { Console.WriteLine("Configurations just created, edit them and start again. See ya!"); return; }

            if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + "log.txt"))
            {
                log("Started new session");
            }
            else
            {
                File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + "log.txt", "");
                log("Created this log file");
            }


            Console.WriteLine("Starting file watcher...");
            
            if (!Directory.Exists(queue_dir + "output/"))
            {
                Directory.CreateDirectory(queue_dir + "output/");
            }

            log("Initialized and starting file watcher");

        FileWatcher:
            queue_dir = File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + "queue_dir.txt");
            cmd = File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + "command.txt");
            Console.WriteLine("Watching folder:" + queue_dir);
            Console.WriteLine("Folder exists? : " + Directory.Exists(queue_dir));
            string[] files = Directory.GetFiles(queue_dir);

            if (files.Length <= 0) { Console.ForegroundColor = ConsoleColor.Red; Console.WriteLine("No files in queue, recursing..."); Console.ForegroundColor = ConsoleColor.White; Thread.Sleep(1000); goto FileWatcher; }

            Console.WriteLine("Found " + files.Length + " Files in queue");
            Console.WriteLine("");

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Starting to process file " + files[0]);
            Console.ForegroundColor = ConsoleColor.White;
            string FileName = files[0];
            string commandToExecute = cmd.Replace("{File}", files[0]).Replace("{output}", File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + "output.txt"));
            FileName = Path.GetFileName(files[0]);
            if(FileName.Contains('.')){FileName=FileName.Substring(0,FileName.LastIndexOf('.'));}
            
            commandToExecute = commandToExecute.Replace("{FileName}",FileName);
            
            Console.WriteLine("Executing Command : " + commandToExecute);
            
            
            p=new Process();
            
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.FileName = file;
            p.StartInfo.Arguments = commandToExecute;
            p.OutputDataReceived += new DataReceivedEventHandler(OutputHandler);
            p.Start();
            p.BeginOutputReadLine();
            p.WaitForExit();

            p= new Process();

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Task finished! Deleting query file Waiting 1s to recurse"); Console.ForegroundColor = ConsoleColor.White;
            log("Task finished!");
            try{
            File.Delete(files[0]);
            log("Deleted file " + files[0]);
            }catch{}
            Thread.Sleep(1000);
            log("RECURSION BABY!");

            goto FileWatcher;
        }

        static void log(string txt)
        {
            File.AppendAllText(AppDomain.CurrentDomain.BaseDirectory + "log.txt", "[" + DateTime.Now.TimeOfDay.ToString() + "] " + txt + Environment.NewLine);
        }

static void OutputHandler(object sendingProcess, DataReceivedEventArgs outLine) 
{
    //* Do your stuff with the output (write to console/log/StringBuilder)
    Console.WriteLine(outLine.Data);
    try{if(outLine.Data.Contains("Saving")){
        Console.WriteLine("So this is the end... I'll wait 10 seconds till u save..");
        Thread.Sleep(10000);
        Console.WriteLine("Time's' up baby, Lets go");
       // File.Move()
        p.Kill();
        p.Dispose();
        
    }}catch{}
}
    }
}
