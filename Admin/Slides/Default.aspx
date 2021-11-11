<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMP.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="KargahProject.Admin.Slides.Default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="AdminCPH" runat="server">
    
    <div class="btn-container text-right">
        <a href="Create.aspx" class="btn btn-success">افزودن اسلاید جدید</a>
    </div>


    <div class="table-top-area">
        <div class="pull-right search-section">
            <input type="text" class="search-input" id="SearchInput" runat="server" placeholder="جستجو"/>
            <button class="btn btn-success search-btn">جستجو</button>
        </div>
        <div  class="pull-left page-size-section">
            <span class="text-gray">اندازه صفحه :</span>
            <select class="page-size" id="PageSize" runat="server">
                <option value="10">10</option>
                <option value="20">20</option>
                <option value="50">50</option>
            </select>
        </div>
    </div>

    <div id="TableContainer" runat="server" class="table-container">

    </div>

 <%--   <table class="table" id="ListTable" runat="server">
        <thead>
            <tr>
                <th>تصویر
                </th>
                <th>عنوان
                </th>
                <th>وضعیت
                </th>
                <th>تاریخ ثبت
                </th>

                <th>مشاهده</th>
                <th>ویرایش اطلاعات</th>
                <th>حذف</th>
            </tr>
        </thead>
        <tbody>
            <tr>
                <td>
                    <img src="content/img/46.jpg" />
                </td>
                <td>مسابقات فوتسال
                </td>
                <td>فعال

                </td>
                <td>1396/09/01
                </td>

                <td>
                    <a href="/Manage/content/DetailsNews/1198?returnUrl=%2Fmanage%2Fcontent%2Flistnews">مشاهده</a>
                </td>
                <td>
                    <a href="/Manage/content/EditNews/1198?returnUrl=%2Fmanage%2Fcontent%2Flistnews">ویرایش اطلاعات</a>
                </td>
                <td>
                    <a href="/Manage/content/DeleteNews/1198?returnUrl=%2Fmanage%2Fcontent%2Flistnews">حذف</a>
                </td>




            </tr>
        </tbody>
    </table>--%>



    <div class="paginate ">
        <ul class="paging-t pagination" id="Pagination" runat="server">
            <%--<li style="background: #ccc">
                <a href="/Manage/content/listnews?PageSize=5&amp;page=1">1</a>
            </li>
            <li style="">
                <a href="/Manage/content/listnews?PageSize=5&amp;page=2">2</a>
            </li>
            <li style="">
                <a href="/Manage/content/listnews?PageSize=5&amp;page=3">3</a>
            </li>
            <li class="page-td">
                <span> ... </span>
            </li>
            <li style="width: 30px">
                <a href="/Manage/content/listnews?PageSize=5&amp;page=37">></a>
            </li>--%>
        </ul>

    </div>
</asp:Content>




<asp:Content ID="Content3" ContentPlaceHolderID="Scripts" runat="server">   

</asp:Content>




