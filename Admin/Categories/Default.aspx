<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMP.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="KargahProject.Admin.Categories.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>



<asp:Content ID="Content2" ContentPlaceHolderID="AdminCPH" runat="server">

    <section class="main_artc">
        <div class="error_div" id="ErrorDiv" runat="server"></div>
        <fieldset name="EditCategory">
            <legend>دسته بندی :</legend>

            <div class="text-warning text-center">
                - برای افزودن زیر دسته، ابتدا بر روی والد و سپس بر روی دکمه افزودن کلیک کنید.
                <br />
                - برای ویرایش دسته بندی، ابتدا بر روی دسته بندی مورد نظر و سپس  بر بروی دکمه ویرایش کلیک کنید.
                <br />
                - برای حذف دسته بندی، ابتدا بر روی دسته بندی مورد نظر و سپس بر روی دکمه حذف کلیک کنید.
            </div>


            <br />
            <br />


            <div>
                <div class="text-center">
                    <button type="button" class="btn btn-success" onclick="StartAddCategory()">افزودن</button>
                    <button type="button" class="btn btn-info" onclick="StartUpdateCategory()">ویرایش</button>
                    <button type="button" class="btn btn-danger" onclick="DeleteCategory()">حذف</button>
                </div>
                <br />
            <br />

                <ul class="category_menu" id="menu">
                    <%--<li>
                        <a data-id="1" data-flag="False" data-type="Group">آیسی </a>
                        <ul>
                            <li><a data-id="1" data-flag="True">آیسی سامسونگ</a></li>
                            <li><a data-id="2" data-flag="False">آیسی هوآوی</a></li>
                            <li><a data-id="3" data-flag="True">آیسی سونی</a></li>
                        </ul>
                        <span>+</span> 

                    </li>
                    
                    <li><a data-id="3" data-flag="False">موبایل</a></li>--%>
                </ul>

            </div>


            <br />
            <br />

            <div class="toggle-slide">
                    <div class="text-center form-title">
                    </div>

                    <div class="form-item">
                        <input type="text" id="CatTitle" class="CatTitle" />
                        <span><span>*</span> عنوان گروه :</span>

                    </div>

                    <div class="form-item">
                        <input type="radio" name="IsEnabled" id="Enabled" class="Enabled" value="true" checked />
                        <label for="Enabled">نمایش </label>

                        <input type="radio" name="IsEnabled" id="Disabled" class="Disabled" value="false"/>
                        <label for="Disabled">عدم نمایش</label>

                        <span><span>*</span> نمایش گروه در سایت :</span>

                    </div>

                    <input type="hidden" class="CategoryId" id="CategoryId" />
                    <input type="hidden" class="ParentId" id="ParentId" />

                    <footer class="btn_holder">
                        <div class="edit_btn">
                            <input type="button" value="ذخیره" onclick="AddOrUpdateCategory()" />
                        </div>

                    </footer>

            </div>
            <br />
            <br />
        </fieldset>


    </section>


</asp:Content>




<asp:Content ID="Content3" ContentPlaceHolderID="Scripts" runat="server">
    <script src="/Content/Admin/js/Category.js"></script>
    <script>
        //لود کردن همه دسته بندی های فعال و غیر فعال
        RenderAllCategory(false);
    </script>
</asp:Content>
