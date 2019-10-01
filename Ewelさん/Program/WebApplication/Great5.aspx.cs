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
    public partial class _Great5 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void Button_Click(object sender, EventArgs e)
        {
            // トップページへ遷移する
            Response.Redirect("./ApplicationPage.aspx");
        }
    }
}
