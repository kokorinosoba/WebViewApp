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
        Stopwatch Stopwatch;
        bool IsTimerActive;
        bool IsLoading;

        public ViewController(IntPtr handle) : base(handle)
        {
            Stopwatch = new Stopwatch();
            IsTimerActive = true;
            IsLoading = false;
        }

        public override void ViewDidLoad()
        {
            Debug.WriteLine("Application Started");
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

            ButtonTimer.TouchUpInside += delegate
            {
                ButtonTimer_TouchUpInside();
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

            // ボタン状態の初期化
            IsLoading = false;
            InitButton();
        }

        // ボタン状態の初期化
        private void InitButton()
        {
            ButtonBack.Enabled = WebView.CanGoBack; // 戻るボタンの有効・無効
            ButtonForward.Enabled = WebView.CanGoForward; // 進むボタンの有効・無効
            ButtonStop.Enabled = IsLoading; // ロード中に有効
            ButtonRefresh.Enabled = !IsLoading; // ロード中は無効
        }

        private void ButtonTimer_TouchUpInside()
        {
            if (IsLoading)
            {
                var alert = UIAlertController.Create("注意", "Webサイトの読み込み中は計測ボタンを操作できません。", UIAlertControllerStyle.Alert);
                alert.AddAction(UIAlertAction.Create("OK", UIAlertActionStyle.Default, null));
                PresentViewController(alert, animated: true, completionHandler: null);
                return;
            }
            IsTimerActive = !IsTimerActive;
            ButtonTimer.SetTitleColor(IsTimerActive ? UIColor.Red : UIColor.White, UIControlState.Normal);
        }

        [Export("webView:didCommitNavigation:")]
        public virtual void DidCommitNavigation(WKWebView webView, WKNavigation navigation)
        {
            Debug.WriteLine("webView:didCommitNavigation:");

            // ネットワークアクティブインジケータ設定
            UIApplication.SharedApplication.NetworkActivityIndicatorVisible = true;

            // 実際のリクエスト行（リダイレクト時などは、ロードがリクエスト指定したものとは限らないため）
            var url = WebView.Url.AbsoluteString;
            // 入力行にコピー
            TextFieldUrl.Text = url;

            // ステータス表示
            // labelStatus.Text = String.Format("LoadStarted {0}", url);
            IsLoading = true;
            InitButton(); // ボタン状態の初期化

            if (IsTimerActive)
            {
                Stopwatch.Restart();
            }
        }

        [Export("webView:didFinishNavigation:")]
        public virtual void DidFinishNavigation(WKWebView webView, WKNavigation navigation)
        {
            Debug.WriteLine("webView:didFinishNavigation:");

            // ネットワークアクティブインジケータ設定
            UIApplication.SharedApplication.NetworkActivityIndicatorVisible = false;

            if (IsTimerActive)
            {
                Stopwatch.Stop();

                // 読み込みにかかった時間を表示
                TimeSpan timeSpan = Stopwatch.Elapsed;
                string loadTimeDescription = $"{timeSpan.Hours} 時間 {timeSpan.Minutes} 分 {timeSpan.Seconds} 秒 {timeSpan.Milliseconds} ミリ秒";
                Debug.WriteLine(loadTimeDescription);

                var alert = UIAlertController.Create("読み込み時間", $"読み込みにかかった時間は\n{loadTimeDescription}\nでした。", UIAlertControllerStyle.Alert);
                alert.AddAction(UIAlertAction.Create("OK", UIAlertActionStyle.Default, null));
                PresentViewController(alert, animated: true, completionHandler: null);
            }

            // ステータス表示
            // labelStatus.Text = "LoadFinished";
            IsLoading = false;
            InitButton();//ボタン状態の初期化
        }

        [Export("webView:didFailNavigation:withError:")]
        public virtual void DidFailNavigation(WKWebView webView, WKNavigation navigation, NSError error)
        {
            Debug.WriteLine("webView:didFailNavigation:withError:");

            // ネットワークアクティブインジケータ設定
            UIApplication.SharedApplication.NetworkActivityIndicatorVisible = false;

            // エラーの内容をダイアログ表示する
            var alert = UIAlertController.Create("エラー", error.LocalizedDescription, UIAlertControllerStyle.Alert);
            alert.AddAction(UIAlertAction.Create("OK", UIAlertActionStyle.Default, null));
            PresentViewController(alert, animated: true, completionHandler: null);

            if (IsTimerActive)
            {
                Stopwatch.Stop();
            }

            // ステータス表示
            // labelStatus.Text = "LoadError";
            IsLoading = false;
            InitButton(); // ボタン状態の初期化
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