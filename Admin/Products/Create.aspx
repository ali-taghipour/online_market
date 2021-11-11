<%@ Page Title="" validateRequest="false" Language="C#" MasterPageFile="~/Admin/AdminMP.Master" AutoEventWireup="true" CodeBehind="Create.aspx.cs" Inherits="KargahProject.Admin.Products.Create" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="AdminCPH" runat="server">

    <form runat="server" id="Form" enctype="multipart/form-data">
        <section class="main_artc">
            <div class="error_div" id="ErrorDiv" runat="server"></div>
            <fieldset name="AddProduct">
                <legend>افزودن محصول :</legend>

                <div class="form-item">
                    <input type="text" class="required max-length" data-maxlength="300" id="ProductTitle" runat="server" />
                    <span><span>*</span> نام محصول :</span>
                    <div class="error"></div>

                </div>
                <div class="form-item">
                    <input type="number" id="MainPrice" runat="server" min="0" class="required" placeholder="قیمت به تومان" />
                    <span><span>*</span> قیمت اصلی (به تومان) :</span>
                    <div class="error"></div>

                </div>
                <div class="form-item">
                    <input type="number" id="OffPrice" min="0" runat="server" class="" placeholder="قیمت به تومان" />
                    <span>قیمت با تخفیف (به تومان) :</span>
                    <div class="error"></div>

                </div>

                <div class="form-item">
                    <input type="number" id="Inventory" min="0" runat="server" class="" placeholder="تعداد موجودی انبار" />
                    <span>موجودی انبار :</span>
                    <div class="error"></div>

                </div>

                <div class="form-item">
                    <div class="category_menu"></div>

                    <span><span>*</span> دسته بندی :</span>
                    <input type="hidden" class="required SelectedCategoryId" runat="server" name="SelectedCategoryId" id="SelectedCategoryId" value="" />
                    <div class="error"></div>
                </div>

                <div class="form-item">
                    <div class="user_pic">
                        <div runat="server">
                            <input type="file" id="pic1" name="pic1" onchange="setpic(this);" />
                            <label for="pic1">انتخاب عکس  </label>
                        </div>
                    </div>

                    <div class="add_pic">
                        <input type="button" value=" +  اضافه کردن عکس بیشتر" />
                    </div>

                    <span>تصاویر محصول :</span>

                </div>
                <div class="form-item">
                    <textarea class="max-length" data-maxlength="1000" id="Summary" name="Summary" runat="server"></textarea>
                    <span>خلاصه محصول :</span>
                    <div class="error"></div>

                </div>
                <div class="form-item">
                    <textarea id="Description" runat="server"></textarea>
                    <span>توضیحات :</span>
                    <div class="error"></div>

                </div>

                <div class="form-item">
                    <input type="radio" name="IsEnabled" id="Enabled" runat="server" class="Enabled" value="true" checked />
                    <label for="Enabled">نمایش </label>

                    <input type="radio" name="IsEnabled" id="Disabled" runat="server" class="Disabled" value="false" />
                    <label for="Disabled">عدم نمایش</label>

                    <span><span>*</span> نمایش در سایت :</span>

                </div>


                <footer class="btn_holder">
                    <div class="sub_btn">
                        <input type="submit" value="افزودن" runat="server"  onserverclick="CreateProduct_ServerClick"/>
                    </div>

                </footer>

            </fieldset>


        </section>
    </form>

</asp:Content>






<asp:Content ID="Content3" ContentPlaceHolderID="Scripts" runat="server">
    <script src="/Content/Admin/js/Category.js"></script>
    <script src="/Content/ckeditor/ckeditor.js"></script>
    <script type="text/javascript">
        //لود کردن همه دسته بندی های فعال
        RenderAllCategory(true);

        //فعال سازی ckeditor
        $(function () {
            CKEDITOR.replace('AdminCPH_Description');
        });
    </script>
</asp:Content>
