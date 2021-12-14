<%@ Page Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="RequestOfferedService.aspx.cs" Inherits="ServiceProvidingSystem.Client.RequestOfferedService" %>

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
    <link href="../RequestOfferedService.css" rel="stylesheet" />

    <asp:ScriptManager ID="ScriptManager1" runat="server">  
    </asp:ScriptManager> 

    <section id="#" class="reqOffServ d-flex align-items-center">
    <div class="container">
        <div class="row mb-3 px-2">
            <h1 data-aos="fade-right"><span style="">R</span>equest Service</h1>
        </div>
<%--Post Service--%>
        <div class="shadow p-4 mb-5 bg-white rounded">
        <%--Service Details--%>
        <div class="row">
            <div class="col-md-12">
                <div data-aos="zoom-out" data-aos-delay="100">  
                    <div class="container shadow p-4 mb-5 bg-white rounded" data-aos="zoom-out" data-aos-delay="50" >       
                            <h3 data-aos="fade-right"><span style="">S</span>ervice Details</h3>                        
                        <div class="reqStatusMargin col-sm px-2 py-3">      
                            <div class="row">
                                <div class="col-md-3 py-4">
                                    <asp:Image ID="imgServicePicture" runat="server" class="imgServicePic" ImageUrl="~/Image/Foto_Formal.jpg" />
                                </div>
                                <div class="col-md-9 py-1">
                                    <div class="row mb-3">
                                        <div class="col-md-8 mx-2">   
                                            <asp:Label ID="lblServiceTypeCat" class="reqDetailsLabel serviceCatType align-middle" runat="server"></asp:Label>
                                            <span style="font-size: 14px;">(</span><asp:Hyperlink ID="lblLocation" class="reqDetailsLabel location align-middle" runat="server"></asp:Hyperlink><span style="font-size: 14px;">)</span>                          
                                            <br />
                                            <asp:Label ID="lblServiceName" class="reqDetailsLabel serviceName align-middle" runat="server"></asp:Label>                                            
                                        </div>
                                    </div>
                                    <div class="row mb-4 mt-2">                                        
                                        <div class="col-md-5 mx-2">
                                            <h4 for="name" class="form-label align-middle">Estimated Service Fee</h4>
                                            <asp:Label ID="lblPrice" class="reqDetailsLabel price" runat="server"></asp:Label>	
                                        </div>
                                        <div class="col-md-6 mx-2">
                                            <h4 for="name" class="form-label align-middle">Transport Fee</h4>
                                            <asp:Label ID="lblTransportFee" class="reqDetailsLabel price" runat="server"></asp:Label>	
                                        </div>
                                    </div>                             
                                    <div class="row">
                                        <div class="col-md-8 mx-2">                                    
                                            <h5 for="name" class="form-label align-middle">Remark</h5>
                                            <asp:Label ID="lblRemark" class="reqDetailsLabel remark" runat="server"></asp:Label>	
                                        </div>
                                    </div>                                   
                                </div>
                            </div>
                        </div>                             
                    </div>
                </div>
            </div>
        </div>

        <%--Personal Details--%>
        <div class="row">
            <div class="col-md-12">
                <div data-aos="zoom-out" data-aos-delay="100">  
                    <div class="container shadow p-4 mb-2 bg-white rounded" data-aos="zoom-out" data-aos-delay="50" >       
                            <h3><span style="">F</span>ill in Personal Details</h3>                        
                        <div class="reqStatusMargin col-sm px-2 py-3">                                  
                                <div class="row mt-3">        
                                    <div class="row">
                                        <div class="col-md-6">
                                            <h5 for="name" class="form-label">Full Name</h5>
				                            <asp:TextBox type="text" ID="txtName" class="form-control" runat="server" MaxLength="30"></asp:TextBox>       
                                             <asp:RequiredFieldValidator style="color:red" ID="NameRequired" runat="server" ControlToValidate="txtName" ErrorMessage="Name is required." ToolTip="Name is required." ValidationGroup="PostRequest">Name is required.</asp:RequiredFieldValidator>
                                        </div>
                                        <div class="col-md-6">
                                            <h5 for="phnno" class="form-label">Phone Number</h5>
				                            <asp:TextBox type="text" ID="txtPhnNo" class="form-control" runat="server" placeholder="01xxxxxxxx"></asp:TextBox>    
                                            <asp:RequiredFieldValidator style="color:red" ID="PhnNoValidator" runat="server" ControlToValidate="txtPhnNo" ErrorMessage="Phone Number is required." ToolTip="Phone Number is required." ValidationGroup="PostRequest">Phone number required.</asp:RequiredFieldValidator>
                                           <asp:RegularExpressionValidator style="color:red" runat="server" ID="PhoneRegEx" ControlToValidate = "txtPhnNo" ValidationExpression = "^(\+?6?01)[02-46-9]-*[0-9]{7}$|^(\+?6?01)[1]-*[0-9]{8}$" ErrorMessage="Invalid phone number format" ValidationGroup="PostRequest"></asp:RegularExpressionValidator>
                                        </div>
                                    </div>
                                </div>      
                            <div class="row mt-3"> 
                                    <div class="row">
                                        <div class="col-md-12">
                                            <h5 for="homeAddress" class="form-label">Home Address</h5>
				                            <asp:TextBox type="text" ID="txtHomeAddress" class="form-control" runat="server" placeholder=""></asp:TextBox>    
                                            <div class="postservMargin col-sm">  
                                                <div class="row">
                                                    <div class="col-12 d-flex justify-content-start">
                                                         <asp:RequiredFieldValidator style="color:red" class="d-flex justify-content-end" ID="HomeAddressValidator" runat="server" ControlToValidate="txtHomeAddress" ErrorMessage="Home address is required." ToolTip="Home address is required." ValidationGroup="PostRequest">Home address is required.</asp:RequiredFieldValidator>                    
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-12 d-flex justify-content-start">
                                                        <asp:CheckBox ID="cbSaveHomeAddress" class="cbSaveHome" runat="server"/>
                                                        <p style="margin-left: 10px; padding-top:1px;">Save Personal Info for next time uses </p>
                                                    </div>
                                                </div>                    
                                            </div>                                                                                               
                                        </div>
                                    </div> 
                                </div>
                            </div>  
                        
                                                                          
                        <div class="reqStatusMargin col-sm px-2 py-3">                  
                             <h3><span style="">F</span>ill in Your Preferences</h3> 
                                <div class="row mt-4">        
                                    <div class="row">
                                        <div class="col-md-6">
                                            <h5 for="budget" class="form-label">Budget (RM)</h5>
				                            <asp:TextBox type="text" ID="txtBudget" class="form-control" runat="server" placeholder="Eg: 200"></asp:TextBox>                        
                                           <asp:RequiredFieldValidator style="color:red" ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtBudget" ErrorMessage="Budget is required." ToolTip="Budget is required." ValidationGroup="PostRequest">Budget is Required</asp:RequiredFieldValidator>
                                           <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtBudget" ErrorMessage="Only numeric number is allowed." ToolTip="Only numeric number is allowed." ForeColor="Red" ValidationExpression="^[0-9]*$" ValidationGroup="PostRequest">Only Digit</asp:RegularExpressionValidator>
                                        </div>
                                    </div>
                                </div>      
                            <div class="row mt-3 mb-4"> 
                                    <div class="row">
                                        <div class="col-md-12">
                                            <h5 for="remark" class="form-label">Remarks (Optional)</h5>
				                            <asp:TextBox type="text" ID="txtRemark" class="form-control" runat="server" TextMode="MultiLine" Rows="5"></asp:TextBox>                                                                                              
                                        </div>
                                    </div> 
                                </div>
                                
                                <div class="row g-3">
                                    <hr />
                                    <div class="text-end text-lg-flex justify-content-center" data-aos="zoom-out">     
                                        <asp:LinkButton ID="btnBack" class="btn-find-now btnDismiss d-inline-flex align-items-center justify-content-center align-self-center" runat="server" OnClick="btnBack_Click">  
                                        <span>Back</span>    
                                            <i class="bi bi-arrow-left"></i>
                                        </asp:LinkButton> 
                                        <asp:LinkButton ID="btnRequest" class="btn-find-now btnView d-inline-flex align-items-center justify-content-center align-self-center" runat="server" OnClick="btnRequest_Click" onClientClick="return fnConfirm();" ValidationGroup="PostRequest">  
                                        <span>Confirm Request</span>     
                                        <i class="bi bi-arrow-right"></i>                           
                                        </asp:LinkButton>   
                                            <script type="text/javascript">
                                                function fnConfirm() {
                                                    return confirm("Confirm Request?");
                                                }
                                            </script>                                                            
                                    </div>
                                </div>   
                            </div> 
                    </div>
                </div>
            </div>
        </div>
        </div>


<%--End Post Service--%>            
    </div>
    </section>  
    <%-- AOS script plugin--%>
<script src="https://unpkg.com/aos@next/dist/aos.js"></script>
<script>
    /* AOS plugin script*/
    AOS.init();    
</script>
</asp:Content>