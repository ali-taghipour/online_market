<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMP.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Shop.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="Content/Style/home.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPH" runat="server">
    <section class="container">
        <div class="row">
            <div class="slide-holder">
                <div class="slider" id="Slider" runat="server">
                    <%--<div class="show">
                        <img src="Content/img/46.jpg" />
                        <span>
                            مدرسه ای با جدیدترین تکنولوژی روز دنیا 
                            <a class="btn">
                                بیشتر بخوانید
                            </a>
                        </span>
                    </div>--%>
                </div>
                <ul class="slide-nav"></ul>
            </div>
        </div>
        <div class="row">
            <section class="item-list floater" id="ProductContainer" runat="server">

                <header class="com-md-12">
                    <img src="Content/img/archive.png" />
                    <span>جدیدترین محصولات</span>

                    <a href="/ListProduct.aspx">مشاهده همه محصولات</a>
                </header>

                <%--<div class="col-md-3 col-sm-4">
                    <a href="#">
                        <figure>
                            <img src="Content/img/46.jpg" />
                        </figure>
                        <h2>عنوان محصول الکترونیکی</h2>
                    </a>
                    <div class="price-holder">
                        <span class="item-off-price">35,000</span>
                        <span class="item-price">25,000  <span>تومان</span></span>
                    </div>
                    <div class="add-basket">
                        <img src="Content/img/icons8-buy-48.png" />
                    </div>
                </div>--%>
            </section>
        </div>
    </section>
    <script src="Content/Script/slider.js"></script>
</asp:Content>
