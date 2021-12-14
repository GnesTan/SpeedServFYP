<%@ Page Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="ViewRequest.aspx.cs" Inherits="ServiceProvidingSystem.Servicer.ViewRequest" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server"> 


        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
        <asp:Panel ID="WholePanel" runat="server">
        <div class="editForm">
        <br/>
        <br/>
        
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
                <td style="width:150px; text-align:right; padding-right:20px;">
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
                <td style="width:150px; text-align:right; padding-right:20px;">
                    Budget:
                </td>
                <td>
                    <asp:Label ID="lblBudget" runat="server" Font-Bold="true"></asp:Label>
                </td>
                <td colspan="2">
                </td>
            </tr>

            

            <tr>
                <td style="width:150px; text-align:right; padding-right:20px;">
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

            <asp:Panel ID="StatusPanel" runat="server" Visible="false">

            <div style="color:green; font-size:larger; text-align:center; border:dotted; border-color:red;">
                <asp:Label ID="lblServingStatus" runat="server" Font-Bold="true" Text="Transaction Done"></asp:Label>
            </div>

            </asp:Panel>

            <asp:Panel ID="ButtonPanel" runat="server">

            <div class="btnClass">

                <asp:Button ID="btnAccept" runat="server" class="btnNew btn-primary btn-lg" OnClick="btnAccept_Click" Text="Accept" CausesValidation="false" />
                &nbsp;&nbsp;&nbsp;
                <asp:Button ID="btnCancel" runat="server" class="btnNew btn-secondary btn-lg" PostBackUrl="~/Servicer/RequestList.aspx" Text="Back" CausesValidation="false" />

                <br /><br />

                <asp:Label ID="lblError" runat="server" ForeColor="Red" Text="Only 5 services allowed to be accept and serv in the same time, please complete before accept for new service." Visible="false"></asp:Label>

            </div>

            </asp:Panel>


            <asp:Panel ID="DoneButtonPanel" runat="server" Visible="false">

            <div class="btnClass">


                <asp:Button ID="btnDoneBack" runat="server" class="btnNew btn-secondary btn-lg" PostBackUrl="~/Servicer/ServiceHistory.aspx" Text="Back" CausesValidation="false" />



            </div>

            </asp:Panel>

            <br />
        </div>
        </asp:Panel>

                      <asp:Panel ID="InputTimePanel" runat="server" Visible="false">
                        <div class="noItemForm">
                       <div class="noItemContainer">
                           <u><h3>Estimate Completion Time</h3></u>
                            <br />
                            <br />
                           <asp:Label ID="Label1" runat="server" Text="Hour & Minutes:"></asp:Label>
                           <br />
                            <asp:TextBox ID="txtFromHour" runat="server" Width="50px" MaxLength="2" Placeholder="HH" Text="0" TextMode="Number" min="0" max="99"></asp:TextBox><asp:RequiredFieldValidator style="color:red" ID="HourRequired" runat="server" ControlToValidate="txtFromHour" ErrorMessage="Hour is required." ToolTip="Hour is required." ValidationGroup="AcceptService">*</asp:RequiredFieldValidator>&nbsp&nbsp&nbsp&nbsp<asp:TextBox ID="txtFromMin" runat="server" Width="50px" MaxLength="2" Placeholder="mm" Text="15" TextMode="Number" min="1" max="59"></asp:TextBox><asp:RequiredFieldValidator style="color:red" ID="MinutesRequired" runat="server" ControlToValidate="txtFromMin" ErrorMessage="Minutes is required." ToolTip="Minutes is required." ValidationGroup="AcceptService">*</asp:RequiredFieldValidator> <br/><br/>
                            <asp:RegularExpressionValidator style="color:red" runat="server" ID="FromHourPattern" ControlToValidate = "txtFromHour" ValidationExpression = "^\d+$" ErrorMessage="*Hour must be number." ValidationGroup="AcceptService"></asp:RegularExpressionValidator><br />
                            <asp:RegularExpressionValidator style="color:red" runat="server" ID="FromMinPattern" ControlToValidate = "txtFromMin" ValidationExpression = "^\d+$" ErrorMessage="*Minute must be number." ValidationGroup="AcceptService"></asp:RegularExpressionValidator><br />
                                       <asp:rangevalidator ID="Rangevalidator1" errormessage="*Please enter value between 1-59 for minutes." forecolor="Red" controltovalidate="txtFromMin" minimumvalue="1" maximumvalue="59" runat="server" Type="Integer" ValidationGroup="AcceptService"></asp:rangevalidator>
                           
                            <div class="btnClass">

                            <asp:Button ID="btnConfirm" runat="server" class="btnNew btn-primary btn-lg" OnClick="btnConfirm_Click" Text="Confirm" ValidationGroup="AcceptService" />
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
    margin-top: 140px;
    margin-bottom: 5%;
    margin-left: auto;
    margin-right: auto;
    min-height: 730px;  

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

.lineStyle{
    margin-left:5%;
    margin-left:5%;
}

.noItemForm{
    background-color: white;
    margin: auto;
    box-shadow: 3px 3px 10px 0 #b3b3b3;
    border-radius: 5px;    
    width: 40%;
    display: block;
    margin-top: 130px;
    margin-bottom: 5%;
    margin-left: auto;
    margin-right: auto;
    height: 400px; 
}


.noItemContainer {                                           
    text-align: center;
}




/*---Button --*/
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


input[type=text]:focus, input[type=password]:focus, input[type=date]:focus{
  outline: none;
  box-shadow: 0 0 0.5pt 0.5pt #900e8c;
  opacity:1;
}



</style>
</asp:Content>

