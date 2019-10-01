using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using WebApplication.Entity;
using WebApplication.Dao;

namespace WebApplication.BusinessLogic
{
    public class LoginBusiness
    {
        // ログイン内容を登録するための処理を記述
        public void LoginRegistration(LoginEntity loginEntity)
        {
            // ログインビジネスロジック用のDao（DataAccessObject)のインスタンス LoginDao を生成
            LoginDao LoginDao = new LoginDao();

            // LoginDao のログイン内容をデータベースに登録する処理 LoginInsertExecute へ loginEntity を渡し実行
            LoginDao.LoginInsertExecute(loginEntity);

        }

        // 登録内容を抜き出すための処理を記述
        public DataTable Select(LoginEntity loginEntity)
        {
            // ログインビジネスロジック用のDao（DataAccessObject)のインスタンス LoginDao を生成
            LoginDao LoginDao = new LoginDao();

            // LoginDao のログイン内容をデータベースに登録する処理 LoginInsertExecute へ loginEntity を渡し実行
            return LoginDao.GetUserTable(loginEntity);

        }
    }
}