using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using WebApplication.Entity;

namespace WebApplication.Dao
{
    //データベースへのアクセスを一元管理するオブジェクト。
    //必要に応じてデータベースへデータの照会、新規作成、更新、削除）を行う
    public class LoginDao : ConnectionDaoBase
    {
        // データベースへの接続プロパティを用意
        // Data Source :サーバ名やIP
        // Catalog     :データベース名
        // User Id     :データベース接続ユーザID
        // Password    :ユーザのログインパスワード
        //private const string connectionStringLogin = @"Data Source=192.168.183.128;Initial Catalog=SAMPLE;Persist Security Info=True;User Id=sa;Password=Welbox123";

        // データベースへ申し込み内容を保存するメソッド
        public void LoginInsertExecute(LoginEntity loginEntity)
        {
            // データベースへの登録処理
            using (SqlConnection connection = new SqlConnection()) // データベースへ接続すためのインスタンスconnectionを生成
            {
                // データベースへ接続するためのプロパティを設定。
                connection.ConnectionString = connectionString;
                // データベースへの接続を開始。以降データベースへの操作が可能となる。
                connection.Open();

                using (SqlCommand command = new SqlCommand()) // データベースへ処理の指示をだすインスタンスcommandを作成
                {
                    // commandが操作をする先の接続を設定
                    command.Connection = connection;
                    // 実際にデータベースに実行するSql文を設定
                    command.CommandText = LoginInsertSql;
                    // 入力項目毎にSqlに渡すパラメータのインスタンスを作成
                    SqlParameter name = new SqlParameter("name", SqlDbType.VarChar);     //nameをデータベース上の定義をvarchar型として

                    // 作成したパラメータに回答してもらった内容(引数:resultEntityの内容)を格納
                    name.Value = loginEntity.LoginName;

                    // 作成したパラメータのインスタンスを実際にcommandに追加
                    // これでSqlファイルの@～が変数が渡したパラメータに置き換えられる
                    command.Parameters.Add(name);

                    using (SqlTransaction transaction = connection.BeginTransaction()) // connectionよりトランザクション（データベースのセーブポイント）を作成
                    {
                        command.Transaction = transaction;
                        //commandに用意した処理を実行
                        try
                        {
                            //とりあえずデータベースに処理を流してみて    
                            command.ExecuteNonQuery();
                            //成功したら保存
                            transaction.Commit();
                        }
                        catch (Exception e)
                        {
                            //失敗したらトランザクションを作ったところまで戻す
                            if (transaction != null)
                            {
                                transaction.Rollback();
                            }
                            //発生したException(エラー)を呼び出し元に返す
                            throw;
                        }
                    }
                }
            }
        }

        // データベースから情報を取得するメソッド
        public DataTable GetUserTable(LoginEntity loginEntity)
        {
            var table = new DataTable();
            // データベースから抜出処理
            using (SqlConnection connection = new SqlConnection()) // データベースへ接続すためのインスタンスconnectionを生成
            {
                // データベースへ接続するためのプロパティを設定。
                connection.ConnectionString = connectionString;
                // データベースへの接続を開始。以降データベースへの操作が可能となる。
                connection.Open();

                using (SqlCommand command = new SqlCommand()) // データベースへ処理の指示をだすインスタンスcommandを作成
                {
                    // commandが操作をする先の接続を設定
                    command.Connection = connection;
                    // 実際にデータベースに実行するSql文を設定
                    command.CommandText = LoginSelectSql;
                    // 入力項目毎にSqlに渡すパラメータのインスタンスを作成
                    SqlParameter name = new SqlParameter("name", SqlDbType.VarChar);     //nameをデータベース上の定義をvarchar型として

                    // 作成したパラメータに回答してもらった内容(引数:resultEntityの内容)を格納
                    name.Value = loginEntity.LoginName;

                    // 作成したパラメータのインスタンスを実際にcommandに追加
                    // これでSqlファイルの@～が変数が渡したパラメータに置き換えられる
                    command.Parameters.Add(name);

                    using (SqlTransaction transaction = connection.BeginTransaction()) // connectionよりトランザクション（データベースのセーブポイント）を作成
                    {
                        command.Transaction = transaction;
                        //commandに用意した処理を実行
                        try
                        {
                            //とりあえずデータベースに処理を流してみて    
                            //command.ExecuteNonQuery();
                            //成功したら保存
                            //transaction.Commit();
                            var adapter = new SqlDataAdapter(command);
                            adapter.Fill(table);
                        }
                        catch (Exception e)
                        {
                            //失敗したらトランザクションを作ったところまで戻す
                            if (transaction != null)
                            {
                                transaction.Rollback();
                            }
                            //発生したException(エラー)を呼び出し元に返す
                            throw;
                        }
                    }
                }
            }

            return table;
        }


        //実際にデータベースへ操作をするためのSql文
        private string LoginInsertSql =
@"
INSERT INTO	dbo.login_result
(
		login_name,
        login_time
)
VALUES
(
		@name
	,	GETDATE()
)
";

        //実際にデータベースへ操作をするためのSql文
        //抜出
        private string LoginSelectSql =
@"
SELECT * FROM dbo.student_test
where id = @name
";
    }
}