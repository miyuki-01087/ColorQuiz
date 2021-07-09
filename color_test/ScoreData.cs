using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace color_test
{
    class ScoreData
    {
        ApplicationDataContainer container;
        private const int LENGTH_OF_STORAGE = 20;

        public ScoreData()
        {
            container = ApplicationData.Current.LocalSettings;
        }

        /// <summary>
        /// スコアを保存する
        /// </summary>
        /// <param name="score">点数</param>
        public void SetScore(int score)
        {
            for(int i=0; i<LENGTH_OF_STORAGE; i++)
            {
                if(container.Values[i.ToString()] == null)
                {
                    container.Values[i.ToString()] = score;
                    break;
                }
            }
        }

        /// <summary>
        /// スコアを格納した配列を取得する
        /// </summary>
        /// <returns>スコアを格納した配列。nullあり。</returns>
        public string[] GetScore()
        {
            string[] scores = new string[LENGTH_OF_STORAGE];
            for(int i=0; i<LENGTH_OF_STORAGE; i++)
            {
                if (container.Values[i.ToString()] != null)
                {
                    scores[i] = container.Values[i.ToString()].ToString();
                }
                else
                {
                    scores[i] = null;
                }
            }
            return scores;
        }

        /// <summary>
        /// スコアの長さの最大値を取得する
        /// </summary>
        /// <returns>スコアの長さの最大値</returns>
        public int GetLengthOfStorage()
        {
            return LENGTH_OF_STORAGE;
        }

        /// <summary>
        /// スコアの長さを取得する
        /// </summary>
        /// <returns>スコアの長さ</returns>
        public int GetLengthOfScore()
        {
            int lengthOfScore = 0;
            for (int i = 0; i < LENGTH_OF_STORAGE; i++)
            {
                if (container.Values[i.ToString()] != null)
                {
                    lengthOfScore++;
                }
                else
                {
                    break;
                }
            }
            return lengthOfScore;
        }
    }
}
