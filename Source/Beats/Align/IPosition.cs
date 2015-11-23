using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beats.Align
{
	/// <summary>
	/// Represents an object that has a position. Fires events when the position gets changed.
	/// </summary>
	public interface IPosition
	{
		/// <summary>
		/// The x position of this object.
		/// </summary>
		int X { get; set; }
		/// <summary>
		/// The y position of this object.
		/// </summary>
		int Y { get; set; }

		/// <summary>
		/// Triggers after the x position of this object changed.
		/// </summary>
		event Action XChanged;
		/// <summary>
		/// Triggers after the y position of this object changed.
		/// </summary>
		event Action YChanged;
	}
}
