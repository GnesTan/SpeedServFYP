<%@ Page Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="PaymentMethodSelectionClient.aspx.cs" Inherits="ServiceProvidingSystem.Client.PaymentMethodSelectionClient" %>
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

    
<%--Login--%>   
    <section id="#" class="login d-flex align-items-center">
        <div class="container">
            <div data-aos="zoom-out" data-aos-delay="100">  
                <div class="shadow p-5 mb-5 bg-white rounded" data-aos="zoom-out" data-aos-delay="50" >   
                    <div class="row mb-3">
                        <div class="col-md-12">
                            <div class="row mb-2">
                                <h3 data-aos="fade-right"><span style="">C</span>hoose Your Payment Method</h3>
                            </div>
                            <div class="row mb-3">
                                <div class="col-md-12 px-2">
                                    <div class="text-end text-lg-flex justify-content-center">  
                                        <asp:LinkButton ID="lbPay" runat="server" OnClick="lbPay_Click" ></asp:LinkButton>
                                        <asp:HiddenField ID="hfPaymentAmount" runat="server" value="0.00"/>
                                    </div>
                                </div>
                            </div>
                            <div class="row mb-3">
                                <div class="col-md-12 px-2">
                                    <div class="text-end text-lg-flex justify-content-center">  
                                        <asp:Button ID="lbBankTransfer" class="w-100 btnClient btn-lg" runat="server" Text="Bank Transfer" OnClick="lblBankTransfer_Click" />
                                        <div class="divider d-flex align-items-center my-4">
                                            <p class="text-center fw-bold mx-3 mb-0 text-muted">OR</p>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row mb-3">
                                <div class="col-md-12 px-2">
                                    <div class="text-end text-lg-flex justify-content-center">  
                                        <div id="smart-button-container">
                                            <div style="text-align: center;">
                                                <div id="paypal-button-container"></div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row mb-3">
                                <div class="col-md-12 px-2">
                                    <div class="text-end text-lg-flex justify-content-center">  
                                        <div class="divider d-flex align-items-center my-4">                                            
                                        </div>
                                        <asp:Button  ID="ibBack" class="w-100 btn btnBack btn-lg" runat="server" Text="BACK" OnClick="ibBack_Click"/>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
<%--End Login--%>
           </div>
    </section>
	<%-- Paypal script plugin--%>
    <script src="https://www.paypal.com/sdk/js?client-id=AfzKtaZ8cqtoZePkKNzPPwY8xYZUT8SK9Nzzxf5eaRDvpxYzUCG47IoYkSjS-sbLFkIT5MB-5HQHD8JM&enable-funding=venmo&currency=USD" data-sdk-integration-source="button-factory"></script>
              <script>
                  function initPayPalButton() {
                      paypal.Buttons({
                          style: {
                              shape: 'rect',
                              color: 'blue',
                              layout: 'vertical',
                              label: 'paypal',

                          },

                          createOrder: function (data, actions) {
                              var value = document.getElementById('<% =hfPaymentAmount.ClientID %>').value;
                              return actions.order.create({
                                  purchase_units: [{ "description": "SpeedServ Service Charges", "amount": { "currency_code": "USD", "value": value } }]
                              });
                          },

                          onApprove: function (data, actions) {
                              return actions.order.capture().then(function (orderData) {

                                  // Full available details
                                  console.log('Capture result', orderData, JSON.stringify(orderData, null, 2));

                                  var button = document.getElementById('<% =lbPay.ClientID %>');

                                  button.click();



                              });
                          },

                          onError: function (err) {
                              console.log(err);
                          }
                      }).render('#paypal-button-container');
                  }
                  initPayPalButton();
              </script>   
    <%-- AOS script plugin--%>
<script src="https://unpkg.com/aos@next/dist/aos.js"></script>
<script>
    /* AOS plugin script*/
    AOS.init();
</script>
</asp:Content>
