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
using LiveCharts;
using LiveCharts.Uwp;

// 空白ページの項目テンプレートについては、https://go.microsoft.com/fwlink/?LinkId=234238 を参照してください

namespace color_test
{
    /// <summary>
    /// それ自体で使用できる空白ページまたはフレーム内に移動できる空白ページ。
    /// </summary>
    public sealed partial class Graph : Page
    {
        private double[] xData = null;
        private long[] yData = null;
        public SeriesCollection Sc { get; set; } = new SeriesCollection();

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
            int maxLengthOfScore = scoreData.GetLengthOfStorage();

            int lengthOfScore = scoreData.GetLengthOfScore();
            xData = new double[lengthOfScore];
            yData = new long[lengthOfScore];

            for (int i=0; i<maxLengthOfScore; i++)
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

        private void DrawGraph()
        {
            Sc.Clear();
            Sc.Add(
                new LineSeries                     //折れ線グラフ
                {
                    //凡例名
                    Title = "折れ線",
                    //系列値
                    Values = new ChartValues<long>(),
                    //線の色（省略：自動で配色されます）
                    Stroke = new Windows.UI.Xaml.Media.SolidColorBrush(Windows.UI.Colors.Red),
                    //直線のスムージング（0：なし、省略：あり）
                    //LineSmoothness = 0,

                });


            /////////////////////////////////////
            //ステップ２:LiveChartの設定
            /////////////////////////////////////
            //凡例の表示位置
            LC_Graph.LegendLocation = LegendLocation.Right;

            //軸の設定
            LC_Graph.AxisX.Clear();     //デフォルトで設定されている軸をクリア
            LC_Graph.AxisX.Add(new Axis { Title = "横軸", FontSize = 20 });
            LC_Graph.AxisY.Clear();
            LC_Graph.AxisY.Add(new Axis { Title = "縦軸", FontSize = 20, MinValue = 0 });


            /////////////////////////////////////
            //ステップ３:値をランダムに追加
            /////////////////////////////////////

            //各系列に、それぞれ値を代入
            for (int iSeries = 0; iSeries < Sc.Count; iSeries++)
            {
                Sc[iSeries].Values.Clear();

                //20点だけ追加
                for (int points = 0; points < 5; ++points)
                {
                    //乱数で数値を代入
                    Sc[iSeries].Values.Add(yData[points]);
                }
            }
        }
    }
}
