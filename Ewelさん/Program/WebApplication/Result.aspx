


<%@ Page Title="キャンペーンページ" Language="C#" MasterPageFile="~/RuritoSite.master" AutoEventWireup="true"
    CodeBehind="Result.aspx.cs" Inherits="WebApplication._Result" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <div style="background-color:#ffffbc;font size="30";>
        <h2>結果</h2><br>
        <h2 style="margin:1.0em 2em;background: #dfefff;box-shadow: 0px 0px 0px 5px #dfefff; border: dashed 1px #96c2fe; padding: 0.2em 0.5em; color: #454545;" >【　1等　】スリランカ旅行</h2>
       <a href="#" class="btn-top-radius">景品の詳細</a>
        <div  align="center">
            <img src="Images/suriranka4.jpg" alt="サンプル"  width="400px" height="300px">
        </div>
    </div>
    <div style="background-color:#ffffbc;" align="right">
    <asp:Button ID="Button" runat="server" Text="TOPへ" onclick="Button_Click" class="sign-up-button"/>
    </div>
    
</asp:Content>
