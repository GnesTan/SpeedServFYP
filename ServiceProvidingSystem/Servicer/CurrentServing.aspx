
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
        <hr>

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
                <td colspan="2">
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

            <div style="color:green; font-size:larger; text-align:center; border:dotted; border-color:red;">
                <asp:Label ID="lblServingStatus" runat="server" Font-Bold="true" Text="Current Serving"></asp:Label>
            </div>

            <div class="btnClass">

                 <asp:Panel ID="ReceivedPanel" runat="server" Visible="false">
                       <div class="receivedButton" >
                            <asp:Button ID="btnReceived" runat="server" class="btnNew btn-primary btn-lg" OnClick="btnReceived_Click" Text="Received" />
                       </div> 

                  </asp:Panel>

                <asp:Button ID="btnMessage" runat="server" BackColor="Gray" class="btnNew btn-primary btn-lg" OnClick="btnMessage_Click" Text="Message" />
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Button ID="btnCancel" runat="server" class="btnNew btn-secondary btn-lg" OnClick="btnCancel_Click" Text="Cancel Serving" CausesValidation="false" />

            </div>

            <div class="btnClass">

                <asp:Button ID="btnComplete" runat="server" Width="220px" class="btnNew btn-secondary btn-lg" OnClick="btnComplete_Click" Text="Done Service" CausesValidation="false" />

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
                           <u><h3>Charging Amount</h3></u>
                            <br />
                            <asp:Label ID="Label1" runat="server" Text="Please enter the amount charging for this services (RM):"></asp:Label>
                            <br />
                            <asp:TextBox ID="txtAmount" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator style="color:red" ID="AmountRequired" runat="server" ControlToValidate="txtAmount" ErrorMessage="Amount is required." ToolTip="Amount is required." ValidationGroup="CompleteService">*</asp:RequiredFieldValidator>
                            <br />
                            <asp:RegularExpressionValidator style="color:red" runat="server" ID="AmountPattern" ControlToValidate = "txtAmount" ValidationExpression = "^[1-9]\d*(\.\d+)?$" ErrorMessage="Must be decimal." ValidationGroup="CompleteService"></asp:RegularExpressionValidator>
                           
                                    <div class="btnClass">

                                        <asp:Button ID="btnConfirm" runat="server" class="btnNew btn-primary btn-lg" OnClick="btnConfirm_Click" Text="Confirm" ValidationGroup="CompleteService" />
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        <asp:Button ID="btnCancelInput" runat="server" class="btnNew btn-secondary btn-lg" OnClick="btnCancelInput_Click" Text="Back" CausesValidation="false" />

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
    text-align:center;
}

.tableClass{
    margin-left:5%;
    margin-right:auto;
}

.editForm {
    background-color: white;
    margin: auto;
    box-shadow: 3px 3px 10px 0 #b3b3b3;
    border-radius: 5px;    
    width: 60%;
    display: block;
    margin-top: 30px;
    margin-bottom: 5%;
    margin-left: auto;
    margin-right: auto;
    height: 800px;  

}

.noItemForm{
    background-color: white;
    margin: auto;
    box-shadow: 3px 3px 10px 0 #b3b3b3;
    border-radius: 5px;    
    width: 40%;
    display: block;
    margin-top: 30px;
    margin-bottom: 5%;
    margin-left: auto;
    margin-right: auto;
    height: 400px; 
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

.lineStyle{
    margin-left:5%;
    margin-left:5%;
}

.topContainer{
    font-family: Arial, Helvetica, sans-serif;   
    text-align: center;
    font-size: 25px;
    background-color: white;
    width: 100%;
    height: 220px;
}

.nav {
  list-style-type: none;
  display: inline-block;
  text-align: center;
  margin: 0;      
}

.ul {
    display: inline-block;
    font-size: 20px;
    padding: 20px;
    color: #c48e0e;
}

.noItemContainer{
    margin-top:30%;
    margin-bottom:auto;
    margin-left:5%;
    margin-right:auto;
    text-align:center;
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

.textbox{
    border-radius: 5px;    
}

input[type=text]:focus, input[type=password]:focus, input[type=date]:focus{
  outline: none;
  box-shadow: 0 0 0.5pt 0.5pt #900e8c;
  opacity:1;
}



</style>




</asp:Content>


