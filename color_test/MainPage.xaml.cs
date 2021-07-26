using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.ViewManagement;


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
