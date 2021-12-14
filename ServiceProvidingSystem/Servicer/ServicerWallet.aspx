
<%@ Page Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="ServicerWallet.aspx.cs" Inherits="ServiceProvidingSystem.Servicer.ServicerWallet" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server"> 
       

    <div class="topicName">
        <br />
        <br />
        <b><u>Credit Wallet</u></b>

    </div>

    <div class="mainContainer">


            <div class="infoContainer">
                    <asp:Label ID="lblBalance" runat="server" Text="Balance"></asp:Label> <br/>
                    
                    <b>RM<asp:Label ID="lblRetreBalance" runat="server" Text="0.00"></asp:Label></b> <br/>
                    <hr/>




            </div>
        
        <div class="errorMsg">
            <asp:Label ID="lblError" runat="server" Text="" ForeColor="Red"></asp:Label>
        </div>

        <div class="btnClass">
            <div class="otherBtn">
                <asp:LinkButton ID="lbWithdraw" class="btnNew btn-primary btn-lg" runat="server" OnClick="lbWithdraw_Click">Withdraw</asp:LinkButton>
            </div> 
        </div> 




    </div>




    <div class="topicName">
        <b><u>Transaction</u></b>
    </div>

    <div class="repeaterForm">

                    <asp:Repeater  ID="RepeaterPayment" runat="server" >   
                      
                          
                    <ItemTemplate>
                        <div class="rowStyle"> 
                                <u><asp:Label ID="lblRemark" runat="server" Font-Bold="true" ForeColor="Black" Text='<%# Eval("remark") %>'></asp:Label></u>
                                <table>
                                    <tr>
                                        <td style="width:150px;">
                                            Payment Date:
                                
                                        </td>
                 
                                        <td style="width:200px;">
                                            Status:
                                        </td>
                                        <td style="width:200px;">
                                            <asp:Label ID="lblPaymentAmt" runat="server" ForeColor="DarkGreen" Text='<%# Convert.ToDouble(Eval("servicer_amount")) >= 0.00 ? "+" :"-" %>'></asp:Label>

                                            &nbsp <asp:Label ID="lblRM" runat="server" ForeColor="DarkGreen" Text="RM"></asp:Label> &nbsp

                                            <asp:Label ID="lblSymbol" runat="server" ForeColor="DarkGreen" Text='<%# Math.Abs(Convert.ToDouble(Eval("servicer_amount"))).ToString("n2") %>'></asp:Label>
                                            
                                        </td>
                    
                                    </tr>
                                    <tr>
                                        <td style="width:150px;">
                                            <asp:Label ID="lblDate" runat="server" ForeColor="Black" Text='<%# Convert.ToDateTime(Eval("date_and_time")).ToString("dd/MM/yyyy") %>'></asp:Label>
                                        </td>
                 
                                        <td colspan="2">
                                            Completed
                                        </td>
                    
                                    </tr>
                                </table>  

                        </div>
                        <br/>

                        <div class="lineStyle">
                        <hr/>
                        </div>



                     </ItemTemplate>




   
                    </asp:Repeater>  

            </div> 

            <div style="overflow: hidden; text-align:center;">

            <asp:Repeater ID="rptPaging" runat="server" OnItemCommand="rptPaging_ItemCommand">
                <ItemTemplate>
                <asp:LinkButton ID="btnPage"
                style="padding: 8px; margin: 2px; background: lightgray; border: solid 1px #666; color: black; font-weight: bold"
                CommandName="Page" CommandArgument="<%# Container.DataItem %>"
                runat="server" ForeColor="White" Font-Bold="True">
                <%# Container.DataItem %>
                </asp:LinkButton>
                </ItemTemplate>
            </asp:Repeater>

            </div>


    <div class="topicName">
        <b><u>Failed Withdrawal</u></b>
    </div>

    <div class="repeaterForm">

                    <asp:Repeater  ID="RejectedRepeater" runat="server" >   
                      
                          
                    <ItemTemplate>
                        <div class="rowStyle"> 
                                <u><asp:Label ID="lblRemark" runat="server" Font-Bold="true" ForeColor="Black" Text="Withdrawal"></asp:Label></u>
                                <table>
                                    <tr>
                                        <td style="width:180px;">
                                            Withdrawal Date:
                                
                                        </td>
                 
                                        <td style="width:170px;">
                                            Status:
                                        </td>
                                        <td style="width:200px;">

                                            <asp:Label ID="lblRM" runat="server" ForeColor="DarkGreen" Text="RM"></asp:Label> &nbsp

                                            <asp:Label ID="lblSymbol" runat="server" ForeColor="DarkGreen" Text='<%# Math.Abs(Convert.ToDouble(Eval("withdrawal_amount"))).ToString("n2") %>'></asp:Label>
                                            
                                        </td>
                    
                                    </tr>
                                    <tr>
                                        <td style="width:180px;">
                                            <asp:Label ID="lblDate" runat="server" ForeColor="Black" Text='<%# Convert.ToDateTime(Eval("date_and_time")).ToString("dd/MM/yyyy") %>'></asp:Label>
                                        </td>
                 
                                        <td colspan="2">
                                            <asp:Label ID="lblFailed" runat="server" ForeColor="Red" Text="Failed"></asp:Label>
                                        </td>
                    
                                    </tr>
                                </table>  

                        </div>
                        <br/>

                        <div class="lineStyle">
                        <hr/>
                        </div>



                     </ItemTemplate>




   
                    </asp:Repeater>  


            </div> 

            <div style="overflow: hidden; text-align:center;">

            <asp:Repeater ID="rptPaging2" runat="server" OnItemCommand="rptPaging2_ItemCommand">
                <ItemTemplate>
                <asp:LinkButton ID="btnPage"
                style="padding: 8px; margin: 2px; background: lightgray; border: solid 1px #666; color: black; font-weight: bold"
                CommandName="Page" CommandArgument="<%# Container.DataItem %>"
                runat="server" ForeColor="White" Font-Bold="True">
                <%# Container.DataItem %>
                </asp:LinkButton>
                </ItemTemplate>
            </asp:Repeater>

            </div>

            <br/>
            <br/>
            <br/>



    

<style>
    body{
        font-family: Arial, Helvetica, sans-serif;   
        background-color: #f2f2f2;      
    }

        header{
        box-shadow: 0 .5rem 1rem rgba(0,0,0,.15);
        background: white;
    }

    .infoContainer {        
        text-align: left;
        height:80px;
        margin-left:5%;
        margin-right:5%;
        font-size:x-large;
    }


    .errorMsg{
        margin-top:10px;
        margin-bottom:10px;
        margin-left:5%;
        height:20px;

    }



    .rowStyle > td {
        padding-bottom: 1em;

    }

    .rowStyle{
        margin-left:20px;
    }

    .lineStyle{
        width:90%;
        margin:auto;
    }
    


.mainContainer {
        background-color: white;
        max-width: 500px;
        margin: auto;
        box-shadow: 3px 3px 10px 0 #b3b3b3;
        border-radius: 5px;
        text-align: left;
        padding-top: 20px;
        padding-bottom: 20px;
        margin-bottom: 20px;
        height:220px;
}


.repeaterForm {
        background-color: white;
        max-width: 500px;
        margin: auto;
        box-shadow: 3px 3px 10px 0 #b3b3b3;
        border-radius: 5px;
        text-align: left;
        padding-top: 20px;
        padding-bottom: 20px;
        margin-bottom: 20px;
}



.topicName{

    margin: auto;
    margin-top: 110px;
    font-size: 25px;
        max-width: 500px;
}


.btnClass{
    text-align:left;
    width:auto;
    margin-top:40px;
    margin-left:10%;
    margin-right:10%;

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

.otherBtn{
    float:right;
}






</style>
</asp:Content>



