/*
 * Created by: egr
 * Created at: 08.09.2010
 * © 2007-2010 Alexander Egorov
 */

using System.Collections.Generic;
using System.Linq;

namespace MSBuild.TeamCity.Tasks.Internal
{
    /// <summary>
    /// Builds strings of values separated by an element (comma) 
    /// using all values of an IEnumerable item.
    /// </summary>
    /// <example>
    /// <code>
    ///	private static IEnumerable&lt;string&gt; EnumerateInt(int[] values)
    ///	{
    ///		foreach (int value in values)
    ///		{
    ///			yield return value.ToString();
    ///		}
    ///	}
    /// 
    /// void Example()
    /// {
    ///		int[] values = new int[] { 1, 2 }
    ///		SequenceBuilder&lt;string&gt; builder = new SequenceBuilder&lt;string&gt;(EnumerateInt(values), ", ", "(", ")");
    ///		// builder.ToString() will output: (1, 2)
    ///		string[] strValues = new string[] { "one", "two" }
    ///		builder = new SequenceBuilder(strValues, ", ", "(", ")");
    ///		// builder.ToString() will output: (one, two)
    /// }
    /// </code>
    /// </example>
    public class SequenceBuilder<T>
    {
        private readonly IEnumerable<T> _enumerator;
        private readonly string _separator;
        private readonly string _head;
        private readonly string _trail;

        /// <summary>
        /// Creates new sequence builder instance.
        /// </summary>
        /// <param name="enumerator">Enumerator that yields values in desired sequence</param>
        /// <param name="separator">Separator string beetwen values</param>
        /// <param name="head">Sequence's header</param>
        /// <param name="trail">Sequence's trail</param>
        public SequenceBuilder( IEnumerable<T> enumerator, string separator, string head, string trail )
        {
            _enumerator = enumerator;
            _separator = separator;
            _head = head;
            _trail = trail;
        }

        /// <summary>
        /// Creates new filter builder instance with null head and trail.
        /// </summary>
        /// <param name="enumerator">Enumerator that yields values in desired sequence</param>
        /// <param name="separator">Separator string beetwen values</param>
        public SequenceBuilder( IEnumerable<T> enumerator, string separator )
            : this(enumerator, separator, null, null)
        {
        }

        /// <summary>
        /// Converts value of the instance into string.
        /// </summary>
        /// <returns>
        /// Filter string constructed from sequence of values or string.Empty if
        /// the sequence is empty.
        /// </returns>
        public override string ToString()
        {
            if ( _enumerator.Count() == 0 )
            {
                return string.Empty;
            }
            IEnumerable<string> strings = from T item in _enumerator select item.ToString();
            return _head + string.Join(_separator, strings.ToArray()) + _trail;
        }
    }
}