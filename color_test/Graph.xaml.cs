using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using LiveCharts;
using LiveCharts.Uwp;
using Windows.UI;
using System.Collections.Generic;


namespace color_test
{
    /// <summary>
    /// 成績をグラフで見せるページ
    /// </summary>
    public sealed partial class Graph : Page
    {
        private double[] xData = null;
        private long[] yData = null;
        public SeriesCollection seriesCollection { get; set; } = new SeriesCollection();

        public Graph()
        {
            this.InitializeComponent();
            InitializeData();
            DataContext = this;
            DrawGraph();
        }

        /// <summary>
        /// グラフに使用するデータを初期化する
        /// </summary>
        private void InitializeData()
        {
            ScoreData scoreData = new ScoreData();
            string[] scores = scoreData.GetScore();

            int lengthOfScore = scoreData.GetLengthOfScore();
            xData = new double[lengthOfScore];
            yData = new long[lengthOfScore];

            for (int i=0; i< lengthOfScore; i++)
            {
                if(scores[i] != null)
                {
                    yData[i] = long.Parse(scores[i]);
                    xData[i] = i + 1;
                }
                else
                {
                    break;
                }
            }
        }

        /// <summary>
        /// グラフを描く
        /// </summary>
        private void DrawGraph()
        {
            seriesCollection.Clear();
            seriesCollection.Add(
                new LineSeries
                {
                    //凡例名
                    Title = "直近20回までの正解数",
                    //系列値
                    Values = new ChartValues<long>(),
                    //線の色（省略：自動で配色されます）
                    Stroke = new Windows.UI.Xaml.Media.SolidColorBrush(Colors.DarkSlateBlue),
                    //直線のスムージング
                    LineSmoothness = 0,
                });

            LC_Graph.LegendLocation = LegendLocation.Top;

            //軸の設定
            ScoreData scoreData = new ScoreData();
            int maxLengthOfScore = scoreData.GetLengthOfStorage();
            LC_Graph.AxisX.Clear();
            LC_Graph.AxisY.Clear();
            LC_Graph.AxisX.Add(new Axis { Title = "受験回数", FontSize = 20 ,MinValue=1, MaxValue = maxLengthOfScore});
            LC_Graph.AxisY.Add(new Axis { Title = "正解数", FontSize = 20, MinValue = 0, MaxValue = 61});

            for (int i = 0; i < seriesCollection.Count; i++) 
            {
                seriesCollection[i].Values.Clear();
                seriesCollection[i].Values.Add(0L);
                for (int points = 0; points < yData.Length; points++)
                {
                    seriesCollection[i].Values.Add(yData[points]);
                }
            }
        }

        private void btn_ReturnToMain_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage));
        }
    }
}
