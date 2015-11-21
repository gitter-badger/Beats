using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Beats.Sprites;
using System.Linq;

namespace Beats.Test
{
	[TestClass]
	public class SpriteTest
	{
		[TestMethod]
		public void TestAllChildren()
		{
			TestSprite sprite = new TestSprite();
			TestSprite sprite2 = new TestSprite();
			TestSprite sprite3 = new TestSprite();
			sprite.AddChild(sprite2);
			sprite2.AddChild(sprite3);

			Assert.IsTrue(sprite.AllChildren.Count() == 2);
		}
	}
}
