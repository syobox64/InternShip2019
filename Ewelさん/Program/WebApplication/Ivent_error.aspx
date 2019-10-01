


<%@ Page Title="キャンペーンページ" Language="C#" MasterPageFile="~/RuritoSite.master" AutoEventWireup="true"
    CodeBehind="Ivent_error.aspx.cs" Inherits="WebApplication._CompletePage" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <div style="background-color:#ffffbc;font size="30";>
        <h2>参加ありがとうございました！</h2><br>
        <h2>イベントは終了しました。<h2>
        <div  align="center">
            <img src="Images/suriranka4.jpg" alt="サンプル"  width="400px" height="300px">
        </div>
    </div>
    <div style="background-color:#ffffbc;" align="right">
    <asp:Button ID="Button" runat="server" Text="TOPへ" onclick="Button_Click" class="sign-up-button"/>
    </div>
    
</asp:Content>
