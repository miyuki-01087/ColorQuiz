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
            nameMap.Add(0, "赤");
            nameMap.Add(1, "青");
            nameMap.Add(2, "黄色");
            nameMap.Add(3, "緑");
            nameMap.Add(4, "黄緑");
            nameMap.Add(5, "橙");
            nameMap.Add(6, "水色");
            nameMap.Add(7, "茶色");
        }

        /// <summary>
        /// 連番と色のRGBの辞書を初期化する
        /// </summary>
        private void InitializergbMap()
        {
            rgbMap.Add(0, new byte[] { 200, 100, 100 });
            rgbMap.Add(1, new byte[] { 100, 100, 200 });
            rgbMap.Add(2, new byte[] { 200, 200, 100 });
            rgbMap.Add(3, new byte[] { 100, 200, 100 });
            rgbMap.Add(4, new byte[] { 150, 200, 100 });
            rgbMap.Add(5, new byte[] { 200, 120, 100 });
            rgbMap.Add(6, new byte[] { 200, 200, 250 });
            rgbMap.Add(7, new byte[] { 100, 30, 30 });
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
