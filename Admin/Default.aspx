<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Shop.Admin.Default" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>ورود به پنل مدیریت</title>
    <link href="../Content/Admin/style/Login.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="c_login">


            <div class="c_logo">
                <img src="../content/admin/images/loguser.png" />
            </div>

            <div class="ttl">ورود کاربر</div>

            <div id="error" style="text-align: center" runat="server"></div>

            <div class="c_inp">
                <span>نام کاربری</span>
                <input id="username" maxlength="50" type="text" runat="server" />
            </div>


            <div class="c_inp">
                <span>رمز عبور</span>
                <input id="password" maxlength="50" type="password" runat="server" />
            </div>

            <div>
                <img style="width: 250px; margin: 10px auto 0; display: block" id="img_c" runat="server" src="../captcha.aspx" />
            </div>

            <div class="c_inp">
                <span>کد امنیتی:</span>
                <input type='text' id="sec_text" runat="server" class="wpcf7-text" />
            </div>

            <div class="foot">
                <div class="c_1btn">
                    <input id="btn_login" class="frm_btn" type="submit" value="ورود" runat="server" onserverclick="Login_ServerClick"  />
                    <div class="btn_over"></div>


                </div>


            </div>

        </div>

    </form>
</body>
</html>
