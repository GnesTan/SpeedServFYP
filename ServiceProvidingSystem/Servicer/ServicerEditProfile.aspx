
<%@ Page Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="ServicerEditProfile.aspx.cs" Inherits="ServiceProvidingSystem.Servicer.ServicerEditProfile" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server"> 



        <div class="editForm">
        <h2><u>Profile</u></h2>
        <div class="tb1">
            <table class="tbDetails" style="border-right-width:1px; border-left-width:0px; border-top-width:0px; border-bottom-width:0px; border-color:black; border-style:solid;">
                <tr>
                    <td colspan="2">
                        <asp:Label ID="lblName" runat="server" Text="Name"></asp:Label>
                        <br />
                        <asp:TextBox ID="txtName" runat="server" MaxLength="30"></asp:TextBox>
                        <asp:RequiredFieldValidator style="color:red" ID="FirstNameRequired" runat="server" ControlToValidate="txtName" ErrorMessage="Name is required." ToolTip="Name is required." ValidationGroup="EditProfile">*</asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:RegularExpressionValidator style="color:red" Display = "Dynamic" ControlToValidate = "txtName" ID="NameREValidator" ValidationExpression = "^[\s\S]{0,30}$" runat="server" ErrorMessage="Maximum 30 characters allowed." ValidationGroup="EditProfile"></asp:RegularExpressionValidator>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style9">
                        <asp:Label ID="lblDob" runat="server" Text="Date Of Birth"></asp:Label>
                    </td>
                    <td class="auto-style12">
                        <asp:Label ID="lblGender" runat="server" Text="Gender"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style9">
                        <asp:TextBox ID="txtDob" runat="server" type="date"></asp:TextBox>

                    </td>
                    <td class="auto-style12">
                        <asp:DropDownList ID="ddlGender" runat="server">
                            <asp:ListItem Value="M">Male</asp:ListItem>

                            <asp:ListItem Value="F">Female</asp:ListItem>

                            <asp:ListItem Value="O">Other</asp:ListItem>

                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:RangeValidator style="color:red" ID="DateValidator" runat="server" ErrorMessage="Age must be between 18 and 100." ControlToValidate="txtDob" Type="Date" ValidationGroup="EditProfile" ></asp:RangeValidator>
                    </td>          
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:Label ID="lblProfilePic" runat="server" Text="Profile Picture"></asp:Label>
                    </td>          
                </tr>
                <tr>
                    <td>
                        <div><asp:image height="140px" Width="140px" ToolTip = "Profile Picture" ID="imgProfile" runat="server" ImageUrl ="~/Image/generaluser.png" style="border-radius:100px; box-shadow: 0 0 0 3px #b3b3b3; text-decoration:none;" ></asp:image></div>

                    </td>
                    <td>
                        <asp:FileUpload ID="ImageUpload" runat="server"/>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:Label ID="lblPhone" runat="server" Text="Phone number"></asp:Label>
                        <br />
                        <asp:TextBox ID="txtPhone" runat="server" MaxLength="20"></asp:TextBox><asp:RequiredFieldValidator style="color:red" ID="PhoneRequired" runat="server" ControlToValidate="txtPhone" ErrorMessage="Phone number is required." ToolTip="Phone number is required." ValidationGroup="EditProfile">*</asp:RequiredFieldValidator>
                        <br />
                        <asp:RegularExpressionValidator style="color:red" runat="server" ID="PhoneRegEx" ControlToValidate = "txtPhone" ValidationExpression = "^(\+?6?01)[02-46-9]-*[0-9]{7}$|^(\+?6?01)[1]-*[0-9]{8}$" ErrorMessage="*Invalid format of phone number." ValidationGroup="EditProfile"></asp:RegularExpressionValidator>

                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:Label ID="lblEmail" runat="server" Text="Email Address"></asp:Label>
                        <br />
                        <asp:TextBox ID="txtEmail" runat="server" MaxLength="40" Enabled="false" ></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                    <asp:Label ID="lblAddress" runat="server" Text="Home Address"></asp:Label>
                    <br />
                    <asp:TextBox runat="server" ID="txtAddress" Width="200px" TextMode="MultiLine" Rows="4" />
                    </td>
                </tr>
            </table>
        </div>

        <div class="tb2">
            <table class="tbDetails">
                <tr>
                    <td colspan="2" style="width:400px;">
                        <asp:Label ID="lblDays" runat="server" Text="Working Days"></asp:Label>
                        <br />
                        <asp:TextBox ID="txtDays" runat="server" Width="380px" MaxLength="50" Placeholder="Eg.(From Mon - Fri)"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:RegularExpressionValidator style="color:red" Display = "Dynamic" ControlToValidate = "txtDays" ID="RegularExpressionValidator1" ValidationExpression = "^[\s\S]{0,20}$" runat="server" ErrorMessage="Maximum 20 characters allowed." ValidationGroup="EditProfile"></asp:RegularExpressionValidator>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:Label ID="lblTime" runat="server" Text="Available Time:"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:Label ID="lblFrom" runat="server" Text="From"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:TextBox ID="txtFromHour" runat="server" Width="50px" MaxLength="2" Placeholder="HH"></asp:TextBox>&nbsp:&nbsp<asp:TextBox ID="txtFromMin" runat="server" Width="50px" MaxLength="2" Placeholder="mm"></asp:TextBox> <br/>
                        <asp:RegularExpressionValidator style="color:red" runat="server" ID="FromHourPattern" ControlToValidate = "txtFromHour" ValidationExpression = "^\d+$" ErrorMessage="Hour must be number." ValidationGroup="EditProfile"></asp:RegularExpressionValidator>
                        <asp:RegularExpressionValidator style="color:red" runat="server" ID="FromMinPattern" ControlToValidate = "txtFromMin" ValidationExpression = "^\d+$" ErrorMessage="Minute must be number." ValidationGroup="EditProfile"></asp:RegularExpressionValidator>
                    </td>          
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblTo" runat="server" Text="To"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:TextBox ID="txtToHour" runat="server" Width="50px" MaxLength="2" Placeholder="HH"></asp:TextBox>&nbsp:&nbsp<asp:TextBox ID="txtToMin" runat="server" Width="50px" MaxLength="2" Placeholder="mm"></asp:TextBox> <br/>
                        <asp:RegularExpressionValidator style="color:red" runat="server" ID="ToHourPattern" ControlToValidate = "txtToHour" ValidationExpression = "^\d+$" ErrorMessage="Hour must be number." ValidationGroup="EditProfile"></asp:RegularExpressionValidator>
                        <asp:RegularExpressionValidator style="color:red" runat="server" ID="ToMinPattern" ControlToValidate = "txtToMin" ValidationExpression = "^\d+$" ErrorMessage="Minute must be number." ValidationGroup="EditProfile"></asp:RegularExpressionValidator>
                    </td>
                </tr>
            </table>

        </div>

        <div class="btnClass">
            <asp:Button ID="btnSave" runat="server" class="btnNew btn-primary btn-lg" OnClick="btnSave_Click" Text="Save" ValidationGroup="EditProfile"/>
            &nbsp;&nbsp;&nbsp;
            <asp:Button ID="btnCancel" runat="server" class="btnNew btn-secondary btn-lg" OnClick="btnCancel_Click" Text="Cancel" CausesValidation="false" />
        </div>






    </div>

<style type="text/css">

body{    
    font-family: Arial, Helvetica, sans-serif;   
    background-color: #f2f2f2;      
}

    header{
        box-shadow: 0 .5rem 1rem rgba(0,0,0,.15);
        background: white;
    }

.tbDetails{
    width:550px;
}

.tbDetails td{
    padding-right:50px;


}

.tb1{
    float:left;
}



.tb2{
    padding-left:40px;
    float:left;
}

.btnClass{
    padding-top:30px;
    padding-left:42%;

    float:left;
    
}

        .btnNew{  
          text-align:center;
          width:100px;
          background: none;
          border: 3px solid;
          border-radius: 10px;
          color: #ffffff;
          font-weight: 600;
          cursor: pointer;
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


.editForm {
    background-color: white;
    margin: auto;
    box-shadow: 3px 3px 10px 0 #b3b3b3;
    border-radius: 5px;    
    width: 1300px;
    display: block;
    margin-top: 110px;
    margin-bottom: 5%;
    margin-left: auto;
    margin-right: auto;
    padding: 3%;
    min-height: 780px;  
}



.textbox{
    border-radius: 5px;    
}

input[type=text]:focus, input[type=password]:focus, input[type=date]:focus{
  outline: none;
  box-shadow: 0 0 0.5pt 0.5pt #900e8c;
  opacity:1;
}



</style>
</asp:Content>

