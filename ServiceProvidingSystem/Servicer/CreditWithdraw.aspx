<%@ Page Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="CreditWithdraw.aspx.cs" Inherits="ServiceProvidingSystem.Servicer.CreditWithdraw" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server"> 




 
    <!-- Credit Withdraw Form -->
              
 
       <div class="withdrawalForm">



       <h2><u>Withdraw Credit</u></h2>

        <div class="withdrawalChild">
            <div class="inputLayout">
                <u><asp:Label ID="lblLeftedBalance" runat="server" Text="Balance"></asp:Label></u><br/>
                <b>RM<asp:Label ID="lblRetreAmt" runat="server" Text="0.00"></asp:Label></b><br/>
                <hr/>
                <div class="fieldStyle">
                    <asp:Label ID="lblAmt" runat="server" Text="Withdraw Amount(RM)"></asp:Label><br/>
                    <asp:TextBox ID="txtAmt" runat="server" MaxLength="20"></asp:TextBox>
                    <asp:RequiredFieldValidator style="color:red" ID="AmtRequired" runat="server" ControlToValidate="txtAmt" ErrorMessage="Withdraw amount is required." ToolTip="Withdraw amount is required.">*</asp:RequiredFieldValidator><br/>
                    <asp:RegularExpressionValidator style="color:red" runat="server" ID="AmtPattern" ControlToValidate = "txtAmt" ValidationExpression = "((\d+)((\.\d{1,2})?))$" ErrorMessage="Withdraw amount must be number or two place decimal."></asp:RegularExpressionValidator>
                </div>
                <div class="fieldStyle">
                <asp:Label ID="lblBankName" runat="server" Text="Bank Name"></asp:Label><br/>
                    <asp:DropDownList ID="ddlBank" runat="server" Width="300px" Height="33px">
                        <asp:ListItem Value="Affin Bank Berhad">Affin Bank Berhad</asp:ListItem>

                        <asp:ListItem Value="Alliance Bank Malaysia Berhad">Alliance Bank Malaysia Berhad</asp:ListItem>

                        <asp:ListItem Value="AmBank (M) Berhad">AmBank (M) Berhad</asp:ListItem>

                        <asp:ListItem Value="Bangkok Bank Berhad">Bangkok Bank Berhad</asp:ListItem>

                        <asp:ListItem Value="Bank of America Malaysia Berhad">Bank of America Malaysia Berhad</asp:ListItem>

                        <asp:ListItem Value="Bank of China (Malaysia) Berhad">Bank of China (Malaysia) Berhad</asp:ListItem>

                        <asp:ListItem Value="CIMB Bank Berhad">CIMB Bank Berhad</asp:ListItem>

                        <asp:ListItem Value="Citibank Berhad">Citibank Berhad</asp:ListItem>

                        <asp:ListItem Value="EON Bank Berhad">EON Bank Berhad</asp:ListItem>

                        <asp:ListItem Value="Hong Leong Bank Berhad">Hong Leong Bank Berhad</asp:ListItem>

                        <asp:ListItem Value="HSBC Bank Malaysia Berhad">HSBC Bank Malaysia Berhad</asp:ListItem>

                        <asp:ListItem Value="J.P. Morgan Chase Bank Berhad">J.P. Morgan Chase Bank Berhad</asp:ListItem>

                        <asp:ListItem Value="Malayan Banking Berhad">Malayan Banking Berhad</asp:ListItem>

                        <asp:ListItem Value="OCBC Bank (Malaysia) Berhad">OCBC Bank (Malaysia) Berhad</asp:ListItem>

                        <asp:ListItem Value="Public Bank Berhad">Public Bank Berhad</asp:ListItem>

                        <asp:ListItem Value="RHB Bank Berhad">RHB Bank Berhad</asp:ListItem>

                        <asp:ListItem Value="Standard Chartered Bank Malaysia Berhad">Standard Chartered Bank Malaysia Berhad</asp:ListItem>

                        <asp:ListItem Value="The Bank of Nova Scotia Berhad">The Bank of Nova Scotia Berhad</asp:ListItem>

                        <asp:ListItem Value="The Royal Bank of Scotland Berhad">The Royal Bank of Scotland Berhad</asp:ListItem>

                        <asp:ListItem Value="United Overseas Bank (Malaysia) Bhd.">United Overseas Bank (Malaysia) Bhd.</asp:ListItem>

                    </asp:DropDownList><br/>
                </div>

                <div class="fieldStyle">
                    <asp:Label ID="lblAccName" runat="server" Text="Account Holder Name"></asp:Label><br/>
                    <asp:TextBox ID="txtAccName" runat="server" MaxLength="50"></asp:TextBox>
                    <asp:RequiredFieldValidator style="color:red" ID="AccNameRequired" runat="server" ControlToValidate="txtAccName" ErrorMessage="Holder name is required." ToolTip="Holder name is required.">*</asp:RequiredFieldValidator><br/>
                </div>
                <div class="fieldStyle">
                    <asp:Label ID="lblAccNo" runat="server" Text="Bank Account No."></asp:Label><br/>
                    <asp:TextBox ID="txtAccNo" runat="server" MaxLength="30"></asp:TextBox>
                    <asp:RequiredFieldValidator style="color:red" ID="accNoRequired" runat="server" ControlToValidate="txtAccNo" ErrorMessage="Account No is required." ToolTip="Account No is required.">*</asp:RequiredFieldValidator><br/>
                    <asp:RegularExpressionValidator style="color:red" runat="server" ID="AccNoPattern" ControlToValidate = "txtAccNo" ValidationExpression = "^\d+$" ErrorMessage="Account number must be number."></asp:RegularExpressionValidator>
                </div>

                <div style="margin-top:10px; margin-right:30px; float:right">
                    <asp:Button ID="btnBack" runat="server" class="btnNew btn-secondary btn-lg" Text="Back" OnClick="btnBack_Click" CausesValidation="false"/>
                    &nbsp&nbsp&nbsp&nbsp
                    <asp:Button ID="btnConfirm" runat="server" class="btnNew btn-primary btn-lg" Text="Confirm" OnClick="btnConfirm_Click"/>
               </div>
               <br/>
               <br/>
               <br/>

               <div class="errorMsg">
                    <asp:Label ID="lblError" runat="server" Text="" ForeColor="Red"></asp:Label>
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

    body{    
        background-color: #f2f2f2;      
    }

    .withdrawalForm {
        background-color: white;
        text-align:left;
        max-width: 700px;
        margin: auto;
        box-shadow: 3px 3px 10px 0 #b3b3b3;
        border-radius: 5px;    
        width: 35%;
        height:630px;
        display: block;
        margin-top: 140px;
        margin-bottom: 5%;
        margin-left: auto;
        margin-right: auto;
        padding: 3%;
        position: relative;
    }

    .withdrawalChild{
      position: absolute;
      width:90%;
      margin-top:10px;
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
        width:90%;
        height:30px;

    }

    .fieldStyle{
        margin-bottom:10px;

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

