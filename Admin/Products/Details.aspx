<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMP.Master" AutoEventWireup="true" CodeBehind="Details.aspx.cs" Inherits="KargahProject.Admin.Products.Details" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="AdminCPH" runat="server">

    <form runat="server" id="Form" enctype="multipart/form-data">
        <section class="main_artc">
            <div class="error_div" id="ErrorDiv" runat="server"></div>
            <fieldset name="AddProduct">
                <legend>جزییات محصول :</legend>

                
                <div class="img-container text-center" id="ProductImage" runat="server">
                </div>


                <div class="form-item">
                    <div id="ProductTitle" runat="server"></div>
                    <span><span>*</span> نام محصول :</span>
                    <div class="error"></div>

                </div>
                <div class="form-item">
                    <div id="MainPrice" runat="server"></div>
                    <span><span>*</span> قیمت اصلی :</span>

                </div>
                <div class="form-item">
                    <div id="OffPrice" runat="server"></div>
                    <span>قیمت با تخفیف :</span>

                </div>

                <div class="form-item">
                    <div id="Inventory" runat="server"></div>
                    <span>موجودی انبار :</span>

                </div>

                <div class="form-item">
                    <span><span>*</span> دسته بندی :</span>
                    <div id="CategoryTitle" runat="server"></div>
                </div>

                <div class="form-item">
                    <div id="Summary" runat="server"></div>
                    <span>خلاصه محصول :</span>
                </div>

                <div class="form-item">
                    <div id="Description" runat="server"></div>
                    <span>توضیحات :</span>
                </div>

                <div class="form-item">
                    <div id="IsEnabled" runat="server"></div>
                    <span><span>*</span> نمایش در سایت :</span>

                </div>
                
                <div class="form-item">
                    <div id="AdminName" runat="server"></div>
                    <span> ادمین درج کننده:</span>

                </div>

                <div class="form-item">
                    <div id="CreateDate" runat="server"></div>
                    <span> تاریخ ایجاد :</span>

                </div>
            </fieldset>


        </section>
    </form>

</asp:Content>






<asp:Content ID="Content3" ContentPlaceHolderID="Scripts" runat="server">
</asp:Content>

