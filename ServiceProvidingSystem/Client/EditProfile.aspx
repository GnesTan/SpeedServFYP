<%@ Page Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="EditProfile.aspx.cs" Inherits="ServiceProvidingSystem.Client.ClientManagement.EditProfile" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server"> 

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
    <link href="../PostServiceRequest.css" rel="stylesheet" />

    <asp:ScriptManager ID="ScriptManager1" runat="server">  
    </asp:ScriptManager> 

    <section id="#" class="requestServ d-flex align-items-center">
    <div class="container">
    <%--Edit Profile--%>



    <div class="requestForm" data-aos="zoom-out" data-aos-delay="100">
        <div class="row mb-3 px-2">
            <h1 data-aos="fade-right"><span style="">E</span>dit Profile</h1>
        </div>
        <div class="shadow p-4 mb-5 bg-white rounded">
            <div class="row mb-3">
                <div class="col-md-6">
                    <h5 for="name" class="form-label">Full Name</h5>
				    <asp:TextBox type="text" ID="txtName" class="form-control" runat="server" MaxLength="30"></asp:TextBox>       
                    <asp:RequiredFieldValidator style="color:red" ID="FirstNameRequired" runat="server" ControlToValidate="txtName" ErrorMessage="Name is required." ToolTip="Name is required." ValidationGroup="EditProfile">*</asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator style="color:red" Display = "Dynamic" ControlToValidate = "txtName" ID="NameREValidator" ValidationExpression = "^[\s\S]{0,30}$" runat="server" ErrorMessage="Maximum 30 characters allowed." ValidationGroup="EditProfile"></asp:RegularExpressionValidator>
                </div>
                <div class="col-md-6">
                    <h5 for="icno" class="form-label">Identity No</h5>
				    <asp:TextBox type="text" ID="txtIc" class="form-control" runat="server" MaxLength="30" Enabled="false"></asp:TextBox>                     
                </div>
            </div>
            <div class="row mb-5">
                <div class="col-md-6">
                    <h5 for="dob" class="form-label">Date of Birth</h5>
				    <asp:TextBox type="date" ID="txtDob" class="form-control" runat="server"></asp:TextBox> 
                    <asp:RangeValidator style="color:red" ID="DateValidator" runat="server" ErrorMessage="Age must be between 18 and 100." ControlToValidate="txtDob" Type="Date" ValidationGroup="EditProfile" ></asp:RangeValidator>
                </div>
                <div class="col-md-6">
                    <h5 for="phnno" class="form-label">Gender</h5>
				        <asp:DropDownList class="form-select" ID="ddlGender" runat="server">
                            <asp:ListItem Value="M">Male</asp:ListItem>
                            <asp:ListItem Value="F">Female</asp:ListItem>
                            <asp:ListItem Value="O">Other</asp:ListItem>
                        </asp:DropDownList>                  
                </div>
            </div>
            <div class="row mb-5">
                <div class="col-md-4">
                    <asp:image height="140px" Width="140px" ToolTip = "Profile Picture" ID="imgProfile" runat="server" ImageUrl ="~/Image/generaluser.png" style="border-radius:100px; box-shadow: 0 0 0 3px #b3b3b3; text-decoration:none;" ></asp:image>                        
                </div>
                <div class="col-md-8 py-4">
                    <h5 for="dob" class="form-label">Profile Picture</h5>
				    <asp:FileUpload ID="ImageUpload" runat="server"/>                 
                </div>
            </div>
            <div class="row mb-3">
                <div class="col-md-6">
                    <h5 for="phnNo" class="form-label">Phone No.</h5>
				    <asp:TextBox type="text" ID="txtPhone" class="form-control" runat="server" MaxLength="20"></asp:TextBox>
                     <asp:RequiredFieldValidator style="color:red" ID="PhoneNoRequired" runat="server" ControlToValidate="txtPhone" ErrorMessage="Phone No is required." ToolTip="Phone No is required."  ValidationGroup="EditProfile">*</asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator style="color:red" runat="server" ID="PhoneRegEx" ControlToValidate = "txtPhone" ValidationExpression = "^(\+?6?01)[02-46-9]-*[0-9]{7}$|^(\+?6?01)[1]-*[0-9]{8}$" ErrorMessage="*Invalid format of phone number." ValidationGroup="EditProfile"></asp:RegularExpressionValidator>
                </div>
                <div class="col-md-6">
                    <h5 for="email" class="form-label">Email Address</h5>
				    <asp:TextBox type="text" ID="txtEmail" class="form-control" runat="server" MaxLength="40" Enabled="false"></asp:TextBox> 
                </div>
            </div>
            <div class="row mb-3">
                <div class="col-md-12">
                    <h5 for="homeAdd" class="form-label">Home Address</h5>
				    <asp:TextBox type="text" ID="txtAddress" class="form-control" runat="server" TextMode="MultiLine" Rows="4" ></asp:TextBox>                       
                </div>
            </div>
            <div class="row g-3">
                <hr />
                <div class="text-end text-lg-flex justify-content-center" data-aos="zoom-out">     
                    <asp:LinkButton ID="btnCancel" class="btn btnCancel d-inline-flex align-items-center justify-content-center align-self-center" runat="server" OnClick="btnCancel_Click" CausesValidation="false">  
                    <span>Cancel</span>                            
                    </asp:LinkButton> 
                    <asp:LinkButton ID="btnSave" class="btn btnRequest d-inline-flex align-items-center justify-content-center align-self-center" runat="server" OnClick="btnSave_Click" onClientClick="return fnConfirm();" ValidationGroup="EditProfile">  
                    <span>Confirm Edit</span>                                                    
                    </asp:LinkButton>   
                        <script type="text/javascript">
                            function fnConfirm() {
                                return confirm("Confirm Edit?");
                            }
                        </script>                                                            
                </div>
            </div>  
        </div>
    </div>












    <%--End Edit Profile--%>
    
    </div>   
    </section>  
    <%-- AOS script plugin--%>
<script src="https://unpkg.com/aos@next/dist/aos.js"></script>
<script>
    /* AOS plugin script*/
    AOS.init();    
</script>
</asp:Content>