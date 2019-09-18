using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Windows.Threading;
using static game_p.SocketClient;

namespace game_p
{
    /// <summary>
    /// Play_page.xaml の相互作用ロジック
    /// </summary>
    /// 
    internal class PN
    {
        public int playernumber { get; set; }

        public int c1 { get; set; }

        public int c2 { get; set; }

       
    }


    public partial class Play_page : Page
    {

        private DispatcherTimer timeupTimer;
        private DispatcherTimer timeup2Timer;
        private DispatcherTimer RandomTimer;
        private DispatcherTimer Random2Timer;
        private DispatcherTimer tienTimer;
        private static Result_page result_page = null;

        public AsynchronousClient clients;

        //制限時間の設定
        private const int stop = 40000;

        //制限時間の設定
        private const int stop2 = 30000;

        //ランダム時間の設定
        private const int　RS  = 1000;

        //ランダム2時間の設定
        private const int RS2 = 500;

        //ラグをなくす時間の設定
        private const int tien = 3000;

        //通信で数値を受け取る場所を作る
        public int PlayerNumber = 1, MoguraNumber = 0, count1 = 0, count2 = 0;

        //モグラのリスト
        public List<mogurahole> MoguraList;

        private PlayPageViewModel playPageViewModel;

        /// <summary>
        /// このページのビューモデル
        /// </summary>
        public class PlayPageViewModel : INotifyPropertyChanged
        {
            private string _name1;
            public string Name1
            {
                get { return _name1; }
                set
                {
                    _name1 = value;
                    OnPropertyChanged("Name1");
                }
            }

            private string _name2;
            public string Name2
            {
                get { return _name2; }
                set
                {

                    _name2 = value;
                    OnPropertyChanged("Name2");
                }
            }

            public event PropertyChangedEventHandler PropertyChanged;
            private void OnPropertyChanged(string propertyName)
            {
                this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }

            private int _score1;
            public int Score1
            {
                get { return _score1; }
                set
                {
                    _score1 = value;
                    OnPropertyChanged(nameof(Score1));
                }
            }

            private int _score2;
            public int Score2
            {
                get { return _score2; }
                set
                {
                    _score2 = value;
                    OnPropertyChanged(nameof(Score2));
                }
            }
        }


        public Play_page()
        {
            InitializeComponent();
            //プレイヤー名の設定
            PlayerNumber = ((PN)Application.Current.Properties["Number"]).playernumber;
            //if (PlayerNumber == 1) this.DataContext = new { Name1 = "プレイヤー１", Name2 = "プレイヤー２", Score1 = count1, Score2 = count2 };
            //else if (PlayerNumber == 2) this.DataContext = new { Name1 = "プレイヤー２", Name2 = "プレイヤー１", Score1 = count1, Score2 = count2 };


            playPageViewModel = new PlayPageViewModel()
            {
                Name1 = (PlayerNumber == 1 ? "プレイヤー１" : "プレイヤー２"),
                Name2 = (PlayerNumber == 1 ? "プレイヤー２" : "プレイヤー１"),
                Score1 = 0,
                Score2 = 0
            };
            this.DataContext = playPageViewModel;

            //モグラをRandomで出すタイマー
            RandomTimer = new DispatcherTimer(DispatcherPriority.Normal);
            RandomTimer.Interval = TimeSpan.FromMilliseconds(RS);
            RandomTimer.IsEnabled = false;
            RandomTimer.Tick += RandomTimerEventHandler;

            //モグラをRandomで出すタイマー2
            Random2Timer = new DispatcherTimer(DispatcherPriority.Normal);
            Random2Timer.Interval = TimeSpan.FromMilliseconds(RS2);
            Random2Timer.IsEnabled = false;
            Random2Timer.Tick += Random2TimerEventHandler;

            //制限時間のタイマー
            timeup2Timer = new DispatcherTimer(DispatcherPriority.Normal);
            timeup2Timer.Interval = TimeSpan.FromMilliseconds(stop2);
            timeup2Timer.IsEnabled = false;
            timeup2Timer.Tick += timeup2TimerEventHandler;

            //制限時間前のタイマー
            timeupTimer = new DispatcherTimer(DispatcherPriority.Normal);
            timeupTimer.Interval = TimeSpan.FromMilliseconds(stop);
            timeupTimer.IsEnabled = false;
            timeupTimer.Tick += timeupTimerEventHandler;

            //ラグをなくすタイマー
            tienTimer = new DispatcherTimer(DispatcherPriority.Normal);
            tienTimer.Interval = TimeSpan.FromMilliseconds(tien);
            tienTimer.IsEnabled = false;
            tienTimer.Tick += tienTimerEventHandler;

            // 制限時間のタイマーを開始
            timeupTimer.IsEnabled = true;
            timeupTimer.Start();

            // 制限時間前のタイマーを開始
            timeup2Timer.IsEnabled = true;
            timeup2Timer.Start();

            // ランダムタイマーを開始
            RandomTimer.IsEnabled = true;
            RandomTimer.Start();

            // リストに各モグラを追加
            MoguraList = new List<mogurahole>();
            MoguraList.Add(Mogura1_1);
            MoguraList.Add(Mogura1_2);
            MoguraList.Add(Mogura1_3);
            MoguraList.Add(Mogura1_4);
            MoguraList.Add(Mogura1_5);
            MoguraList.Add(Mogura1_6);
            MoguraList.Add(Mogura2_1);
            MoguraList.Add(Mogura2_2);
            MoguraList.Add(Mogura2_3);
            MoguraList.Add(Mogura2_4);
            MoguraList.Add(Mogura2_5);
            MoguraList.Add(Mogura2_6);
            MoguraList.Add(Mogura3_1);
            MoguraList.Add(Mogura3_2);
            MoguraList.Add(Mogura3_3);
            MoguraList.Add(Mogura3_4);
            MoguraList.Add(Mogura3_5);
            MoguraList.Add(Mogura3_6);
            MoguraList.Add(Mogura4_1);
            MoguraList.Add(Mogura4_2);
            MoguraList.Add(Mogura4_3);
            MoguraList.Add(Mogura4_4);
            MoguraList.Add(Mogura4_5);
            MoguraList.Add(Mogura4_6);

            // 全てのボタンにクリックイベントを設定しておく
            foreach(var targetbutton in MoguraList)
            {
                // クリック時のイベントを設定
                targetbutton.AttackEvent = () =>
                {
                    Mogura_Click();
                    count1++;
                    //clients.DataReceiveCallback = Doscore;

                    


                    if (PlayerNumber == 1)
                    {

                        try
                            {

                            clients.receiveDone.Reset();
                            clients.Send("<p1>" + count1.ToString() + "<score><EOF>");
                            //clients.receiveDone.WaitOne();
                            // Receive the clients.response from the remote device.  
                            if (clients.response.IndexOf("<score>") > -1) //score送信の部分
                            {
                                clients.response = (clients.response.Replace("<score>", ""));
                                var score = clients.response.Split(',');
                                count2 = int.Parse(score[1].Replace("<EOF>", ""));
                            }

                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.ToString());
                        }

                    }
                    else if (PlayerNumber == 2)
                    {

                        try
                        {

                            clients.receiveDone.Reset();
                            clients.Send("<p2>" + count1.ToString() + "<score><EOF>");
                            //clients.receiveDone.WaitOne();
                            // Receive the clients.response from the remote device.  
                            if (clients.response.IndexOf("<score>") > -1) //score送信の部分
                            {
                                clients.response = (clients.response.Replace("<score>", ""));
                                var score = clients.response.Split(',');
                                count2 = int.Parse(score[0].Replace("<EOF>", ""));
                            }

                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.ToString());
                        }
                    }

                    // Application.Propertiesに設定しておく。
                    PN number = new PN() { playernumber = PlayerNumber };
                    PN C1 = new PN() { c1 = count1 };
                    PN C2 = new PN() { c2 = count2 };

                    Application.Current.Properties["Number"] = number;
                    Application.Current.Properties["C1"] = C1;
                    Application.Current.Properties["C2"] = C2;
                };
            }
        }

        // ラグをなくすタイマーが発生した時のイベント

        public void tienTimerEventHandler(Object sender, EventArgs e)

        {

            // ラグをなくすタイマーを停止
            tienTimer.IsEnabled = false;
            tienTimer.Stop();

            if (PlayerNumber == 1)
            {

                try
                {

                    clients.receiveDone.Reset();
                    clients.Send("<p1>" + count1.ToString() + "<score><EOF>");
                    //clients.receiveDone.WaitOne();
                    // Receive the clients.response from the remote device.  
                    if (clients.response.IndexOf("<score>") > -1) //score送信の部分
                    {
                        clients.response = (clients.response.Replace("<score>", ""));
                        var score = clients.response.Split(',');
                        count2 = int.Parse(score[1].Replace("<EOF>", ""));
                    }

                }
                catch (Exception ea)
                {
                    Console.WriteLine(ea.ToString());
                }

            }
            else if (PlayerNumber == 2)
            {

                try
                {

                    clients.receiveDone.Reset();
                    clients.Send("<p2>" + count1.ToString() + "<score><EOF>");
                    //clients.receiveDone.WaitOne();
                    // Receive the clients.response from the remote device.  
                    if (clients.response.IndexOf("<score>") > -1) //score送信の部分
                    {
                        clients.response = (clients.response.Replace("<score>", ""));
                        var score = clients.response.Split(',');
                        count2 = int.Parse(score[0].Replace("<EOF>", ""));
                    }

                }
                catch (Exception ea)
                {
                    Console.WriteLine(ea.ToString());
                }

                // Application.Propertiesに設定しておく。
                PN number = new PN() { playernumber = PlayerNumber };
                PN C1 = new PN() { c1 = count1 };
                PN C2 = new PN() { c2 = count2 };

                Application.Current.Properties["Number"] = number;
                Application.Current.Properties["C1"] = C1;
                Application.Current.Properties["C2"] = C2;
            }

            if (result_page == null)
            {
                // 次に表示するページ（結果画面)を生成、以後使いまわし
                result_page = new Result_page();
            }

            // 制限時間のタイマーを終了
            timeupTimer.IsEnabled = false;
            timeupTimer.Stop();

            // 結果ページへ移動
            this.NavigationService.Navigate(result_page);

        }


        //scoreを受け取る部分

        // 時間制限タイマーが発生した時のイベント

        public void timeupTimerEventHandler(Object sender, EventArgs e)

        {
            // ランダム2タイマーを停止
            Random2Timer.IsEnabled = false;
            Random2Timer.Stop();

            // ラグをなくすタイマーを開始
            tienTimer.IsEnabled = true;
            tienTimer.Start();

        }

        // 時間制限タイマーが発生した時のイベント

        public void timeup2TimerEventHandler(Object sender, EventArgs e)

        {
            // ランダムタイマーを停止
            RandomTimer.IsEnabled = false;
            RandomTimer.Stop();

            // ランダム2タイマーを開始
            Random2Timer.IsEnabled = true;
            Random2Timer.Start();

        }


            // ランダムタイマーが発生した時のイベント

            public void RandomTimerEventHandler(Object sender, EventArgs e)

        {
            try
            {
                //送信
                clients.Send("<rand><EOF>");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }


        public void Random2TimerEventHandler(Object sender, EventArgs e)

        {
            try
            {
                //送信
                clients.Send("<rand><EOF>");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            // 受信時のコールバック関数をセット
            clients.DataReceiveCallback = DataReceiveCallback;
        }

        /// <summary>
        /// 受信時のコールバック関数
        /// </summary>
        /// <param name="data">受信データ</param>
        private void DataReceiveCallback(String data)
        {
            Dispatcher.Invoke((Action)(() =>
            {
                Console.WriteLine(data);
                // EOFが見つからない場合は無視
                if (data.IndexOf("<EOF>") < 0) return;
                // EOFを削除
                data = data.Replace("<EOF>", "");


                if (data.IndexOf("<score>") > -1) //score送信の部分
                {
                    data = (data.Replace("<score>", ""));
                    data = (data.Replace("<EOF>", ""));
                    var score = data.Split(',');

                    if (PlayerNumber == 1)
                    {
                        count2 = int.Parse(score[1]);
                    }

                    if (PlayerNumber == 2)
                    {
                        count2 = int.Parse(score[0]);
                    }
                    //  this DataContex = {Score2 = count2}; ここに処理を入れたい
                    playPageViewModel.Score1 = count1;
                    playPageViewModel.Score2 = count2;


                }

                if (data.IndexOf("<rand>") >= 0)
                {
                    data = data.Replace("<rand>", "");
                    MoguraNumber = int.Parse(data);
                    // 配列は0～23だが乱数が1～24で渡されるため1を引く
                    if (MoguraNumber == 0) MoguraNumber = 1;
                    MoguraList[MoguraNumber - 1].ShowMogura();
                }

            }));
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
             App.Current.MainWindow.Show();
        }



        private void Show_Click(object sender, RoutedEventArgs e)
        {
            //コントローラーを入れる場所を作る
            mogurahole targetbutton = Mogura1_1;


                // 押されたボタンの名前を取得
                String buttonName = ((Button)sender).Name;

                // 押されたボタンによって、対象のイメージコントローラーを切り替える
                if (buttonName == "Show1") targetbutton = Mogura1_1;
                else if (buttonName == "Show2") targetbutton = Mogura1_2;
                else if (buttonName == "Show3") targetbutton = Mogura1_3;
                else if (buttonName == "Show4") targetbutton = Mogura2_1;
                else targetbutton = Mogura1_1;



                // クリック時のイベントを設定
                targetbutton.AttackEvent = () =>
                {
                    Mogura_Click();
                    count1++;
                    if (PlayerNumber == 1) this.DataContext = new { Name1 = "プレイヤー１", Name2 = "プレイヤー２", Score1 = count1, Score2 = count2 };
                    else if (PlayerNumber == 2) this.DataContext = new { Name1 = "プレイヤー２", Name2 = "プレイヤー１", Score1 = count1, Score2 = count2 };
                    // Application.Propertiesに設定しておく。
                    PN number = new PN() { playernumber = PlayerNumber };
                    PN C1 = new PN() { c1 = count1 };
                    PN C2 = new PN() { c2 = count2 };
                    Application.Current.Properties["Number"] = number;
                    Application.Current.Properties["C1"] = C1;
                    Application.Current.Properties["C2"] = C2;

                };

                // モグラを表示
                targetbutton.ShowMogura();
        }

        private void Mogura_Click()
        {
            //MessageBox.Show("Hit!");
        }

        //プレイヤー名を表示

    }
}