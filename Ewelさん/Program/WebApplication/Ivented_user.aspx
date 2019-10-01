


<%@ Page Title="キャンペーンページ" Language="C#" MasterPageFile="~/RuritoSite.master" AutoEventWireup="true"
    CodeBehind="Ivented_user.aspx.cs" Inherits="WebApplication._CompletePage" %> 



<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <div style="background-color:#ffffbc;font size="30";>
        <h2>参加ありがとうございます！</h2>
        <h2>100コインゲット(^_-)ミ☆</h2>
        <div  align="center">
            <img src="Images/coin.png" alt="サンプル"  width="200px" height="200px">

        </div>
       </div>
    <div style="background-color:#ffffbc;" align="right">
    <!--<asp:Button  class="btn-square" onclick="Button_Click">TOPページ</asp:Button>-->
    <asp:Button ID="Button" runat="server" Text="TOPへ" onclick="Button_Click" class="sign-up-button"/>
    </div>
</asp:Content>
