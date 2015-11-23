using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beats.Align
{
	/// <summary>
	/// Represents an object that has a size. Fires events when the size changes.
	/// </summary>
	public interface ISize
	{
		/// <summary>
		/// The width of the object.
		/// </summary>
		int Width { get; set; }
		/// <summary>
		/// The height of the object.
		/// </summary>
		int Height { get; set; }

		/// <summary>
		/// Triggers after the width of the object has changed.
		/// </summary>
		event Action WidthChanged;
		/// <summary>
		/// Triggers after the height of the object has changed.
		/// </summary>
		event Action HeightChanged;
	}
}
