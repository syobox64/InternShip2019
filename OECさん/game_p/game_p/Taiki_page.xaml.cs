using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
using static game_p.SocketClient;

namespace game_p
{
    /// <summary>
    /// Taiki_page.xaml の相互作用ロジック
    /// </summary>
    public partial class Taiki_page : Page
    {
        private static Play_page play_page = null;
        //通信で数値を受け取る場所を作る
        private string NextWindow;
        public AsynchronousClient clients;
        private object e;
        public int PlayerNumber = 1;
        

        public Taiki_page()
        {
            InitializeComponent();

        }

        private void DataReceiveCallback(String data)
        {
            Dispatcher.Invoke((Action)(() =>
            {
                if (data.IndexOf("OK.") > -1)
                {
                    if (play_page == null)
                    {

                        string a = data.Replace("OK.", "");
                        PlayerNumber = int.Parse(a);
                        
                        //サーバから送られてきたplayernumberをプレイページに送る。
                        // Application.Propertiesに設定しておく。
                        PN number = new PN() { playernumber = PlayerNumber };
                        Application.Current.Properties["Number"] = number;


                        // 次に表示するページ（プレイ画面)を生成、以後使いまわし
                        play_page = new Play_page();
                        play_page.clients = clients;
                    }

                    // プレイページへ移動
                    this.NavigationService.Navigate(play_page);
                }
            }));
            
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            clients.DataReceiveCallback = DataReceiveCallback;

            await Task.Run(() =>
            {
                do
                {
                    Thread.Sleep(100);
                } while (!Application.Current.Dispatcher.Invoke(() => { return IsLoaded; }));
            });
            try
            {

                clients.Receive();
                clients.receiveDone.Reset();

                clients.Send("VS<EOF>");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}

