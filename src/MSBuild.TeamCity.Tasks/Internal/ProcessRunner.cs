/*
 * Created by: egr
 * Created at: 02.09.2010
 * © 2007-2015 Alexander Egorov
 */

using System.Collections.Generic;
using System.Diagnostics;

namespace MSBuild.TeamCity.Tasks.Internal
{
    /// <summary>
    ///     Represents an executable file run wrapper
    /// </summary>
    internal sealed class ProcessRunner
    {
        #region Constants and Fields

        private readonly string testExePath;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="ProcessRunner" /> class
        /// </summary>
        /// <param name="testExePath">Path to executable file</param>
        internal ProcessRunner(string testExePath)
        {
            this.testExePath = testExePath;
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Runs executable
        /// </summary>
        /// <param name="commandLine">
        ///     Command line
        /// </param>
        /// <returns>Redirected standart output lines</returns>
        internal IList<string> Run(params string[] commandLine)
        {
            IList<string> result = null;
            using (var app = new Process())
            {
                app.StartInfo = new ProcessStartInfo
                {
                    FileName = this.testExePath,
                    Arguments = string.Join(" ", commandLine),
                    UseShellExecute = false,
                    RedirectStandardOutput = this.RedirectStandardOutput,
                    WorkingDirectory = this.testExePath.GetDirectoryName(),
                    CreateNoWindow = true
                };
                app.Start();
                if (this.RedirectStandardOutput)
                {
                    result = app.StandardOutput.ReadLines();
                }
                if (this.ExecutionTimeoutMilliseconds > 0)
                {
                    if (app.WaitForExit(this.ExecutionTimeoutMilliseconds) && this.UseAppExitCode)
                    {
                        this.ProcessExitCode = app.ExitCode;
                    }
                }
                else
                {
                    app.WaitForExit();
                    if (this.UseAppExitCode)
                    {
                        this.ProcessExitCode = app.ExitCode;
                    }
                }
            }
            return result ?? new List<string>();
        }

        #endregion

        #region Properties

        /// <summary>
        ///     Gets or sets the time to wait the specified number of milliseconds for the test process to finish.
        ///     By default waiting indefinitely.
        /// </summary>
        internal int ExecutionTimeoutMilliseconds { get; set; }

        internal int ProcessExitCode { get; set; }

        internal bool UseAppExitCode { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether the output of an application is written to the StandardOutput stream.
        /// </summary>
        internal bool RedirectStandardOutput { get; set; }

        #endregion
    }
}