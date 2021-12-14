<%@ Page Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="PostServiceRequest.aspx.cs" Inherits="ServiceProvidingSystem.Client.PostServiceRequest" %>

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
<%--Post Service--%>
    <div class="requestForm" data-aos="zoom-out" data-aos-delay="100">
        <div class="py-5 text-center" data-aos="fade-right" data-aos-delay="200">
            <h1 data-aos="fade-right"><span style="">R</span>equest</h1>
        </div>                                    
        <h3 class="mb-3">Personal Info</h3> 
        <div class="postservMargin col-sm">        
            <div class="row">
                <div class="col-md-6">
                    <h5 for="name" class="form-label">Full Name</h5>
				    <asp:TextBox type="text" ID="txtName" class="form-control" runat="server" MaxLength="30"></asp:TextBox>       
                     <asp:RequiredFieldValidator style="color:red" ID="NameRequired" runat="server" ControlToValidate="txtName" ErrorMessage="Name is required." ToolTip="Name is required." ValidationGroup="PostRequest">Name is required.</asp:RequiredFieldValidator>
                </div>
                <div class="col-md-6">
                    <h5 for="phnno" class="form-label">Phone Number</h5>
				    <asp:TextBox type="text" ID="txtPhnNo" class="form-control" runat="server" placeholder="01xxxxxxxx"></asp:TextBox>    
                    <asp:RequiredFieldValidator style="color:red" ID="PhnNoValidator" runat="server" ControlToValidate="txtPhnNo" ErrorMessage="Phone number required." ToolTip="Phone Number is required." ValidationGroup="PostRequest">Phone number required</asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator style="color:red" runat="server" ID="PhoneRegEx" ControlToValidate = "txtPhnNo" ValidationExpression = "^(\+?6?01)[02-46-9]-*[0-9]{7}$|^(\+?6?01)[1]-*[0-9]{8}$" ErrorMessage="Invalid phone number format" ValidationGroup="PostRequest"></asp:RegularExpressionValidator>
                </div>
            </div>
        </div>     
        <div class="postservMargin col-sm">        
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
        
        <hr />
            <%--Location Details--%>
            <h3 class="mb-3">Location</h3>
 
        <div class="postservMargin col-sm">
            <div class="row">
                <div class="col-md-6 mb-3">
                <h5 for="state" class="form-label">State</h5>
                <asp:UpdatePanel ID="statepanel" runat="server">  
                    <ContentTemplate>
                <asp:DropDownList class="form-select" ID="ddlState" runat="server" AppendDataBoundItems="true" AutoPostBack="true" OnSelectedIndexChanged="ddlState_SelectedIndexChanged">
                </asp:DropDownList>                              
                </ContentTemplate>
                    <Triggers>  
                    <asp:AsyncPostBackTrigger ControlID ="ddlState" />  
                    </Triggers>  
                </asp:UpdatePanel>
                </div>
                <div class="col-md-6">
                    <h5 for="district" class="form-label">City</h5>
                        <asp:UpdatePanel ID="districtpanel" runat="server">  
                        <ContentTemplate>
                    <asp:DropDownList class="form-select" ID="ddlDistrict" runat="server" AutoPostBack="true" AppendDataBoundItems="true" ValidationGroup="FindNow" OnSelectedIndexChanged="ddlDistrict_SelectedIndexChanged">
                        <asp:ListItem Value="--Please Select District--">--Please Select District--</asp:ListItem>
                    </asp:DropDownList>                      
                            <asp:Label ID="lblError" runat="server" Text="" ForeColor="Red"></asp:Label>
                    </ContentTemplate>  
                        <Triggers>  
                        <asp:AsyncPostBackTrigger ControlID ="ddlDistrict" />  
                        </Triggers>  
                    </asp:UpdatePanel>        
                    </div>
                </div>
            </div>
        <hr />
            <%--Request Details--%>
            <h3 class="mb-3">Request Details</h3>

            <div class="postservMargin col-sm">        
                <div class="row">
                    <div class="col-md-12">
                        <h5 for="servicetitle" class="form-label">Service Title</h5>
				        <asp:TextBox type="text" ID="txtServiceTitle" class="form-control" runat="server" placeholder="Request for xxx"></asp:TextBox>     
                          <asp:RequiredFieldValidator style="color:red" ID="serviceTitleValidator" runat="server" ControlToValidate="txtServiceTitle" ErrorMessage="Service Title cannot be blanked" ToolTip="Service Title cannot be blanked" ValidationGroup="PostRequest">Service Title cannot be blanked</asp:RequiredFieldValidator>
                    </div>
                </div>
            </div>
            <div class="postservMargin col-sm"> 
                <div class="row">
                <div class="postservMargin col-sm">
                    <h5 for="service-cat" class="form-label">Service Category</h5>
                    <asp:UpdatePanel ID="servcatpanel" runat="server">  
                        <ContentTemplate>
                            <asp:DropDownList class="form-select" ID="ddlCategory" runat="server" AppendDataBoundItems="true" OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged" AutoPostBack="True">
                                <asp:ListItem Value="Installation">Installation</asp:ListItem>
                                <asp:ListItem Value="Repairing">Repairing</asp:ListItem>
                                <asp:ListItem Value="Others">Others</asp:ListItem>                        
                            </asp:DropDownList>
                        </ContentTemplate>
                      <Triggers>  
                      <asp:AsyncPostBackTrigger ControlID ="ddlCategory" />  
                      </Triggers>  
                    </asp:UpdatePanel>
                </div>                  
                <div class="postservMargin col-sm">       
                    <h5 for="service-type" class="form-label">Service Type</h5>
                        <asp:UpdatePanel ID="installservpanel" runat="server">  
                        <ContentTemplate>
                            <asp:DropDownList class="form-select" ID="ddlInstallType" runat="server">
                                <asp:ListItem Value="Vehicles">Vehicles</asp:ListItem>
                                <asp:ListItem Value="Home appliances">Home appliances</asp:ListItem>
                                <asp:ListItem Value="Mobile & Gadget">Mobile & Gadget</asp:ListItem>
                                <asp:ListItem Value="Computer & Accessories">Computer & Accessories</asp:ListItem>
                                <asp:ListItem Value="Musical instrument">Musical instrument</asp:ListItem>
                                <asp:ListItem Value="Industrial Machine">Industrial Machine</asp:ListItem>
                                <asp:ListItem Value="Watches">Watches</asp:ListItem>
                                <asp:ListItem Value="Gaming PC">Gaming PC</asp:ListItem>
                                <asp:ListItem Value="Camera & Drones">Camera & Drones</asp:ListItem>
                                <asp:ListItem Value="Network Infrastructure">Network Infrastructure</asp:ListItem>
                                <asp:ListItem Value="Others">Others</asp:ListItem>
                            </asp:DropDownList>
                          </ContentTemplate>
                          <Triggers>  
                          <asp:AsyncPostBackTrigger ControlID ="ddlInstallType" />  
                          </Triggers>  
                     </asp:UpdatePanel>
                    <asp:UpdatePanel ID="repairservpanel" runat="server">  
                        <ContentTemplate>
                            <asp:DropDownList class="form-select" ID="ddlRepairType" runat="server" Visible="False">
                                <asp:ListItem Value="Vehicles">Vehicles</asp:ListItem>
                                <asp:ListItem Value="Home appliances">Home appliances</asp:ListItem>
                                <asp:ListItem Value="Mobile & Gadget">Mobile & Gadget</asp:ListItem>
                                <asp:ListItem Value="Computer & Accessories">Computer & Accessories</asp:ListItem>
                                <asp:ListItem Value="Musical instrument">Musical instrument</asp:ListItem>
                                <asp:ListItem Value="Industrial Machine">Industrial Machine</asp:ListItem>
                                <asp:ListItem Value="Clothing">Clothing</asp:ListItem>
                                <asp:ListItem Value="Shoes">Shoes</asp:ListItem>
                                <asp:ListItem Value="Watches">Watches</asp:ListItem>
                                <asp:ListItem Value="Game Console">Game Console</asp:ListItem>
                                <asp:ListItem Value="Camera & Drones">Camera & Drones</asp:ListItem>
                                <asp:ListItem Value="Network Infrastructure">Network Infrastructure</asp:ListItem>
                                <asp:ListItem Value="Others">Others</asp:ListItem>
                            </asp:DropDownList>
                        </ContentTemplate>
                      <Triggers>  
                      <asp:AsyncPostBackTrigger ControlID ="ddlRepairType" />  
                      </Triggers>  
                    </asp:UpdatePanel>
                    <asp:UpdatePanel ID="otherservpanel" runat="server">  
                    <ContentTemplate>
                        <asp:DropDownList class="form-select" ID="ddlOtherType" runat="server" Visible="False">
                            <asp:ListItem Value="Insecticide">Insecticide</asp:ListItem>

                            <asp:ListItem Value="Porter services">Porter services</asp:ListItem>

                            <asp:ListItem Value="Data entry">Data entry</asp:ListItem>

                            <asp:ListItem Value="Distribute flyers">Distribute flyers</asp:ListItem>

                            <asp:ListItem Value="Transportation">Transportation</asp:ListItem>

                            <asp:ListItem Value="Others">Others</asp:ListItem>


                        </asp:DropDownList>
                    </ContentTemplate>
                      <Triggers>  
                      <asp:AsyncPostBackTrigger ControlID ="ddlOtherType" />  
                      </Triggers>  
                    </asp:UpdatePanel>
                </div>
                <div class="postservMargin col-sm">
                    <h5 for="budget" class="form-label">Budget (RM)</h5>
				    <asp:TextBox type="text" ID="txtBudget" class="form-control" runat="server" placeholder="Eg: 200"></asp:TextBox>                        
                    <asp:RequiredFieldValidator style="color:red" ID="BudgetValidator" runat="server" ControlToValidate="txtBudget" ErrorMessage="Budget is required" ToolTip="Budget is required" ValidationGroup="PostRequest">Budget is Required</asp:RequiredFieldValidator>                   
                     <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtBudget" ErrorMessage="Only numeric allowed." ToolTip="Only numeric number is allowed" ForeColor="Red" ValidationExpression="^[0-9]*$" ValidationGroup="PostRequest">Only Digit</asp:RegularExpressionValidator>
                </div>
                </div>   
                <div class="row">
                    <div class="postservMargin col-sm">
                        <h5 for="remark" class="form-label">Remarks (Optional)</h5>
				        <asp:TextBox type="text" ID="txtRemark" class="form-control" runat="server" TextMode="MultiLine" Rows="5"></asp:TextBox>    
                    </div>
                </div>
                <%--Buttons--%>
                 <div class="row">
                    <div class="postservMargin col-sm">
                        <div class="col">
                            <div class="text-end text-lg-flex justify-content-center">                        
                                <asp:LinkButton ID="btnCancel" class="btn btnCancel d-inline-flex align-items-center justify-content-center align-self-center" runat="server" OnClick="btnCancel_Click">  
                                <span>Cancel</span>                                
                                </asp:LinkButton>             
                                <asp:LinkButton ID="btnRequest" class="btn btnRequest d-inline-flex align-items-center justify-content-center align-self-center" runat="server" OnClick="btnRequest_Click" onClientClick="return fnConfirm();" ValidationGroup="PostRequest">  
                                <span>Request</span>                                
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
<%--End Post Service--%>
    
        </div>
    </div>
    </section>  
    <%-- AOS script plugin--%>
<script src="https://unpkg.com/aos@next/dist/aos.js"></script>
<script>
    /* AOS plugin script*/
    AOS.init();    
</script>
</asp:Content>