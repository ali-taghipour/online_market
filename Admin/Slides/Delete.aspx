<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMP.Master" AutoEventWireup="true" CodeBehind="Delete.aspx.cs" Inherits="KargahProject.Admin.Slides.Delete" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    
</asp:Content>



<asp:Content ID="Content2" ContentPlaceHolderID="AdminCPH" runat="server">
    
        <section class="main_artc">
            <div class="error_div error" id="ErrorDiv" runat="server">
                آیا از حذف این مورد اطمینان دارید؟!
            </div>
            <fieldset name="AddProduct">
                <legend>حذف اسلایدر :</legend>


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
                    <div id="IsEnabled" runat="server"></div>

                    <span> وضعیت :</span>

                </div>

                <form id="DeleteForm" runat="server">
                    <div class="btn-container text-center">
                        <a href="Default.aspx" class="btn btn-default">لغو</a>
                        <button type="submit" class="btn btn-danger" runat="server" onserverclick="DeleteSlide_ServerClick"> بله، حذف شود</button>
                    </div>
                </form>

            </fieldset>
        </section>

</asp:Content>



<asp:Content ID="Content3" ContentPlaceHolderID="Scripts" runat="server">
</asp:Content>




