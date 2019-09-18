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
using static game_p.SocketClient;

namespace game_p
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : NavigationWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void NavigationWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            var serverSocket = AsynchronousSocketListener.GetInstance();
            // サーバーが実行中の場合
            if (serverSocket.IsEnabled)
            {
                // スレッドを停止
                await serverSocket.StopListeningThread();
            }
        }

    }

    

}
