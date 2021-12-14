﻿<%@ Page Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="ClientViewReport.aspx.cs" Inherits="ServiceProvidingSystem.Client.ClientViewReport" %>

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

    
<%--Client View Report--%>   
    <section id="#" class="login d-flex align-items-center">
        <div class="container">
            <div data-aos="zoom-out" data-aos-delay="100">  
                <div class="shadow p-5 mb-5 bg-white rounded" data-aos="zoom-out" data-aos-delay="50" >   
                    <div class="row">
                        <div class="col-md-12">
                            <div class="row mb-4">
                                <h3 data-aos="fade-right"><span style="">V</span>iew Report</h3>
                            </div>
                            <div class="row mb-4">
                                <div class="col-md-12 px-3 d-flex justify-content-start">
                                    <asp:ImageButton ID="ib1" runat="server" ImageUrl="/Image/ServiceDetails.JPG" Width="80px" Height="80px" BorderStyle="Solid" BorderColor="Black" BorderWidth="1px" CssClass="btn1Style" OnClick="ib1_Click" />
                                    <div class="py-4">
                                        <h5 class="rmbMeText" style="margin-left: 15px;"><a href="ServiceRequestReportSetting.aspx" runat="server">Service Request Report</a></h5>
                                    </div>
                                </div>
                            </div> 
                            <div class="row mb-4">
                                <div class="col-md-12 px-3 d-flex justify-content-start">
                                    <asp:ImageButton ID="ib2" runat="server" ImageUrl="/Image/AnnualService.JPG" Width="80px" Height="80px" BorderStyle="Solid" BorderColor="Black" BorderWidth="1px" CssClass="btn1Style" OnClick="ib2_Click" />
                                    <div class="py-4">
                                        <h5 class="rmbMeText" style="margin-left: 15px;"><a href="TransactionReportSetting.aspx" runat="server">Transaction Report</a></h5>
                                    </div>                                   
                                </div>
                            </div> 
                            <div class="row g-3 mb-3">     
                                <div class="col-md-12">
                                    <div class="text-end text-lg-flex justify-content-center">   
                                        <asp:LinkButton ID="btnBack" class="btn-find-now btnBack d-inline-flex align-items-center justify-content-center align-self-center" runat="server" PostBackUrl="~/Client/ClientHomePage.aspx">  
                                        <span>Back</span>                                
                                        </asp:LinkButton>                                        
                                    </div>
                                </div>                              
                            </div>
                        </div>
                    </div>
                </div>
            </div>
<%--End Client View Report--%>
           </div>
    </section>
    <style>
            .btn1Style{
        box-shadow: 3px 3px 10px 0 #b3b3b3;
        border-radius: 10px;

    }
    </style>
    <%-- AOS script plugin--%>
<script src="https://unpkg.com/aos@next/dist/aos.js"></script>
<script>
    /* AOS plugin script*/
    AOS.init();
</script>
</asp:Content>
