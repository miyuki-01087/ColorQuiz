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
using Windows.System;

// 空白ページの項目テンプレートについては、https://go.microsoft.com/fwlink/?LinkId=234238 を参照してください

namespace color_test
{
    /// <summary>
    /// 色彩検定二級のクイズを提供するクラス
    /// </summary>
    public sealed partial class Quiz_Grade2 : Page
    {
        private const int NUM_OPTIONS = 8;
        private ColorDataG2 colordata;
        private VectorData vectordata;
        private int[] optionsArray = new int[NUM_OPTIONS];
        private int quizCounter = 0;
        private int correctCounter = 0;
        TextBlock answerStatusBlock = new TextBlock();
        TextBlock answerColorNameBlock = new TextBlock();
        private int counterNotDeletedPerAnswer = 0;
        private RadioButton checkedButton;

        public Quiz_Grade2()
        {
            this.InitializeComponent();
            InitializeForQuiz();
            ShowQuiz(quizCounter); // 1問目を出題
        }

        /// <summary>
        /// 選択肢を初期化して、問題文と答えをする
        /// </summary>
        private void InitializeForQuiz()
        {
            colordata = new ColorDataG2(NUM_OPTIONS);
            vectordata = new VectorData(NUM_OPTIONS);
            InitializeQuizDescription();
            InitializeRadioButton();
            InitializeAnswerStatus();
            InitializeColorNameOfQuiz(quizCounter);
        }

        /// <summary>
        /// 長方形と色名を表示する
        /// </summary>
        /// <param name="answerNum">クイズの答え</param>
        private void ShowQuiz(int answerNum)
        {
            InitializeOptionsArray(optionsArray);
            // はじめに正解の選択肢を配列に入れる
            int answerPosition = SetAnswerTooptionsArray(quizCounter);
            for (int i = 0; i < NUM_OPTIONS; i++)
            {
                // ダミーの選択肢を配列に入れる
                if(i != answerPosition)
                {
                    optionsArray[i] = GetRandomColorOffset();
                }
                ShowRectangle(i, optionsArray[i]);
            }
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
        /// 回答状況の説明文を初期化する
        /// </summary>
        private void InitializeAnswerStatus()
        {
            answerStatusBlock.Text += "回答状況：";
            answerStatusBlock.Text += colordata.GetnameMapLength()+"問中 "+(quizCounter+1) + "問目";
            answerStatusBlock.Text += "　"+correctCounter+"問正解";
            answerStatusBlock.FontSize = 25;
            answerStatusBlock.Translation = vectordata.GetpositionVectorForAnswerStatus();
            layoutRoot.Children.Add(answerStatusBlock);
            counterNotDeletedPerAnswer++;
        }

        private int SetAnswerTooptionsArray(int quizCounter)
        {
            Random rand = new Random();
            int answerPosition = rand.Next(NUM_OPTIONS);
            optionsArray[answerPosition] = quizCounter;
            return answerPosition;
        }

        /// <summary>
        /// 0以上arrayLength未満で引数の配列内で重複しない値を返す
        /// </summary>
        /// <returns>選択肢の番号</returns>
        private int GetRandomColorOffset()
        {
            Random rand = new Random();
            int nextOption;
            bool isDuplicated;
            do
            {
                isDuplicated = false;
                int startOfColors = colordata.GetCurrentStartOfColors(quizCounter);
                int endOfColors = colordata.GetCurrentEndOfColors(quizCounter);
                nextOption = rand.Next(startOfColors, endOfColors+1);
                for (int i = 0; i < NUM_OPTIONS; i++)
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
        private void ShowColorNameDescription()
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
                RadioButton button = new RadioButton();
                Vector3 positionRectangle = vectordata.GetpositionVectorForRectangle(i);
                button.Translation = positionRectangle + new Vector3(65.0f, -35.0f, 1.0f);
                button.GroupName = "Options";
                button.Content = (i+1);
                button.FontSize = 20;
                button.FontFamily = new FontFamily("Global Serif");
                button.Click += OnClickRadioButton;
                layoutRoot.Children.Add(button);
                counterNotDeletedPerAnswer++;
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
            counterNotDeletedPerAnswer++;
        }

        /// <summary>
        /// 答えを表示する
        /// </summary>
        /// <param name="answerNum">答えの番号</param>
        private void InitializeColorNameOfQuiz(int answerNum)
        {
            answerColorNameBlock.Text = "(" + (answerNum + 1) + ")";
            answerColorNameBlock.Text += colordata.GetnameMapValue(answerNum);
            answerColorNameBlock.FontSize = 27;
            answerColorNameBlock.Translation = vectordata.GetpositionVectorForAnswerColorName();
            if(!layoutRoot.Children.Contains(answerColorNameBlock)){
                layoutRoot.Children.Add(answerColorNameBlock);
            }
            counterNotDeletedPerAnswer++;
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

        /// <summary>
        /// クイズに答えた際の正誤判定および画面の初期化を行う
        /// </summary>
        /// <param name="answerNum">クイズの答えを示すint型</param>
        private void AnswerQuiz(int answerNum)
        {
            bool isCorrect = CheckAnswer(answerNum);
            ShowisCorrected(isCorrect);
            UpdatecorrectCounter(isCorrect);
            UpdateAnswerStatus();
            ShowColorNameDescription();
            quizCounter++;
        }

        /// <summary>
        /// 回答状況の説明文を更新する
        /// </summary>
        private void UpdateAnswerStatus()
        {
            answerStatusBlock.Text = "回答状況：";
            answerStatusBlock.Text += colordata.GetnameMapLength() + "問中 " + (quizCounter + 1) + "問目";
            answerStatusBlock.Text += "　" + correctCounter + "問正解";
        }

        /// <summary>
        /// 問題に正解したなら、正解カウンターをインクリメントする
        /// </summary>
        /// <param name="isCorrect">問題に正解したかどうか</param>
        private void UpdatecorrectCounter(bool isCorrect)
        {
            if (isCorrect)
            {
                correctCounter++;
            }
        }

        /// <summary>
        /// 正解か不正解かを判定する
        /// </summary>
        /// <param name="answerNum">クイズの答えを示すint型</param>
        /// <returns>正解ならtrue、不正解ならfalseを返す</returns>
        private bool CheckAnswer(int answerNum)
        {
            bool isCorrected = false;
            if(quizCounter == optionsArray[answerNum])
            {
                isCorrected = true;
            }
            return isCorrected;
        }

        /// <summary>
        /// 正解か不正解かを文字列で表示する
        /// </summary>
        /// <param name="isCorrect">正解か不正解かを表すbool型</param>
        private void ShowisCorrected(bool isCorrect)
        {
            if (isCorrect)
            {
                answerColorNameBlock.Text += "　正解！";
            }
            else
            {
                answerColorNameBlock.Text += "　不正解！";
            }
            answerColorNameBlock.Text += "(エンターキーで次の問題を表示)";
        }

        /// <summary>
        /// 次の問題を表示するために長方形および色名と正解の色名を削除する
        /// </summary>
        private void DeleteComponents()
        {
            int panelLength = layoutRoot.Children.Count;
            for (int i = panelLength - 1; i >= counterNotDeletedPerAnswer; i--)
            {
                layoutRoot.Children.RemoveAt(i);
            }
        }

        /// <summary>
        /// ラジオボタンをクリックしたときに呼ばれる
        /// </summary>
        /// <param name="sender">イベントハンドラがアタッチされたオブジェクト</param>
        /// <param name="e">イベントのデータ</param>
        private void OnClickRadioButton(object sender, RoutedEventArgs e)
        {
            RadioButton radioButton = (RadioButton)sender;
            checkedButton = radioButton;
            int answerNum = (int)radioButton.Content - 1;
            AnswerQuiz(answerNum);
        }

        /// <summary>
        /// キー入力を処理する
        /// </summary>
        /// <param name="sender">イベントハンドラがアタッチされたオブジェクト</param>
        /// <param name="e">イベントのデータ</param>
        public void OnDownAnyKey(object sender, KeyRoutedEventArgs e)
        {
            switch (e.Key)
            {
                case VirtualKey.Enter:
                    DeleteComponents();
                    checkedButton.IsChecked = false;
                    InitializeColorNameOfQuiz(quizCounter);
                    UpdateAnswerStatus();
                    ShowQuiz(quizCounter); // 1問目を出題
                    break;
                default:
                    break;
            }
        }
    }
}
