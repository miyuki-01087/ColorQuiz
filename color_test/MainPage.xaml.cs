using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.ViewManagement;


// 空白ページの項目テンプレートについては、https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x411 を参照してください

namespace color_test
{
    /// <summary>
    /// それ自体で使用できる空白ページまたはフレーム内に移動できる空白ページ。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private const int formWidth = 1010;
        private const int formHeight = 700;
        public MainPage()
        {
            this.InitializeComponent();
            SetFormSize();
        }

        /// <summary>
        /// フォームの大きさを設定する
        /// </summary>
        private void SetFormSize()
        {
            ApplicationView.PreferredLaunchViewSize = new Size(formWidth, formHeight);
            ApplicationView.PreferredLaunchWindowingMode = ApplicationViewWindowingMode.PreferredLaunchViewSize;
        }

        private void btn_G2_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Quiz_Grade2));
        }
    }
}
