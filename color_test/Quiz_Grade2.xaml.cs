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
using Windows.UI.Xaml.Shapes;
using Windows.UI;
using System.Numerics;

// 空白ページの項目テンプレートについては、https://go.microsoft.com/fwlink/?LinkId=234238 を参照してください

namespace color_test
{
    /// <summary>
    /// それ自体で使用できる空白ページまたはフレーム内に移動できる空白ページ。
    /// </summary>
    public sealed partial class Quiz_Grade2 : Page
    {
        public Quiz_Grade2()
        {
            this.InitializeComponent();
            int numOfColors = InitializeNameMap();
            InitializeRgbArray(numOfColors);
            InitializeVoctor();
            for(int i=0; i<8; i++)
            {
                showRectangle(i, i);
                InitializeText(i, i);
            }
        }

        Dictionary<int, string> nameMap = new Dictionary<int, string>();
        Dictionary<int, byte[]> rgbMap = new Dictionary<int, byte[]>();
        Vector3[] positionVector = new Vector3[8];
        TextBlock[] boxColorName = new TextBlock[8];

        private int InitializeNameMap()
        {
            nameMap.Add(0, "赤");
            nameMap.Add(1, "青");
            nameMap.Add(2, "黄色");
            nameMap.Add(3, "緑");
            nameMap.Add(4, "黄緑");
            nameMap.Add(5, "橙");
            nameMap.Add(6, "水色");
            nameMap.Add(7, "茶色");
            return nameMap.Count();
        }

        private void InitializeRgbArray(int Length)
        {
            rgbMap.Add(0, new byte[] { 200, 100, 100 });
            rgbMap.Add(1, new byte[] { 100, 100, 200 });
            rgbMap.Add(2, new byte[] { 200, 200, 100 });
            rgbMap.Add(3, new byte[] { 100, 200, 100 });
            rgbMap.Add(4, new byte[] { 150, 200, 100 });
            rgbMap.Add(5, new byte[] { 200, 120, 100 });
            rgbMap.Add(6, new byte[] { 150, 250, 150 });
            rgbMap.Add(7, new byte[] { 100, 30, 30 });
        }

        private void InitializeVoctor()
        {
            positionVector[0] = new Vector3(50.0f, 350.0f, 0.0f );
            positionVector[1] = new Vector3(303.0f, 350.0f, 0.0f);
            positionVector[2] = new Vector3(556.0f, 350.0f, 0.0f);
            positionVector[3] = new Vector3(810.0f, 350.0f, 0.0f);
            positionVector[4] = new Vector3(50.0f, 550.0f, 0.0f);
            positionVector[5] = new Vector3(303.0f, 550.0f, 0.0f);
            positionVector[6] = new Vector3(556.0f, 550.0f, 0.0f);
            positionVector[7] = new Vector3(810.0f, 550.0f, 0.0f);
        }

        private void InitializeText(int offset_color, int offset_postion)
        {
            boxColorName[offset_postion] = new TextBlock();
            boxColorName[offset_postion].Text = nameMap[offset_color];
            Vector3 positionColorName = positionVector[offset_postion];
            positionColorName += new Vector3(60.0f, 100.0f, 1.0f);
            boxColorName[offset_postion].Translation = positionColorName;
            boxColorName[offset_postion].FontSize = 20;
            layoutRoot.Children.Add(boxColorName[offset_postion]);
        }

        private void showRectangle(int offset_color, int offset_postion)
        {
            Rectangle r = new Rectangle();
            SolidColorBrush color_01 = new SolidColorBrush();
            byte[] tmp = rgbMap[offset_color];
            color_01.Color = Color.FromArgb(255, tmp[0], tmp[1], tmp[2]);
            r.Fill = color_01;
            r.Translation = positionVector[offset_postion];
            r.Width = 150;
            r.Height = 100;
            layoutRoot.Children.Add(r);
        }


        private void showOptions()
        {
            Rectangle r = new Rectangle();
            Windows.UI.Xaml.Media.SolidColorBrush color_01 = new Windows.UI.Xaml.Media.SolidColorBrush();
            color_01.Color = Color.FromArgb(255, 100, 100, 200);
            r.Fill = color_01;
            r.Translation = new Vector3(10.0f, 100.0f, 0.0f);
            r.Width = 200;
            r.Height = 100;
            layoutRoot.Children.Add(r);
        }
    }
}
