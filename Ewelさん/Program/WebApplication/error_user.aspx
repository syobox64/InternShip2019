


<%@ Page Title="キャンペーンページ" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="error_user.aspx.cs" Inherits="WebApplication._CompletePage" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <div style="background-color:#ffffbc;font size="30";>
        <h2>招待コードが違います。確認してください！</h2>
        <h2>もしくは、紹介者の招待コードが４回使用されました。</h2>
        <div  align="center">
            <img src="Images/error.png" alt="サンプル"  width="150px" height="150px">
        </div>
    </div>
    <div style="background-color:#ffffbc;" align="right">
    <asp:Button ID="Button" runat="server" Text="TOPへ" onclick="Button_Click" class="sign-up-button"/>
    </div>
    
</asp:Content>
