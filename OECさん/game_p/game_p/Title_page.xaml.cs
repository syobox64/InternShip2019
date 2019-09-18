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
using System.Threading;

using static game_p.SocketClient;


namespace game_p
{
    /// <summary>
    /// Title_page.xaml の相互作用ロジック
    /// </summary>
    /// 
    public partial class Title_page : Page
    {
        private static Play_page play_page = null;
        private static Taiki_page taiki_page = null;
        public AsynchronousClient clients;
        private object e;
        public int PlayerNumber = 1;

        private const string serverName = "DESKTOP-1QM7B6T";


        public Title_page()
        {
            InitializeComponent();
            clients = new AsynchronousClient();
        }

        private void Non(String data)
        {
            Dispatcher.Invoke((Action)(() =>
            {
            }));
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {

            var serverSocket = AsynchronousSocketListener.GetInstance();
            // サーバーが実行中の場合
            if (serverSocket.IsEnabled)
            {
                // スレッドを停止
                await serverSocket.StopListeningThread();
            }


            // サーバーの起動
            serverSocket.StartListeningThread();

            // 自分のPC名を取得
            string myPC = Environment.MachineName;

            clients.DataReceiveCallback = Non;
            clients.Connect(myPC, 11000);

            clients.Receive();
            clients.Send("CPU<EOF>");


            PN number = new PN() { playernumber = PlayerNumber };
            Application.Current.Properties["Number"] = number;

            play_page = new Play_page();
            play_page.clients = clients;
            


            // プレイページへ移動
            this.NavigationService.Navigate(play_page);
        }

        private async void Button_Click_1(object sender, RoutedEventArgs e)
        {
            // 自分のPC名を取得
            string myPC = Environment.MachineName;

            // 自分のPCがサーバー名と同一ならサーバーを起動させる
            if (myPC == serverName)
            {
                var serverSocket = AsynchronousSocketListener.GetInstance();
                // サーバーが実行中の場合
                if (serverSocket.IsEnabled)
                {
                    // スレッドを停止
                    await serverSocket.StopListeningThread();
                }

                // サーバーの起動
                serverSocket.StartListeningThread();
            }

            clients.DataReceiveCallback = Non;
            clients.Connect(serverName, 11000);

            // 次に表示するページ（待機画面）を生成、以後使いまわし
            taiki_page = new Taiki_page();
            taiki_page.clients = clients;

            // 待機ページへ移動
            this.NavigationService.Navigate(taiki_page);
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            App.Current.MainWindow.Close();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }

    
}
