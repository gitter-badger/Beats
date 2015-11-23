using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beats.Align
{
	/// <summary>
	/// Represents a box, which has a size and a position and triggers events when any of those changes.
	/// </summary>
	public interface IBox : IPosition, ISize
	{
	}
}
