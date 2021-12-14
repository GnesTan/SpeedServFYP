<%@ Page Language="C#" Debug="true" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="ServiceProvidingSystem.Login" %>
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

    
<%--Login--%>   
    <section id="#" class="login d-flex align-items-center">
        <div class="container">
            <div data-aos="zoom-out" data-aos-delay="100">  
                <div class="shadow p-5 mb-5 bg-white rounded" data-aos="zoom-out" data-aos-delay="50" >   
                    <div class="row">
                        <div class="col-md-12">
                            <div class="row mb-2">
                                <h3 data-aos="fade-right"><span style="">L</span>ogin</h3>
                            </div>
                            
                            <div class="row">
                                <div class="col-md-12">
                                    <asp:TextBox type="text" ID="txtEmail" class="form-control" runat="server" Placeholder="Email Address"></asp:TextBox>
                                    <asp:RequiredFieldValidator style="color:red" ID="EmailRequired" runat="server" ControlToValidate="txtEmail" ErrorMessage="Email is required." ToolTip="Email is required." ValidationGroup="Login1">*</asp:RequiredFieldValidator>
                                </div>
                            </div> 
                            <div class="row mb-2">
                                <div class="col-md-12">
                                    <asp:TextBox type="text" ID="txtPassword" class="form-control" runat="server" Placeholder="Password" TextMode="Password"></asp:TextBox> 
                                     <asp:RequiredFieldValidator style="color:red" ID="PasswordRequired" runat="server" ControlToValidate="txtPassword" ErrorMessage="Password is required." ToolTip="Password is required." ValidationGroup="Login1">*</asp:RequiredFieldValidator>
                                </div>
                            </div> 
                            <div class="row mb-3">
                                <div class="col-md-6 px-3 d-flex justify-content-start">
                                    <asp:CheckBox ID="cbRemember" class="cbRememberMe" runat="server" />
                                    <p class="rmbMeText" style="margin-left: 15px;">Remember Me</p> 
                                </div>
                                <div class="col-md-6 px-3 d-flex flex-row-reverse">
                                     <asp:HyperLink ID="HyperLink2" class="hlFrgtPwd" runat="server" NavigateUrl="~/PasswordRecovery.aspx">Forgot Password?</asp:HyperLink>                                   
                                </div>
                            </div> 
                            <div class="row mb-3">
                                <div class="col-md-12 px-2">
                                    <div class="text-center text-lg-flex justify-content-center">  
                                        <asp:Button  ID="btnLogin" class="w-100 btn btnLogin btn-lg" runat="server" Text="LOGIN" ValidationGroup="Login1" OnClick="btnLogin_Click"/>
                                        <asp:Label ID="lblWrong" ForeColor="Red" runat="server"></asp:Label>
                                    </div>
                                    <div class="divider d-flex align-items-center my-4">
                                        <p class="text-center fw-bold mx-3 mb-0 text-muted">OR</p>
                                    </div>
                                </div>
                            </div>
                            <div class="row mb-3">
                                <div class="col-md-6 px-2">
                                    <div class="text-end text-lg-flex justify-content-center">  
                                        <asp:Button ID="hlServicer" class="w-100 btnServicer btn-lg" runat="server" Text="Register As Servicer" OnClick="hlServicer_Click" />                                          
                                    </div>
                                </div>
                                <div class="col-md-6 px-2">
                                    <div class="text-end text-lg-flex justify-content-center">  
                                       <asp:Button ID="hlClient1" class="w-100 btnClient btn-lg" runat="server" Text="Register As Client" OnClick="hlClient_Click" />                                         
                                    </div>
                                </div>
                            </div>
                            <div class="row mb-3">
                                <div class="col-md-12 px-2">
                                    <div class="text-end text-lg-flex justify-content-center">  
                                        <asp:Button ID="btnStaffLogin" class="w-100 btn btnStaffLogin btn-lg" runat="server" Text="Login As Staff" OnClick="btnStaffLogin_Click" CausesValidation="false"/>                                          
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
<%--End Login--%>
           </div>
    </section>
    
    <%-- AOS script plugin--%>
<script src="https://unpkg.com/aos@next/dist/aos.js"></script>
<script>
    /* AOS plugin script*/
    AOS.init();
</script>
</asp:Content>