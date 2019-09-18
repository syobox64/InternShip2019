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
using System.Windows.Media.Animation;
using System.Windows.Threading;

namespace game_p
{
    /// <summary>
    /// mogurahole.xaml の相互作用ロジック
    /// </summary>
    public partial class mogurahole : UserControl
    {
        private const int up = 500;
        private const int stop = 1000;
        private const int down = 500;
        private const int down2 = 200;

        private Storyboard ShowingStoryboard;
        private Storyboard HiddingStoryboard;
        private Storyboard pikopikoStoryboard;
        private Storyboard attackedStoryboard;
        private Storyboard initializeStoryboard;

        private DispatcherTimer downTimer;
        private DispatcherTimer pikopikoTimer;

        public Action AttackEvent;

        public mogurahole()
        {
            InitializeComponent();
            //モグラを表示するストーリーボードを作成
            ShowingStoryboard = CreateShowStoryboard();
            //モグラを隠すストーリーボードを作成
            HiddingStoryboard = CreateHideStoryboard();
            // モグラ初期化時のストーリーボードを作成
            initializeStoryboard = CreateInitialzeStoryboard();
            //叩かれた時のストリーボードを作成
            attackedStoryboard = CreateAttackStoryboard();
            //ピコピコハンマーを出して落とすストーリーボードを作成
            pikopikoStoryboard = CreatepikopikoStoryboard();

            //モグラが出てきてから戻っていくまでのタイマー
            downTimer = new DispatcherTimer(DispatcherPriority.Normal);
            downTimer.Interval = TimeSpan.FromMilliseconds(up + stop);
            downTimer.IsEnabled = false;
            downTimer.Tick += downTimerEventHandler;
        }
        //モグラの表示開始
        public void ShowMogura()
        {
            // モグラの角度を初期化
            MoguraButton.BeginStoryboard(initializeStoryboard);

            // モグラボタンを有効化かつ表示
            MoguraButton.IsEnabled = true;
            MoguraButton.Visibility = Visibility.Visible;

            // アニメーションの開始
            MoguraButton.BeginStoryboard(ShowingStoryboard);

            // モグラを隠すためのタイマーを開始
            downTimer.IsEnabled = true;
            downTimer.Start();
        }



        // モグラが隠れるタイマーが発生した時のイベント

        public void downTimerEventHandler(Object sender, EventArgs e)

        {

            // モグラが隠れるアニメーションを開始

            MoguraButton.BeginStoryboard(HiddingStoryboard);

            // タイマーをストップ

            downTimer.Stop();
            downTimer.IsEnabled = false;
        }

        // ピコハンマーが隠れるタイマーが発生した時のイベント

        public void pikopikoTimerEventHandler(Object sender, EventArgs e)

        {

            // ピコハンマーが隠れるアニメーションを開始

            pikopiko.IsEnabled = false;
            pikopiko.Visibility = Visibility.Hidden;

            // ヒットが隠れるアニメーションを開始

            hit.IsEnabled = false;
            hit.Visibility = Visibility.Hidden;

            // タイマーをストップ

            pikopikoTimer.Stop();
            pikopikoTimer.IsEnabled = false;
        }

        /// <summary>
        /// モグラが叩かれた時のストーリーボードを作成します
        /// </summary>
        /// <returns>モグラが叩かれた時のストーリーボード</returns>
        private Storyboard CreateAttackStoryboard()
        {
            // Storyboardを新規作成
            var storyboard = new Storyboard
            {
                // 1度のみ実行
                RepeatBehavior = new RepeatBehavior(1),
                // 逆方向の再生を行わない
                AutoReverse = false
            };

            // アニメーション設定
            // 半回転
            var animation = new DoubleAnimation
            {
                From = 0,
                To = 180,
                Duration = TimeSpan.FromMilliseconds(200)
            };
            Storyboard.SetTargetName(animation, "rotate");
            Storyboard.SetTargetProperty(animation, new PropertyPath("(Angle)"));

            // Storyboardにアニメーションを設定
            storyboard.Children.Add(animation);

            return storyboard;
        }
        /// <summary>
        /// モグラ初期化のストーリーボードを作成します
        /// </summary>
        /// <returns>モグラ初期化のストーリーボード</returns>
        private Storyboard CreateInitialzeStoryboard()
        {
            // Storyboardを新規作成
            var storyboard = new Storyboard
            {
                // 1度のみ実行
                RepeatBehavior = new RepeatBehavior(1),
                // 逆方向の再生を行わない
                AutoReverse = false
            };

            // 角度を0度に設定
            var animation = new DoubleAnimation
            {
                To = 0,
                Duration = TimeSpan.FromMilliseconds(1)
            };
            Storyboard.SetTargetName(animation, "rotate");
            Storyboard.SetTargetProperty(animation, new PropertyPath("(Angle)"));

            // Storyboardにアニメーションを設定
            storyboard.Children.Add(animation);

            return storyboard;
        }

        // モグラを表示させるストーリーボードを作成します

        private Storyboard CreateShowStoryboard()
        {
            // Storyboardを新規作成
            var storyboard = new Storyboard
            {
                // 1度のみ実行
                RepeatBehavior = new RepeatBehavior(1),
                // 逆方向の再生を行わない
                AutoReverse = false
            };

            // アニメーション設定
            // 縦の初期位置100から0まで移動
            var animation = new DoubleAnimation
            {
                From = 100,
                To = 10,
                Duration = TimeSpan.FromMilliseconds(up)
            };
            Storyboard.SetTargetProperty(animation, new PropertyPath("(Canvas.Top)"));

            // Storyboardにアニメーションを設定
            storyboard.Children.Add(animation);

            return storyboard;
        }

        // モグラを隠するストーリーボードを作成します
        private Storyboard CreateHideStoryboard()
        {
            // Storyboardを新規作成
            var storyboard = new Storyboard
            {
                // 1度のみ実行
                RepeatBehavior = new RepeatBehavior(1),
                // 逆方向の再生を行わない
                AutoReverse = false
            };

            // アニメーション設定
            // 縦の初期位置100へ移動
            var animation = new DoubleAnimation
            {
                To = 100,
                Duration = TimeSpan.FromMilliseconds(down)
            };
            Storyboard.SetTargetProperty(animation, new PropertyPath("(Canvas.Top)"));

            // Storyboardにアニメーションを設定
            storyboard.Children.Add(animation);

            return storyboard;
        }


        //ピコピコハンマーのストーリーボードを作成します。
        private Storyboard CreatepikopikoStoryboard()
        {
            // Storyboardを新規作成
            var storyboard = new Storyboard
            {
                // 1度のみ実行
                RepeatBehavior = new RepeatBehavior(1),
                // 逆方向の再生を行わない
                AutoReverse = false
            };

            // アニメーション設定

            // 縦の初期位置0から100まで移動
            var animation = new DoubleAnimation
            {
                From = 0,
                To = 100,
                Duration = TimeSpan.FromMilliseconds(down2)
            };
            Storyboard.SetTargetProperty(animation, new PropertyPath("(Canvas.Top)"));

            //ピコピコハンマーが出てきてから戻っていくまでのタイマー
            pikopikoTimer = new DispatcherTimer(DispatcherPriority.Normal);
            pikopikoTimer.Interval = TimeSpan.FromMilliseconds(stop);
            pikopikoTimer.IsEnabled = false;
            pikopikoTimer.Tick += pikopikoTimerEventHandler;

            // Storyboardにアニメーションを設定
            storyboard.Children.Add(animation);

            return storyboard;


        }


        // モグラが叩かれた時のイベント
        private void MoguraButton_Click(object sender, RoutedEventArgs e)
        {
            // タイマーをストップ
            downTimer.IsEnabled = false;
            downTimer.Stop();

            // モグラボタンの無効化
            MoguraButton.IsEnabled = false;

            // TODO 叩かれた時のアニメーションを表示
            MoguraButton.BeginStoryboard(attackedStoryboard);

            // ピコピコハンマーを有効化かつ表示

            pikopiko.IsEnabled = true;
            pikopiko.Visibility = Visibility.Visible;

            // ヒットを有効化かつ表示
            hit.IsEnabled = true;
            hit.Visibility = Visibility.Visible;

            // アニメーションの開始
            pikopiko.BeginStoryboard(pikopikoStoryboard);

            // ピコハンマーを隠すためのタイマーを開始
                pikopikoTimer.IsEnabled = true;
            pikopikoTimer.Start();

            // タイマーをスタート
            downTimer.IsEnabled = true;
            downTimer.Start();

            // モグラが叩かれたイベントを発生させる
            AttackEvent();
        }


    }
}
