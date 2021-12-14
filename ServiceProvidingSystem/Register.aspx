<%@ Page Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="ServiceProvidingSystem.Register" %>
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
                                    <asp:TextBox type="text" ID="txtName" class="form-control" runat="server" Placeholder="Full Name"></asp:TextBox>                                   
                                </div>
                                <div class="col-md-1 py-3">
                                     <asp:RequiredFieldValidator style="color:red" ID="NameRequired" runat="server" ControlToValidate="txtName" ErrorMessage="Name is required." ToolTip="Name is required." ValidationGroup="Register1">*</asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-11">
                                    <asp:TextBox type="text" ID="txtPhone" class="form-control" runat="server" Placeholder="Phone No. (eg. 01XXXXXXXX)"></asp:TextBox>   
                                      <asp:RegularExpressionValidator style="color:red" runat="server" ID="PhoneRegEx" ControlToValidate = "txtPhone" ValidationExpression = "^(\+?6?01)[02-46-9]-*[0-9]{7}$|^(\+?6?01)[1]-*[0-9]{8}$" ErrorMessage="Invalid format of phone number." ValidationGroup="Register1"></asp:RegularExpressionValidator>
                                </div>
                                <div class="col-md-1 py-3">
                                      <asp:RequiredFieldValidator style="color:red" ID="PhoneRequired" runat="server" ControlToValidate="txtPhone" ErrorMessage="Phone number is required." ToolTip="Phone number is required." ValidationGroup="Register1">*</asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="row mb-2">
                                <div class="col-md-11">
                                    <asp:TextBox type="text" ID="txtIC" class="form-control" runat="server" Placeholder="Identity Card No."></asp:TextBox>   
                                     <asp:RegularExpressionValidator style="color:red" runat="server" ID="ICCheck" ControlToValidate = "txtIC" ValidationExpression = "(([[0-9]{2})(0[1-9]|1[0-2])(0[1-9]|[12][0-9]|3[01]))([0-9]{2})([0-9]{4})" ErrorMessage="It must be a valid Identity No. (Eg. 990101121234)" ValidationGroup="Register1"></asp:RegularExpressionValidator>
                                </div>
                                <div class="col-md-1 py-3">
                                  <asp:RequiredFieldValidator style="color:red" ID="ICRequired" runat="server" ControlToValidate="txtIC" ErrorMessage="Identity Card No. is required." ToolTip="Identity Card No. is required." ValidationGroup="Register1">*</asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="row mb-2">
                                <div class="col-md-11">
                                    <h5 for="name" class="form-label">Date of Birth</h5>
                                    <asp:TextBox type="date" ID="txtDob" class="form-control" runat="server"></asp:TextBox>   
                                   <asp:RangeValidator style="color:red" ID="DateValidator" runat="server" ErrorMessage="Age must be between 18 and 100." ControlToValidate="txtDob" Type="Date" ValidationGroup="Register1" ></asp:RangeValidator>
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

                                        <asp:LinkButton ID="btnNext" class="btn-find-now btnNext d-inline-flex align-items-center justify-content-center align-self-center" runat="server" Text="NEXT" ValidationGroup="Register1" OnClick="btnNext_Click">  
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