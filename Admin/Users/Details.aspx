<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMP.Master" AutoEventWireup="true" CodeBehind="Details.aspx.cs" Inherits="KargahProject.Admin.Users.Details" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>




<asp:Content ID="Content2" ContentPlaceHolderID="AdminCPH" runat="server">
    
        <section class="main_artc">
            <div class="error_div error" id="ErrorDiv" runat="server"></div>
            <fieldset name="AddProduct">
                <legend>جزییات کاربر :</legend>


                <div class="img-container text-center" id="UserImage" runat="server">
                </div>


                <div class="form-item">
                    <div id="FullName" runat="server"></div>
                    <span> نام کامل :</span>

                </div>
                <div class="form-item">
                    <div id="Username" runat="server"></div>
                    <span> نام کاربری :</span>

                </div>
                <div class="form-item">
                    <div id="Email" runat="server"></div>
                    <span> ایمیل :</span>

                </div>
                <div class="form-item">
                    <div id="Address" runat="server"></div>
                    <span> آدرس :</span>

                </div>
                <div class="form-item">
                    <div id="PostalCode" runat="server"></div>
                    <span> کد پستی :</span>

                </div>
                <div class="form-item">
                    <div id="UserType" runat="server"></div>
                    <span>نوع کاربر :</span>

                </div>

                <div class="form-item">
                    <div id="IsMale" runat="server"></div>
                    <span> جنسیت :</span>

                </div>

                <div class="form-item">
                    <div id="IsEnabled" runat="server"></div>

                    <span> وضعیت :</span>

                </div>
                
                <div class="form-item">
                    <div id="CreateDate" runat="server"></div>
                    <span> تاریخ ایجاد :</span>

                </div>
                

            </fieldset>
        </section>

</asp:Content>




<asp:Content ID="Content3" ContentPlaceHolderID="Scripts" runat="server">
</asp:Content>
