using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Shapes;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Composition;
using Windows.UI.ViewManagement;
using System.Numerics;


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
            DrawBackGround();
            
        }

        /// <summary>
        /// フォームの大きさを設定する
        /// </summary>
        private void SetFormSize()
        {
            ApplicationView.PreferredLaunchViewSize = new Size(formWidth, formHeight);
            ApplicationView.PreferredLaunchWindowingMode = ApplicationViewWindowingMode.PreferredLaunchViewSize;
        }

        private void DrawBackGround()
        {
            DrawGradientRectangle();
        }

        private void DrawGradientRectangle()
        {
            LinearGradientBrush gradientBrush = new LinearGradientBrush();
            Border border = new Border();

            GradientStop gradientStopTop = new GradientStop();
            gradientStopTop.Color = Colors.Red;
            gradientStopTop.Offset = 0.0;
            gradientBrush.GradientStops.Add(gradientStopTop);

            GradientStop gradientStopBottom = new GradientStop();
            gradientStopBottom.Color = Colors.Yellow;
            gradientStopBottom.Offset = 1.0;
            gradientBrush.GradientStops.Add(gradientStopBottom);

            border.Background = gradientBrush;

        }

        private void btn_G2_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Quiz_Grade2));
        }

        private void btn_Graph_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Graph));
        }

        private void btn_DeleteScore_Click(object sender, RoutedEventArgs e)
        {
            ScoreData scoreData = new ScoreData();
            scoreData.DeleteScores();
        }
    }
}
