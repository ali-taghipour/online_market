<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMP.Master" AutoEventWireup="true" CodeBehind="ListProduct.aspx.cs" Inherits="Shop.ListProduct" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPH" runat="server">
    <div class="container">
        <div class="row">
            <section class="item-list floater">

                <header class="com-md-12">
                    <img src="Content/img/archive.png" />
                    <span id="CategoryTitle" runat="server">جدیدترین محصولات</span>

                    <div class="float-left">
                        <select class="page-size">
                            <option value="12">12</option>
                            <option value="24">20</option>
                            <option value="12">40</option>
                            <option value="">همه</option>
                        </select>
                    </div>
                </header>

                <div id="ProductContainer" runat="server" class="list-of-products">
               <%-- <div class="col-md-3 col-sm-4">
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
                </div>

            </section>
            <div class="paginate" id="Pagination" runat="server">
                <%--<a href="#">1</a>
                <a href="#" class="selected">2</a>
                <a href="#">3</a>
                <a href="#">1024</a>--%>

            </div>
        </div>
    </div>

</asp:Content>
