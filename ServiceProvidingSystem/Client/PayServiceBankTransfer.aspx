<%@ Page Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="PayServiceBankTransfer.aspx.cs" Inherits="ServiceProvidingSystem.Client.PayServiceBankTransfer" %>

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
    <link href="../PayServiceBankTransfer.css" rel="stylesheet" />

    <asp:ScriptManager ID="ScriptManager1" runat="server">  
    </asp:ScriptManager> 

    <section id="#" class="PayServBankTrans d-flex align-items-center">
    <div class="container">
<%--Payment--%>

        <div data-aos="zoom-out" data-aos-delay="100">  
            <div class="shadow p-5 mb-5 bg-white rounded" data-aos="zoom-out" data-aos-delay="50" >       
                 <h3 data-aos="fade-right"><span style="">B</span>ank Transfer Payment</h3>
                    <div class="row mb-1 mt-5" data-aos="fade-right" data-aos-delay="200">                                       
                        <div class=" col justify-content-center" data-aos="zoom-out" data-aos-delay="300">
                                                                       
                        <div class="col-md-12 my-1 d-flex justify-content-center">
                            <h2 for="name" class="form-label">Payment Amount</h2>
                        </div>
                        <div class="col-md-12 my-1 d-flex justify-content-center">
                            <asp:Label ID="lblPayTotal" runat="server" class="payDetails headTotal">RM 800.00</asp:Label>	 
                        </div>
                        </div>                                                                                   
                    </div>
                    <hr />
                    <div class="row" data-aos="fade-right" data-aos-delay="200">                                       
                        <div class=" col justify-content-center" data-aos="zoom-out" data-aos-delay="300">
                            <div class="row mb-3 mt-3">
                                <div class="col-md-12">
                                    <h4 for="name" class="form-label">Transfer according following details:</h4>                                    
                                </div>
                            </div>

                            <div class="row mb-5 mt-3">
                                <div class="col-md-4">
                                    <h5 for="comment" class="form-label">Bank Name</h5> 
                                    <asp:Label ID="lblDateTime" runat="server" class="payDetails bankDetails">Public Bank</asp:Label>	 
                                </div>
                                <div class="col-md-4">
                                    <h5 for="comment" class="form-label">Account No.</h5> 
                                    <asp:Label ID="Label1" runat="server" class="payDetails bankDetails">123123123456</asp:Label>	 
                                </div>
                                <div class="col-md-4">
                                    <h5 for="comment" class="form-label">Bank Holder Name</h5> 
                                    <asp:Label ID="Label2" runat="server" class="payDetails bankDetails">SpeedServ Sdn Bhd</asp:Label>	 
                                </div>
                            </div>

                            <hr/>

                            <div class="row mb-3 mt-4">
                                <div class="col-md-12">
                                    <h4 for="name" class="form-label">Upload Your Bank Receipt</h4>                                    
                                </div>
                            </div>

                            <div class="row mb-5">
                                    <div class="col-md-12">
                                        <h5 for="comment" class="form-label">Bank Receipt</h5> 
                                        <asp:FileUpload ID="ImageUpload" runat="server" accept=".png,.jpg,.jpeg,.gif"/> 
                                        <asp:Label ID="lblError" runat="server" Text="" ForeColor="Red"></asp:Label>
                                    </div> 
                            </div> 
                            <hr />                          
                            <div class="row g-3">     
                                <div class="text-end text-lg-flex justify-content-center">  
                                    <asp:LinkButton ID="btnBack" class="btn-find-now btnBack d-inline-flex align-items-center justify-content-center align-self-center" runat="server" OnClick="btnBack_Click">  
                                    <span>Back</span>                                
                                    </asp:LinkButton>     

                                    <asp:LinkButton ID="btnProceedPay" class="btn-find-now btnProceedPay d-inline-flex align-items-center justify-content-center align-self-center" runat="server" OnClick="btnProceedPay_Click">  
                                    <span>Proceed Payment</span>                                
                                    </asp:LinkButton>                        
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