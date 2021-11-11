<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMP.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="KargahProject.Admin.Comments.Default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="/Content/DataTable/datatables.min.css" rel="stylesheet" />
    <link href="/Content/DataTable/responsive.dataTables.min.css" rel="stylesheet" />
</asp:Content>



<asp:Content ID="Content2" ContentPlaceHolderID="AdminCPH" runat="server">

    <br />
    <h3 class="text-center text-success">
        نظرات بررسی نشده کاربران
    </h3>

    <div class="btn-container text-right">
        <a href="Archive.aspx" class="btn btn-success">آرشیو نظرات</a>
    </div>
    

    <div id="TableContainer" class="table-container" runat="server">

    </div>




    <div class="modal">
        <div class="modal-container">
            <div class="modal-header">
                <h3>جزییات کامنت
                </h3>
                <span class="close-modal">x</span>
            </div>

            <div class="modal-body">
                <div class="row-item">
                    <div class="row-title">
                        متن کامنت
                    </div>
                    <div class="row-body CommentText">
                    </div>
                </div>
                
                <div class="row-item">
                    <div class="row-title">
                        عنوان محصول
                    </div>
                    <div class="row-body ProductTitle">
                    </div>
                </div>
                
                <div class="row-item">
                    <div class="row-title">
                        اطلاعات کاربر 
                    </div>
                    <div class="row-body UserFullName">
                    </div>
                </div>

            </div>

            <div class="modal-footer text-center">
                <button class="btn btn-danger close-modal">بستن</button>
            </div>
        </div>
    </div>


</asp:Content>




<asp:Content ID="Content3" ContentPlaceHolderID="Scripts" runat="server">
    <script src="/Content/DataTable/datatables.min.js"></script>
    <script src="/Content/DataTable/dataTables.responsive.min.js"></script>
    <script src="/Content/Admin/js/Comments.js"></script>
</asp:Content>

