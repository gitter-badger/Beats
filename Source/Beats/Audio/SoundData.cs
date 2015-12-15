using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beats.Audio
{
	/// <summary>
	/// Represents sound data that has information on what to play when.
	/// </summary>
	public class SoundData
	{
		/// <summary>
		/// The absolute time offset in ms from the start of the map at which this sound will be played.
		/// </summary>
		[JsonProperty("offset")]
		public int Offset { get; private set; }

		/// <summary>
		/// The path to the sound file that should be played.
		/// </summary>
		[JsonProperty("soundFile")]
		public string SoundFile { get; private set; }
	}
}
