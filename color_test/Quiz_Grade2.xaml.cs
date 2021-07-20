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
using Windows.Media.Core;


namespace color_test
{
    /// <summary>
    /// 色彩検定二級のクイズを提供するクラス
    /// </summary>
    public sealed partial class Quiz_Grade2 : Page
    {
        private const int formWidth = 1010;
        private const int formHeight = 700;

        private const int NUM_OPTIONS = 8;
        private bool isAnsweringNow = true;
        private ColorDataG2 colorData;
        private VectorData vectorData;
        private int[] optionsArray = new int[NUM_OPTIONS];
        private RadioButton[] radioButtons = new RadioButton[NUM_OPTIONS];
        private int quizCounter = 0;
        private int correctCounter = 0;
        private int answerNum = 0;
        private TextBlock answerStatusBlock = new TextBlock();
        private int counterNotDeletedPerAnswer = 0;
        private RadioButton checkedButton;

        public Quiz_Grade2()
        {
            this.InitializeComponent();
            InitializeForQuiz();
            CallShowTriangle();
            ShowColorGenre();
            ShowQuiz(); // 1問目を出題
        }
        
        /// <summary>
        /// 選択肢を初期化して、問題文と答えをする
        /// </summary>
        private void InitializeForQuiz()
        {
            colorData = new ColorDataG2(NUM_OPTIONS);
            vectorData = new VectorData(NUM_OPTIONS);
            InitializeQuizDescription();
            InitializeUnderLines();
            InitializeRadioButton();
            InitializeAnswerStatus();
            InitializeColorNameOfQuiz(quizCounter);
        }

        /// <summary>
        /// 長方形と色名を表示する
        /// </summary>
        /// <param name="answerNum">クイズの答え</param>
        private void ShowQuiz()
        {
            InitializeOptionsArray(optionsArray);
            // はじめに正解の選択肢を配列に入れる
            int answerPosition = SetAnswerTooptionsArray(quizCounter);
            answerNum = answerPosition;
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
            answerStatusBlock.Text += colorData.GetnameMapLength()+"問中 "+(quizCounter+1) + "問目";
            answerStatusBlock.Text += "　"+correctCounter+"問正解";
            answerStatusBlock.FontSize = 25;
            answerStatusBlock.Translation = vectorData.GetpositionVectorForAnswerStatus();
            layoutRoot.Children.Add(answerStatusBlock);
            counterNotDeletedPerAnswer++;
        }

        /// <summary>
        /// 選択肢の配列の中のランダムな位置に正解の番号を代入する
        /// </summary>
        /// <param name="quizCounter">正解の番号</param>
        /// <returns>選択肢の配列内での正解の位置</returns>
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
                int startOfColors = colorData.GetCurrentStartOfColors(quizCounter);
                int endOfColors = colorData.GetCurrentEndOfColors(quizCounter);
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
                textBlock.Text = colorData.GetnameMapValue(colorOffset);
                Vector3 positionRectangle = vectorData.GetpositionVectorForRectangle(i);
                textBlock.Translation = positionRectangle + new Vector3(-20.0f, 105.0f, 1.0f);
                textBlock.Width = 190;
                textBlock.TextAlignment = TextAlignment.Center;
                textBlock.FontSize = 18;
                if(i == answerNum) // 答えのキャプションだけ赤い字にする
                {
                    SolidColorBrush solidBrush = new SolidColorBrush();
                    solidBrush.Color = Colors.Red;
                    textBlock.Foreground = solidBrush;
                }
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
                Vector3 positionRectangle = vectorData.GetpositionVectorForRectangle(i);
                button.Translation = positionRectangle + new Vector3(65.0f, -35.0f, 1.0f);
                button.GroupName = "Options";
                button.Content = (i+1);
                button.FontSize = 20;
                button.FontFamily = new FontFamily("Global Serif");
                button.Click += OnClickRadioButton;
                layoutRoot.Children.Add(button);
                counterNotDeletedPerAnswer++;
                radioButtons[i] = button;
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
            descriptionBlock.Translation = vectorData.GetpositionVectorForDescription();
            layoutRoot.Children.Add(descriptionBlock);
            counterNotDeletedPerAnswer++;
        }

        /// <summary>
        /// 答えを表示する
        /// </summary>
        /// <param name="answerNum">答えの番号</param>
        private void InitializeColorNameOfQuiz(int answerNum)
        {
            TextBlock answerColorNameBlock = new TextBlock();
            answerColorNameBlock.Text = "(" + (answerNum + 1) + ")";
            string colorNameIncludesNewLine = colorData.GetnameMapValue(answerNum);
            answerColorNameBlock.Text += colorNameIncludesNewLine.Replace("\n", "");
            answerColorNameBlock.Translation = vectorData.GetpositionVectorForAnswerColorName();
            // 初めに呼ばれたときだけ、RelativePanelに追加する
            if (!layoutRoot.Children.Contains(answerColorNameBlock)) {
                answerColorNameBlock.FontSize = 30;
                layoutRoot.Children.Add(answerColorNameBlock);
            }
        }

        /// <summary>
        /// 回答状況と問題文に下線を追加する
        /// </summary>
        private void InitializeUnderLines()
        {
            ShowGrayLines(vectorData.GetpositionVectorForAnswerStatus());
            ShowGrayLines(vectorData.GetpositionVectorForDescription());
        }

        /// <summary>
        /// 与えられた位置ベクトルを基に、横幅800の線を引く
        /// </summary>
        /// <param name="parentPosition">下線を引きたい文章の位置ベクトル</param>
        private void ShowGrayLines(Vector3 parentPosition)
        {
            Line line = new Line();
            line.Stroke = new SolidColorBrush(Colors.DarkSlateGray);
            line.StrokeThickness = 2;
            line.Y1 = line.Y2 = parentPosition.Y + 40.0f;
            line.X1 = parentPosition.X;
            line.X2 = line.X1 + 800f;
            layoutRoot.Children.Add(line);
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
            SolidColorBrush solidBrush = new SolidColorBrush();
            optionRectangle.Fill = solidBrush;
            byte[] tmp = colorData.GetrgbMapValue(colorOffset);
            solidBrush.Color = Color.FromArgb(255, tmp[0], tmp[1], tmp[2]);
            optionRectangle.Translation = vectorData.GetpositionVectorForRectangle(optionOffset);
            optionRectangle.Width = 150;
            optionRectangle.Height = 100;
            layoutRoot.Children.Add(optionRectangle);        
        }

        /// <summary>
        /// 画面右上隅に三角形を描く
        /// </summary>
        /// <param name="isBack">後景の三角形であるか</param>
        private void ShowTriangle(bool isBack)
        {
            var polygon = new Polygon();
            int lengthOfTriangle;
            if (isBack)
            {
                polygon.Fill = new SolidColorBrush(Colors.DarkSlateGray);
                lengthOfTriangle = 170;
            }
            else
            {
                lengthOfTriangle = 150;
                if (quizCounter < 10) // 赤系
                {
                    polygon.Fill = new SolidColorBrush(Colors.Crimson);
                }
                else if(quizCounter < 18) // 黄赤系
                {
                    polygon.Fill = new SolidColorBrush(Colors.DarkOrange);
                }
                else if (quizCounter < 29) // 黄系
                {
                    polygon.Fill = new SolidColorBrush(Colors.Gold);
                }
                else if (quizCounter < 35) // 黄緑系
                {
                    polygon.Fill = new SolidColorBrush(Colors.YellowGreen);
                }
                else if (quizCounter < 41) // 緑系
                {
                    polygon.Fill = new SolidColorBrush(Colors.Green);
                }
                else if (quizCounter < 47) // 青緑系
                {
                    polygon.Fill = new SolidColorBrush(Colors.DarkCyan);
                }
                else if (quizCounter < 50) // 青系
                {
                    polygon.Fill = new SolidColorBrush(Colors.DarkBlue);
                }
                else if (quizCounter < 56) // 青紫系
                {
                    polygon.Fill = new SolidColorBrush(Colors.MediumPurple);
                }
                else if (quizCounter < 58) // 紫系
                {
                    polygon.Fill = new SolidColorBrush(Colors.Purple);
                }
                else if (quizCounter < 61) // 無彩色
                {
                    polygon.Fill = new SolidColorBrush(Colors.Silver);
                }

            }
            var points = new PointCollection();
            points.Add(new Point(formWidth, 0));
            points.Add(new Point(formWidth- lengthOfTriangle, 0));
            points.Add(new Point(formWidth, lengthOfTriangle));

            polygon.Points = points;
            layoutRoot.Children.Add(polygon);
        }

        /// <summary>
        /// ShowTriangleを2回呼んで、背景と前掲の三角形を描く
        /// </summary>
        private void CallShowTriangle()
        {
            ShowTriangle(true);
            ShowTriangle(false);
        }

        /// <summary>
        /// 右上隅に色のジャンルを表示する
        /// </summary>
        private void ShowColorGenre()
        {
            TextBlock textBlock = new TextBlock();
            textBlock.Foreground = new SolidColorBrush(Colors.White);
            textBlock.TextAlignment = TextAlignment.Center;
            textBlock.FontSize = 50;
            int offsetFromRight = 10 + (int)textBlock.FontSize;
            int offsetFromTop = 8;

            if (quizCounter < 10) // 赤系
            {
                textBlock.Text = "赤";
            }
            else if (quizCounter < 18) // 黄赤系
            {
                textBlock.Text = "黄赤";
                textBlock.FontSize = 40;
                offsetFromRight = 10 + (int)textBlock.FontSize*2;
            }
            else if (quizCounter < 29) // 黄系
            {
                textBlock.Text = "黄";
            }
            else if (quizCounter < 35) // 黄緑系
            {
                textBlock.Text = "黄緑";
                textBlock.FontSize = 40;
                offsetFromRight = 10 + (int)textBlock.FontSize * 2;
            }
            else if (quizCounter < 41) // 緑系
            {
                textBlock.Text = "緑";
            }
            else if (quizCounter < 47) // 青緑系
            {
                textBlock.Text = "青緑";
                textBlock.FontSize = 40;
                offsetFromRight = 10 + (int)textBlock.FontSize * 2;
            }
            else if (quizCounter < 50) // 青系
            {
                textBlock.Text = "青";
            }
            else if (quizCounter < 56) // 青紫系
            {
                textBlock.Text = "青紫";
                textBlock.FontSize = 40;
                offsetFromRight = 10 + (int)textBlock.FontSize * 2;
            }
            else if (quizCounter < 58) // 紫系
            {
                textBlock.Text = "紫";
            }
            else if (quizCounter < 61) // 無彩色
            {
                textBlock.Text = "無彩色";
                textBlock.FontSize = 30;
                offsetFromRight = 10 + (int)textBlock.FontSize * 3;
            }
            textBlock.Translation = new Vector3(formWidth - offsetFromRight, offsetFromTop, 5);
            layoutRoot.Children.Add(textBlock);
        }

        /// <summary>
        /// クイズに答えた際の正誤判定および画面の初期化を行う
        /// </summary>
        /// <param name="answerNum">クイズの答えを示すint型</param>
        private void AnswerQuiz(int answerNum)
        {
            bool isCorrect = CheckAnswer(answerNum);
            ShowisCorrected(isCorrect);
            ShowPressEnterKey();
            PlaySE(isCorrect);
            UpdatecorrectCounter(isCorrect);
            UpdateAnswerStatus();
            ShowColorNameDescription();
            quizCounter++;
        }

        /// <summary>
        /// クイズを終了する
        /// </summary>
        private void TerminateQuiz()
        {
            SetScore();
            Frame.Navigate(typeof(MainPage));
        }

        /// <summary>
        /// ScoreDataクラスにスコアを保存する
        /// </summary>
        private void SetScore()
        {
            ScoreData scoreData = new ScoreData();
            scoreData.SetScore(correctCounter);
        }

        /// <summary>
        /// 回答状況の説明文を更新する
        /// </summary>
        private void UpdateAnswerStatus()
        {
            answerStatusBlock.Text = "回答状況：";
            answerStatusBlock.Text += colorData.GetnameMapLength() + "問中 " + (quizCounter + 1) + "問目";
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
            TextBlock textBlock = new TextBlock();
            textBlock.FontSize = 27;
            textBlock.Translation = vectorData.GetpositionVectorForIsCorrected();
            textBlock.Width = 120;
            textBlock.TextAlignment = TextAlignment.Right;
            if (isCorrect)
            {
                textBlock.Text = "正解！";
                SolidColorBrush solidburush = new SolidColorBrush();
                solidburush.Color = Colors.Red;
                textBlock.Foreground = solidburush;
            }
            else
            {
                textBlock.Text = "不正解！";
                SolidColorBrush solidburush = new SolidColorBrush();
                solidburush.Color = Colors.Blue;
                textBlock.Foreground = solidburush;
            }
            layoutRoot.Children.Add(textBlock);
        }

        /// <summary>
        /// エンターキーを押すようメッセージを表示する
        /// </summary>
        private void ShowPressEnterKey()
        {
            TextBlock textBlock = new TextBlock();
            textBlock.FontSize = 27;
            Vector3 parentPosition = vectorData.GetpositionVectorForIsCorrected();
            textBlock.Translation = parentPosition + new Vector3(120.0f, 0.0f, 0.0f);
            if(quizCounter < 60)
            {
                textBlock.Text = "(エンターキーで次の問題を表示)";
            }
            else
            {
                textBlock.Text = "(エンターキーでクイズを終了)";
            }
            layoutRoot.Children.Add(textBlock);
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
            if (isAnsweringNow)
            {
                isAnsweringNow = false;
                RadioButton radioButton = (RadioButton)sender;
                checkedButton = radioButton;
                int answerNum = (int)radioButton.Content - 1;
                AnswerQuiz(answerNum);
            }
            // 既にボタンにチェックがある場合は、最初に選択したラジオボタンにチェックを戻す
            else
            {
                checkedButton.IsChecked = true;
            }
        }

        /// <summary>
        /// 選択肢のラジオボタンのチェックを外す
        /// </summary>
        private void UnCheckRadioButtons()
        {
            for(int i=0; i<NUM_OPTIONS; i++)
            {
                radioButtons[i].IsChecked = false;
            }
        }

        /// <summary>
        /// クイズの正誤に合わせたSEを鳴らす
        /// </summary>
        /// <param name="isCorrect">クイズの正誤</param>
        private void PlaySE(bool isCorrect)
        {
            MediaPlayerElement player = new MediaPlayerElement();
            if (isCorrect)
            {
                player.Source = MediaSource.CreateFromUri(new Uri("ms-appx:///Assets/ok.mp3"));
            }
            else
            {
                player.Source = MediaSource.CreateFromUri(new Uri("ms-appx:///Assets/ng.mp3"));
            }
            player.Visibility = Visibility.Collapsed;
            player.AutoPlay = true;
            layoutRoot.Children.Add(player);
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
                    if (!isAnsweringNow) // ラジオボタンを選択したらエンターキーが反応する
                    {
                        int numOfQuiz = colorData.GetnameMapLength();
                        if (quizCounter == numOfQuiz) // 最終問題に答えてエンターした場合
                        {
                            TerminateQuiz();
                        }
                        else
                        {
                            DeleteComponents();
                            CallShowTriangle();
                            ShowColorGenre();
                            UnCheckRadioButtons();
                            InitializeColorNameOfQuiz(quizCounter);
                            UpdateAnswerStatus();
                            ShowQuiz();
                        }
                        isAnsweringNow = true;
                    }
                    break;
                case VirtualKey.Number1:
                case VirtualKey.NumberPad1:
                    radioButtons[0].IsChecked = true;
                    OnClickRadioButton(radioButtons[0], new RoutedEventArgs());
                    break;
                case VirtualKey.Number2:
                case VirtualKey.NumberPad2:
                    radioButtons[1].IsChecked = true;
                    OnClickRadioButton(radioButtons[1], new RoutedEventArgs());
                    break;
                case VirtualKey.Number3:
                case VirtualKey.NumberPad3:
                    radioButtons[2].IsChecked = true;
                    OnClickRadioButton(radioButtons[2], new RoutedEventArgs());
                    break;
                case VirtualKey.Number4:
                case VirtualKey.NumberPad4:
                    radioButtons[3].IsChecked = true;
                    OnClickRadioButton(radioButtons[3], new RoutedEventArgs());
                    break;
                case VirtualKey.Number5:
                case VirtualKey.NumberPad5:
                    radioButtons[4].IsChecked = true;
                    OnClickRadioButton(radioButtons[4], new RoutedEventArgs());
                    break;
                case VirtualKey.Number6:
                case VirtualKey.NumberPad6:
                    radioButtons[5].IsChecked = true;
                    OnClickRadioButton(radioButtons[5], new RoutedEventArgs());
                    break;
                case VirtualKey.Number7:
                case VirtualKey.NumberPad7:
                    radioButtons[6].IsChecked = true;
                    OnClickRadioButton(radioButtons[6], new RoutedEventArgs());
                    break;
                case VirtualKey.Number8:
                case VirtualKey.NumberPad8:
                    radioButtons[7].IsChecked = true;
                    OnClickRadioButton(radioButtons[7], new RoutedEventArgs());
                    break;
                default:
                    break;
            }
        }
    }
}
