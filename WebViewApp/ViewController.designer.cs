// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace WebViewApp
{
	[Register ("ViewController")]
	partial class ViewController
	{
		[Outlet]
		UIKit.UIButton ButtonBack { get; set; }

		[Outlet]
		UIKit.UIButton ButtonForward { get; set; }

		[Outlet]
		UIKit.UIButton ButtonLoad { get; set; }

		[Outlet]
		UIKit.UIButton ButtonRefresh { get; set; }

		[Outlet]
		UIKit.UIButton ButtonStop { get; set; }

		[Outlet]
		UIKit.UIButton ButtonTimer { get; set; }

		[Outlet]
		UIKit.UITextField TextFieldUrl { get; set; }

		[Outlet]
		WebKit.WKWebView WebView { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (ButtonBack != null) {
				ButtonBack.Dispose ();
				ButtonBack = null;
			}

			if (ButtonForward != null) {
				ButtonForward.Dispose ();
				ButtonForward = null;
			}

			if (ButtonLoad != null) {
				ButtonLoad.Dispose ();
				ButtonLoad = null;
			}

			if (ButtonTimer != null) {
				ButtonTimer.Dispose ();
				ButtonTimer = null;
			}

			if (ButtonRefresh != null) {
				ButtonRefresh.Dispose ();
				ButtonRefresh = null;
			}

			if (ButtonStop != null) {
				ButtonStop.Dispose ();
				ButtonStop = null;
			}

			if (TextFieldUrl != null) {
				TextFieldUrl.Dispose ();
				TextFieldUrl = null;
			}

			if (WebView != null) {
				WebView.Dispose ();
				WebView = null;
			}
		}
	}
}
