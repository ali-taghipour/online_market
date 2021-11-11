<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMP.Master" AutoEventWireup="true" CodeBehind="Edit.aspx.cs" Inherits="KargahProject.Admin.Users.Edit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>





<asp:Content ID="Content2" ContentPlaceHolderID="AdminCPH" runat="server">
    
    <form runat="server" id="EditForm" enctype="multipart/form-data" autocomplete="off">
        <section class="main_artc">
            <div class="error_div error" id="ErrorDiv" runat="server"></div>
            <fieldset name="AddProduct">
                <legend>ویرایش کاربر :</legend>

                
                <div class="img-container text-center" id="UserImage" runat="server">
                </div>

                
                <div class="form-item">
                    <input type="text" name="FirstName" id="FirstName" runat="server" />
                    <span> نام :</span>
                    <div class="error"></div>
                </div>
                <div class="form-item">
                    <input type="text" name="LastName" id="LastName" runat="server" />
                    <span> نام خانوادگی :</span>
                    <div class="error"></div>
                </div>
                <div class="form-item">
                    <input type="text" class="required min-length" data-minlength="3" name="Username" autocomplete="off" id="Username" runat="server" />
                    <span><span>*</span> نام کاربری :</span>
                    <div class="error"></div>
                </div>

                <div>
                    <br />
                    <button type="button" class="btn btn-danger" onclick="ToggleSlide('.change-password')">تغییر کلمه عبور</button>
                    &nbsp; اگر مایل به تغییر کلمه عبور نیستید، فیلد های مربوطه را خالی بگذارید.
                    <br />
                    <br />
                </div>
                <div class="toggle-slide change-password">
                    <div class="form-item">
                        <input type="password" class="min-length" data-minlength="4" name="OldPassword" id="OldPassword" autocomplete="nope" runat="server" />
                        <span>کلمه عبور فعلی :</span>
                        <div class="error"></div>
                    </div>

                    <div class="form-item">
                        <input type="password" class="min-length NewPassword" data-minlength="4" name="NewPassword" id="NewPassword" autocomplete="off" runat="server" />
                        <span>کلمه عبور جدید :</span>
                        <div class="error"></div>
                    </div>

                    <div class="form-item">
                        <input type="password" class="equal" data-equal=".NewPassword" name="RePassword" id="RePassword" autocomplete="off" runat="server" />
                        <span> تکرار کلمه عبور جدید :</span>
                        <div class="error"></div>
                    </div>
                </div>


                <div class="form-item">
                    <input type="text" name="Email" id="Email" autocomplete="off" runat="server" />
                    <span> ایمیل :</span>
                    <div class="error"></div>
                </div>
                 
                <div class="form-item">
                    <input type="text" name="Address" id="Address" runat="server" />
                    <span> آدرس :</span>
                    <div class="error"></div>
                </div>
                 
                <div class="form-item">
                    <input type="number" class="min-length max-length" data-maxlength="10" data-minlength="10" name="PostalCode" id="PostalCode" runat="server" />
                    <span> کد پستی :</span>
                    <div class="error"></div>
                </div>
                
                <div class="form-item">
                    <div class="user_pic">
                        <div runat="server">
                            <input type="file" id="file" name="file" onchange="setpic(this);" />
                            <label for="file">انتخاب عکس  </label>
                        </div>
                    </div>
                    <span> تصویر  :</span>
                    <div class="error"></div>
                </div>

                
                <div class="form-item">
                    <select id="UserType" name="UserType" runat="server">

                    </select>
                    <span><span>*</span> نوع کاربر :</span>
                    <div class="error"></div>
                </div>
                

                <div class="form-item">
                    <input type="radio" name="IsMale" id="Male" value="1" runat="server" checked />
                    <label for="AdminCPH_Male">مرد </label>

                    <input type="radio" name="IsMale" id="Female" value="0" runat="server" />
                    <label for="AdminCPH_Female">زن </label>

                    <span><span>*</span> جنسیت :</span>
                    <div class="error"></div>
                </div>

                <div class="form-item">
                    <input type="radio" name="IsEnabled" id="Enabled" value="1" runat="server" checked />
                    <label for="AdminCPH_Enabled">فعال </label>

                    <input type="radio" name="IsEnabled" id="Disabled" value="0" runat="server" />
                    <label for="AdminCPH_Disabled">غیر فعال</label>

                    <span><span>*</span> وضعیت :</span>
                    <div class="error"></div>
                </div>


                


                <footer class="btn_holder">
                    <div class="sub_btn">
                        <input type="submit" value="ذخیره" runat="server" onserverclick="EditUser_ServerClick"/>
                    </div>
                </footer>

            </fieldset>
        </section>
    </form>

</asp:Content>




<asp:Content ID="Content3" ContentPlaceHolderID="Scripts" runat="server">
</asp:Content>
