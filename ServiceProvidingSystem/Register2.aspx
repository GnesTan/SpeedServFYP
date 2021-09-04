<%@ Page Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Register2.aspx.cs" Inherits="ServiceProvidingSystem.Register2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server"> 




 
    <!-- Register2 Section -->


             
              <!-- Register2 Form -->
 
       <div class="loginForm">
        <div style="text-align:left; float:left;">
            <asp:ImageButton runat="server" Height="33px" ImageUrl="~/Image/BackIcon.jpg" Width="33px"></asp:ImageButton>
        </div>


        <div>
             <h1>Sign up as <asp:Label ID="lblUser" runat="server"></asp:Label></h1>
        </div>
        <div class="loginChild">
            <div class="inputLayout">
                <table>

                    <tr>
                        <td style="text-align:center">
                            <asp:TextBox ID="txtEmail" runat="server" Placeholder="Email Address"></asp:TextBox>               
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align:center">
                            <asp:RequiredFieldValidator style="color:red" ID="EmailRequired" runat="server" ControlToValidate="txtEmail" ErrorMessage="Email Address is required." ToolTip="Email Address is required." ValidationGroup="Register2">Email Address is required.</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align:center">
                            <asp:RegularExpressionValidator style="color:red" ID="EmailREValidator" runat="server" ControlToValidate="txtEmail" ErrorMessage="Email Address must be a valid format. Eg: abc123@gmail.com" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ValidationGroup="Register2"></asp:RegularExpressionValidator>
                        </td>
                    </tr>


                    <tr>
                        <td style="text-align:center">
                            <asp:TextBox ID="txtPassword" runat="server" Placeholder="Password" TextMode="Password"></asp:TextBox>
                        </td>             
                    </tr>
                    <tr>
                        <td style="text-align:center">
                            <asp:RequiredFieldValidator style="color:red" ID="PasswordRequired" runat="server" ControlToValidate="txtPassword" ErrorMessage="Password is required." ToolTip="Password is required." ValidationGroup="Register2">Password is required.</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align:center">
                            <asp:RegularExpressionValidator style="color:red" runat="server" ID="PasswordMin" ControlToValidate = "txtPassword" ValidationExpression = "^[\s\S]{6,}$" ErrorMessage="Minimum 6 characters required." ValidationGroup="Register2"></asp:RegularExpressionValidator>
                        </td>
                    </tr>

                    <tr>
                        <td style="text-align:center">
                            <asp:TextBox ID="txtConfirmPass" runat="server" Placeholder="Confirm Password" TextMode="Password"></asp:TextBox>
                        </td>             
                    </tr>

                    <tr>
                        <td style="text-align:center">
                            <asp:RequiredFieldValidator style="color:red" ID="ConfirmPassRequired" runat="server" ControlToValidate="txtConfirmPass" ErrorMessage="Confirm Password is required." ToolTip="Confirm Password is required." ValidationGroup="Register2">Confirm Password is required.</asp:RequiredFieldValidator>
                        </td>
                    </tr>



                </table> 

                                        * By continuing, I confirm that I have read and agree to the SpeedServ
                            <asp:HyperLink ID="hlTermsPrivacy" runat="server" NavigateUrl="~/ChangePassword.aspx">Terms of Use and Privacy Policy</asp:HyperLink>

                <div style="margin-top:25px;">
                <asp:Button ID="btnNext" runat="server" Text="NEXT" ValidationGroup="Register2" OnClick="btnNext_Click"/>
               </div>

            </div>

            <!-- Error Messages -->
            <asp:Label ID="lblEmailExist" ForeColor="Red" runat="server"></asp:Label>
            <br>
            <asp:Label ID="lblNotMatch" ForeColor="Red" runat="server"></asp:Label>

        </div>




     </div>

    <style type="text/css">

    h1{
        font-size:larger;
    }

    body{    
        background-color: #f2f2f2;      
    }

    .loginForm {
        background-color: white;
        text-align:center;
        max-width: 680px;
        margin: auto;
        box-shadow: 3px 3px 10px 0 #b3b3b3;
        border-radius: 5px;    
        width: 35%;
        height:500px;
        display: block;
        margin-top: 30px;
        margin-bottom: 5%;
        margin-left: auto;
        margin-right: auto;
        padding: 3%;
        position: relative;
    }

    .loginChild{
      position: absolute;
      top: 10%;
      left: 10%;
      margin: -25px 0 0 -25px; /* apply negative top and left margins to truly center the element */
      margin-top:10%;

    }

    .inputLayout{
      margin-left:auto;
      margin-right:auto;

    }



    </style>

</asp:Content>
