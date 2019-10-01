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
    public partial class _CompletePage : System.Web.UI.Page
    {
        protected void error_user(object sender, EventArgs e)
        {

        }
        protected void Button_Click(object sender, EventArgs e)
        {
            // トップページへ遷移する
            Response.Redirect("./ApplicationPage.aspx");
        }
    }

}
