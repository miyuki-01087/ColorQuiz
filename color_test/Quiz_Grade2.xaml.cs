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
using System.Windows;

// 空白ページの項目テンプレートについては、https://go.microsoft.com/fwlink/?LinkId=234238 を参照してください

namespace color_test
{
    /// <summary>
    /// 色彩検定二級のクイズを提供するクラス
    /// </summary>
    public sealed partial class Quiz_Grade2 : Page
    {
        private const int NUM_OPTIONS = 8;
        ColorDataG2 colordata;
        VectorData vectordata;
        int quizCounter = 0;

        public Quiz_Grade2()
        {
            this.InitializeComponent();
            InitializeForQuiz();
            ShowQuiz(0); // 1問目を出題
        }

        /// <summary>
        /// ボタンと選択肢を初期化する
        /// </summary>
        private void InitializeForQuiz()
        {
            colordata = new ColorDataG2(NUM_OPTIONS);
            vectordata = new VectorData(NUM_OPTIONS);
            int numOfColors = colordata.GetnameMapLength();
            InitializeRadioButton();
            InitializeQuizDescription();
            InitializeColorNameOfQuiz(0);
        }

        /// <summary>
        /// 長方形と色名を表示する
        /// </summary>
        /// <param name="answerNum">この問いの答え</param>
        private void ShowQuiz(int answerNum)
        {
            int[] optionsArray = new int[NUM_OPTIONS];
            InitializeOptionsArray(optionsArray);
            for (int i = 0; i < NUM_OPTIONS; i++)
            {
                optionsArray[i] = GetRandomColorOffset(optionsArray);
                ShowRectangle(i, optionsArray[i]);
            }
            ShowColorNameDescription(optionsArray);
        }

        /// <summary>
        /// 与えられた配列のすべての要素を-1で初期化する
        /// </summary>
        /// <param name="array">初期化対象の配列</param>
        private void InitializeOptionsArray(int[] array)
        {
            for(int i=0; i< array.Length; i++)
            {
                array[i] = -1;
            }
        }

        /// <summary>
        /// 0以上arrayLength未満で引数の配列内で重複しない値を返す
        /// </summary>
        /// <param name="optionsArray">選択肢の番号を格納した配列</param>
        /// <returns>選択肢の番号</returns>
        private int GetRandomColorOffset(int[] optionsArray)
        {
            Random rand = new Random();
            int nextOption;
            bool isDuplicated;
            do
            {
                isDuplicated = false;
                nextOption = rand.Next(NUM_OPTIONS);
                for (int i = 0; i < optionsArray.Length; i++)
                {
                    if (optionsArray[i] == nextOption)
                    {
                        isDuplicated = true;
                    }
                }
            } while (isDuplicated);
                return nextOption;
        }

        /// <summary>
        /// 色名のキャプションを表示する
        /// </summary>
        /// <param name="optionsArray">選択肢を格納した配列</param>
        private void ShowColorNameDescription(int[] optionsArray)
        {
            for (int i = 0; i < NUM_OPTIONS; i++)
            {
                int colorOffset = optionsArray[i];
                TextBlock textBlock = new TextBlock();
                textBlock.Text = colordata.GetnameMapValue(colorOffset);
                Vector3 positionRectangle = vectordata.GetpositionVectorForRectangle(i);
                textBlock.Translation = positionRectangle + new Vector3(60.0f, 100.0f, 1.0f);
                textBlock.FontSize = 20;
                layoutRoot.Children.Add(textBlock);
            }
        }

        /// <summary>
        /// ラジオボタンと1~8の連番を表示する
        /// 位置は長方形の位置から相対的に決める
        /// </summary>
        private void InitializeRadioButton()
        {
            for (int i=0; i<NUM_OPTIONS; i++)
            {
                RadioButton buttun = new RadioButton();
                Vector3 positionRectangle = vectordata.GetpositionVectorForRectangle(i);
                buttun.Translation = positionRectangle + new Vector3(65.0f, -35.0f, 1.0f);
                buttun.GroupName = "Options";
                buttun.Content = (i+1);
                buttun.FontSize = 20;
                buttun.FontFamily = new FontFamily("Global Serif");
                layoutRoot.Children.Add(buttun);
            }
        }

        /// <summary>
        /// 問題文を初期化して表示する
        /// </summary>
        private void InitializeQuizDescription()
        {
            TextBlock descriptionBlock = new TextBlock();
            descriptionBlock.Text = "以下の選択肢から、色名に合致するものを選択してください。";
            descriptionBlock.FontSize = 25;
            descriptionBlock.Translation = vectordata.GetpositionVectorForDescription();
            layoutRoot.Children.Add(descriptionBlock);
        }

        /// <summary>
        /// 答えを表示する
        /// </summary>
        /// <param name="answerNum">答えの番号</param>
        private void InitializeColorNameOfQuiz(int answerNum)
        {
            TextBlock answerColorNameBlock = new TextBlock();
            answerColorNameBlock.Text = "(" + (answerNum + 1) + ")";
            answerColorNameBlock.Text += colordata.GetnameMapValue(answerNum);
            answerColorNameBlock.FontSize = 30;
            answerColorNameBlock.Translation = vectordata.GetVectorForAnswerColorName();
            layoutRoot.Children.Add(answerColorNameBlock);
        }

        /// <summary>
        /// 長方形を表示する。長方形の色オフセットと位置オフセットを引数にとる。
        /// </summary>
        /// <param name="colorOffset">選択肢の番号</param>
        /// <param name="optionOffset">色を示す番号</param>
        private void ShowRectangle(int optionOffset, int colorOffset)
        {
            Rectangle optionRectangle = new Rectangle();
            SolidColorBrush color_01 = new SolidColorBrush();
            optionRectangle.Fill = color_01;
            byte[] tmp = colordata.GetrgbMapValue(colorOffset);
            color_01.Color = Color.FromArgb(255, tmp[0], tmp[1], tmp[2]);
            optionRectangle.Translation = vectordata.GetpositionVectorForRectangle(optionOffset);
            optionRectangle.Width = 150;
            optionRectangle.Height = 100;
            layoutRoot.Children.Add(optionRectangle);        
        }
    }
}
