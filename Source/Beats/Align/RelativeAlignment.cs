using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beats.Align
{
	/// <summary>
	/// Represents an alignment that positions its target relative to a given box.
	/// </summary>
	public class RelativeAlignment : Alignment
	{
		/// <summary>
		/// The x offset from the box.
		/// </summary>
		public int OffsetX { get; set; }
		/// <summary>
		/// The y offset from the box.
		/// </summary>
		public int OffsetY { get; set; }

		/// <summary>
		/// The percentage across the size of the box the object should be positioned (0.5 is the center).
		/// </summary>
		public float PercentX { get; set; }
		/// <summary>
		/// The percentage across the size of the box the object should be positioned (0.5 is the center).
		/// </summary>
		public float PercentY { get; set; }

		private IBox alignmentBox;

		private bool alignmentBoxContainsAligned;

		/// <summary>
		/// Constructs a new relative alignment.
		/// </summary>
		/// <param name="aligned">The object that should be aligned.</param>
		/// <param name="relativeTo">The box that the object should be aligned relative to.</param>
		/// <param name="relativeToContainsAligned">True if the aligned objects coordinates are relative to the coordinates of the box. If relativeTo is a parent (directly or indirectly) of aligned, then this should be set to true, otherwise this should be set to false.</param>
		public RelativeAlignment(IPosition aligned, IBox relativeTo, bool relativeToContainsAligned)
			: base(aligned)
		{
			alignmentBox = relativeTo;

			alignmentBoxContainsAligned = relativeToContainsAligned;

			alignmentBox.WidthChanged += updateX;
			alignmentBox.HeightChanged += updateY;
			if(!alignmentBoxContainsAligned)
			{
				alignmentBox.XChanged += updateX;
				alignmentBox.YChanged += updateY;
			}

			updateX();
			updateY();
		}

		public override void Update()
		{
			updateX();
			updateY();
		}

		private void updateX()
		{
			if(!alignmentBoxContainsAligned)
				Target.X = (int)(alignmentBox.Width * PercentX + alignmentBox.X + OffsetX);
			else
				Target.X = (int)(alignmentBox.Width * PercentX + OffsetX);
		}
		private void updateY()
		{
			if (!alignmentBoxContainsAligned)
				Target.Y = (int)(alignmentBox.Height * PercentY + alignmentBox.Y + OffsetY);
			else
				Target.Y = (int)(alignmentBox.Height * PercentY + OffsetY);
		}
	}
}
