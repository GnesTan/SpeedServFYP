
<%@ Page Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="CurrentServing.aspx.cs" Inherits="ServiceProvidingSystem.Servicer.CurrentServing" %>



<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server"> 


    <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>

        <asp:Panel ID="WholeFormPanel" runat="server">
        <div class="editForm">
        <br/>
        <br/>

                    <asp:Panel ID="ExistPanel" runat="server" Visible="false">
        <div class="topForm">
            <div class="profilePicForm">
                <asp:image Height="90px" style="border-radius:100px; box-shadow: 0 0 0 3px #b3b3b3; text-decoration:none;" Width="90px" ImageAlign="Middle" ToolTip = "Profile Picture" ID="ProfilePic" runat="server" ImageUrl ="~/imgs/generaluser.png" ></asp:image>
            </div>
            <div>
                <b><asp:Label ID="lblClientName" runat="server"></asp:Label></b>
                <br /><br />
                <asp:Label ID="lblCategory1" runat="server"></asp:Label>:&nbsp<asp:Label ID="lblTitle" runat="server"></asp:Label>
            </div>

        </div>
        <div class="lineStyle">
            <hr>
        </div>
        <div class="midForm">
            Remark:
            <br />
            <asp:Label ID="lblRemark" runat="server"></asp:Label>

        </div>
        <div class="lineStyle">
            <hr>
        </div>

        <div class="tableClass">
        <table class="tbDetails">
            <tr>
                <td style="width:180px; text-align:right; padding-right:20px;">
                    Category:
                </td>
                <td style="width:200px;">
                    <asp:Label ID="lblCategory3" runat="server" Font-Bold="true"></asp:Label>
                </td>
                    
                <td style="width:80px;">
                    Type:
                    
                </td>
                <td style="width:200px;">
                    <asp:Label ID="lblType" runat="server" Font-Bold="true"></asp:Label>
                </td>
            </tr>

            <tr>
                <td style="width:180px; text-align:right; padding-right:20px;">
                    Budget:
                </td>
                <td>
                    <asp:Label ID="lblBudget" runat="server" Font-Bold="true"></asp:Label>
                </td>
                <td>
                    Estimate Complete Time:
                </td>
                <td>
                    <asp:Label ID="lblEstimate" runat="server" Font-Bold="true"></asp:Label>
                </td>
            </tr>

            

            <tr>
                <td style="width:180px; text-align:right; padding-right:20px;">
                    Detailed Address:
                </td>
                <td colspan="3">
                    <asp:Label ID="lblAddress" runat="server" Font-Bold="true"></asp:Label>
                </td>
            </tr>

            <tr>
                <td style="width:180px; text-align:right; padding-right:20px;">
                    <asp:Label ID="lblPrice" runat="server" Text="Amount Charged(RM):" Visible="false"></asp:Label>
                </td>
                <td style="width:200px;">
                    <asp:Label ID="lblPriceAmount" runat="server" Font-Bold="true" Visible="false"></asp:Label>
                </td>
                <td colspan="2">

                </td>
            </tr>

            </table>
            </div>


            <asp:Panel ID="ItemPricePanel" runat="server" Visible="false">
                <div class="ItemPriceRepeater">
                    <asp:Repeater ID="rptTable" runat="server">
                        <HeaderTemplate>
                            <table class="table" border="1" >
                            <tr>
                                <th class="column1" >Item Description</th>
                                <th class="column2">Price(RM)</th>

                            </tr>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr>
                                <td class="column1"><%# Eval("description") %></td>
                                <td class="column2" style="text-align:right;" ><%# Eval("price") %></td>
                            </tr>
                        </ItemTemplate>
                        <FooterTemplate>
                            </table>
                        </FooterTemplate>
                    </asp:Repeater>
                </div> 

            </asp:Panel>

            <div style="color:green; font-size:larger; text-align:center; border:dotted; border-color:red;">
                <asp:Label ID="lblServingStatus" runat="server" Font-Bold="true" Text="Current Serving"></asp:Label>
            </div>


            <div class="btnClass">


                <asp:Button ID="btnComplete" runat="server" Width="220px" class="btnNew btn-primary btn-lg" OnClick="btnComplete_Click" Text="Done Service" CausesValidation="false" />
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Button ID="btnCancel" runat="server" class="btnNew btn-secondary btn-lg" OnClick="btnCancelInterface_Click" Text="Cancel Serving" CausesValidation="false" />

            </div>

            <div class="btnClass">

                <asp:Image ID="Image1" runat="server" ImageUrl="/Image/whatsapp-icon.png" Width="30px" Height="30px" />&nbsp;<asp:Button ID="btnMessage" runat="server" Width="120px" class="btnNew btn-chat btn-lg" Text="Chat" CausesValidation="false" OnClick="btnMessage_Click" />

            </div>

                    </asp:Panel>

                    <asp:Panel ID="NonePanel" runat="server" Visible="false">
                       <div class="noItemContainer">
                            <br />
                            <asp:Image ID="noItemLogo" runat="server" ImageUrl="~/Image/—Pngtree—info vector icon_3791375.png" class="noItemLogo" Width="200px" Height="200px" />            
                            <br /><br />
                            <h3><asp:Label ID="noItemText" runat="server" Text="There is currently no service is in progressing."></asp:Label></h3>
                            <br />
                        <asp:LinkButton  ID="noItemLinkText" class="noItemLinkText" runat="server" PostBackUrl="~/Servicer/RequestList.aspx">Look for Service Request</asp:LinkButton>
                       </div> 

                    </asp:Panel>

  
            <br />
        </div>
        </asp:Panel>
                    <asp:Panel ID="InputAmountPanel" runat="server" Visible="false">
                        <div class="noItemForm">
                       <div class="noItemContainer">
                           <u><h3>Charging Items & Amount</h3></u>

                    <div class="repeaterClass">
                        <asp:Repeater ID="Repeater1" runat="server">
                        <HeaderTemplate>
                            <div class="row">
                                <div class="col-md-2" style="width:400px; margin-right:20px;">
                                    <asp:Label ID="Label1" runat="server" Text="Description"></asp:Label>
                                </div>
                                <div class="col-md-2" style="width:150px;">
                                    <asp:Label ID="Label2" runat="server" Text="Price(RM)"></asp:Label>
                                </div>
                            </div>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <div class="row">
                                <div class="col-md-2" style="width:400px; margin-right:20px;">
                                    <asp:TextBox ID="txtDesc" runat="server" CssClass="form-control" Width="380px" MaxLength="50" Placeholder="XXXX Fees" ></asp:TextBox>
                                    <asp:RequiredFieldValidator style="color:red" ID="DescRequired" runat="server" ControlToValidate="txtDesc" ErrorMessage="Description is required." ToolTip="Description is required." ValidationGroup="CompleteService" Display="Dynamic" >*</asp:RequiredFieldValidator>
                                </div>
                                <div class="col-md-2" style="width:150px;">
                                    <asp:TextBox ID="txtPrice" runat="server" CssClass="form-control" MaxLength="12" Placeholder="XXX.XX" Width="120px" AutoPostBack="true" OnTextChanged="txtPrice_TextChanged"></asp:TextBox>
                                    <asp:RequiredFieldValidator style="color:red" ID="PriceRequired" runat="server" ControlToValidate="txtPrice" ErrorMessage="Price is required." ToolTip="Price is required." ValidationGroup="CompleteService" Display="Dynamic">*</asp:RequiredFieldValidator>
                                    <div style="text-align:left;">
                                    <asp:RegularExpressionValidator style="color:red" runat="server" ID="PricePattern" ControlToValidate = "txtPrice" ValidationExpression = "^[1-9]\d*(\.\d+)?$" ErrorMessage="Invalid Price." ValidationGroup="CompleteService" Display="Dynamic"></asp:RegularExpressionValidator>
                                    </div>
                                </div>
                                <div class="col-sm-1">
                                    <asp:LinkButton ID="btnadd" runat="server" Text="+" CssClass="btn btn-success" OnClick="btnadd_Click"></asp:LinkButton>
                                </div>
                                <div class="col-sm-1" style="float: left">
                                    <asp:LinkButton ID="btndel" runat="server" Text="-" CssClass="btn btn-danger left"
                                        OnClick="btndel_Click"></asp:LinkButton>
                                </div>
                            </div>
                            
                            <br />
                            
                        </ItemTemplate>
                        <FooterTemplate>
                            <div class="row">
                                <div class="col-md-2" style="width:400px; margin-right:20px; margin-top:5px; text-align:right;">
                                    <asp:Label ID="lblTotal" runat="server" Text="Total:"></asp:Label>
                                </div>
                                <div class="col-md-2" style="width:150px;">
                                    <asp:TextBox ID="txtTotal" runat="server" CssClass="form-control" MaxLength="12" Width="120px" Enabled="false" Font-Bold="true" Text="0.00" ></asp:TextBox>
                                </div>
                            </div>

                        </FooterTemplate>

                    </asp:Repeater>

                           
                </div>
               


                                    <div class="btnClass">

                                        <asp:Button ID="btnConfirm" runat="server" class="btnNew btn-primary btn-lg" OnClick="btnConfirm_Click" Text="Confirm" ValidationGroup="CompleteService" />
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        <asp:Button ID="btnCancelInput" runat="server" class="btnNew btn-secondary btn-lg" OnClick="btnCancelInput_Click" Text="Back" CausesValidation="false" />

                                    </div>

                                    <br /><br />

                       </div> 
                        </div>

                    </asp:Panel>

                    <asp:Panel ID="CancelPanel" runat="server" Visible="false">
                        <div class="noItemForm">
                       <div class="noItemContainer">
                           <u><h3>Service Cancellation</h3></u>
                            <br />
                            <asp:Label ID="Label2" runat="server" Text="Please provide reason for cancelling this service:"></asp:Label>
                            <br />
                            <asp:TextBox ID="txtReason" runat="server"  Width="400px" TextMode="MultiLine" Rows="4"></asp:TextBox>
                            <asp:RequiredFieldValidator style="color:red" ID="ReasonRequired" runat="server" ControlToValidate="txtReason" ErrorMessage="Amount is required." ToolTip="Amount is required." ValidationGroup="CancelReason">*</asp:RequiredFieldValidator>
                            <br />
                            <asp:RegularExpressionValidator style="color:red" runat="server" ID="MinChar" ControlToValidate = "txtReason" ValidationExpression = ".{10,}$" ErrorMessage="Must input at least 10 characters." ValidationGroup="CancelReason"></asp:RegularExpressionValidator>
                           
                                    <div class="btnClass">

                                        <asp:Button ID="btnCancelService" runat="server" class="btnNew btn-primary btn-lg" OnClick="btnCancelService_Click" Text="Confirm" ValidationGroup="CancelReason" />
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        <asp:Button ID="btnCancelInput2" runat="server" class="btnNew btn-secondary btn-lg" OnClick="btnCancelInput_Click" Text="Back" CausesValidation="false" />

                                    </div>

                       </div> 
                        </div>

                    </asp:Panel>


                </ContentTemplate>
        </asp:UpdatePanel>


 

<style type="text/css">

body{    
    font-family: Arial, Helvetica, sans-serif;   
    background-color: #f2f2f2;      
}

    header{
        box-shadow: 0 .5rem 1rem rgba(0,0,0,.15);
        background: white;
    }

.topicName{
    margin: auto;
    font-size: 25px;
}

.tbDetails{
    border-collapse:separate;
    border-spacing:0 20px;
}


.tbDetails td{
    height:20px;
    width:300px;
}



.btnClass {
    margin-top:40px;
    margin-bottom:20px;
    text-align:center;
}

.tableClass{
    margin-left:50px;
    margin-right:auto;
}

.editForm {
    background-color: white;
    margin: auto;
    box-shadow: 3px 3px 10px 0 #b3b3b3;
    border-radius: 5px;    
    width: 900px;
    display: block;
    margin-top: 140px;
    margin-bottom: 5%;
    margin-left: auto;
    margin-right: auto;
    min-height: 850px;  

}

.noItemForm{
    background-color: white;
    margin: auto;
    box-shadow: 3px 3px 10px 0 #b3b3b3;
    border-radius: 5px;    
    width: 800px;
    display: block;
    margin-top: 30px;
    margin-bottom: 100px;
    margin-left: auto;
    margin-right: auto;
    min-height: 500px; 
    height:auto;
}

.profilePicForm{
    float:left;
    margin-right:20px;
}

.topForm{
   margin-left:5%;
   margin-right:auto;
   height:100px;
}

.midForm{
    margin-left:5%;
    margin-right:auto;
    margin-bottom:20px;
    height:150px;
}


.noItemLogo {
    max-width: 70px;
    max-height: 70px;
}
.noItemLinkText{
    background: none;
    border: 3px solid;
    border-radius: 5px;
    color: black;
    font-weight: 500;  
    text-transform: uppercase;
    cursor: pointer;
    font-size: 16px; 
    padding: 10px 10px 10px 10px;
    box-shadow: 0 5px 5px 0 rgba(0,0,0,0.1), 0 5px 10px 0 rgba(0,0,0,0.1);
    text-decoration: none;
}
.noItemLinkText:hover{
    color: slategray;
    text-decoration: none;
}

.lineStyle{
    margin-left:30px;
    margin-right:30px;
}



.noItemContainer{
    margin-top:120px;
    margin-bottom:auto;
    margin-left:5px;
    margin-right:50px;
    text-align:center;
    min-width:800px;
    height:auto;
}

.receivedButton{
    margin-bottom:20px;
}



/*---Button --*/
.btnNew{  
    text-align:center;
    width:180px;
    background: none;
    border: 3px solid;
    border-radius: 10px;
    color: #ffffff;
    font-weight: 600;
    cursor: pointer;
    position: relative;
    box-shadow: 0 5px 5px 0 rgba(0,0,0,0.1), 0 5px 10px 0 rgba(0,0,0,0.1);
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
    background-color: #E74C3C;
    border: 1px solid #E74C3C;
}

.btn-secondary:hover {
        background-color: #F56A51;
        border: 1px solid #F56A51;
}

.btn-chat {
    background-color: #00E676;
    border: 1px solid #00E676;
}

.btn-chat:hover {
        background-color: #09EA7C;
        border: 1px solid #09EA7C;
}


input[type=text]:focus, input[type=password]:focus, input[type=date]:focus{
  outline: none;
  box-shadow: 0 0 0.5pt 0.5pt #900e8c;
  opacity:1;
}

.repeaterClass{
    margin-top:50px;
    margin-bottom:100px;
    margin-left:50px;
    margin-right:auto;
}

.column1{
    width:350px;
}

.column2{
    width:100px;
    margin-right:40px;
}

.ItemPriceRepeater{
    width:500px;
    margin-left:auto;
    margin-right:auto;
    margin-top:30px;
    margin-bottom:50px;
}

.ItemPriceRepeater table{
    border: 1px solid #000000;


}



</style>




</asp:Content>


