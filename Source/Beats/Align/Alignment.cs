using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beats.Align
{
	/// <summary>
	/// Represents an alignment which controls the size and positioning of an object.
	/// </summary>
	public abstract class Alignment
	{
		/// <summary>
		/// The target whose size and/or position this alignment controls.
		/// </summary>
		public IPosition Target { get; private set; }

		/// <summary>
		/// Constructs a new alignment with the given target.
		/// </summary>
		/// <param name="target">The target of the alignment.</param>
		public Alignment(IPosition target)
		{
			Target = target;
		}
	}
}
