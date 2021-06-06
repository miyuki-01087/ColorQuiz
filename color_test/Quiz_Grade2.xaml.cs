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

        private const int NUM_OPTIONS = 8;
        public Quiz_Grade2()
        {
            this.InitializeComponent();
            int numOfColors = InitializeNameMap();
            InitializeRgbArray(numOfColors);
            InitializeVoctor();
            for(int i=0; i< NUM_OPTIONS; i++)
            {
                showRectangle();
                InitializeColorNameDescription();
                InitializeRadioButton();
            }
        }

        Dictionary<int, string> nameMap = new Dictionary<int, string>();
        Dictionary<int, byte[]> rgbMap = new Dictionary<int, byte[]>();
        Vector3[] positionVectorForRectangle = new Vector3[NUM_OPTIONS];
        TextBlock[] boxColorName = new TextBlock[NUM_OPTIONS];
        RadioButton[] optionButtons = new RadioButton[NUM_OPTIONS];

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
            positionVectorForRectangle[0] = new Vector3(50.0f, 350.0f, 0.0f );
            positionVectorForRectangle[1] = new Vector3(303.0f, 350.0f, 0.0f);
            positionVectorForRectangle[2] = new Vector3(556.0f, 350.0f, 0.0f);
            positionVectorForRectangle[3] = new Vector3(810.0f, 350.0f, 0.0f);
            positionVectorForRectangle[4] = new Vector3(50.0f, 550.0f, 0.0f);
            positionVectorForRectangle[5] = new Vector3(303.0f, 550.0f, 0.0f);
            positionVectorForRectangle[6] = new Vector3(556.0f, 550.0f, 0.0f);
            positionVectorForRectangle[7] = new Vector3(810.0f, 550.0f, 0.0f);
        }

        private void InitializeColorNameDescription()
        {
            for (int i = 0; i < NUM_OPTIONS; i++)
            {
                boxColorName[i] = new TextBlock();
                boxColorName[i].Text = nameMap[i];
                Vector3 positionRectangle = positionVectorForRectangle[i];
                boxColorName[i].Translation = positionRectangle + new Vector3(60.0f, 100.0f, 1.0f);
                boxColorName[i].FontSize = 20;
                layoutRoot.Children.Add(boxColorName[i]);
            }
        }

        private void InitializeRadioButton()
        {
            for(int i=0; i<NUM_OPTIONS; i++)
            {
                optionButtons[i] = new RadioButton();
                Vector3 positionRectangle = positionVectorForRectangle[i];
                optionButtons[i].Translation = positionRectangle + new Vector3(65.0f, -35.0f, 1.0f);
                optionButtons[i].GroupName = "Options";
                optionButtons[i].Content = (i+1);
                optionButtons[i].FontSize = 20;
                optionButtons[i].FontFamily = new FontFamily("Global Serif");
                layoutRoot.Children.Add(optionButtons[i]);
            }
        }

        private void showRectangle()
        {
            for (int i = 0; i < NUM_OPTIONS; i++)
            {
                Rectangle optionRectangle = new Rectangle();
                SolidColorBrush color_01 = new SolidColorBrush();
                optionRectangle.Fill = color_01;
                byte[] tmp = rgbMap[i];
                color_01.Color = Color.FromArgb(255, tmp[0], tmp[1], tmp[2]);
                optionRectangle.Translation = positionVectorForRectangle[i];
                optionRectangle.Width = 150;
                optionRectangle.Height = 100;
                layoutRoot.Children.Add(optionRectangle);
            }
        }
    }
}
