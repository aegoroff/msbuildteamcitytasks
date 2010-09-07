/*
 * Created by: egr
 * Created at: 06.06.2009
 * © 2007-2009 Alexander Egorov
 */

namespace MSBuild.TeamCity.Tasks
{
	/// <summary>
	/// Represents Team City import report types
	/// </summary>
	public enum ImportType
	{
		/// <summary>
		/// Represents JUnit report "junit"
		/// </summary>
		Junit,

		/// <summary>
		/// Represents Surefire report "surefire"
		/// </summary>
		Surefire,

		/// <summary>
		/// Represents NUnit report "nunit"
		/// </summary>
		Nunit,

		/// <summary>
		/// Represents FindBugs report "findBugs"
		/// </summary>
		FindBugs,

		/// <summary>
		/// Represents PMD report "pmd"
		/// </summary>
		Pmd,

		/// <summary>
		/// Represents FxCop report "FxCop"
		/// </summary>
		FxCop,

		/// <summary>
		/// Represents .NET coverage report "dotNetCoverage"
		/// </summary>
		DotNetCoverage,
		
		/// <summary>
		/// Represents MS test report "mstest"
		/// </summary>
		Mstest,
	}
}