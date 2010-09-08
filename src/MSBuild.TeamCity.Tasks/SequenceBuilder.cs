/*
 * Created by: egr
 * Created at: 08.09.2010
 * © 2007-2010 Alexander Egorov
 */

using System.Collections.Generic;
using System.Text;

namespace MSBuild.TeamCity.Tasks
{
	/// <summary>
	/// Builds strings of values separated by an element (comma) 
	/// using all values of an IEnumerable&lt;T&gt; item.
	/// </summary>
	/// <typeparam name="T">The type of sequence element</typeparam>
	/// <example>
	/// <code>
	/// void Example()
	/// {
	///		int[] values = new int[] { 1, 2 }
	///		SequenceBuilder&lt;int&gt; bi = new SequenceBuilder&lt;int&gt;(values, ", ", "(", ")");
	///		// bi.ToString() will output: (1, 2)
	///		string[] strValues = new string[] { "one", "two" }
	///		SequenceBuilder&lt;string&gt; bs = new SequenceBuilder&lt;string&gt;(strValues, ", ", "(", ")");
	///		// bs.ToString() will output: (one, two)
	/// }
	/// </code>
	/// </example>
	public class SequenceBuilder<T>
	{
		private readonly StringBuilder _builder = new StringBuilder();
		private readonly IEnumerable<T> _enumerator;
		private readonly string _head;
		private readonly string _separator;
		private readonly string _trail;

		/// <summary>
		/// Initializes a new instance of the SequenceBuilder class
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
			AddHead();
			Enumerate();
			AddTrail();
		}

		/// <summary>
		/// Initializes a new instance of the SequenceBuilder class with null head and trail.
		/// </summary>
		/// <param name="enumerator">Enumerator that yields values in desired sequence</param>
		/// <param name="separator">Separator string beetwen values</param>
		public SequenceBuilder( IEnumerable<T> enumerator, string separator )
			: this(enumerator, separator, null, null)
		{
		}

		private int TrailLength
		{
			get { return !string.IsNullOrEmpty(_trail) ? _trail.Length : 0; }
		}

		private int HeadLength
		{
			get { return !string.IsNullOrEmpty(_head) ? _head.Length : 0; }
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
			return ( _builder.Length == HeadLength - _separator.Length + TrailLength )
			       	? string.Empty
			       	: _builder.ToString();
		}

		private void AddTrail()
		{
			if ( _builder.Length <= _separator.Length )
			{
				return;
			}
			_builder.Remove(_builder.Length - _separator.Length, _separator.Length);
			_builder.Append(_trail);
		}

		private void Enumerate()
		{
			foreach ( T s in _enumerator )
			{
				_builder.Append(s);
				_builder.Append(_separator);
			}
		}

		private void AddHead()
		{
			_builder.Append(_head);
		}
	}
}