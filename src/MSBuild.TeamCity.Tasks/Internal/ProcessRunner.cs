/*
 * Created by: egr
 * Created at: 02.09.2010
 * © 2007-2012 Alexander Egorov
 */

using System.Diagnostics;

namespace MSBuild.TeamCity.Tasks.Internal
{
    ///<summary>
    /// Represents an executable file run wrapper
    ///</summary>
    internal sealed class ProcessRunner
    {
        private readonly string testExePath;

        ///<summary>
        /// Initializes a new instance of the <see cref="ProcessRunner"/> class
        ///</summary>
        ///<param name="testExePath">Path to executable file</param>
        internal ProcessRunner(string testExePath)
        {
            this.testExePath = testExePath;
        }

        /// <summary>
        /// Gets or sets the time to wait the specified number of milliseconds for the test process to finish.
        /// By default waiting indefinitely.
        /// </summary>
        internal int ExecutionTimeoutMilliseconds { get; set; }

        /// <summary>
        /// Runs executable
        /// </summary>
        /// <param name="commandLine">
        /// Command line
        /// </param>
        internal void Run(params string[] commandLine)
        {
            using (var app = new Process())
            {
                app.StartInfo = new ProcessStartInfo
                                    {
                                        FileName = testExePath,
                                        Arguments = string.Join(" ", commandLine),
                                        UseShellExecute = false,
                                        RedirectStandardOutput = false,
                                        WorkingDirectory = testExePath.GetDirectoryName(),
                                        CreateNoWindow = true
                                    };
                app.Start();
                if (ExecutionTimeoutMilliseconds > 0)
                {
                    app.WaitForExit(ExecutionTimeoutMilliseconds);
                }
                else
                {
                    app.WaitForExit();
                }
            }
        }
    }
}