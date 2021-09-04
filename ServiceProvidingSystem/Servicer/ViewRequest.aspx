<%@ Page Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="ViewRequest.aspx.cs" Inherits="ServiceProvidingSystem.Servicer.ViewRequest" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server"> 


        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>

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
                <asp:Button ID="btnCancel" runat="server" class="btnNew btn-secondary btn-lg" OnClick="btnCancel_Click" Text="Back" CausesValidation="false" />

                <br /><br />

                <asp:Label ID="lblError" runat="server" ForeColor="Red" Text="Only 1 service allowed to be accept and serv in the same time, please complete before accept for new service." Visible="false"></asp:Label>

            </div>

            </asp:Panel>

            <br />
        </div>



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
    height: 730px;  

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

.mapForm{

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

