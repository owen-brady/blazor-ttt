// WARNING
//
// This file has been generated automatically by Rider IDE
//   to store outlets and actions made in Xcode.
// If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace iOS
{
	[Register ("ViewController")]
	partial class ViewController
	{
		[Outlet]
		UIKit.UILabel LetsPlayLabel { get; set; }

		[Outlet]
		UIKit.UILabel TieLabel { get; set; }

		[Outlet]
		UIKit.UIButton[] TileButtons { get; set; }

		[Outlet]
		UIKit.UILabel TurnIndicatorLabel { get; set; }

		[Outlet]
		UIKit.UILabel WinnerLabel { get; set; }

		void ReleaseDesignerOutlets ()
		{
			if (LetsPlayLabel != null) {
				LetsPlayLabel.Dispose ();
				LetsPlayLabel = null;
			}

			if (TieLabel != null) {
				TieLabel.Dispose ();
				TieLabel = null;
			}

			if (TurnIndicatorLabel != null) {
				TurnIndicatorLabel.Dispose ();
				TurnIndicatorLabel = null;
			}

			if (WinnerLabel != null) {
				WinnerLabel.Dispose ();
				WinnerLabel = null;
			}

		}
	}
}
