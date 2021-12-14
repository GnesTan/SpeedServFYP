
<%@ Page Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="MySubscription.aspx.cs" Inherits="ServiceProvidingSystem.Servicer.MySubscription" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server"> 
       

    <div class="topicName">
        <br />
        <br />
        <b><u>My Subscription</u></b>
    </div>

    <div class="mainContainer">
        <%--If No Item Section--%>


            <div class="infoContainer">
                <div class="infoContainer2">
                    <div style="float:left;margin-top:20px;margin-right:40px;">
                        <asp:Image ID="imgIcon" runat="server" ImageUrl="/Image/IconSubscription.JPG" Width="80px" Height="80px" />
                    </div>
                    <div class="tbDetails">
                    <table>
                        <tr class="rowStyle">
                            <td>
                                <asp:Label ID="lblId" runat="server" Text="Your ID:"></asp:Label>
                            </td>
                            <td style="padding-right:40px;">
                                <b><asp:Label ID="lblRetreId" runat="server" Text="S000152142"></asp:Label></b>
                            </td>
                            <td>
                                <asp:Label ID="lblAmountPay" runat="server" Text="Amount to pay:"></asp:Label>
                            </td>
                        </tr>
                        <tr class="rowStyle">
                            <td colspan="2">
               
                            </td>
                            <td style="padding-right:40px;">
                                <b>RM<asp:Label ID="lblRetreAmount" runat="server" Text="0.00"></asp:Label></b>
                            </td>
                        </tr>
                        <tr class="rowStyle">
                            <td>
                                <asp:Label ID="lblStatus" runat="server" Text="Status:"></asp:Label>
                            </td>
                            <td style="padding-right:20px;">
                                <b><asp:Label ID="lblRetreStatus" runat="server" Text="Active"></asp:Label></b>
                            </td>
                            <td>
                            </td>
                        </tr>
                    </table>

                


               </div> 

                <div class="displayText">
                    <b>
                    <asp:Label ID="lblValidity" runat="server" Text="Validity until:"></asp:Label>
                    <asp:Label ID="lblRetreValidity" runat="server" Text="21 May 2021"></asp:Label>
                    </b>
                </div>


            </div> 



    </div>
        <div class="errorMsg">
        <asp:Label ID="lblError" runat="server" Text="" ForeColor="Red"></asp:Label>
        </div>
        <div class="btnClass">
            <div class="otherBtn">
                <asp:LinkButton ID="lbPay" class="btnNew btn-primary btn-lg" runat="server" OnClick="lbPay_Click">Pay Now</asp:LinkButton>
            </div> 
        </div> 
    <br />
    <br /> 
        
    </div>


    <div class="topicName">
        <b><u>Failed Subscription</u></b>
    </div>

    <div class="repeaterForm">

                    <asp:Repeater  ID="RejectedRepeater" runat="server" >   
                      
                          
                    <ItemTemplate>
                        <div class="rowStyle"> 
                                <u><asp:Label ID="lblRemark" runat="server" Font-Bold="true" ForeColor="Black" Text="Withdrawal"></asp:Label></u>
                                <table>
                                    <tr>
                                        <td style="width:180px;">
                                            Request Date:
                                
                                        </td>
                 
                                        <td style="width:170px;">
                                            Status:
                                        </td>
                                        <td style="width:200px;">

                                            <asp:Label ID="lblRM" runat="server" ForeColor="DarkGreen" Text="RM"></asp:Label> &nbsp

                                            <asp:Label ID="lblSymbol" runat="server" ForeColor="DarkGreen" Text='<%# Math.Abs(Convert.ToDouble(Eval("payment_amount"))).ToString("n2") %>'></asp:Label>
                                            
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
        height:300px;
        box-shadow: 3px 3px 10px 0 #b3b3b3;
        border-radius: 2px;  
        margin-left:5%;
        margin-right:5%;
    }

    .infoContainer2 {                                           
        margin-top:30px;
        margin-left:30px;
        float:left;
    }



    .errorMsg{
        margin-top:10px;
        margin-bottom:10px;
        margin-left:5%;
        height:20px;

    }



    .tbDetails {                                           
        font-size:large;
        margin-right:80px;
        float:left;
    }

    .rowStyle > td {
        padding-bottom: 1em;

    }
    


.mainContainer {
        background-color: white;
        max-width: 1100px;
        margin: auto;
        box-shadow: 3px 3px 10px 0 #b3b3b3;
        border-radius: 5px;
        text-align: left;
        padding-top: 50px;
        padding-bottom: 100px;
        margin-bottom: 100px;
        height:500px;
}


.topicName{
    max-width: 1100px;
    margin: auto;
    margin-top: 110px;
    font-size: 25px;
}

.displayText{
    float:right;
    margin-top:40px;
    margin-right:10px;
    color:red;
    font-size:x-large;

}

.btnClass{
    text-align:left;
    width:auto;
    margin-top:40px;
    margin-left:10%;
    margin-right:10%;

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


