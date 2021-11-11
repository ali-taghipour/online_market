<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMP.Master" AutoEventWireup="true" CodeBehind="Details.aspx.cs" Inherits="KargahProject.Admin.Slides.Details" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    
</asp:Content>



<asp:Content ID="Content2" ContentPlaceHolderID="AdminCPH" runat="server">
    
        <section class="main_artc">
            <div class="error_div error" id="ErrorDiv" runat="server"></div>
            <fieldset name="AddProduct">
                <legend>جزییات اسلایدر :</legend>


                <div class="img-container text-center" id="SlideImage" runat="server">
                </div>


                <div class="form-item">
                    <div id="SlideTitle" runat="server"></div>
                    <span> عنوان :</span>

                </div>
                <div class="form-item">
                    <div id="Link" runat="server"></div>
                    <span> لینک :</span>

                </div>
                <div class="form-item">
                    <div id="Order" runat="server"></div>
                    <span>اولویت :</span>

                </div>

                <div class="form-item">
                    <div id="StartDate" runat="server"></div>
                    <span> تاریخ شروع :</span>

                </div>
                <div class="form-item">
                    <div id="EndDate" runat="server"></div>
                    <span> تاریخ پایان :</span>

                </div>

                <div class="form-item">
                    <div id="IsEnabled" runat="server"></div>

                    <span> وضعیت :</span>

                </div>
                
                <div class="form-item">
                    <div id="CreateDate" runat="server"></div>
                    <span> تاریخ ایجاد :</span>

                </div>
                
                <div class="form-item">
                    <div id="Admin" runat="server"></div>
                    <span> ادمین ایجاد کننده :</span>

                </div>

            </fieldset>
        </section>

</asp:Content>



<asp:Content ID="Content3" ContentPlaceHolderID="Scripts" runat="server">
</asp:Content>

