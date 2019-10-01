

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="WebApplication.Login" %>


<head>

	<!--  css  include -->
	<link rel="stylesheet" type="text/css" href="./Css/login.css">

	<!--  script  include -->
	<script type="text/javascript" src="./Scripts/jquery.js"></script>
	
	
	<script>
        window.onload = function () {

        }
	</script>


</head>

<body>
    <form id="form1" runat="server"  class="sign-up">
    <h3 class="sign-up-title">ログイン</h3>
    <p>
        <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
        
        <asp:TextBox ID="TextBox1" runat="server" class="sign-up-input" placeholder="IDを入力してください"></asp:TextBox>
        
    </p>


    <p>
        <font color="#ff0000"><asp:Label ID="error1" runat="server" Text="未入力です。 "  Visible="False"></asp:Label></font>
    </p>


    <p>
        <asp:Button ID="Button" runat="server" Text="ログイン" onclick="Button_Click" class="sign-up-button"
             />
    </p>
        
</form>
    <asp:Label ID="Label2" runat="server" Text=""></asp:Label>
    <a>DBログイン登録情報</a>
    <ul>
        <li>ID：10001　名前：testuser</li>
        <li>ID：10002　名前：user01</li>
        <li>ID：10003　名前：user02</li>
        <li>ID：10004　名前：user03</li>
        <li>ID：10005　名前：user04</li>
        <li>ID：10006　名前：user05</li>
        <li>ID：10007　名前：user06</li>
    </ul>

</body>