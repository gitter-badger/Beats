using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beats.Graphics
{
	/// <summary>
	/// Something that can be drawn.
	/// </summary>
	public interface IDrawable
	{
		/// <summary>
		/// Draws the object.
		/// </summary>
		void Draw();
	}
}
