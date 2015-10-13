/*
 * Created by: egr
 * Created at: 08.09.2010
 * © 2007-2013 Alexander Egorov
 */

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace MSBuild.TeamCity.Tasks.Internal
{
    /// <summary>
    ///     Builds strings of values separated by an element (comma)
    ///     using all values of an IEnumerable&lt;T&gt; item.
    /// </summary>
    /// <typeparam name="T">The type of sequence element</typeparam>
    /// <example>
    ///     <code>
    /// void Example()
    /// {
    ///     int[] values = new int[] { 1, 2 }
    ///     SequenceBuilder&lt;int&gt; bi = new SequenceBuilder&lt;int&gt;(values, ", ", "(", ")");
    ///     // bi.ToString() will output: (1, 2)
    ///     string[] strValues = new string[] { "one", "two" }
    ///     SequenceBuilder&lt;string&gt; bs = new SequenceBuilder&lt;string&gt;(strValues, ", ", "(", ")");
    ///     // bs.ToString() will output: (one, two)
    /// }
    /// </code>
    /// </example>
    public class SequenceBuilder<T>
    {
        private readonly IEnumerable<T> enumerator;
        private readonly string head;
        private readonly string separator;
        private readonly string trail;

        /// <summary>
        ///     Initializes a new instance of the SequenceBuilder class
        /// </summary>
        /// <param name="enumerator">Enumerator that yields values in desired sequence</param>
        /// <param name="separator">Separator string beetwen values</param>
        /// <param name="head">Sequence's header</param>
        /// <param name="trail">Sequence's trail</param>
        [SuppressMessage("Microsoft.Design", "CA1026:DefaultParametersShouldNotBeUsed", Justification = "By design")]
        public SequenceBuilder(IEnumerable<T> enumerator, string separator, string head = null, string trail = null)
        {
            this.enumerator = enumerator;
            this.separator = separator;
            this.head = head;
            this.trail = trail;
        }

        /// <summary>
        ///     Converts value of the instance into string.
        /// </summary>
        /// <returns>
        ///     Filter string constructed from sequence of values or string.Empty if
        ///     the sequence is empty.
        /// </returns>
        public override string ToString()
        {
            if (!this.enumerator.Any())
            {
                return string.Empty;
            }
            var strings = from T item in this.enumerator
                where !string.IsNullOrEmpty(item.ToString())
                select item.ToString();
            return this.head + strings.Join(this.separator) + this.trail;
        }
    }
}