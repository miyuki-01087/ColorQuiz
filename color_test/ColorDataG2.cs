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
        }

        /// <summary>
        /// 連番と色のRGBの辞書を初期化する
        /// </summary>
        private void InitializergbMap()
        {
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
