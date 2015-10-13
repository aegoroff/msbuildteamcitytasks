/*
 * Created by: egr
 * Created at: 26.04.2009
 * © 2007-2013 Alexander Egorov
 */

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Microsoft.Build.Framework;
using MSBuild.TeamCity.Tasks.Messages;

namespace MSBuild.TeamCity.Tasks
{
    /// <summary>
    ///     Publish (upload) artifact(s) to JetBrains TeamCity server
    /// </summary>
    /// <example>
    ///     Publish several artifacts.
    ///     <code><![CDATA[
    /// <PublishArtifacts
    ///     Artifacts="File1.zip;File2.zip"
    /// />
    /// ]]></code>
    ///     Publish several artifacts full example (with all optional attributes)
    ///     <code><![CDATA[
    /// <PublishArtifacts
    ///     IsAddTimestamp="true"
    ///     FlowId="1"
    ///     Artifacts="File1.zip;File2.zip"
    /// />
    /// ]]></code>
    ///     Publish several artifacts complex example
    ///     <code><![CDATA[
    /// <ItemGroup>
    ///     <Artifact Include="File1.zip" />
    ///     <Artifact Include="File2.zip" />
    ///     <Artifact Include="File3.zip => Path" />
    /// </ItemGroup>
    /// <PublishArtifacts
    ///     Artifacts="@(Artifact)"
    /// />
    /// ]]></code>
    /// </example>
    public class PublishArtifacts : TeamCityTask
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="PublishArtifacts" /> class
        /// </summary>
        public PublishArtifacts()
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="PublishArtifacts" /> class using
        ///     logger specified
        /// </summary>
        /// <param name="logger"><see cref="ILogger" /> implementation</param>
        public PublishArtifacts(ILogger logger)
            : base(logger)
        {
        }

        /// <summary>
        ///     Gets or sets the artifacts to publish.
        /// </summary>
        /// <remarks>
        ///     Artifacts should adhere following format:<br />
        ///     file_name|directory_name|wildcard [ => target_directory|target_archive ]<p />
        ///     Details <a href="http://confluence.jetbrains.net/display/TCD5/Build+Artifact">in TeamCity documentation</a>
        /// </remarks>
        /// <value>The artifacts.</value>
        [Required]
        public ITaskItem[] Artifacts { get; set; }

        /// <summary>
        ///     Reads TeamCity messages
        /// </summary>
        /// <returns>TeamCity messages list</returns>
        [SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters",
            MessageId = "MSBuild.TeamCity.Tasks.Messages.SimpleTeamCityMessage.#ctor(System.String,System.String)")]
        protected override IEnumerable<TeamCityMessage> ReadMessages()
        {
            return this.Artifacts.Select(item => new SimpleTeamCityMessage("publishArtifacts", item.ItemSpec));
        }
    }
}