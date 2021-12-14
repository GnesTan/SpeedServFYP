
<%@ Page Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="PayByBankTransfer.aspx.cs" Inherits="ServiceProvidingSystem.Servicer.PayByBankTransfer" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server"> 




 
    <!-- Login Section -->



             
              <!-- Login Form -->
 
       <div class="loginForm">


       <h2><u>Pay by Bank Transfer</u></h2>

        <div class="loginChild">
            <div class="inputLayout">
                <b><u>Subscription Amount</u></b><br />
                    RM<asp:Label ID="lblAmount" runat="server" Text="80.00"></asp:Label><br/>
                <hr/>
                <b><u>Transfer according following details:</u></b><br/>
                Bank name: Public Bank<br/>
                Account No: 123123123456<br/>
                Bank Holder Name: SpeedServ Sdn Bhd<br/>
                <br/>
                <b><u>Bank Transfer Receipt</u></b><br/>
                <asp:FileUpload ID="ImageUpload" runat="server" accept=".png,.jpg,.jpeg,.gif"/>
                <br/>
                <div class="errorMsg">
                    <asp:Label ID="lblError" runat="server" Text="" ForeColor="Red"></asp:Label>
                </div>

                <div style="margin-top:25px; margin-right:30px; float:right">
                    <asp:Button ID="btnBack" runat="server" class="btnNew btn-secondary btn-lg" Text="Back" OnClick="btnBack_Click"/>
                    &nbsp&nbsp&nbsp&nbsp
                    <asp:Button ID="btnConfirm" runat="server" class="btnNew btn-primary btn-lg" Text="Confirm" OnClick="btnConfirm_Click"/>
               </div>
               <br/>
                

            </div>



        </div>




     </div>

    <style type="text/css">

    body{
        font-family: Arial, Helvetica, sans-serif;   
        background-color: #f2f2f2;      
    }

        header{
        box-shadow: 0 .5rem 1rem rgba(0,0,0,.15);
        background: white;
    }

    h1{
        font-size:larger;
    }


    .loginForm {
        background-color: white;
        text-align:left;
        max-width: 680px;
        margin: auto;
        box-shadow: 3px 3px 10px 0 #b3b3b3;
        border-radius: 5px;    
        width: 35%;
        height:550px;
        display: block;
        margin-top: 140px;
        margin-bottom: 5%;
        margin-left: auto;
        margin-right: auto;
        padding: 3%;
        position: relative;
    }

    .loginChild{
      position: absolute;
      width:90%;
      margin-top:10%;
      margin-right:50px;

    }

    .inputLayout{
      margin-left:auto;
      margin-right:auto;

    }

    .errorMsg{
        margin-top:10px;
        margin-bottom:15px;
        margin-right:20px;
        height:30px;

    }

    .btnNew{  
          text-align:center;
          width:100px;
          background: none;
          border: 3px solid;
          border-radius: 10px;
          color: #ffffff;
          font-weight: 600;
          cursor: pointer;
          position: relative;
          box-shadow: 0 5px 5px 0 rgba(0,0,0,0.1), 0 5px 10px 0 rgba(0,0,0,0.1);
          font-size:medium;
    }


    .btn-primary {
        background-color: #EF7E7E;
        border: 1px solid #EF7E7E;
    }

    .btn-primary:hover {
            background-color: #F68888;
            border: 1px solid #F68888;
    }

    .btn-secondary {
        background-color: #7B51F2;
        border: 1px solid #7B51F2;
    }

    .btn-secondary:hover {
            background-color: #8F6BF6;
            border: 1px solid #8F6BF6;
    }

    </style>

</asp:Content>
