
<%@ Page Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="ServicerWithdrawal.aspx.cs" Inherits="ServiceProvidingSystem.BackendUser.ServicerWithdrawal" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server"> 
       

    <div class="topicName">
        <br />
        <br />
        <b><u>Servicer Withdrawal Request</u></b>&nbsp&nbsp&nbsp<a href ="ServicerWithdrawal.aspx"  class="btnNew btn-primary btn-lg" > Refresh</a>
    </div>

    
    <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>


    <div class="formBorder">

        <asp:Panel ID="RepeaterPanel" runat="server">

        <asp:Repeater  ID="RepeaterRequest" runat="server" >   
                      
                          
        <ItemTemplate>
            <div class="rowStyle"> 
                <div class="rowMargin">
                    <table class="tblStyle">
                        <tr>
                            <td style="width:180px; text-align:right; padding-right:20px;">
                                Withdrawal No.:
                                
                            </td>
                 
                            <td style="width:150px;">
                                <asp:Label ID="lblWithdrawalNo" runat="server" Font-Bold="true" ForeColor="Black" Text='<%# Eval("withdrawal_no") %>'></asp:Label>
                            </td>
                            <td style="width:200px;">
                            </td>
                            <td style="width:300px;" colspan="2">
                                Bank Account:
                            </td>

                    
                        </tr>
                        <tr>
                            <td style="width:180px; text-align:right; padding-right:20px;">
                                Servicer ID:
                            </td>
                 
                            <td style="width:150px;">
                                <asp:Label ID="lblServicerID" runat="server" Font-Bold="true" ForeColor="Black" Text='<%# Eval("servicer_id") %>'></asp:Label>
                            </td>
                            <td style="width:200px;">
                                Withdrawal amount:
                            </td>
                            <td style="width:200px;">
                                <asp:Label ID="lblBankName" runat="server" Font-Bold="true" ForeColor="Black" Text='<%# Eval("bank_name") %>'></asp:Label>
                            </td>
                            <td style="width:100px;">
                                Status:
                            </td>

                    
                        </tr>
                        <tr>
                            <td style="width:180px; text-align:right; padding-right:20px;">
                                Date:
                            </td>    
                            <td style="width:150px;">
                                <asp:Label ID="lblDate" runat="server" Font-Bold="true" ForeColor="Black" Text='<%# Convert.ToDateTime(Eval("date_and_time")).ToString("dd/MM/yyyy") %>'></asp:Label>
                            </td>
                            <td style="width:200px;">
                                <b>RM</b><asp:Label ID="lblAmount" runat="server" Font-Bold="true" ForeColor="Black" Text='<%# Eval("withdrawal_amount") %>'></asp:Label>
                            </td>
                            <td style="width:200px;">
                                <asp:Label ID="lblHolderName" runat="server" Font-Bold="true" ForeColor="Black" Text='<%# Eval("holder_name") %>'></asp:Label>
                            </td>
                            <td style="width:100px;">
                                <b>Pending</b>
                            </td>

                    
                        </tr>
                        <tr>
                            <td style="width:180px; text-align:right; padding-right:20px;">
                                Time:
                            </td>
                 
                            <td style="width:150px;">
                                <asp:Label ID="lblTime" runat="server" Font-Bold="true" ForeColor="Black" Text='<%# Convert.ToDateTime(Eval("date_and_time")).ToString("HH:mm") %>'></asp:Label>
                            </td>
                            <td style="width:200px;">
                                
                            </td>
                            <td style="width:300px;" colspan="2">
                                <asp:Label ID="lblAccNo" runat="server" Font-Bold="true" ForeColor="Black" Text='<%# Eval("bank_account_no") %>'></asp:Label>
                            </td>

             
                        </tr>
                    </table>  
               </div>
                <div class="btnClass">
                    <asp:Button class="btnNew btn-primary btn-lg" ID="btnPaid" runat="server"  CausesValidation="False" CommandName="Cancel" Text="Paid" OnClick="btnPaid_Click" />
                    <br/>
                    <br/>
                    <asp:Button class="btnNew btn-secondary btn-lg" ID="btnReject" runat="server"  CausesValidation="False" CommandName="Cancel" Text="Reject" OnClick="btnReject_Click" onClientClick="return fnConfirmDelete();" />
                    <script type="text/javascript">
                        function fnConfirmDelete() {
                            return confirm("Are you sure you want to reject this request?");
                        }
                    </script>
                </div>


            </div>
            <br>



         </ItemTemplate>




   
        </asp:Repeater>  

        </asp:Panel>

                    <asp:Panel ID="NonePanel" runat="server" Visible="false">
                       <div class="noItemContainer">
                            <br />
                            <asp:Image ID="noItemLogo" runat="server" ImageUrl="~/Image/—Pngtree—info vector icon_3791375.png" class="noItemLogo" Width="200px" Height="200px" />            
                            <br /><br />
                            <h3><asp:Label ID="noItemText" runat="server" Text="There is currently no withdrawal request from servicer, please try to refresh later."></asp:Label></h3>
                            <br />
                        <asp:LinkButton  ID="noItemLinkText" class="noItemLinkText" runat="server" PostBackUrl="~/BackendUser/ServicerWithdrawal.aspx">Refresh</asp:LinkButton>
                       </div> 

                    </asp:Panel>

        </div>

        

                        </ContentTemplate>
        </asp:UpdatePanel>


    <br />
    <br />               
<style>
    body{
        font-family: Arial, Helvetica, sans-serif;   
        background-color: #f2f2f2;      
    }

        header{
        box-shadow: 0 .5rem 1rem rgba(0,0,0,.15);
        background: white;
    }

.rowMargin{
    padding-left:5px;
    padding-top:15px;
    float:left;
}


.rowStyle{
    height:140px;
    width:100%;
    background-color: white;
    box-shadow: 3px 3px 10px 0 #b3b3b3;
    border-radius: 5px;
}

.formBorder {
        max-width: 1000px;
        margin: auto;
        text-align: left;
        padding-bottom: 50px;
        margin-top: 50px;
        margin-bottom: 100px;
        margin-left:17%;
        margin-right:5%;
}




.topicName{
    max-width: 1000px;
    margin: auto;
    font-size: 25px;
    margin-top:110px;
}


    .noItemContainer {                                           
        text-align: center;
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



.btnClass {
    float:right;
    margin-top:20px;
    margin-right:20px;
}

/*---Button --*/
.btnNew{  
    text-align:center;
    width:100px;
    background: none;
    border: 2px solid;
    border-radius: 10px;
    color: #ffffff;
    font-weight: 600;
    cursor: pointer;
    position: relative;
    box-shadow: 0 5px 5px 0 rgba(0,0,0,0.1), 0 5px 10px 0 rgba(0,0,0,0.1);
    font-size:16px;
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



