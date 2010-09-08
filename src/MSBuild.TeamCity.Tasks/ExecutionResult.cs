/*
 * Created by: egr
 * Created at: 02.09.2010
 * © 2007-2010 Alexander Egorov
 */

using System.Collections.Generic;

namespace MSBuild.TeamCity.Tasks
{
	///<summary>
	/// Represents a task execution result
	///</summary>
	public struct ExecutionResult
	{
		///<summary>
		/// Gets or sets <see cref="TeamCityMessage"/> to output
		///</summary>
		public IList<TeamCityMessage> Messages { get; set; }

		///<summary>
		/// Gets or sets a value indicating whether the import was successful.
		/// True if the operation was successful and there are no failing tests 
		/// otherwise false
		///</summary>
		public bool Status { get; set; }
	}
}