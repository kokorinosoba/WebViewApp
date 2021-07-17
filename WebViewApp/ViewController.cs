using Foundation;
using System;
using UIKit;
using WebKit;

namespace WebViewApp
{
    public partial class ViewController : UIViewController
    {
        public ViewController(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            // WKWebView webView = new WKWebView(View.Frame, new WKWebViewConfiguration());
            // View.AddSubview(webView);

            var url = new NSUrl("https://www.google.co.jp/");
            WebView.LoadRequest(new NSUrlRequest(url));
        }

        public override void ViewDidLayoutSubviews()
        {
            base.ViewDidLayoutSubviews();
            // WebView.Frame = View.Bounds;
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }
    }
}