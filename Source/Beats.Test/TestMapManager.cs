using Beats.Maps;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beats.Test
{
	[TestClass]
	public class TestMapManager
	{
		[TestMethod]
		public void TestListMaps()
		{
			List<MapSetMetadata> sets = MapManager.GetMapSets();
			Assert.IsTrue(sets.Count == 1);
			List<Map> maps = sets[0].GetMaps();
			Assert.IsTrue(maps.Count == 1);
			Assert.IsTrue(maps[0].Sounds[0].Offset == 50);
			Assert.IsTrue(maps[0].Sounds[0].SoundFile == "Blip Stream.mp3");
		}
	}
}
