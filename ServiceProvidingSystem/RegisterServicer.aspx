<%@ Page Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="RegisterServicer.aspx.cs" Inherits="ServiceProvidingSystem.RegisterServicer" %>
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
             <h1>Sign up as Servicer</h1>
        </div>
        <div class="loginChild">
            <div class="inputLayout">
                <table>
                    <tr>
                        <td style="text-align:center">
                            <asp:TextBox ID="txtName" runat="server" Placeholder="Full Name"></asp:TextBox>
                            <asp:RequiredFieldValidator style="color:red" ID="NameRequired" runat="server" ControlToValidate="txtName" ErrorMessage="Name is required." ToolTip="Name is required." ValidationGroup="Register1">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align:center">
                            <asp:TextBox ID="txtPhone" runat="server" Placeholder="Phone"></asp:TextBox>
                            <asp:RequiredFieldValidator style="color:red" ID="PhoneRequired" runat="server" ControlToValidate="txtPhone" ErrorMessage="Phone number is required." ToolTip="Phone number is required." ValidationGroup="Register1">*</asp:RequiredFieldValidator>
                        </td>             
                    </tr>

                    <tr>
                        <td style="text-align:center">
                            <asp:TextBox ID="txtIC" runat="server" Placeholder="Identity Card No."></asp:TextBox>
                            <asp:RequiredFieldValidator style="color:red" ID="ICRequired" runat="server" ControlToValidate="txtIC" ErrorMessage="Identity Card No. is required." ToolTip="Identity Card No. is required." ValidationGroup="Register1">*</asp:RequiredFieldValidator>
                        </td>             
                    </tr>

                    <tr>
                        <td style="text-align:left">
                        Date of Birth
                        </td>             
                    </tr>

                    <tr>
                        <td style="text-align:center;">
                            <asp:TextBox ID="txtDob" runat="server" type="date"></asp:TextBox>
                            <asp:RangeValidator style="color:red" ID="DateValidator" runat="server" ErrorMessage="Age must be between 18 and 100." ControlToValidate="txtDob" Display="Dynamic" Type="Date" ></asp:RangeValidator>
                        </td>             
        
                    </tr>


                </table> 

                <div style="margin-top:25px;">
                <asp:Button ID="btnNext" runat="server" Text="NEXT" ValidationGroup="Register1"/>
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
      left: 20%;
      margin: -25px 0 0 -25px; /* apply negative top and left margins to truly center the element */
      margin-top:10%;

    }

    .inputLayout{
      margin-left:30%;
      margin-right:auto;

    }



    </style>

</asp:Content>