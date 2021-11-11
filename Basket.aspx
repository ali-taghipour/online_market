<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMP.Master" AutoEventWireup="true" CodeBehind="Basket.aspx.cs" Inherits="Shop.Basket" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="Content/Style/basket.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPH" runat="server">
    <div class="container">
        <div class="row">
            <form id="BasketForm" runat="server">
                <section class="page-wrapper">
                    <header class="page-title">
                        <img src="Content/img/cart2.png" />
                        <h2>سبد خرید</h2>
                    </header>
                    <div id="TableContainer" runat="server" class="table-container">
                        <%--<table class="cart-table">
                    <thead>
                        <tr>
                            <th>کالا</th>
                            <th>تعداد</th>
                            <th>قیمت واحد</th>
                            <th>قیمت کل</th>
                            <th>حذف</th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr>
                            <td>
                                <a href="Basket.aspx">
                                    <img src="Content/img/46.jpg" />
                                    <span>
                                         دزدگیر تمام هوشمند مدل A21 BC
                                    </span>
                                </a>
                            </td>
                            <td>
                                <input type="number" min="1" max="10" value="1" />
                            </td>
                            <td>25000 تومان</td>
                            <td><span>25000</span> <span>تومان</span></td>
                            <td>
                                <span class="remove">
                                    <img src="Content/img/close.png" />
                                </span>

                            </td>
                        </tr>
                        <tr>
                            <td>
                                <a href="Basket.aspx">
                                    <img src="Content/img/46.jpg" />

                                    دزدگیر تمام هوشمند مدل A21 BC
                                </a>
                            </td>
                            <td>
                                <input type="number" min="1" max="10" value="1" />
                            </td>
                            <td><span>25000</span> <span>تومان</span></td>
                            <td><span>25000</span> <span>تومان</span></td>
                            <td>
                                <span class="remove">
                                    <img src="Content/img/close.png" />
                                </span>

                            </td>
                        </tr>
                    </tbody>
                </table>--%>
                    </div>
                    <section class="basket-pricing" id="TotalPriceSection" runat="server">
                        <div>
                            جمع مبلغ کل:
                        </div>
                        <span>0</span>
                        <span>تومان</span>
                    </section>
                    <footer class="cart-info" id="PaymentSection" runat="server">
                        <input type="hidden" id="BasketId" name="BasketId" runat="server" />
                        <button type="submit" class="product-buy" runat="server" onserverclick="StartPayment_ServerClick">
                            <span>پرداخت آنلاین </span>
                            <img src="content/img/credit-card.png">
                        </button>
                        <div id="PaymentError" runat="server">
                        </div>
                    </footer>

                </section>
            </form>
        </div>
    </div>


    <script src="/Content/Script/Basket.js"></script>
</asp:Content>
