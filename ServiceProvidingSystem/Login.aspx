<%@ Page Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="ServiceProvidingSystem.Login" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server"> 











 
    <!-- Login Section -->


         <hr>
      <div>
             
              <!-- Login Form -->
 
       <div class="loginForm">
        <div>
             <h1> Login</h1>
        </div>
        <div class="loginChild">
            <div class="inputLayout">
                <table>
                    <tr>
                        <td style="text-align:center">
                            <asp:TextBox ID="txtEmail" runat="server" Placeholder="Email Address"></asp:TextBox>
                            <asp:RequiredFieldValidator style="color:red" ID="EmailRequired" runat="server" ControlToValidate="txtEmail" ErrorMessage="Email is required." ToolTip="Email is required." ValidationGroup="Login1">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align:center">
                            <asp:TextBox ID="txtPassword" runat="server" Placeholder="Password" TextMode="Password"></asp:TextBox>
                            <asp:RequiredFieldValidator style="color:red" ID="PasswordRequired" runat="server" ControlToValidate="txtPassword" ErrorMessage="Password is required." ToolTip="Password is required." ValidationGroup="Login1">*</asp:RequiredFieldValidator>
                        </td>             
                    </tr>

                    <tr>
                        <td style="text-align:center">
                   
                            <asp:Button ID="btnLogin" runat="server" Text="LOGIN" ValidationGroup="Login1" OnClick="btnLogin_Click"/>
                   
                        </td>             
                    </tr>

                </table> 
               
            </div>

            <div style="text-align:left;">

            <asp:CheckBox ID="cbRemember" runat="server" /> Remember Me

            </div>

            <hr>

            <table>


                <tr>
                    <td style="text-align:center">
                            Want to register as Service Provider? Click <asp:LinkButton ID="hlServicer" runat="server" Text="Here" OnClick="hlServicer_Click"/>
                    </td>
                </tr>
                <tr>
                    <td style="text-align:center">
                            Want to register as User? Click <asp:LinkButton ID="hlClient1" runat="server" Text="Here" OnClick="hlClient_Click"/>
                    </td>
                </tr>

                <tr>
                    <td style="text-align:center">
                            <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl="~/RegisterUser.aspx">Forgot your password?</asp:HyperLink>
                    </td>
                </tr>

                <tr>
                        <td style="text-align:center">
                   
                            <asp:Button ID="btnStaffLogin" runat="server" Text="LOGIN AS STAFF" ValidationGroup="Login1" OnClick="btnStaffLogin_Click"/>
                   
                        </td>             
                </tr>


                
                <tr>
                        <td style="text-align:center">
                   
                            <asp:Label ID="lblWrong" ForeColor="Red" runat="server"></asp:Label>
                   
                        </td>             
                </tr>

            </table>



        </div>

        </div>




     </div>

    <style type="text/css">

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
        height:450px;
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
      top: 20%;
      left: 20%;
      margin: -25px 0 0 -25px; /* apply negative top and left margins to truly center the element */
      margin-top:10%;

    }

    .inputLayout{
      margin-left:20%;
      margin-right:auto;

    }



    </style>

</asp:Content>