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
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        //「ログイン」ボタンを押下した時の処理
        protected void Button_Click(object sender, EventArgs e)
        {
            error1.Visible = false;


            // ログイン内容を保持するオブジェクトのインスタンスを生成
            LoginEntity loginEntity = new LoginEntity();

            // 「ログインID」欄が未入力だった時の処理
            if (!TextBox1.Text.Trim().Equals(""))
            {
                // ログイン内容をresultEntityのメンバに格納
                loginEntity.LoginName = TextBox1.Text;
            }
            else
            {
                // エラーメッセージを表示する
                error1.Visible = true;
            }

            // エラーメッセージが表示されていない = 入力欄に不備が無い
            if (!error1.Visible)
            {
                // ログイン画面のBusinessLogic（計算・加工処理・実施）のインスタンス applicationBusiness を生成
                LoginBusiness loginBusiness = new LoginBusiness();

                // applicationBusinessの申し込み結果の登録ロジック ApplicationRegistration に resultEntity を渡し実行
                loginBusiness.LoginRegistration(loginEntity);
                var table = loginBusiness.Select(loginEntity);
                if (table.Rows.Count != 0) //入力した人が存在している
                {
                    string test = (string)table.Rows[0][1];


                    string textValue = TextBox1.Text;

                    //セッションに値格納
                    Session["id"] = textValue;

                    //var a = (string)Session["id"];//設定した値によってキャストが必要
                    //Label2.Text = a;



                    // トップページへ遷移する
                    Response.Redirect("./Top.aspx");
                }
            }
        }


    }
}