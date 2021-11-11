<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMP.Master" AutoEventWireup="true" CodeBehind="ShowProduct.aspx.cs" Inherits="Shop.ShowProduct" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <link href="Content/Style/more.css" rel="stylesheet" />

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPH" runat="server">
    <article class="container product-details">
        <div class="row">
            <div class="col-md-5" id="PictureSection" runat="server">
                <%--<div class="product-slider">
                    <figure class="product-slider-show">
                        <img src="Content/img/46.jpg"/>
                    </figure>
                    <ul class="product-slider-nav">
                        <li><img src="Content/img/46.jpg" /></li>
                        <li><img src="Content/img/backhead.jpg" /></li>
                        <li><img src="Content/img/46.jpg" /></li>
                        <li><img src="Content/img/46.jpg" /></li>
                    </ul>
                </div>--%>
            </div>
            <div class="col-md-7" id="SummarySection" runat="server">
                <header class="product-header">
                    <h1 id="ProductTitle" runat="server"> </h1>
                    <p id="Summary" runat="server">
                    </p>
                </header>
                <div class="product-pricing">
                    <div class="add-basket" id="AddToBasketButton" runat="server">
                        <span>افزودن به سبد خرید</span>
                        <img src="Content/img/icons8-buy-48.png" />
                    </div>
                    <div class="price-holder">
                        <span class="item-off-price" id="MainPrice" runat="server"></span>
                        <span class="item-price" id="FinalPrice" runat="server">  <span>تومان</span></span>
                    </div>
                </div>
                <div class="product-like">
                    <span class="like-btn" id="LikeButton" runat="server">
                        <%--<img src="Content/img/heart-outline.png" />--%>
                        <!--<img class="like-btn" src="Content/img/heart.png" />-->
                    </span>
                    <span class="like-count" id="LikeCount" runat="server"></span>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="product-description-title">
                توضیحات محصول
            </div>
            <section class="product-description" id="Description" runat="server">
            </section>
        </div>
        <footer class="comment-holder">
            <div class="row">
                <div class="col-md-5">
                    <%--<div class="sign-needed">
                        برای ارسال نظر ابتدا باید با <a href="#">حساب کاربری</a> خود وارد شوید
                    </div>--%>
                    <fieldset class="form-group">
                        <legend>افزودن نظر جدید</legend>

                        <div class="show-error comment-error hide">
                            کد امنیتی اشتباه است!
                        </div>
                        <%--<div class="show-success">
                            پیام شما با موفقیت ارسال شد و بعد از تایید نمایش داده می شود
                        </div>--%>


                        <div class="input-text" id="CommentFullName" runat="server">
                            <label>نام و نام خانوادگی :</label>
                            <input type="text" class="FullName" placeholder="نام خود را وارد کنید" />
                        </div>

                        <div class="input-text" id="CommentEmail" runat="server">
                            <label>ایمیل :</label>
                            <input type="email" class="Email" placeholder="ایمیل خود را وارد کنید" />
                        </div>

                        <div class="input-text">
                            <label>نظر شما :</label>
                            <textarea class="Text" placeholder="نظر خود را بنویسید"></textarea>
                        </div>
                        <div class="captcha-holder">
                            <img id="img_c" runat="server" src="../captcha.aspx" />
                            <input class="Code" type="text" placeholder="کد امنیتی" />
                        </div>
                        <div class="comment-btn">
                            <input type="hidden" class="ProductId" id="ProductId" runat="server"/>
                            <input type="hidden" class="UserId" id="UserId" runat="server"/>
                            <input type="submit" value="ارسال نظر" class="btn btn-primary" onclick="SendComment()" />
                        </div>
                    </fieldset>
                </div>
                <div class="col-md-7" id="CommentSection" runat="server">
                    <%--<ul class="product-comment">
                        <li>
                            <div>
                                <span>محمد جوانی</span>
                                <span>3 روز پیش</span>
                            </div>
                            <div>
                                این محصول فوق العادست
                            </div>
                        </li>
                        <li>
                            <div>
                                <span>محمد جوانی</span>
                                <span>3 روز پیش</span>
                            </div>
                            <div>
                                این محصول فوق العادست
                            </div>
                        </li>
                        <li>
                            <div>
                                <span>محمد جوانی</span>
                                <span>3 روز پیش</span>
                            </div>
                            <div>
                                این محصول فوق العادست
                            </div>
                        </li>
                    </ul>--%>
                </div>
            </div>
        </footer>
    </article>

</asp:Content>
