using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Un4seen.Bass;

namespace Beats.Audio
{
	/// <summary>
	/// Represents sound data that has information on what to play when.
	/// </summary>
	public class SoundData : IDisposable
	{
		/// <summary>
		/// The absolute time offset in ms from the start of the map at which this sound will be played.
		/// </summary>
		[JsonProperty("offset")]
		public int Offset { get; private set; }

		/// <summary>
		/// The path to the sound file that should be played. This path is relative to the value of the "Path" property. If the "Path" property is null, the sound file path is relative to the current working directory.
		/// </summary>
		[JsonProperty("soundFile")]
		public string SoundFile { get; private set; }

		/// <summary>
		/// The base path the sound file is relative to. Do not terminate with a directory separator.
		/// </summary>
		public string Path { get; set; }

		private int streamHandle;
		private bool isDisposed;

		/// <summary>
		/// Plays the sound file this sound data refers to.
		/// </summary>
		public void Play()
		{
			if (isDisposed)
				throw new ObjectDisposedException(ToString());
			if(streamHandle == 0)
				streamHandle = Bass.BASS_StreamCreateFile(Path != null ? (Path + "/" + SoundFile) : SoundFile, 0, 0, BASSFlag.BASS_DEFAULT);

			Bass.BASS_ChannelPlay(streamHandle, true);
		}

		/// <summary>
		/// Disposes unmanaged audio data that was loaded by bass.
		/// </summary>
		public void Dispose()
		{
			if (isDisposed)
				throw new ObjectDisposedException(ToString());
			if (streamHandle != 0)
				Bass.BASS_StreamFree(streamHandle);

			isDisposed = true;
		}

		public override string ToString()
		{
			return $"SoundData: {SoundFile} @ {Offset}";
		}
	}
}
