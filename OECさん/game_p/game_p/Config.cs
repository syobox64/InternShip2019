using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text;


namespace game_p
{
    /// <summary>
    /// 設定クラス
    /// </summary>
    public class Config
    {
        #region フィールド定義

        /// <summary>
        /// 人
        /// </summary>
        public Person Person { get; set; }

        /*
        /// <summary>
        /// メモ
        /// </summary>
        public string Memo { get; set; }
        */


        #endregion

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public Config()
        {
            this.Person = new Person();
        }

        /// <summary>
        /// 設定ファイルのフルパスを取得
        /// </summary>
        /// <returns>設定ファイルのフルパス</returns>
        public static string GetConfigFilePath()
        {
            // 実行ファイルのフルパスを取得
            string appFilePath = System.Reflection.Assembly.GetEntryAssembly().Location;

            // 実行ファイルのフルパス末尾（拡張子）を変えて返す
            return System.Text.RegularExpressions.Regex.Replace(
                appFilePath,
                ".exe",
                ".json",
                System.Text.RegularExpressions.RegexOptions.IgnoreCase);
        }

        /// <summary>
        /// 設定読み込み
        /// </summary>
        /// <returns></returns>
        public static Config ReadConfig()
        {
            // 設定ファイルのフルパスを取得
            string configFile = GetConfigFilePath();

            if (File.Exists(configFile) == false)
            {
                // 設定ファイルなし
                return null;
            }

            using (var reader = new StreamReader(configFile, Encoding.UTF8))
            {
                // 設定ファイル読み込み
                string buf = reader.ReadToEnd();

                // デシリアライズして返す
                var js = new System.Web.Script.Serialization.JavaScriptSerializer();
                return js.Deserialize<Config>(buf);
            }
        }

        /// <summary>
        /// 設定書き込み
        /// </summary>
        /// <param name="cfg"></param>
        public static void WriteConfig(Config cfg)
        {
            // シリアライズ
            var js = new System.Web.Script.Serialization.JavaScriptSerializer();
            string buf = js.Serialize(cfg);

            // 設定ファイルのフルパス取得
            string configFile = GetConfigFilePath();

            using (var writer = new StreamWriter(configFile, false, Encoding.UTF8))
            {
                // 設定ファイルに書き込む
                writer.Write(buf);
            }
        }

    }
}