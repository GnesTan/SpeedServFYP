<%@ Page Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="CreateAccount.aspx.cs" Inherits="ServiceProvidingSystem.BackendUser.CreateAccount" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server"> 

          <div class="topContainer"> 
       <!-- Welcome Column Section -->
         <div class="row welcome text-center">  
         <div class="col-12">  
            <div class="nav">                 
            <div class="ul"><asp:LinkButton class="btn" runat="server" PostBackUrl="~/AddCloth.aspx" CausesValidation="false">Add Cloth</asp:LinkButton></div>
            <div class="ul"><asp:LinkButton class="btn" runat="server" PostBackUrl="~/ViewCloth.aspx" CausesValidation="false">View Cloth</asp:LinkButton></div>
            <div class="ul"><asp:LinkButton class="btn" runat="server" PostBackUrl="~/AccountMaintenance.aspx" CausesValidation="false">Account Maintenance</asp:LinkButton></div>             
           </div>         
         </div>
         </div> 
    </div>

        <div class="editForm">
        <div class="topicName">
       <b><u>Add User</u></b>
        </div>
        <br/>
        <br/>


        <div class="tableClass">
        <table class="tbDetails">
            <tr>
                <td>
                    <asp:Label ID="lblFormUsername" runat="server" Text="Username"></asp:Label>
                    <br />
                </td>
                <td>
                    <asp:TextBox ID="txtUsername" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator style="color:red" ID="UsernameRequired" runat="server" ControlToValidate="txtUsername" ErrorMessage="Username is required." ToolTip="Username is required." ValidationGroup="CreateAccount">*</asp:RequiredFieldValidator>
                </td>

                <td>
                    <asp:Label ID="lblPassword" runat="server" Text="Password"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtPassword" runat="server" TextMode="Password"></asp:TextBox>
                    <asp:RequiredFieldValidator style="color:red" ID="PasswordRequired" runat="server" ControlToValidate="txtPassword" ErrorMessage="Password is required." ToolTip="Password is required." ValidationGroup="CreateAccount">*</asp:RequiredFieldValidator>
                </td>
            </tr>

            <tr style="padding-bottom:20px;">
                <td>
                </td>
                <td>
                    <asp:RegularExpressionValidator style="color:red" runat="server" ID="UsernameMin" ControlToValidate = "txtUsername" ValidationExpression = "^[\s\S]{3,}$" ErrorMessage="Minimum 3 characters required." ValidationGroup="CreateAccount"></asp:RegularExpressionValidator>
                    <asp:Label ID="lblErrorUsername" runat="server" ForeColor="Red" ></asp:Label>
                </td>
                <td>
                </td>
                <td>
                    <asp:RegularExpressionValidator style="color:red" runat="server" ID="PasswordMin" ControlToValidate = "txtPassword" ValidationExpression = "^[\s\S]{6,}$" ErrorMessage="Minimum 6 characters required." ValidationGroup="CreateAccount"></asp:RegularExpressionValidator>
                </td>
            </tr>

            

            <tr>
                <td>
                    <asp:Label ID="lblName" runat="server" Text="Name"></asp:Label>
                    <br />
                </td>
                <td>
                    <asp:TextBox ID="txtName" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator style="color:red" ID="NameRequired" runat="server" ControlToValidate="txtName" ErrorMessage="Name is required." ToolTip="Name is required." ValidationGroup="CreateAccount">*</asp:RequiredFieldValidator>
                </td>

                <td>
                    <asp:Label ID="lblConPass" runat="server" Text="Confirm Password"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtConPass" runat="server" TextMode="Password" ></asp:TextBox>
                    <asp:RequiredFieldValidator style="color:red" ID="ConPassRequired" runat="server" ControlToValidate="txtConPass" ErrorMessage="Confirm Password is required." ToolTip="Confirm Password is required." ValidationGroup="CreateAccount">*</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr style="padding-bottom:30px;">
                <td colspan="3">
                </td>
                <td>
                    <asp:CompareValidator ID="NewPasswordCompare" runat="server" ControlToCompare="txtPassword" ControlToValidate="txtConPass" Display="Dynamic" ErrorMessage="The Confirm Password must match the Password entry." ValidationGroup="CreateAccount" ForeColor="red"></asp:CompareValidator>
                </td>
            </tr>

            </table>
            </div>

            <div class="btnClass">

            <asp:Button ID="btnCreate" runat="server" class="btnNew btn-primary btn-lg" OnClick="btnCreate_Click" Text="Create" ValidationGroup="CreateAccount" />
            &nbsp;&nbsp;&nbsp;
            <asp:Button ID="btnCancel" runat="server" class="btnNew btn-secondary btn-lg" OnClick="btnCancel_Click" Text="Cancel" CausesValidation="false" />

            </div>

            <br />


            <p style="color:blue">
                <asp:Label ID="lblSucessful" runat="server"></asp:Label>
            </p>

        </div>




 

<style type="text/css">

body{    
    font-family: Arial, Helvetica, sans-serif;   
    background-color: #f2f2f2;      
}

.topicName{
    margin: auto;
    font-size: 25px;
}

.tbDetails{
    border-collapse:separate;
    border-spacing:0 20px;
}


.tbDetails td{
    height:20px;
    width:300px;
}



.btnClass {
    margin-top:20px;
    text-align:center;
}

.tableClass{
    margin-left:5%;
    margin-right:auto;
}

.editForm {
    background-color: white;
    margin: auto;
    box-shadow: 3px 3px 10px 0 #b3b3b3;
    border-radius: 5px;    
    width: 80%;
    display: block;
    margin-top: 30px;
    margin-bottom: 5%;
    margin-left: auto;
    margin-right: auto;
    padding: 3%;
    height: 500px;  

}


.topContainer{
    font-family: Arial, Helvetica, sans-serif;   
    text-align: center;
    font-size: 25px;
    background-color: white;
    width: 100%;
    height: 220px;
}

.nav {
  list-style-type: none;
  display: inline-block;
  text-align: center;
  margin: 0;      
}

.ul {
    display: inline-block;
    font-size: 20px;
    padding: 20px;
    color: #c48e0e;
}



        /*---Button --*/
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
