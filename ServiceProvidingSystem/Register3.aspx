<%@ Page Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Register3.aspx.cs" Inherits="ServiceProvidingSystem.Register3" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server"> 




 
    <!-- Login Section -->


         <hr>
      <div>
             
              <!-- Login Form -->
 
       <div class="loginForm">
        <div style="text-align:left; float:left;">
            <asp:ImageButton runat="server" Height="33px" ImageUrl="~/Image/BackIcon.jpg" Width="33px"></asp:ImageButton>
        </div>


        <div>
             <h1>Email Verification</h1>
        </div>
        <div class="loginChild">
            <div class="inputLayout">
                <table>
                    <tr>
                        <td style="text-align:center">
                            Your account have created sucessfully, please verify your email to get started.
                           
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align:center">
                            <br>
                        </td>
                    </tr>


                    <tr>
                        <td style="text-align:center">
                            An 6-digit pin number have sent to email address <br>
                            <asp:Label ID="lblEmail" runat="server" ForeColor="Blue"></asp:Label>
                        </td>             
                    </tr>

                    <tr>
                        <td style="text-align:center">
                            Please check your email and enter the pin number below for verification.
                        </td>
                    </tr>

                    <tr>
                        <td style="text-align:center">
                            <asp:TextBox ID="txtPinNumber" runat="server" Placeholder="XXXXXX"></asp:TextBox>
                        </td>             
                    </tr>

                    <tr>
                        <td style="text-align:center">
                            <asp:RequiredFieldValidator style="color:red" ID="PinNumberRequired" runat="server" ControlToValidate="txtPinNumber" ErrorMessage="PIN number is required." ToolTip="Confirm Password is required." ValidationGroup="Register3">*</asp:RequiredFieldValidator>
                            <asp:Label ID="lblInvalid" ForeColor="Red" runat="server"></asp:Label>
                        </td>
                    </tr>



                </table> 

                <div style="margin-top:25px;">
                <asp:Button ID="btnConfirm" runat="server" Text="CONFIRM" ValidationGroup="Register3" OnClick="btnConfirm_Click"/>
               </div>

            </div>



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

