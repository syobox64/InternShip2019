using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace game_p
{
    /// <summary>
    /// Result_page.xaml の相互作用ロジック
    /// </summary>


    internal class ST
    {
        public int playername { get; set; }

        public int score { get; set; }

    }


    //ランキングに使う
    class Runk
    {
        // フィールドの宣言

        public String runker = ""; //名前

        public int score = 0; //スコア

        // メソッドの宣言
        public void status(String r, int s)
        { 
            this.runker = r; //インスタンスの名前を設定
            this.score = s; //インスタンスのスコアを設定
        }
    }



    public partial class Result_page : Page
    {

        //通信で数値を受け取る場所を作る
        public int PlayerNumber = 1, count1 = 0, count2 = 0, win = 0;
        //public int R1, R2, R3, R4, R5,R6,R7,R8,R9,R10,R11,R12,R13;
        public int[] run = new int[11] {0,0,0,0,0,0,0,0,0,0,0};
        public string name;

        //private static Ranking_page ranking_page = null;
        private static Title_page title_page = null;


        //ランキングクラスの呼び出し
        public void status()
        {
            Runk Mystatus = new Runk();
            Mystatus.status(name,count1) ;

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            App.Current.MainWindow.Close();

        }

        public Result_page()
        {
            InitializeComponent();

            // Application.Propertiesから受け取る。
            PlayerNumber = ((PN)Application.Current.Properties["Number"]).playernumber;
            count1 = ((PN)Application.Current.Properties["C1"]).c1;
            count2 = ((PN)Application.Current.Properties["C2"]).c2;
            if (count1 > count2) win = 1;
            else if (count2 > count1) win = 2;
            else if (count2 == count1) win = 0;

            //プレイヤー名の設定
            if (PlayerNumber == 1)
            {
                if (win == 0) this.DataContext = new { Name1 = "プレイヤー１", Name2 = "プレイヤー２", Score1 = count1, Score2 = count2, winner = "引き分け", MyRunkerName = name, Myscore = count1 };
                if (win == 1) this.DataContext = new { Name1 = "プレイヤー１", Name2 = "プレイヤー２", Score1 = count1, Score2 = count2, winner = "プレイヤー１", MyRunkerName = name, Myscore = count1 };
                if (win == 2) this.DataContext = new { Name1 = "プレイヤー１", Name2 = "プレイヤー２", Score1 = count1, Score2 = count2, winner = "プレイヤー２", MyRunkerName = name, Myscore = count1 };
            }

            else if (PlayerNumber == 2)
            {
                if (win == 0) this.DataContext = new { Name1 = "プレイヤー２", Name2 = "プレイヤー１", Score1 = count1, Score2 = count2, winner = "引き分け", MyRunkerName = name, Myscore = count1 };
                if (win == 1) this.DataContext = new { Name1 = "プレイヤー２", Name2 = "プレイヤー１", Score1 = count1, Score2 = count2, winner = "プレイヤー２", MyRunkerName = name, Myscore = count1 };
                if (win == 2) this.DataContext = new { Name1 = "プレイヤー２", Name2 = "プレイヤー１", Score1 = count1, Score2 = count2, winner = "プレイヤー１", MyRunkerName = name, Myscore = count1 };
            }
            //ranking(run, count1);

            
        }

        public void makeStatus()
        {
            Runk Mystatus = new Runk();
            Mystatus.status(name,count1);
            
        }
            
    }

        



}

