
<%@ Page Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="PaymentMethodSelection.aspx.cs" Inherits="ServiceProvidingSystem.Servicer.PaymentMethodSelection" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server"> 




 
    <!-- Login Section -->


             
              <!-- Login Form -->
 
        <div class="loginForm">
        <div style="text-align:left; float:left;">
            <asp:ImageButton ID="ibBack" runat="server" Height="33px" ImageUrl="~/Image/BackIcon.jpg" Width="33px" OnClick="ibBack_Click"></asp:ImageButton>
        </div>            
        <div>
             <h1>Select Payment Method</h1>
        </div>
            <asp:HiddenField ID="hfPaymentAmount" runat="server" value="0"/>

        <div class="loginChild">
            <div class="inputLayout">

                <asp:LinkButton ID="lbPay" runat="server" OnClick="lbPay_Click" ></asp:LinkButton>

        <div class="btnClass">
                <asp:LinkButton ID="lbBankTransfer" runat="server" class="btnNew btn-primary btn-lg" OnClick="lbBankTransfer_Click" Width="200px">Bank Transfer</asp:LinkButton>
                <br/>
                <br/>
                OR
                <br/>
                <br/>


            <div id="smart-button-container">
                  <div style="text-align: center;">
                    <div id="paypal-button-container"></div>
                  </div>
                </div>
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
                                  purchase_units: [{ "description": "Subscription", "amount": { "currency_code": "USD", "value": value } }]
                              });
                          },

                          onApprove: function (data, actions) {
                              return actions.order.capture().then(function (orderData) {

                                  // Full available details
                                  console.log('Capture result', orderData, JSON.stringify(orderData, null, 2));

                                  var button = document.getElementById('<% =lbPay.ClientID %>');

                                  //click button if success
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
        </div> 


            </div>



        </div>

        </div>






    <style type="text/css">

    h1{
        font-size:larger;
    }

        header{
        box-shadow: 0 .5rem 1rem rgba(0,0,0,.15);
        background: white;
    }

    body{    
        background-color: #f2f2f2;      
    }

    .loginForm {
        background-color: white;
        text-align:center;
        max-width: 680px;
        margin: auto;
        box-shadow: 3px 3px 10px 0 #b3b3b3;
        border-radius: 5px;    
        width: 35%;
        height:auto;
        min-height:500px;
        display: block;
        margin-top: 140px;
        margin-bottom: 5%;
        margin-left: auto;
        margin-right: auto;
        padding: 3%;
        position: relative;
    }

    .loginChild{
      margin: -25px 0 0 -25px; /* apply negative top and left margins to truly center the element */
      margin-top:10%;
      height:auto;

    }

    .inputLayout{
      margin-left:auto;
      margin-right:auto;
      height:auto;

    }

    .btnClass
    {
        margin-left:17%;
        margin-right:17%;
        width:300px;
        height:auto;

    }


    /*---Button --*/
    .btnNew{  
      max-width: 100%;
      max-height: 100%;
      background: none;
      border: 3px solid;
      border-radius: 10px;
      color: #ffffff;
      font-weight: 600;
      text-transform: uppercase;
      cursor: pointer;
      font-size: 16px;
      position: relative;
      box-shadow: 0 5px 5px 0 rgba(0,0,0,0.1), 0 5px 10px 0 rgba(0,0,0,0.1);
    }


    .btn-primary {
        background-color: #898989;
        border: 1px solid #563d7c;
    }

    .btn-primary:hover {
            background-color: #A19F9F;
            border: 1px solid #A19F9F;
    }

    </style>

</asp:Content>
