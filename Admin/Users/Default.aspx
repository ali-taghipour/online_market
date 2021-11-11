<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMP.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="KargahProject.Admin.Users.Default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="/Content/DataTable/datatables.min.css" rel="stylesheet" />
    <link href="/Content/DataTable/responsive.dataTables.min.css" rel="stylesheet" />
</asp:Content>



<asp:Content ID="Content2" ContentPlaceHolderID="AdminCPH" runat="server">

    <div class="btn-container text-right">
        <a href="Create.aspx" class="btn btn-success">افزودن کاربر جدید</a>
    </div>
    

    <div id="TableContainer" class="table-container" runat="server">
        
    </div>



</asp:Content>




<asp:Content ID="Content3" ContentPlaceHolderID="Scripts" runat="server">
    <script src="/Content/DataTable/datatables.min.js"></script>
    <script src="/Content/DataTable/dataTables.responsive.min.js"></script>
    <script>
        $(document).ready(function () {
            
            $('.datatable').DataTable({
                language: {
                    url: "/Content/DataTable/fa-lang.json"
                },
                "pagingType": "full_numbers",
                "lengthMenu": [
                    [10, 25, 50, -1],
                    [10, 25, 50, "All"]
                ],
                "columnDefs": [
                    { "orderable": false, "targets": 6}
                ],
                "order": [[0, "desc"]],
                "responsive": true
            });

        });
    </script>
</asp:Content>
