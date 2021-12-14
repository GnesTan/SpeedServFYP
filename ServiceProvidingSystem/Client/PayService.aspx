﻿<%@ Page Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="PayService.aspx.cs" Inherits="ServiceProvidingSystem.Client.PayService" %>

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
    <link href="../PayService.css" rel="stylesheet" />

    <asp:ScriptManager ID="ScriptManager1" runat="server">  
    </asp:ScriptManager> 

    <section id="#" class="payServ d-flex align-items-center">
    <div class="container">
<%--Payment--%>

      <%--Progress Bar--%>
    <div data-aos="zoom-out" data-aos-delay="100">  
        <div class="container shadow p-4 mb-5 bg-white rounded" data-aos="zoom-out" data-aos-delay="50" >       
             <h3 data-aos="fade-right"><span style="">P</span>ayment Status</h3>
            <div class="row" data-aos="fade-right" data-aos-delay="200">                                       
                <div class=" col justify-content-center" style="padding-right: 100px;" data-aos="zoom-out" data-aos-delay="300">
                   <div class="row">
                     <div class="col align-content-center">
                       <ul class="progressbar">
                         <li class="active">Completed Service</li>
                         <li class="active">Pending Payment</li>
                         <li id="servbar" runat="server">Verifying Payment</li>
                         <li>Payment Complete</li>
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
        <%--Service Details--%>
    <div data-aos="zoom-out" data-aos-delay="200">  
        <div class="container shadow p-4 mb-5 bg-white rounded" data-aos="zoom-out" data-aos-delay="50" >       
             <h3 data-aos="fade-right"><span style="">S</span>ervice Details</h3>
        <div class="reqStatusMargin col-sm">        
            <div class="row mt-4">
                <div class="col-md-12">
                    <h4 for="name" class="form-label">Service Title</h4>
                    <asp:Label ID="lblServiceTitle" class="reqDetailsLabel title" runat="server"></asp:Label>				                        
                </div>
            </div>
            <div class="row mt-4">
                <div class="col-md-4">
                    <h5 for="name" class="form-label">Request Date</h5>
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
                <div class="col-md-6">
                    <h5 for="name" class="form-label">State</h5>
				      <asp:Label ID="lblState" runat="server" class="reqDetailsLabel"></asp:Label>	                          
                </div>
                <div class="col-md-6">
                    <h5 for="name" class="form-label">District</h5>
				      <asp:Label ID="lblDistrict" runat="server" class="reqDetailsLabel"></asp:Label>	                          
                </div>
            </div>
            <div class="row mt-4">
                <div class="col-md-12">
                    <h5 for="name" class="form-label">Remark</h5>
				      <asp:Label ID="lblRemark" runat="server" class="reqDetailsLabel"></asp:Label>	                          
                </div>
            </div>
        </div>     
             
        </div>
 </div>
 <%--Payment Details--%>
    <div>          
        <div class="container">  
            <div class="row">            
              <div class="col-md-7  p-4 mb-5 shadow bg-white rounded" data-aos="fade-right" data-aos-delay="200">
                    <h3 ><span style="">P</span>ayment Summary</h3>
                        <div class="reqStatusMargin col-sm">        
                            <div class="row mt-4">
                                <div class="col-md-6">
                                    <h4 for="name" class="form-label">Subtotal</h4>
                                    <asp:Label ID="lblSubTotal" class="reqDetailsLabel subtotal" runat="server"></asp:Label>				                        
                                </div>
                                <div class="col-md-6 d-flex justify-content-center">
                                    <asp:LinkButton ID="LinkButton1" class="btn-find-now btnRefresh d-inline-flex align-items-center justify-content-center align-self-center" runat="server" OnClick="btnMoreDetails_Click">  
                                    <span>View Pricing</span>
                                    <i class="bi bi-arrow-right"></i>
                                    </asp:LinkButton>                            
                                </div>
                            </div>
                            <hr class="topline"/> 
                            <div class="row mt-4">
                                <div class="col-md-4">
                                    <h5 for="name" class="form-label">Current Reward Points</h5>
                                    <asp:UpdatePanel ID="statepanel" runat="server">  
                                        <ContentTemplate>
                                   <asp:TextBox type="text" ID="lblRewardPoints" class="txtDisplay" style="border: none; font-size: 20px; background-color: white;" runat="server" Enabled="false" AutoPostBack="True"></asp:TextBox>                                                                     
                                    </ContentTemplate>
                                        <Triggers>  
                                        <asp:AsyncPostBackTrigger ControlID ="lblRewardPoints" />  
                                        </Triggers>  
                                    </asp:UpdatePanel>				                   
                                </div>
                                <div class="col-md-4">
                                    <h5 for="name" class="form-label">Total Discount</h5>
				                   <asp:UpdatePanel ID="discountpanel" runat="server">  
                                        <ContentTemplate>
                                   <asp:TextBox type="text" ID="lblDis" class="txtDisplay" style="border: none; font-size: 20px; background-color: white;" runat="server" Enabled="false" AutoPostBack="True" Text="-"></asp:TextBox>                                                                     
                                    </ContentTemplate>
                                        <Triggers>  
                                        <asp:AsyncPostBackTrigger ControlID ="lblDis" />  
                                        </Triggers>  
                                    </asp:UpdatePanel>	
                                </div>
                                <div class="col-md-4" autopostback="True">
                                    <h5 for="name" class="form-label">Points After Discount</h5>
				                   <asp:UpdatePanel ID="remainpanel" runat="server">  
                                        <ContentTemplate>
                                   <asp:TextBox type="text" ID="lblRemain" class="txtDisplay" style="border: none; font-size: 20px; background-color: white;" runat="server" Enabled="false" AutoPostBack="True" Text="-"></asp:TextBox>                                                                     
                                    </ContentTemplate>
                                        <Triggers>  
                                        <asp:AsyncPostBackTrigger ControlID ="lblRemain" />  
                                        </Triggers>  
                                    </asp:UpdatePanel>	
                                </div>
                            </div>
                           <hr class="topline"/>
                            <div class="row mt-4">
                            <div class="col-12 d-flex justify-content-start">
                                <asp:UpdatePanel ID="convertpanel" runat="server">  
                                    <ContentTemplate>
                                <asp:CheckBox ID="cbConfirmDiscount" class="cbSaveHome" runat="server" OnCheckedChanged="cbConfirmDiscount_CheckedChanged" AutoPostBack="True" />
                                    </ContentTemplate>
                                    <Triggers>  
                                    <asp:AsyncPostBackTrigger ControlID ="cbConfirmDiscount" />  
                                    </Triggers> 
                                </asp:UpdatePanel>
                                <p class="discounttext" style="margin-left: 15px;">Use Reward Points for Lower Price (Max 50% Discount)</p>                                
                            </div>
                            </div>
                    </div>                  
              </div>

              <div class="col-md-1  p-4 mb-5"></div>              

                  <div class="col-md-4" >
                      <div class="p-4 mb-5 shadow bg-white rounded" data-aos="fade-left"  data-aos-delay="300">
                    <div class="row">
                    <div class="col-md-12">
                        <h3><span style="">T</span>otal</h3>
                    </div>                           
                    <div class="row">
                    <div class="col-md-12">

                         <asp:UpdatePanel ID="UpdatePanel2" runat="server">  
                                <ContentTemplate>
                            <asp:TextBox type="text" ID="lblTotal" class="txtDisplay" style="border: none; font-size: 32px; font-weight: 800; background-color: white;" runat="server" Enabled="false" AutoPostBack="True" Text="-"></asp:TextBox>                                                                     
                            </ContentTemplate>
                                <Triggers>  
                                <asp:AsyncPostBackTrigger ControlID ="lblTotal" />  
                                </Triggers>  
                        </asp:UpdatePanel>	

                    </div> 
				                                      
                    </div>

                    </div>
                    <hr class="topline"/>     
                    <hr />
                    <div class="row g-3">     
                        <div class="text-end text-lg-flex justify-content-center">  
                            <asp:Button ID="btnContinuePay" class="w-100 btn btnPayNow btn-lg" runat="server" Text="Continue Payment" OnClick="btnContinuePay_Click" />                                          
                        </div>
                    <div class="row">
                        <asp:Label ID="lblPending" class="pending" runat="server" Visible="false">Pending Bank Transfer Request Found, please wait while we verify the payment.</asp:Label>	
                    </div>
                    </div> 
                  </div>
              </div>



        </div>
            </div>

    </div>        
     
<%--End Payment--%>                  
    </div>
    </section>  
    <%-- AOS script plugin--%>
<script src="https://unpkg.com/aos@next/dist/aos.js"></script>
<script>
    /* AOS plugin script*/
    AOS.init();    
</script>
</asp:Content>