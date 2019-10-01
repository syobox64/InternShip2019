<%@ Page Title="キャンペーンページ" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="ApplicationPage.aspx.cs" Inherits="WebApplication._Default" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>


<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <div style="background-color:#ffffbc;">
     
        <tr>
         <div class="q">
             <td><img id="img1" src="images/Event.png" style="border-width:0px;" width="871" height="284" alt="画像"/></td>
         </div>
        </tr> 

        <tr>
            <div class="q">
              <td><img id="img2" src="images/Content.png" style="border-width:0px;" width="871" height="284" alt="画像"/></td>
            </div>
        </tr>

        <tr>
            <td><div class="sample" style="background-image:url(images/Write.png);height:413px;"></td>
                <!--<img id="img3" src="images/Write.png" style="border-width:0px;" width="871" height="413" alt="画像"/>-->
                    
                    <div style="padding:150px 200px 100px 170px; ">
                        <!--<p class="sample" style="margin-top:100px">-->
                        <div style="margin:0 auto;">
                            <!--<span style="background-color: #0099FF;"> -->
                            <asp:TextBox ID="TextBox1" runat="server" Font-Size="30"></asp:TextBox>
                            <asp:Button ID="Button1" runat="server" Text="決定" onclick="Button_Click" Font-Size="30" />
                            <!--</span>-->
                        </div>
                       <!--</p>-->
                        <p>
                            <font color="#ff0000"><asp:Label ID="error1" runat="server" Text="既に投票済みです。" Visible="False"></asp:Label></font>
                       </p>
                       <p>
                           <font size="15">ユーザーID：<asp:Label ID="Label2" runat="server" Text=""></asp:Label></font>
                       </p>
                   </div>
              </div>
        <p>
            
               <div style="padding:0px 0px 0px 800px; "><asp:Button ID="Button3" runat="server" Text="詳細画面へ" onclick="Button_Click3" Font-Size="10" /></div>
        </p>

     <tr>
            <td><div class="q" style="background-image:url(images/lottery.png);height:550px;width=871px;background-repeat:no-repeat;background-position-x:center;"></td>
                    <div style="padding:450px 0px 0px 350px; ">
                        <div style="margin:0 auto;">
                           <font size="15">〇人招待  </font>
                            <asp:Button ID="Button2" runat="server" Text="抽選へ" onclick="Button_Click2" Font-Size="30" />
                            <!--</span>-->
                        </div>
                      </div>
                 </div>
     </tr>

     <tr>
             <td><div class="q" style="background-image:url(images/Lottery_detail.png);height:500px;background-repeat:no-repeat;background-position-x:center;"></td></div>
        </tr> 
    </div>
</asp:Content>
