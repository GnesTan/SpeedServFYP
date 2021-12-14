<%@ Page Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Register2.aspx.cs" Inherits="ServiceProvidingSystem.Register2" %>
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
                                <h3 data-aos="fade-right"><span style="">S</span>ign Up As <asp:Label ID="lblUser" runat="server" ForeColor="black"></asp:Label></h3>
                            </div>
                            <div class="row mb-2">
                                <div class="col-md-11">
                                    <asp:TextBox type="text" ID="txtEmail" class="form-control" runat="server" Placeholder="Email Address"></asp:TextBox>
                                    <asp:RegularExpressionValidator style="color:red" ID="EmailREValidator" runat="server" ControlToValidate="txtEmail" ErrorMessage="Email Address must be a valid format. Eg: abc123@gmail.com" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ValidationGroup="Register2"></asp:RegularExpressionValidator>
                                </div>
                                <div class="col-md-1 py-3">
                                     <asp:RequiredFieldValidator style="color:red" ID="EmailRequired" runat="server" ControlToValidate="txtEmail" ErrorMessage="Email Address is required." ToolTip="Email Address is required." ValidationGroup="Register2">*</asp:RequiredFieldValidator> 
                                </div>
                            </div>
                            <div class="row mb-2">
                                <div class="col-md-11">
                                    <asp:TextBox type="password" ID="txtPassword" class="form-control" runat="server" Placeholder="Password"></asp:TextBox>
                                    <asp:RegularExpressionValidator style="color:red" runat="server" ID="PasswordMin" ControlToValidate = "txtPassword" ValidationExpression = "^(?=.*[A-Za-z])(?=.*\d)(?=.*[@$!%*#?&])[A-Za-z\d@$!%*#?&]{8,}$" ErrorMessage="Password must contain minimum eight characters, at least one letter, one number and one special character" ValidationGroup="Register2"></asp:RegularExpressionValidator>
                                </div>
                                <div class="col-md-1 py-3">
                                    <asp:RequiredFieldValidator style="color:red" ID="PasswordRequired" runat="server" ControlToValidate="txtPassword" ErrorMessage="Password is required." ToolTip="Password is required." ValidationGroup="Register2">*</asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="row mb-3">
                                <div class="col-md-11">
                                    <asp:TextBox type="password" ID="txtConfirmPass" class="form-control" runat="server" Placeholder="Confirm Password"></asp:TextBox>                                   
                                </div>
                                <div class="col-md-1 py-3">
                                    <asp:RequiredFieldValidator style="color:red" ID="ConfirmPassRequired" runat="server" ControlToValidate="txtConfirmPass" ErrorMessage="Confirm Password is required." ToolTip="Confirm Password is required." ValidationGroup="Register2">*</asp:RequiredFieldValidator>
                                </div>
                            </div>                            
                            <div class="row mb-2">
                                <div class="col-md-11">
                                    <div class="text-center text-lg-flex justify-content-center">
                                        <p class="rmbMeText">
                                            * By continuing, I confirm that I have read and agree to the SpeedServ
                                            <a href="TermOfUse.aspx" runat="server" target="_blank">Terms of Use</a>&nbsp;and&nbsp;<a href="PrivacyPolicy.aspx" runat="server" target="_blank">Privacy Policy</a>
                                        </p>
                                    </div>                                   
                                </div>
                                <div class="col-md-1 py-3">
                                   
                                </div>
                            </div>
                            <div class="row g-3 mb-3">     
                                <div class="col-md-11">
                                    <div class="text-end text-lg-flex justify-content-center">  
                                        <asp:LinkButton ID="btnBack" class="btn-find-now btnBack d-inline-flex align-items-center justify-content-center align-self-center" runat="server" PostBackUrl="~/Login.aspx" CausesValidation="false">  
                                        <span>Back</span>                                
                                        </asp:LinkButton>     

                                        <asp:LinkButton ID="btnNext" class="btn-find-now btnNext d-inline-flex align-items-center justify-content-center align-self-center" runat="server" Text="NEXT" ValidationGroup="Register2" OnClick="btnNext_Click">  
                                        <span>Next</span>                                
                                        </asp:LinkButton>                                        
                                    </div>
                                </div>                              
                            </div>
                            <div class="row">
                                <div class="col-md-11">
                                    <asp:Label ID="lblError" ForeColor="Red" runat="server"></asp:Label>
                                </div>
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
