using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Beats.Libraries
{
	/// <summary>
	/// Helper class to load native libraries.
	/// </summary>
	public static class LibraryLoader
	{
		[DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		private static extern IntPtr LoadLibrary(string libname);

		[DllImport("kernel32.dll", CharSet = CharSet.Auto)]
		private static extern bool FreeLibrary(IntPtr hModule);

		/// <summary>
		/// Loads all referenced libraries.
		/// </summary>
		public static void LoadAll()
		{
			load("Libraries/bass.dll");
		}
		private static void load(string fileName)
		{
			//Load
			IntPtr Handle = LoadLibrary(fileName);
			if (Handle == IntPtr.Zero)
			{
				int errorCode = Marshal.GetLastWin32Error();
				throw new Exception($"Failed to load library (ErrorCode: {errorCode})");
			}
		}
	}
}
