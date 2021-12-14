<%@ Page Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="PaymentSuccess.aspx.cs" Inherits="ServiceProvidingSystem.Client.PaymentSuccess" %>

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
    <link href="../PaymentSuccess.css" rel="stylesheet" />

    <%--Cancel Service--%>   
 <section id="#" class="paySuccess d-flex align-items-center">
        <div class="container">
            <div data-aos="zoom-out" data-aos-delay="100">  
                <div class="shadow p-5 mb-5 bg-white rounded" data-aos="zoom-out" data-aos-delay="50" >       
                    <div class="row mb-1">                        
                        <div class="col-md-12 my-1 d-flex justify-content-center">
                            <i class="bi bi-check-circle completeIcon"></i>
                        </div>
                        <div class="col-md-12 my-1 d-flex justify-content-center">
                            <h2 for="name" class="form-label">Payment Success!</h2>
                        </div>
                        <div class="col-md-12 my-1 d-flex justify-content-center">
                            <asp:Label ID="lblHeadTotal" runat="server" class="payDetails headTotal"></asp:Label>	 
                        </div>
                    </div>
                    <hr/>
                    <div class="row" data-aos="fade-right" data-aos-delay="200">                                       
                        <div class=" col justify-content-center" data-aos="zoom-out" data-aos-delay="300">
                            <div class="row mb-3 mt-3">
                                <div class="col-md-12">
                                    <h4 for="name" class="form-label">Payment Details</h4>                                    
                                </div>
                            </div>

                            <div class="row mb-3 mt-3">
                                <div class="col-md-12">
                                    <h5 for="comment" class="form-label">Date & Time</h5> 
                                    <asp:Label ID="lblDateTime" runat="server" class="payDetails datetime"></asp:Label>	 
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-md-6 d-flex justify-content-start">
                                    <h5 for="comment" class="form-label">Subtotal</h5>                                    
                                                                 
                                </div>      
                                <div class="col-md-6 d-flex justify-content-end">
                                     <asp:Label ID="lblPrice" runat="server" class="payDetails"></asp:Label>	                                      	                                   
                                </div>    
                            </div>

                            <div class="row">
                                <div class="col-md-6 d-flex justify-content-start">
                                    <h5 for="comment" class="form-label">Membership Discount</h5>                                    
                                                                 
                                </div>      
                                <div class="col-md-6 d-flex justify-content-end">
                                     <asp:Label ID="lblDiscountAmount" runat="server" class="payDetails"></asp:Label>	                                      	                                   
                                </div>    
                            </div>
                            
                            <div class="row mt-3 mb-3">
                                <div class="col-md-6 d-flex justify-content-start">
                                    <p for="comment" class="form-label totalprice">Total</p>                                    
                                                                 
                                </div>      
                                <div class="col-md-6 d-flex justify-content-end">
                                     <asp:Label ID="lblTotal" runat="server" class="payDetails total"></asp:Label>	                                      	                                   
                                </div>    
                            </div>
                            <hr/>


                            <div class="row mb-3 mt-4">
                                <div class="col-md-12">
                                    <h4 for="name" class="form-label">Membership Details</h4>                                    
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-md-6 d-flex justify-content-start">
                                    <h5 for="comment" class="form-label">Reward Points Earned</h5>                                    
                                                                 
                                </div>      
                                <div class="col-md-6 d-flex justify-content-end">
                                    <h5 for="comment" class="form-label currentPointsLabel">Current Reward Points</h5>
                                    	                                   
                                </div>    
                            </div>
                            
                            <div class="row mb-4">
                                <div class="col-md-6 d-flex justify-content-start">
                                                                    
                                    <asp:Label ID="lblEarnedPoints" runat="server" class="payDetails earnedPoints"></asp:Label>	                                    
                                </div>      
                                <div class="col-md-6 d-flex justify-content-end">
                                   
                                    <asp:Label ID="lblCurrentTotalPoints" runat="server" class="payDetails currentPoints"></asp:Label>	                                   
                                </div>    
                            </div>


                        </div>                                 
                    </div>                    
                    <hr />
                    <div class="row g-3">     
                        <div class="text-end text-lg-flex justify-content-center">  
                                <asp:LinkButton ID="btnReview" class="btn-find-now btnReview d-inline-flex align-items-center justify-content-center align-self-center" runat="server" OnClick="btnProceedReview_Click">  
                                <span>Proceed to Review</span>                                
                                </asp:LinkButton>                          
                        </div>
                        </div>        
                   </div>                
             </div>

    <%--End Cancel Service--%>
           </div>
    </section>
     
    
    <%-- AOS script plugin--%>
<script src="https://unpkg.com/aos@next/dist/aos.js"></script>
<script>
    /* AOS plugin script*/
    AOS.init();
</script>
</asp:Content>
 
