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
    }
}
