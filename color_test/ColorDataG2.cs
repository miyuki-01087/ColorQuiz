using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace color_test
{
    /// <summary>
    /// 色彩検定二級の色のデータを提供するクラス
    /// </summary>
    class ColorDataG2
    {
        Dictionary<int, string> nameMap = new Dictionary<int, string>();
        Dictionary<int, byte[]> rgbMap = new Dictionary<int, byte[]>();
         public ColorDataG2(int NUM_OPTIONS)
        {
            InitializeNameMap();
            InitializergbMap();
        }

        /// <summary>
        /// 連番と色名の辞書を初期化する
        /// </summary>
        private void InitializeNameMap()
        {
            nameMap.Add(0, "鴇色(ときいろ)(明るい紫みの赤)");
            nameMap.Add(1, "オールドローズ(やわらかい赤)");
            nameMap.Add(2, "韓紅色(からくれない)(あざやかな赤)");
            nameMap.Add(3, "ポピーレッド(あざやかな赤)");
            nameMap.Add(4, "テラコッタ(くすんだ黄みの赤)");
            nameMap.Add(5, "蘇芳(すおう)(くすんだ赤)");
            nameMap.Add(6, "鳶色(とびいろ)(暗い黄みの赤)");
            nameMap.Add(7, "弁柄色(べんがらいろ)(暗い黄みの赤)");
            nameMap.Add(8, "海老茶(えびちゃ)暗い黄みの赤");
            nameMap.Add(9, "マルーン(暗い赤)");
            nameMap.Add(10, "チャイニーズレッド(あざやかな黄赤)");
            nameMap.Add(11, "黄丹(おうに)(つよい黄赤)");
            nameMap.Add(12, "ローシェンナ(つよい黄赤)");
            nameMap.Add(13, "桧皮色(ひわだいろ(暗い灰みの黄赤)");
            nameMap.Add(14, "バーントシェンナ(くすんだ黄赤)");
            nameMap.Add(15, "タン(くすんだ黄赤)");
            nameMap.Add(16, "代赭(たいしゃ)(くすんだ黄赤)");
            nameMap.Add(17, "柑子色(こうじいろ)(明るい黄赤)");

        }

        /// <summary>
        /// 連番と色のRGBの辞書を初期化する
        /// </summary>
        private void InitializergbMap()
        {
            /*  ここからが出題範囲1つめ  */
            rgbMap.Add(0, new byte[] { 244, 179, 194 }); // 鴇色
            rgbMap.Add(1, new byte[] { 226, 147, 153 }); // オールドローズ
            rgbMap.Add(2, new byte[] { 233, 84, 100 }); // 韓紅色
            rgbMap.Add(3, new byte[] { 234, 85, 80 }); // ポピーレッド
            rgbMap.Add(4, new byte[] { 189, 104, 86 }); // テラコッタ
            rgbMap.Add(5, new byte[] { 158, 61, 63 }); // 蘇芳
            rgbMap.Add(6, new byte[] { 149, 72, 63 }); // 鳶色
            rgbMap.Add(7, new byte[] { 143, 46, 20 }); // 弁柄色
            rgbMap.Add(8, new byte[] { 119, 60, 48 }); // 海老茶
            rgbMap.Add(9, new byte[] { 106, 25, 23 }); // マルーン
            rgbMap.Add(10, new byte[] { 237, 109, 70 }); // チャイニーズレッド
            rgbMap.Add(11, new byte[] { 238, 121, 72 }); // 黄丹
            rgbMap.Add(12, new byte[] { 225, 123, 52 }); // ローシェンナ
            rgbMap.Add(13, new byte[] { 150, 80, 54 }); // 桧皮色
            rgbMap.Add(14, new byte[] { 187, 85, 53 }); // バーントシェンナ
            rgbMap.Add(15, new byte[] { 191, 120, 62 }); // タン
            rgbMap.Add(16, new byte[] { 187, 85, 32 }); // 代赭
            rgbMap.Add(17, new byte[] { 246, 173, 73 }); // 柑子色
            /*  ここからが出題範囲2つめ  */
        }

        /// <summary>
        /// 問題の番号を受け取って、出題範囲の始まりを返す
        /// </summary>
        /// <param name="quizCounter">問題の番号</param>
        /// <returns>出題範囲の始まり</returns>
        public int GetCurrentStartOfColors(int quizCounter)
        {
            int startNum;
            if(quizCounter <= 17)
            {
                startNum = 0;
            }
            else
            {
                startNum = 18;
            }
            return startNum;
        }

        /// <summary>
        /// 問題の番号を受け取って、出題範囲の終わりを返す
        /// </summary>
        /// <param name="quizCounter">問題の番号</param>
        /// <returns>出題範囲の終わり</returns>
        public int GetCurrentEndOfColors(int quizCounter)
        {
            int endNum;
            if(quizCounter <= 17)
            {
                endNum = 17;
            }else
            {
                endNum = 18; // 後で変更する
            }
            return endNum;
        }

        /// <summary>
        /// nameMapで指定した値を取得する
        /// </summary>
        /// <param name="key">値を指定するためのキー</param>
        /// <returns>nameMapの値</returns>
        public string GetnameMapValue(int key)
        {
            return nameMap[key];
        }

        /// <summary>
        /// nameMapの長さを取得する
        /// </summary>
        /// <returns>nameMapの長さ</returns>
        public int GetnameMapLength()
        {
            return nameMap.Count();
        }

        /// <summary>
        /// rgbMapで指定した値を取得する
        /// </summary>
        /// <param name="key"></param>
        /// <returns>rgbMapの値</returns>
        public byte[] GetrgbMapValue(int key)
        {
            return rgbMap[key];
        }
    }
}
