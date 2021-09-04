
<%@ Page Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="PasswordRecovery2.aspx.cs" Inherits="ServiceProvidingSystem.PasswordRecovery2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server"> 




 
    <!-- Password Recovery Section -->


             
              <!-- Password Recovery Form -->
 
       <div class="loginForm">
        <!-- Back Button -->
        <div style="text-align:left; float:left;">
            <asp:ImageButton runat="server" Height="33px" ImageUrl="~/Image/BackIcon.jpg" Width="33px" ID="imgBack" OnClick="imgBack_Click"></asp:ImageButton>
        </div>

        <!-- Title -->
        <div>
             <h1><u>Password Recovery</u></h1>
        </div>
        <div class="loginChild">
            <div class="inputLayout">
                <table>
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

                    <!-- Text field for enter PIN number -->
                    <tr>
                        <td style="text-align:center">
                            <asp:TextBox ID="txtPinNumber" runat="server" Placeholder="XXXXXX"></asp:TextBox>
                        </td>             
                    </tr>

                    <!-- Error Message -->
                    <tr>
                        <td style="text-align:center">
                            <asp:RequiredFieldValidator style="color:red" ID="PinNumberRequired" runat="server" ControlToValidate="txtPinNumber" ErrorMessage="PIN number is required." ToolTip="Confirm Password is required." ValidationGroup="Register3">*</asp:RequiredFieldValidator>
                            <asp:Label ID="lblInvalid" ForeColor="Red" runat="server"></asp:Label>
                        </td>
                    </tr>



                </table> 

                <div style="margin-top:25px;">
                <asp:Button class="btnNew btn-primary btn-lg" ID="btnNext" runat="server" Text="Next" ValidationGroup="Recovery1" OnClick="btnNext_Click"/>
                &nbsp&nbsp

                <asp:Button class="btnNew btn-secondary btn-lg" ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click"/>
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

                /*---Button --*/
        .btnNew{  
          max-width: 100%;
          max-height: 100%;
          background: none;
          border: 3px solid;
          border-radius: 10px;
          color: #ffffff;
          font-weight: 600;
          cursor: pointer;
          font-size: 16px;
          position: relative;
          box-shadow: 0 5px 5px 0 rgba(0,0,0,0.1), 0 5px 10px 0 rgba(0,0,0,0.1);
        }


        .btn-primary {
            background-color: #EF7E7E;
            border: 1px solid #EF7E7E;
        }

        .btn-primary:hover {
                background-color: #F68888;
                border: 1px solid #F68888;
        }

        .btn-secondary {
            background-color: #7B51F2;
            border: 1px solid #7B51F2;
        }

        .btn-secondary:hover {
                background-color: #8F6BF6;
                border: 1px solid #8F6BF6;
        }



    </style>

</asp:Content>


