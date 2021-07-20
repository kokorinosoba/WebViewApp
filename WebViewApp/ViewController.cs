using CoreText;
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

            TextFieldUrl.Layer.BorderColor = UIColor.LightGray.CGColor;
            TextFieldUrl.Layer.BorderWidth = 1;
            TextFieldUrl.Layer.CornerRadius = 20;
            TextFieldUrl.Layer.BackgroundColor = UIColor.LightGray.CGColor;
            TextFieldUrl.ClipsToBounds = true;
            TextFieldUrl.AttributedPlaceholder = new NSAttributedString(TextFieldUrl.Placeholder, null, UIColor.Gray);
            TextFieldUrl.LeftView = new UIView(frame: new CoreGraphics.CGRect(0, 0, 10, TextFieldUrl.Frame.Size.Height));
            TextFieldUrl.LeftViewMode = UITextFieldViewMode.Always;


            var url = new NSUrl("https://www.photoback.jp/");
            WebView.LoadRequest(new NSUrlRequest(url));

            ButtonBack.TouchUpInside += delegate
            {
                WebView.GoBack();
            };

            ButtonForward.TouchUpInside += delegate
            {
                WebView.GoForward();
            };

            ButtonRefresh.TouchUpInside += delegate
            {
                WebView.Reload();
            };

            ButtonStop.TouchUpInside += delegate
            {
                WebView.StopLoading();
            };

            ButtonLoad.TouchUpInside += delegate
            {
                if (Uri.IsWellFormedUriString(TextFieldUrl.Text, UriKind.Absolute))
                {
                    WebView.LoadRequest(new NSUrlRequest(new NSUrl(TextFieldUrl.Text)));
                }
                else
                {
                    var alert = UIAlertController.Create("注意", "URLが間違っています。正しい形式のURLを入力して下さい。", UIAlertControllerStyle.Alert);
                    alert.AddAction(UIAlertAction.Create("OK", UIAlertActionStyle.Default, null));
                    PresentViewController(alert, animated: true, completionHandler: null);
                }
            };
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