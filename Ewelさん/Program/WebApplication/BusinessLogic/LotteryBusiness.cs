using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using WebApplication.Entity;
using WebApplication.Dao;

namespace WebApplication.BusinessLogic
{
    public class LotteryBusiness
    {
        // 申し込み内容を登録するための処理を記述
        public void LotteryRegistration(int id,int plize)
        {
            // 申し込みビジネスロジック用のDao（DataAccessObject)のインスタンス applicationDao を生成
            ApplicationDao applicationDao = new ApplicationDao();

            // applicationDao のアンケート結果をデータベースに登録する処理 ApplicationInsertExecute へ resultEntity を渡し実行
            applicationDao.LotteryInsertExecute(id,plize);
        }

    }
}