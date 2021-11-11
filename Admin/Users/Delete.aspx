<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMP.Master" AutoEventWireup="true" CodeBehind="Delete.aspx.cs" Inherits="KargahProject.Admin.Users.Delete" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>



<asp:Content ID="Content2" ContentPlaceHolderID="AdminCPH" runat="server">
    
        <section class="main_artc">
            <div class="error_div error" id="ErrorDiv" runat="server">
                آیا از حذف این مورد اطمینان دارید؟!
            </div>
            <fieldset name="AddProduct">
                <legend>حذف کاربر :</legend>


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
                    <div id="UserType" runat="server"></div>
                    <span>نوع کاربر :</span>

                </div>

                <div class="form-item">
                    <div id="IsEnabled" runat="server"></div>

                    <span> وضعیت :</span>

                </div>
                
                
                <form id="DeleteForm" runat="server">
                    <div class="btn-container text-center">
                        <a href="Default.aspx" class="btn btn-default">لغو</a>
                        <button type="submit" class="btn btn-danger" runat="server" onserverclick="DeleteUser_ServerClick"> بله، حذف شود</button>
                    </div>
                </form>

            </fieldset>
        </section>

</asp:Content>





<asp:Content ID="Content3" ContentPlaceHolderID="Scripts" runat="server">
</asp:Content>
