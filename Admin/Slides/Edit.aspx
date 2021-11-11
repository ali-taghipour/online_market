<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMP.Master" AutoEventWireup="true" CodeBehind="Edit.aspx.cs" Inherits="KargahProject.Admin.Slides.Edit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    
    <link href="../../Content/Admin/style/datepicker.css" rel="stylesheet" />
    <script src="../../Content/Admin/js/kamadatepicker.js"></script>
    
</asp:Content>



<asp:Content ID="Content2" ContentPlaceHolderID="AdminCPH" runat="server">
    
    <form runat="server" id="Form" enctype="multipart/form-data">
        <section class="main_artc">
            <div class="error_div error" id="ErrorDiv" runat="server"></div>
            <fieldset name="AddProduct">
                <legend>ویرایش اسلایدر :</legend>


                <div class="img-container text-center" id="SlideImage" runat="server">
                </div>


                <div class="form-item">
                    <input type="text" name="SlideTitle" id="SlideTitle" runat="server" />
                    <span> عنوان :</span>
                    <div class="error"></div>

                </div>
                <div class="form-item">
                    <input type="text" name="Link" id="Link" runat="server" placeholder="به صورت http://example.com" />
                    <span> لینک :</span>
                    <div class="error"></div>

                </div>
                <div class="form-item">
                    <input type="number" value="1" name="Order" id="Order" runat="server" />
                    <span>اولویت :</span>
                    <div class="error"></div>

                </div>

                <div class="form-item">
                    <div class="user_pic">
                        <div runat="server">
                            <input type="file" id="file" name="file" onchange="setpic(this);" />
                            <label for="file">انتخاب عکس  </label>
                        </div>
                    </div>
                    <span><span>*</span> تصویر اسلایدر :</span>
                    <div class="error"></div>

                </div>
                <div class="form-item">
                    <input type="text" name="StartDate" id="StartDate" runat="server" />
                    <span> تاریخ شروع :</span>
                    <div class="error"></div>

                </div>
                <div class="form-item">
                    <input type="text" name="EndDate" id="EndDate" runat="server" />
                    <span> تاریخ پایان :</span>
                    <div class="error"></div>

                </div>

                <div class="form-item">
                    <input type="radio" name="IsEnabled" id="Enabled" value="1" runat="server" checked />
                    <label for="AdminCPH_Enabled">نمایش </label>

                    <input type="radio" name="IsEnabled" id="Disabled" value="0" runat="server" />
                    <label for="AdminCPH_Disabled">عدم نمایش</label>

                    <span><span>*</span> نمایش اسلایدر در سایت :</span>
                    <div class="error"></div>

                </div>


                <footer class="btn_holder">
                    <div class="sub_btn">
                        <input type="submit" value="ذخیره تغییرات" runat="server" onserverclick="EditSlide_ServerClick" />
                    </div>
                </footer>

            </fieldset>
        </section>
    </form>

</asp:Content>



<asp:Content ID="Content3" ContentPlaceHolderID="Scripts" runat="server">    
    <script src="../../Content/Admin/js/kamadatepicker.js"></script>    
    <script>
        kamaDatepicker('AdminCPH_StartDate');
        kamaDatepicker('AdminCPH_EndDate');
    </script>
</asp:Content>

