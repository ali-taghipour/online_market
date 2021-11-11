<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMP.Master" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="Shop.Admin.Dashboard" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="AdminCPH" runat="server">
    <section class="main_artc">
        <div class="link_holder">
             <a href="/admin/products" id="CountKalaHolder" runat="server">
                <i class="entypo-archive"></i>
                <span id="AllProductCount" runat="server"></span>
                <h4>تعداد محصولات</h4>
            </a>
            <a href="/admin/Users">
                <i class="entypo-user"></i>
                <span id="AllUserCount" runat="server"></span>
                <h4>تعداد کاربران</h4>
            </a>
            <a href="/admin/Comments">
                <i class="entypo-comment"></i>
                <span  id="UnreadCommentCount" runat="server"></span>
                <h4>نظرات بررسی نشده</h4>
            </a>
        </div>

    </section>
</asp:Content>
