<%@ Page Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="ViewRequestStatus.aspx.cs" Inherits="ServiceProvidingSystem.Client.ViewRequestStatus" %>

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
    <link href="../ViewRequestStatus.css" rel="stylesheet" />    
    <asp:ScriptManager ID="ScriptManager1" runat="server">  
    </asp:ScriptManager> 

    <section id="#" class="requestServ d-flex align-items-center">
    <div class="container">
<%--Post Service--%>
        <%--Progress Bar--%>
    <div data-aos="zoom-out" data-aos-delay="100">  
        <div class="container shadow p-4 mb-5 bg-white rounded" data-aos="zoom-out" data-aos-delay="50" >       
             <h3 data-aos="fade-right"><span style="">R</span>equest Status</h3>
            <div class="row" data-aos="fade-right" data-aos-delay="200">                                       
                <div class=" col justify-content-center" style="padding-right: 100px;" data-aos="zoom-out" data-aos-delay="300">
                   <div class="row">
                     <div class="col align-content-center">
                       <ul class="progressbar">
                         <li class="active">Sent</li>
                         <li class="active">Looking</li>
                         <li id="servbar" runat="server">Serving</li>
                         <li>Pending Payment</li>
                      </ul>
                     </div>
                  </div>
                </div>                                 
            </div>
             <hr class="topline"/>
            <hr />
            <div class="row g-3">     
                    <div class="text-end text-lg-flex justify-content-center" data-aos="zoom-out" data-aos-delay="400">                        
                        <asp:LinkButton ID="LinkButton2" class="btn-find-now btnRefresh d-inline-flex align-items-center justify-content-center align-self-center" runat="server" OnClick="btnRefresh_Click">  
                        <span>Refresh</span>
                        <i class="bi bi-arrow-clockwise"></i>
                        </asp:LinkButton>                                                              
                    </div>
                </div>        
            </div>
        </div>
        <%--Progress Bar--%>
    <div data-aos="zoom-out" data-aos-delay="200">  
        <div class="container shadow p-4 mb-5 bg-white rounded" data-aos="zoom-out" data-aos-delay="50" >       
             <h3 data-aos="fade-right"><span style="">R</span>equest Details</h3>
        <div class="reqStatusMargin col-sm">        
            <div class="row mt-4">
                <div class="col-md-12">
                    <h4 for="name" class="form-label">Service Title</h4>
                    <asp:Label ID="lblServiceTitle" class="reqDetailsLabel title" runat="server"></asp:Label>				                        
                </div>
            </div>
            <div class="row mt-4">
                <div class="col-md-4">
                    <h5 for="name" class="form-label"><asp:Label ID="lblDateText" runat="server"></asp:Label></h5>
				      <asp:Label ID="lblDateTime" runat="server" class="reqDetailsLabel"></asp:Label>	                          
                </div>
                <div class="col-md-4">
                    <h5 for="name" class="form-label">Service Category</h5>
				      <asp:Label ID="lblServiceCat" runat="server" class="reqDetailsLabel"></asp:Label>	                         
                </div>
                <div class="col-md-4">
                    <h5 for="name" class="form-label">Service Type</h5>
				      <asp:Label ID="lblServiceType" runat="server" class="reqDetailsLabel"></asp:Label>	                       
                </div>
            </div>
            <div class="row mt-4">
                <div class="col-md-12">
                    <h5 for="name" class="form-label">Home Address</h5>
				      <asp:Label ID="lblHomeAddress" runat="server" class="reqDetailsLabel"></asp:Label>	                          
                </div>
            </div>
            <div class="row mt-4">
                <div class="col-md-4">
                    <h5 for="name" class="form-label">State</h5>
				      <asp:Label ID="lblState" runat="server" class="reqDetailsLabel"></asp:Label>	                          
                </div>
                <div class="col-md-4">
                    <h5 for="name" class="form-label">District</h5>
				      <asp:Label ID="lblDistrict" runat="server" class="reqDetailsLabel"></asp:Label>	                          
                </div>
                <div class="col-md-4">
                    <h5 for="name" class="form-label"><asp:Label ID="txtEstTime" runat="server" Text="Estimate Time Completed"></asp:Label></h5>
				      <asp:Label ID="lblEstTime" runat="server" class="reqDetailsLabel"></asp:Label>	                          
                </div>
            </div>
            <div class="row mt-4">
                <div class="col-md-12">
                    <h5 for="name" class="form-label">Remark</h5>
				      <asp:Label ID="lblRemark" runat="server" class="reqDetailsLabel"></asp:Label>	                          
                </div>
            </div>
        </div>     
             <hr class="topline"/>
            <hr />
            <div class="row g-3">     
                    <div class="text-end text-lg-flex justify-content-center">  
                        <asp:LinkButton ID="btnViewServicer" class="btn-find-now btnView d-inline-flex align-items-center justify-content-center align-self-center" runat="server" Visible="False" OnClick="btnViewServicer_Click">  
                        <span>View Servicer</span>                        
                        </asp:LinkButton>  

                        <asp:LinkButton ID="btnCancel" class="btn-find-now d-inline-flex align-items-center justify-content-center align-self-center" runat="server" OnClick="btnCancel_Click">  
                        <span>Cancel</span>                        
                        </asp:LinkButton>                            
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
