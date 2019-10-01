<%@ Page Title="キャンペーンページ" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="DetailsPage.aspx.cs" Inherits="WebApplication._DetailsPage" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <div style="background-color:#ffffbc;padding-bottom:100px;text-align:center;">
     <tr>
        <td><img id="img1" src="images/Event.png" style="border-width:0px;" width="871" height="284" alt="画像"/></td>
     </tr>
     <tr>
         <td>
             <div style="background-image:url(images/Detail.png);width:605px; height:315px; margin-left:150px;" >
                   <div style="padding:1px 0px 0px 0px; ">
                    <font size="6">
                    <h1>諸注意</h1></font>
                    <font size="5">
                   
                    ※紹介できるのは4人までです。<br />
                    ※抽選が行えるのはあなたに付与されたコードを<br />4人の方が入力した後です。<br />
                    ※紹介された方は紹介する側の何人目に限らず<br />100コインしか付与されません。
                        </font>
                    </div>
                <p style="padding-top:50px;color:#000;font-size:25px;">
                </p>
             </div>
         </td>
     </tr>
        <tr>
            <td><asp:Button ID="Button" runat="server" Text="TOPへ" onclick="Button_Click" Font-Size="30" class="sign-up-button"/></td>
        </tr>
    </div>
</asp:Content>
