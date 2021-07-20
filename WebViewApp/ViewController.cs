using CoreText;
using Foundation;
using System;
using System.Diagnostics;
using UIKit;
using WebKit;

namespace WebViewApp
{
    public partial class ViewController : UIViewController, IWKNavigationDelegate
    {
        public ViewController(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            System.Diagnostics.Debug.WriteLine("Application Started");
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

            WebView.NavigationDelegate = this;

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

            /*
            // ロード開始時
            WebView.LoadStarted += (sender, args) => {
                // ネットワークアクティブインジケータ設定
                UIApplication.SharedApplication.NetworkActivityIndicatorVisible = true;
                // 実際のリクエスト行（リダイレクト時などは、ロードがリクエスト指定したものとは限らないため）
                var url = webView.Request.Url.AbsoluteString;
                // 入力行にコピー
                textBoxUrl.Text = url;
                // ステータス表示
                labelStatus.Text = String.Format("LoadStarted {0}", url);
                InitButton(true); // ボタン状態の初期化
            };

            // ロード完了時
            WebView.LoadFinished += (sender, args) => {
                // ネットワークアクティブインジケータ設定
                UIApplication.SharedApplication.NetworkActivityIndicatorVisible = false;
                // ステータス表示
                // labelStatus.Text = "LoadFinished";
                InitButton(false);//ボタン状態の初期化
            };

            // エラー発生時
            WebView.LoadError += (sender, e) => {
                // ネットワークアクティブインジケータ設定
                UIApplication.SharedApplication.NetworkActivityIndicatorVisible = false;
                // ステータス表示
                // labelStatus.Text = "LoadError";
                // エラーの内容をダイアログ表示する
                (new UIAlertView("ERROR", e.Error.LocalizedDescription, null, "OK", null)).Show();
                InitButton(true); // ボタン状態の初期化
            };

            // ボタン状態の初期化
            InitButton(false);
            */
        }

        // ボタン状態の初期化
        private void InitButton(bool isLoading)
        {
            ButtonBack.Enabled = WebView.CanGoBack; // 戻るボタンの有効・無効
            ButtonForward.Enabled = WebView.CanGoForward; // 進むボタンの有効・無効
            ButtonStop.Enabled = isLoading; // ロード中に有効
            ButtonRefresh.Enabled = !isLoading; // ロード中は無効
        }

        [Export("webView:didCommitNavigation:")]
        public virtual void DidCommitNavigation(WKWebView webView, WKNavigation navigation)
        {
            System.Diagnostics.Debug.WriteLine("webView:didCommitNavigation:");
        }

        [Export("webView:didFinishNavigation:")]
        public virtual void DidFinishNavigation(WKWebView webView, WKNavigation navigation)
        {
            System.Diagnostics.Debug.WriteLine("webView:didFinishNavigation:");
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