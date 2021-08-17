
<%@ Page Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="PasswordRecovery.aspx.cs" Inherits="ServiceProvidingSystem.PasswordRecovery" %>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

   

    <div class="editForm">
        <h3 style="text-align: center">Change Password</h3>
                            <table>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblCurrentPassword" runat="server" AssociatedControlID="txtCurrentPassword">Password:</asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:TextBox ID="txtCurrentPassword" runat="server" TextMode="Password"></asp:TextBox>
                                        <%--<asp:RequiredFieldValidator ID="CurrentPasswordRequired" runat="server" ControlToValidate="txtCurrentPassword" ErrorMessage="Password is required." ToolTip="Password is required." ValidationGroup="ChangePassword1" ForeColor="Red">*</asp:RequiredFieldValidator>--%>
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
                                        <%--<asp:RequiredFieldValidator ID="NewPasswordRequired" runat="server" ControlToValidate="txtNewPassword" ErrorMessage="New Password is required." ToolTip="New Password is required." ValidationGroup="ChangePassword1" ForeColor="Red">*</asp:RequiredFieldValidator>--%>
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
                                        <%--<asp:RequiredFieldValidator ID="ConfirmNewPasswordRequired" runat="server" ControlToValidate="txtConfirmNewPassword" ErrorMessage="Confirm New Password is required." ToolTip="Confirm New Password is required." ValidationGroup="ChangePassword1" ForeColor="Red">*</asp:RequiredFieldValidator>--%>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align:center" colspan="2">
                                        <%--<asp:CompareValidator ID="NewPasswordCompare" runat="server" ControlToCompare="txtNewPassword" ControlToValidate="txtConfirmNewPassword" Display="Dynamic" ErrorMessage="The Confirm New Password must match the New Password entry." ValidationGroup="ChangePassword1" ForeColor="red"></asp:CompareValidator>--%>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align:center; color:Red;" colspan="2">
                                    </td>
                                </tr>
                                <tr>
                                    <td">
                                        <asp:Button class="button" ID="ChangePasswordPushButton" runat="server" CommandName="ChangePassword" Text="Change Password" ValidationGroup="ChangePassword1" />
                                    </td>
                                    <td>
                                        <asp:Button CssClass="button" ID="CancelPushButton" runat="server" CausesValidation="False" CommandName="Cancel" Text="Cancel" />
                                    </td>
                                </tr>
                            </table>


    </div>


    <style>
        body {
            font-family: Arial, Helvetica, sans-serif;
            background-color: #f2f2f2;
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

        table, tr, td {
            margin: 5px 5px 5px 5px;
            padding: 5px 5px 5px 5px;
        }

        .chgpwd {
            margin-left: 70px;
        }

        .button {
            height: 80%;
            width: 80%;
            font-size: 14px;
        }
    </style>

</asp:Content>
