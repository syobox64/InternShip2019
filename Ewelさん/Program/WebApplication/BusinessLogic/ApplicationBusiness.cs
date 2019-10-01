using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using WebApplication.Entity;
using WebApplication.Dao;

namespace WebApplication.BusinessLogic
{
    public class ApplicationBusiness
    {
        // 申し込み内容を登録するための処理を記述
        public void ApplicationRegistration(InviteHistoryEntity inviteEntity)
        {
            // 申し込みビジネスロジック用のDao（DataAccessObject)のインスタンス applicationDao を生成
            ApplicationDao applicationDao = new ApplicationDao();

            // applicationDao のアンケート結果をデータベースに登録する処理 ApplicationInsertExecute へ resultEntity を渡し実行
            applicationDao.ApplicationInsertExecute(inviteEntity);
        }

        public DataTable Selects(int inviteid)
        {
            // ログインビジネスロジック用のDao（DataAccessObject)のインスタンス LoginDao を生成
            ApplicationDao ApplicationDao = new ApplicationDao();

            // LoginDao のログイン内容をデータベースに登録する処理 LoginInsertExecute へ loginEntity を渡し実行
            return ApplicationDao.GetUserTables(inviteid);

        }

        public DataTable Selected(InviteHistoryEntity inviteEntity)
        {
            // ログインビジネスロジック用のDao（DataAccessObject)のインスタンス LoginDao を生成
            ApplicationDao ApplicationDao = new ApplicationDao();

            // LoginDao のログイン内容をデータベースに登録する処理 LoginInsertExecute へ loginEntity を渡し実行
            return ApplicationDao.GetMyTables(inviteEntity);

        }

        public DataTable Selecte(InviteHistoryEntity inviteEntity)
        {
            // ログインビジネスロジック用のDao（DataAccessObject)のインスタンス LoginDao を生成
            ApplicationDao ApplicationDao = new ApplicationDao();

            // LoginDao のログイン内容をデータベースに登録する処理 LoginInsertExecute へ loginEntity を渡し実行
            return ApplicationDao.GetMyTable(inviteEntity);

        }

        public DataTable LotterySelect(InviteHistoryEntity inviteEntity)
        {
            // ログインビジネスロジック用のDao（DataAccessObject)のインスタンス LoginDao を生成
            ApplicationDao ApplicationDao = new ApplicationDao();

            // LoginDao のログイン内容をデータベースに登録する処理 LoginInsertExecute へ loginEntity を渡し実行
            return ApplicationDao.GetLotteryHistory(inviteEntity);

        }

        public DataTable InvitehistorySelect(int inputUserId)
        {
            // ログインビジネスロジック用のDao（DataAccessObject)のインスタンス LoginDao を生成
            ApplicationDao ApplicationDao = new ApplicationDao();

            // Invite_history テーブルより入力したユーザーの招待履歴の数を取得
            return ApplicationDao.GetUserInviteHistory(inputUserId);

        }

        public void Changecoin(InviteHistoryEntity inviteEntity,int inviteusercoin)
        {
            // ログインビジネスロジック用のDao（DataAccessObject)のインスタンス LoginDao を生成
            ApplicationDao ApplicationDao = new ApplicationDao();

            // inviteEntityの値を元にコインの変更を行う
            ApplicationDao.ChangeCoin(inviteEntity, inviteusercoin);
        }

        public void LotteryRegistration(InviteHistoryEntity inviteEntity)
        {
            // 申し込みビジネスロジック用のDao（DataAccessObject)のインスタンス applicationDao を生成
            ApplicationDao applicationDao = new ApplicationDao();

            // applicationDao のアンケート結果をデータベースに登録する処理 ApplicationInsertExecute へ resultEntity を渡し実行
            applicationDao.ApplicationInsertExecute(inviteEntity);
        }

    }
}