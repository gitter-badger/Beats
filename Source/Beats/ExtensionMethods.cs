using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beats
{
	/// <summary>
	/// Static class that holds extension methods.
	/// </summary>
	public static class ExtensionMethods
	{
		/// <summary>
		/// Calls the action, if it is not null.
		/// </summary>
		/// <param name="action">The action that should be called.</param>
		public static void Trigger(this Action action)
		{
			if (action != null)
				action();
		}
	}
}
