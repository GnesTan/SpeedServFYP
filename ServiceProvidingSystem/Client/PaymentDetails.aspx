<%@ Page Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="PaymentDetails.aspx.cs" Inherits="ServiceProvidingSystem.Client.PaymentDetails" %>

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
    <link href="../PaymentDetails.css" rel="stylesheet" />

    <%--Cancel Service--%>   
 <section id="#" class="pytDetails d-flex align-items-center">
        <div class="container">
        <div data-aos="zoom-out" data-aos-delay="100">  
            <div class="shadow p-5 mb-5 bg-white rounded" data-aos="zoom-out" data-aos-delay="50" >  
                <div class="row mb-2">
                        <div class="col-md-12 my-1 d-flex justify-content-center">
                            <h2 for="name" class="form-label">Pricing Details</h2>
                        </div>
                </div>
                <div class="row mb-3">
                    <div class="col-md-12">
                       <asp:Panel ID="RepeaterPanel" runat="server">
                            <asp:Repeater ID="displayPayDet" runat="server" >  
                                <HeaderTemplate>
                                    <div class="row mb-2">
                                        <div class="col-md-8">
                                            <h5 for="comment" class="form-label">Description</h5>  
                                        </div>
                                        <div class="col-md-4">
                                            <h5 for="comment" class="form-label">Price</h5>  
                                        </div>
                                    </div>
         
                                </HeaderTemplate>
                            <ItemTemplate>
                                <div class="row mb-1">
                                    <div class="col-md-8">
                                            <asp:Label ID="lblFees" runat="server" Font-Size="Large" Text='<%# Eval("description") %>' ></asp:Label>
                                        </div>
                                    <div class="col-md-4">
                                        <asp:Label ID="Label1" runat="server" Font-Size="Large" Text='<%#"RM " + String.Format("{0:#.00}",Eval("price")) %>' ></asp:Label>
                                    </div>
                                </div>
      
                                </ItemTemplate>   
                            </asp:Repeater>  
                            </asp:Panel>
                    </div>                 
                </div>
                
                <div class="row">
                    <div class="col-md-8">
                        <h3 for="comment" class="form-label">Total</h3>  
                    </div>
                    <div class="col-md-4">
                        <h3 for="comment" class="form-label"><asp:Label ID="lblTotal" runat="server"></asp:Label></h3> 
                    </div>
                </div>

                            <div class="row mt-3">
                                <div class="col-md-12">
                                    <div class="text-end text-lg-flex justify-content-center">  
                                        <asp:LinkButton ID="LinkButton2" class="btn-find-now btnReview d-inline-flex align-items-center justify-content-center align-self-center" runat="server" OnClick="btnBack_Click">  
                                        <span>Back</span>                                                
                                        </asp:LinkButton> 
                                    </div>                      
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
