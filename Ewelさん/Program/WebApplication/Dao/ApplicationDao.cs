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
    public class ApplicationDao : ConnectionDaoBase
    {
        // データベースへの接続プロパティを用意
        // Data Source :サーバ名やIP
        // Catalog     :データベース名
        // User Id     :データベース接続ユーザID
        // Password    :ユーザのログインパスワード
        //private const string connectionString = @"Data Source=192.168.183.128;Initial Catalog=SAMPLE;Persist Security Info=True;User Id=sa;Password=Welbox123";
        
        // データベースへ申し込み内容を保存するメソッド
        public void ApplicationInsertExecute(InviteHistoryEntity inviteEntity)
        {
            // データベースへの登録処理
            using (SqlConnection connection = new SqlConnection()) // データベースへ接続すためのインスタンスconnectionを生成
            {
                // データベースへ接続するためのプロパティを設定。
                connection.ConnectionString = connectionString;
                // データベースへの接続を開始。以降データベースへの操作が可能となる。
                connection.Open();

                using(SqlCommand command = new SqlCommand()) // データベースへ処理の指示をだすインスタンスcommandを作成
                {
                    // commandが操作をする先の接続を設定
                    command.Connection = connection;
                    // 実際にデータベースに実行するSql文を設定
                    command.CommandText = ApplicationInsertSql;
                    // 入力項目毎にSqlに渡すパラメータのインスタンスを作成
                    SqlParameter inputuserid    = new SqlParameter("inputUserId", SqlDbType.Int);     //dateをデータベース上の定義をvarchar型として
                    SqlParameter loginuserid    = new SqlParameter("loginUserId", SqlDbType.Int);     //nameをデータベース上の定義をvarchar型として
                    SqlParameter coin           = new SqlParameter("coin", SqlDbType.Int);     //nameをデータベース上の定義をvarchar型として

                    // 作成したパラメータに回答してもらった内容(引数:resultEntityの内容)を格納
                    inputuserid.Value = inviteEntity.inviteUser;
                    loginuserid.Value = inviteEntity.invitedUser;
                    coin.Value        = inviteEntity.coin;

                    // 作成したパラメータのインスタンスを実際にcommandに追加
                    // これでSqlファイルの@～が変数が渡したパラメータに置き換えられる
                    command.Parameters.Add(inputuserid);
                    command.Parameters.Add(loginuserid);
                    command.Parameters.Add(coin);

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
                        }catch(Exception e)
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


        public DataTable GetUserTables(int inviteid)
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
                    command.CommandText = ApplicationSelectSql;
                    // 入力項目毎にSqlに渡すパラメータのインスタンスを作成
                    SqlParameter id = new SqlParameter("id", SqlDbType.Int);     //nameをデータベース上の定義をvarchar型として

                    // 作成したパラメータに回答してもらった内容(引数:resultEntityの内容)を格納
                    id.Value = inviteid;

                    // 作成したパラメータのインスタンスを実際にcommandに追加
                    // これでSqlファイルの@～が変数が渡したパラメータに置き換えられる
                    command.Parameters.Add(id);

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

        public DataTable GetMyTables(InviteHistoryEntity inviteEntity)
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
                    command.CommandText = MySelectSql;
                    // 入力項目毎にSqlに渡すパラメータのインスタンスを作成
                    SqlParameter id = new SqlParameter("id", SqlDbType.Int);     //nameをデータベース上の定義をvarchar型として

                    // 作成したパラメータに回答してもらった内容(引数:resultEntityの内容)を格納
                    id.Value = inviteEntity.invitedUser;

                    // 作成したパラメータのインスタンスを実際にcommandに追加
                    // これでSqlファイルの@～が変数が渡したパラメータに置き換えられる
                    command.Parameters.Add(id);

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

        public DataTable GetMyTable(InviteHistoryEntity inviteEntity)
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
                    command.CommandText = MySelectsSql;
                    // 入力項目毎にSqlに渡すパラメータのインスタンスを作成
                    SqlParameter id = new SqlParameter("id", SqlDbType.Int);     //nameをデータベース上の定義をvarchar型として

                    // 作成したパラメータに回答してもらった内容(引数:resultEntityの内容)を格納
                    id.Value = inviteEntity.invitedUser;

                    // 作成したパラメータのインスタンスを実際にcommandに追加
                    // これでSqlファイルの@～が変数が渡したパラメータに置き換えられる
                    command.Parameters.Add(id);

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

        public DataTable GetLotteryHistory(InviteHistoryEntity inviteEntity)
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
                    command.CommandText = LotterySelectsSql;
                    // 入力項目毎にSqlに渡すパラメータのインスタンスを作成
                    SqlParameter id = new SqlParameter("id", SqlDbType.Int);     //nameをデータベース上の定義をvarchar型として

                    // 作成したパラメータに回答してもらった内容(引数:resultEntityの内容)を格納
                    id.Value = inviteEntity.invitedUser;

                    // 作成したパラメータのインスタンスを実際にcommandに追加
                    // これでSqlファイルの@～が変数が渡したパラメータに置き換えられる
                    command.Parameters.Add(id);

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
        public DataTable GetUserInviteHistory(int inputUserId)
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
                    command.CommandText = InviteHistorySelectSql;
                    // 入力項目毎にSqlに渡すパラメータのインスタンスを作成
                    SqlParameter userId = new SqlParameter("userId", SqlDbType.Int);     //nameをデータベース上の定義をvarchar型として

                    // 作成したパラメータに回答してもらった内容(引数:resultEntityの内容)を格納
                    userId.Value = inputUserId;

                    // 作成したパラメータのインスタンスを実際にcommandに追加
                    // これでSqlファイルの@～が変数が渡したパラメータに置き換えられる
                    command.Parameters.Add(userId);

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

        public void ChangeCoin(InviteHistoryEntity inviteEntity, int inviteusercoin)
        {
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
                    command.CommandText = CoinUpdateSql;
                    // 入力項目毎にSqlに渡すパラメータのインスタンスを作成
                    SqlParameter coin = new SqlParameter("coin", SqlDbType.Int);     //nameをデータベース上の定義をvarchar型として
                    SqlParameter inviteUser = new SqlParameter("inviteUser", SqlDbType.Int);     //nameをデータベース上の定義をvarchar型として

                    // 作成したパラメータに回答してもらった内容(引数:resultEntityの内容)を格納
                    coin.Value = inviteusercoin;
                    inviteUser.Value = inviteEntity.inviteUser;

                    // 作成したパラメータのインスタンスを実際にcommandに追加
                    // これでSqlファイルの@～が変数が渡したパラメータに置き換えられる
                    command.Parameters.Add(coin);
                    command.Parameters.Add(inviteUser);

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
                            //var adapter = new SqlDataAdapter(command);
                            // adapter.Fill(table);
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

        public void LotteryInsertExecute(int id,int plize)
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
                    command.CommandText = LotteryInsertSql;
                    // 入力項目毎にSqlに渡すパラメータのインスタンスを作成
                    SqlParameter user_id = new SqlParameter("user_id", SqlDbType.Int);     //dateをデータベース上の定義をvarchar型として
                    SqlParameter plize_id = new SqlParameter("plize_id", SqlDbType.Int);     //nameをデータベース上の定義をvarchar型として

                    // 作成したパラメータに回答してもらった内容(引数:resultEntityの内容)を格納
                    user_id.Value = id;
                    plize_id.Value = plize;

                    // 作成したパラメータのインスタンスを実際にcommandに追加
                    // これでSqlファイルの@～が変数が渡したパラメータに置き換えられる
                    command.Parameters.Add(user_id);
                    command.Parameters.Add(plize_id);

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


        //実際にデータベースへ操作をするためのSql文
        private string LotteryInsertSql =
@"
INSERT INTO	dbo.lottery_history
(
		lottery_name
	,	lottery_plize
    ,   datetime
)
VALUES
(
		@user_id
	,	@plize_id
    ,   getdate()
)
";

        private string ApplicationInsertSql =
@"
INSERT INTO	dbo.invite_history
(
		invite_user
	,	invited_user
    ,   datetime
    ,   coin
)
VALUES
(
		@inputuserid
	,	@loginuserid
    ,   getdate()
    ,   @coin
)
";

        private string ApplicationSelectSql =
@"
SELECT * FROM dbo.student_test
where id = @id
";

        private string MySelectSql =
@"
SELECT * FROM dbo.invite_history
where invited_user = @id
";

        private string MySelectsSql =
@"
SELECT * FROM dbo.invite_history
where invite_user = @id
";

        private string LotterySelectsSql =
@"
SELECT * FROM dbo.lottery_history
where lottery_name = @id
";

        private string InviteHistorySelectSql =
@"
SELECT * FROM dbo.invite_history
where invite_user = @userId
";
        private string CoinUpdateSql =
@"
UPDATE	dbo.student_test
SET coin = @coin
where id = @inviteUser
";

    }
}