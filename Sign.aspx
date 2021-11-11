<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMP.Master" AutoEventWireup="true" CodeBehind="Sign.aspx.cs" Inherits="Shop.Sign" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="Content/Style/sign.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPH" runat="server">

    <form id="MainForm" runat="server">
        <div class="container">
            <div class="row">

                <div class="sign-form">
                    <div class="sign-form-login show" runat="server" id="LoginForm">
                        <fieldset class="form-group">
                            <legend>ورود به سایت</legend>

                            <div id="LoginError" runat="server" class="">
                            </div>
                            <%--<div class="show-success">
                                ورود با موفقیت انجام شد ، در حال هدایت به صفحه اصلی...
                            </div>--%>

                            <div class="input-text">
                                <label>نام کاربری :</label>
                                <input type="text" id="LoginUsername" runat="server" placeholder="نام کاربری خود را وارد کنید" />
                            </div>

                            <div class="input-text">
                                <label>کلمه عبور :</label>
                                <input type="password" id="LoginPassword" runat="server" placeholder="کلمه عبور خود را وارد کنید" />
                            </div>

                            <%--<div class="captcha-holder">
                                <img id="img_c" runat="server" src="../captcha.aspx" />
                                <input type="text" placeholder="کد امنیتی" />
                            </div>--%>


                            <div class="captcha-holder">
                                <div class="g-recaptcha" id="LoginCaptcha" data-sitekey="6Lddd5MUAAAAALbqtymbkPTP1xnYF5ClczlUWM9i"></div>
                            </div>



                            <div class="text-center">
                                <input type="submit" value="ورود به سایت" class="btn btn-primary" runat="server" onserverclick="Login_ServerClick" />
                            </div>

                            <div class="form-text">
                                عضو نیستید؟ <a class="register-form-btn">ثبت نام کنید</a>
                            </div>
                        </fieldset>
                    </div>




                    <div class="sign-form-register" id="RegisterForm" runat="server">
                        <fieldset class="form-group">
                            <legend>ثبت نام</legend>

                            <div class=""  id="RegisterError" runat="server">
                            </div>
                            <%--<div class="show-success">
                                ثبت نام با موفقیت انجام شد
                            </div>--%>

                            <div class="input-text">
                                <label>نام کاربری :</label>
                                <input type="text" id="RegisterUsername" runat="server" placeholder="نام کاربری را وارد کنید" />
                            </div>

                            <div class="input-text">
                                <label>کلمه عبور :</label>
                                <input type="password" id="RegisterPassword" runat="server" placeholder="کلمه عبور را وارد کنید" />
                            </div>

                            <div class="input-text">
                                <label>تکرار کلمه عبور :</label>
                                <input type="password" id="RegisterRePassword" runat="server" placeholder="کلمه عبور را وارد کنید" />
                            </div>

                            <%--<div class="captcha-holder">
                                <img id="img1" runat="server" src="../captcha.aspx" />
                                <input type="text" placeholder="کد امنیتی" />
                            </div>--%>
                            
                            <div class="captcha-holder">
                                <div class="g-recaptcha" id="RegisterCaptcha" data-sitekey="6Lddd5MUAAAAALbqtymbkPTP1xnYF5ClczlUWM9i"></div>
                            </div>

                            <div class="text-center">
                                <input type="submit" value="ثبت نام" class="btn btn-primary" runat="server" onserverclick="Register_ServerClick" />
                            </div>

                            <div class="form-text">
                                قبلا عضو شده اید؟ <a class="login-form-btn">وارد شوید</a>
                            </div>
                        </fieldset>
                    </div>
                </div>

            </div>
        </div>

    </form>


    <script src='https://www.google.com/recaptcha/api.js?hl=fa'></script>

    <script>
        $(document).on("click", ".login-form-btn", function () {

            $(".sign-form-login").addClass("show");
            $(".sign-form-register").removeClass("show");

        })
        $(document).on("click", ".register-form-btn", function () {

            $(".sign-form-register").addClass("show");
            $(".sign-form-login").removeClass("show");

        })
    </script>
</asp:Content>
