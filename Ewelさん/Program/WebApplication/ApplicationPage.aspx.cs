using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebApplication.Entity;
using WebApplication.BusinessLogic;

namespace WebApplication
{
    public partial class _Default : System.Web.UI.Page
    {
        // ページ読み込み処理
        protected void Page_Load(object sender, EventArgs e)
        {
            // このページに初めてアクセスする場合
            var a = (string)Session["id"];
            //設定した値によってキャストが必要
            Label2.Text = a;

        }

        // 「送信」ボタンを押下した時の処理
        protected void Button_Click(object sender, EventArgs e)
        {
            error1.Visible = false;

            // 申込内容を保持するオブジェクトのインスタンスを生成
            ApplicationEntity resultEntity = new ApplicationEntity();
            InviteHistoryEntity inviteEntity = new InviteHistoryEntity();
            int inviteid = 0;
            int inviteusercoin = 0;
            var a = (string)Session["id"];

            // 申込内容をresultEntityのメンバに格納
            if (!TextBox1.Text.Trim().Equals(""))
            {
                inviteid = int.Parse(TextBox1.Text);
            }

            // エラーメッセージが表示されていない = 入力欄に不備が無い
            if (!error1.Visible)
            {
                // 申し込み画面のBusinessLogic（計算・加工処理・実施）のインスタンス applicationBusiness を生成
                ApplicationBusiness applicationBusiness = new ApplicationBusiness();



                // applicationBusinessの申し込み結果の登録ロジック ApplicationRegistration に resultEntity を渡し実行

                var table = applicationBusiness.Selects(inviteid);
                inviteEntity.invitedUser = int.Parse(a); //自分のID獲得
                var mytable = applicationBusiness.Selected(inviteEntity); //自分が招待されたことがあるか確認

                if (mytable.Rows.Count == 0) //自分が招待されたことが無い
                {
                    
                    // 入力されたユーザID
                    if (table.Rows.Count != 0)　//入力した人が存在している
                    {

                        inviteEntity.inviteUser = (int)table.Rows[0][0];
                        inviteusercoin = (int)table.Rows[0][3];
                        // 入力したユーザID

                        var inviteHistoryTable = applicationBusiness.InvitehistorySelect(inviteEntity.inviteUser);
                        
                        //入力した相手が４人以上招待していない　&&　入力した相手が自分ではない
                        if (inviteHistoryTable.Rows.Count < 4 && inviteEntity.inviteUser != inviteEntity.invitedUser)
                        {
                            inviteEntity.coin = (inviteHistoryTable.Rows.Count + 1) * 100;
                            inviteusercoin += inviteEntity.coin;
                            applicationBusiness.ApplicationRegistration(inviteEntity);
                            applicationBusiness.Changecoin(inviteEntity, inviteusercoin);

                            // 完了画面ページへ遷移する
                            Response.Redirect("./Ivented_user.aspx");
                        }

                    }


                    Response.Redirect("./error_user.aspx");
                }
                else //招待されたことがある
                {
                    // エラーメッセージを表示する
                    error1.Visible = true;
                }

                
            }
        }

        protected void Button_Click2(object sender, EventArgs e)
        {
            InviteHistoryEntity inviteEntity = new InviteHistoryEntity();
            var a = (string)Session["id"];
            ApplicationBusiness applicationBusiness = new ApplicationBusiness();

            // applicationBusinessの申し込み結果の登録ロジック ApplicationRegistration に resultEntity を渡し実行

            inviteEntity.invitedUser = int.Parse(a); //自分のID獲得
            var mytable = applicationBusiness.Selecte(inviteEntity);
            if (mytable.Rows.Count == 4) //自分が４人招待した
            {
                var lottable = applicationBusiness.LotterySelect(inviteEntity);
                if (lottable.Rows.Count==0)
                {
                    Response.Redirect("./ResultPage.aspx");

                }
                else
                {
                    Response.Redirect("./Ivent_error.aspx");
                }
                
            }
        }

        protected void Button_Click3(object sender, EventArgs e)
        {
            Response.Redirect("./DetailsPage.aspx");
        }
    }
}
