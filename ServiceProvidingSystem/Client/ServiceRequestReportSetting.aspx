<%@ Page Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="ServiceRequestReportSetting.aspx.cs" Inherits="ServiceProvidingSystem.Client.ServiceRequestReportSetting" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.4000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
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
                            <div class="row mb-4">
                                <h3 data-aos="fade-right"><span style="">S</span>ervice Request Report Date Range</h3>
                            </div>
                            <div class="row mb-4">
                                <div class="col-md-12">
                                    <h5 for="name" class="form-label">From</h5>
                                    <asp:TextBox type="date" ID="txtFrom" class="form-control" runat="server"></asp:TextBox>                                     
                                </div>
                            </div>
                            <div class="row mb-2">
                                <div class="col-md-12">
                                    <h5 for="name" class="form-label">To</h5>
                                    <asp:TextBox type="date" ID="txtTo" class="form-control" runat="server"></asp:TextBox>                                     
                                </div>
                            </div>                            
                            <div class="row">
                                <div class="col-md-12">  
                                    <asp:CompareValidator ID="CompareDate" ValidationGroup = "Date" ForeColor = "Red" runat="server" ControlToValidate = "txtFrom" ControlToCompare = "txtTo" Operator = "LessThanEqual" Type = "Date" ErrorMessage="*To date must be more than or equal From date."></asp:CompareValidator>                                    
                                </div>
                            </div>
                            <div class="row mb-2">
                                <div class="col-md-12">                                      
                                    <asp:Label ID="lblError" runat="server" Text="" ForeColor="Red"></asp:Label>
                                </div>
                            </div>
                            <div class="row g-3 mb-3">     
                                <div class="col-md-12 ">
                                    <div class="text-end text-lg-flex justify-content-center">  
                                        <asp:LinkButton ID="btnBack" class="btn-find-now btnBack d-inline-flex align-items-center justify-content-center align-self-center" runat="server" PostBackUrl="~/Client/ClientViewReport.aspx" >  
                                        <span>Back</span>                                
                                        </asp:LinkButton>     

                                        <asp:LinkButton ID="btnView" class="btn-find-now btnNext d-inline-flex align-items-center justify-content-center align-self-center" runat="server" OnClick="btnView_Click" ValidationGroup = "Date">  
                                        <span>Next</span>                                
                                        </asp:LinkButton>                                        
                                    </div>
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
  

