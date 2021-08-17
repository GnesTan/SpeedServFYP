<%@ Page Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="ResetPassword.aspx.cs" Inherits="ServiceProvidingSystem.ResetPassword" %>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

   

    <div class="editForm">
        <h3 style="text-align: center"><u>Reset Password</u></h3>
        <div class="editForm2">
                            <table>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblCurrentPassword" runat="server" AssociatedControlID="txtCurrentPassword">Password:</asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:TextBox ID="txtCurrentPassword" runat="server" TextMode="Password"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:RequiredFieldValidator ID="CurrentPasswordRequired" runat="server" ControlToValidate="txtCurrentPassword" ErrorMessage="Password is required." ToolTip="Password is required." ValidationGroup="ChangePassword1" ForeColor="Red">Password is required.</asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblNewPassword" runat="server" AssociatedControlID="txtNewPassword">New Password:</asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:TextBox ID="txtNewPassword" runat="server" TextMode="Password"></asp:TextBox>
                                        
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:RequiredFieldValidator ID="NewPasswordRequired" runat="server" ControlToValidate="txtNewPassword" ErrorMessage="New Password is required." ToolTip="New Password is required." ValidationGroup="ChangePassword1" ForeColor="Red">New Password is required.</asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator style="color:red" runat="server" ID="PasswordMin" ControlToValidate = "txtNewPassword" ValidationExpression = "^[\s\S]{6,}$" ErrorMessage="Minimum 6 characters required." ValidationGroup="ChangePassword1"></asp:RegularExpressionValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblConfirmNewPassword" runat="server" AssociatedControlID="txtConfirmNewPassword">Confirm New Password:</asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:TextBox ID="txtConfirmNewPassword" runat="server" TextMode="Password"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:RequiredFieldValidator ID="ConfirmNewPasswordRequired" runat="server" ControlToValidate="txtConfirmNewPassword" ErrorMessage="Confirm New Password is required." ToolTip="Confirm New Password is required." ValidationGroup="ChangePassword1" ForeColor="Red">Confirm New Password is required.</asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align:left">
                                        <asp:CompareValidator ID="NewPasswordCompare" runat="server" ControlToCompare="txtNewPassword" ControlToValidate="txtConfirmNewPassword" Display="Dynamic" ErrorMessage="The Confirm New Password must match the New Password entry." ValidationGroup="ChangePassword1" ForeColor="red"></asp:CompareValidator>
                                    </td>
                                </tr>
                            </table>

                            <div class="btnPosition">

                                    
                                <asp:Button class="btnNew btn-primary btn-lg" ID="btnConfirm" runat="server" CommandName="Confirm" Text="Change Password" ValidationGroup="ChangePassword1" OnClick="btnConfirm_Click" />
                                &nbsp&nbsp
                
                                <asp:Button class="btnNew btn-secondary btn-lg" ID="btnCancel" runat="server" CausesValidation="False" CommandName="Cancel" Text="Cancel" OnClick="btnCancel_Click" />
                             </div>

            <asp:Label ID="lblError" runat="server" ForeColor="Red" ></asp:Label>



            </div>
    </div>


    <style>
        body {
            font-family: Arial, Helvetica, sans-serif;
            background-color: #f2f2f2;
            font-size:20px;
        }

        .editForm {
            background-color: white;
            max-width: 800px;
            margin: auto;
            margin-top: 100px;
            margin-bottom: 100px;
            padding: 20px 20px 20px 20px;
            box-shadow: 3px 3px 10px 0 #b3b3b3;
            border-radius: 5px;
        }


        .editForm2 {
            margin-left: 30%;
            margin-right: 10%;
            margin-top:30px;
            margin-bottom: 100px;
            text-align:left;
        }


    td {
        padding-top: 0em;
        padding-bottom: 0em;

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