<%@ Page Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="PasswordRecovery3.aspx.cs" Inherits="ServiceProvidingSystem.PasswordRecovery3" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server"> 
    <%--Plugins--%>
    <link rel="stylesheet" href="https://unpkg.com/aos@next/dist/aos.css" />
    <link rel="preconnect" href="https://fonts.googleapis.com">
    <link rel="preconnect" href="https://fonts.gstatic.com" crossorigin>
    <link href="https://fonts.googleapis.com/css2?family=Lato&family=PT+Sans&family=Roboto:wght@300&display=swap" rel="stylesheet"> 
    <script src="https://code.jquery.com/jquery-3.3.1.slim.min.js" integrity="sha384-q8i/X+965DzO0rT7abK41JStQIAqVgRVzpbzo5smXKp4YfRvH+8abtTE1Pi6jizo" crossorigin="anonymous"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.14.3/umd/popper.min.js" integrity="sha384-ZMP7rVo3mIykV+2+9J3UJ46jBk0WLaUAdn689aCwoqbBJiSnjAK/l8WvCWPIPm49" crossorigin="anonymous"></script>
    <script src="https://stackpath.bootstrapcdn.com/bootstrap/4.1.3/js/bootstrap.min.js" integrity="sha384-ChfqqxuZUCnJSK3+MXmPNIyE6ZbWh2IMqE241rYiqJxyMiZ6OW/JmZQ5stwEULTy" crossorigin="anonymous"></script> 

    <%--End Plugins--%>

    <%--CSS File Plugins--%>
    <link href="../General.css" rel="stylesheet" />   
    <link href="../ClientHomePage.css" rel="stylesheet" />  
    <link href="../DisplayService.css" rel="stylesheet" />
    <link href="../Login.css" rel="stylesheet" />

    <asp:ScriptManager ID="ScriptManager1" runat="server">  
    </asp:ScriptManager> 

    
<%--Register--%>   
    <section id="#" class="login d-flex align-items-center">
        <div class="container">
            <div data-aos="zoom-out" data-aos-delay="100">  
                <div class="shadow p-5 mb-5 bg-white rounded" data-aos="zoom-out" data-aos-delay="50" >   
                    <div class="row">
                        <div class="col-md-12">
                            <div class="row mb-2">
                                <h3 data-aos="fade-right"><span style="">S</span>et New Password</h3>
                            </div>
                        </div>
                    </div>
                    <div class="row mb-2">
                        <div class="col-md-11">
                            <asp:Label ID="lblNewPassword" runat="server" Text="New Password" AssociatedControlID="txtNewPassword"></asp:Label>
                            <asp:TextBox type="password" ID="txtNewPassword" class="form-control" runat="server"></asp:TextBox>
                            <asp:RegularExpressionValidator style="color:red" runat="server" ID="PasswordMin" ControlToValidate = "txtNewPassword" ValidationExpression = "^(?=.*[A-Za-z])(?=.*\d)(?=.*[@$!%*#?&])[A-Za-z\d@$!%*#?&]{8,}$" ErrorMessage="Password must contain minimum eight characters, at least one letter, one number and one special character" ValidationGroup="ChangePassword1"></asp:RegularExpressionValidator>
                        </div>
                        <div class="col-md-1 py-5">
                           <asp:RequiredFieldValidator ID="NewPasswordRequired" runat="server" ControlToValidate="txtNewPassword" ErrorMessage="New Password is required." ToolTip="New Password is required." ValidationGroup="ChangePassword1" ForeColor="Red">*</asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="row mb-2">
                        <div class="col-md-11">
                            <asp:Label ID="lblConfirmNewPassword" runat="server" Text="Confirm New Password" AssociatedControlID="txtConfirmNewPassword"></asp:Label>
                            <asp:TextBox type="password" ID="txtConfirmNewPassword" class="form-control" runat="server"></asp:TextBox>
                             <asp:CompareValidator ID="NewPasswordCompare" runat="server" ControlToCompare="txtNewPassword" ControlToValidate="txtConfirmNewPassword" Display="Dynamic" ErrorMessage="The Confirm New Password must match the New Password entry." ValidationGroup="ChangePassword1" ForeColor="red"></asp:CompareValidator>
                        </div>
                        <div class="col-md-1 py-5">
                          <asp:RequiredFieldValidator ID="ConfirmNewPasswordRequired" runat="server" ControlToValidate="txtConfirmNewPassword" ErrorMessage="Confirm New Password is required." ToolTip="Confirm New Password is required." ValidationGroup="ChangePassword1" ForeColor="Red">*</asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="row g-3 mb-3">     
                        <div class="col-md-11">
                            <div class="text-end text-lg-flex justify-content-center">  
                                <asp:LinkButton class="btnCancelChangePwd d-inline-flex align-items-center justify-content-center align-self-center" ID="btnCancel" CausesValidation="False" CommandName="Cancel" runat="server" OnClick="btnCancel_Click">  
                                <span>Cancel</span>                                
                                </asp:LinkButton>     

                                <asp:LinkButton ID="btnConfirm" class="btnChangePassword d-inline-flex align-items-center justify-content-center align-self-center" CommandName="Confirm" runat="server" ValidationGroup="ChangePassword1" OnClick="btnConfirm_Click">  
                                <span>Change Password</span>                                
                                </asp:LinkButton>                                        
                            </div>
                        </div>                              
                    </div>
                </div>
            </div>
<%--End Register--%>
           </div>
    </section>
    
    <%-- AOS script plugin--%>
<script src="https://unpkg.com/aos@next/dist/aos.js"></script>
<script>
    /* AOS plugin script*/
    AOS.init();
</script>
</asp:Content>
