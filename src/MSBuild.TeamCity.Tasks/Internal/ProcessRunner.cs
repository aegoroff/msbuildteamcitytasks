/*
 * Created by: egr
 * Created at: 02.09.2010
 * © 2007-2010 Alexander Egorov
 */

using System.Diagnostics;
using System.IO;

namespace MSBuild.TeamCity.Tasks.Internal
{
	///<summary>
	/// Represents an executable file run wrapper
	///</summary>
	internal sealed class ProcessRunner
	{
		private readonly string _testExePath;

		///<summary>
		/// Initializes a new instance of the <see cref="ProcessRunner"/> class
		///</summary>
		///<param name="testExePath">Path to executable file</param>
		internal ProcessRunner( string testExePath )
		{
			_testExePath = testExePath;
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
		internal void Run( params string[] commandLine )
		{
			string dir = Path.GetDirectoryName(Path.GetFullPath(_testExePath));

			SequenceBuilder<string> cmdLine = new SequenceBuilder<string>(commandLine, " ");

			Process app = new Process
			              	{
			              		StartInfo =
			              			{
			              				FileName = _testExePath,
			              				Arguments = cmdLine.ToString(),
			              				UseShellExecute = false,
			              				RedirectStandardOutput = false,
			              				WorkingDirectory = dir,
			              				CreateNoWindow = true
			              			}
			              	};

			using ( app )
			{
				app.Start();
				if ( ExecutionTimeoutMilliseconds > 0 )
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