/*
 * Created by: egr
 * Created at: 02.09.2010
 * © 2007-2013 Alexander Egorov
 */

using System.Collections.Generic;
using System.Diagnostics;

namespace MSBuild.TeamCity.Tasks.Internal
{
    ///<summary>
    /// Represents an executable file run wrapper
    ///</summary>
    internal sealed class ProcessRunner
    {
        #region Constants and Fields

        private readonly string testExePath;

        #endregion

        #region Constructors and Destructors

        ///<summary>
        /// Initializes a new instance of the <see cref="ProcessRunner"/> class
        ///</summary>
        ///<param name="testExePath">Path to executable file</param>
        internal ProcessRunner(string testExePath)
        {
            this.testExePath = testExePath;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the time to wait the specified number of milliseconds for the test process to finish.
        /// By default waiting indefinitely.
        /// </summary>
        internal int ExecutionTimeoutMilliseconds { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the output of an application is written to the StandardOutput stream.
        /// </summary>
        internal bool RedirectStandardOutput { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// Runs executable
        /// </summary>
        /// <param name="commandLine">
        /// Command line
        /// </param>
        /// <returns>Redirected standart output lines</returns>
        internal IList<string> Run(params string[] commandLine)
        {
            IList<string> result = null;
            using (var app = new Process())
            {
                app.StartInfo = new ProcessStartInfo
                                    {
                                        FileName = testExePath,
                                        Arguments = string.Join(" ", commandLine),
                                        UseShellExecute = false,
                                        RedirectStandardOutput = RedirectStandardOutput,
                                        WorkingDirectory = testExePath.GetDirectoryName(),
                                        CreateNoWindow = true
                                    };
                app.Start();
                if (RedirectStandardOutput)
                {
                    result = app.StandardOutput.ReadLines();
                }
                if (ExecutionTimeoutMilliseconds > 0)
                {
                    app.WaitForExit(ExecutionTimeoutMilliseconds);
                }
                else
                {
                    app.WaitForExit();
                }
            }
            return result ?? new List<string>();
        }

        #endregion
    }
}