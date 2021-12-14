
<%@ Page Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="ServiceDetailsReportSetting.aspx.cs" Inherits="ServiceProvidingSystem.Servicer.ServiceDetailsReportSetting" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.4000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server"> 
       

    <div class="topicName">
        <br />
        <br />
        <b><u>Service Details Report</u></b>

    </div>

    <div class="mainContainer">


            <div class="infoContainer">
                    <b><asp:Label ID="lblDate" runat="server" Text="Date Range:"></asp:Label></b> <br/><br/>
                    <table ID="tblDate">
                        <tr>
                            <td class="col1">
                                From
                            </td>
                            <td>
                                <asp:TextBox ID="txtFrom" runat="server" type="date"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="col1">
                                To
                            </td>
                            <td>
                                <asp:TextBox ID="txtTo" runat="server" type="date"></asp:TextBox>
                            </td>
                        </tr>

                    </table>



            </div>
        
        <div class="errorMsg">
            <asp:CompareValidator ID="CompareDate" ValidationGroup = "Date" ForeColor = "Red" runat="server" 
ControlToValidate = "txtFrom" ControlToCompare = "txtTo" Operator = "LessThanEqual" Type = "Date"
ErrorMessage="*To date must be more than or equal From date."></asp:CompareValidator><br/>
            <asp:Label ID="lblError" runat="server" Text="" ForeColor="Red"></asp:Label>
        </div>

        <div class="btnClass">
            <div class="otherBtn">
                <asp:Button ID="btnView" class="btnNew btn-primary btn-lg" runat="server" Text="View" OnClick="btnView_Click" ValidationGroup = "Date" />
                &nbsp&nbsp&nbsp&nbsp&nbsp
                <asp:Button ID="btnBack" class="btnNew btn-secondary btn-lg" runat="server" Text="Back" PostBackUrl="~/Servicer/ViewReport.aspx"  />

                
            </div> 
            <cr:crystalreportviewer runat="server" ID="crystalreportviewer1" autodatabind="true"></cr:crystalreportviewer>
        </div> 

  




    </div>




    

<style>
    .col1{
        width:100px;
    }

    #tblDate td{
        padding-bottom:20px;
    }

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
    }


    .errorMsg{
        margin-top:100px;
        margin-bottom:10px;
        margin-left:5%;
        height:20px;

    }



    .rowStyle > td {
        padding-bottom: 1em;

    }

    .rowStyle{
        margin-left:10%;
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
        margin-bottom: 100px;
        height:350px;
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
        margin-bottom: 100px;
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
    margin-top:50px;
    margin-left:10%;
    margin-right:10%;

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

.btn{  
  max-width: 100%;
  max-height: 100%;
  background: none;
  border: 3px solid;
  border-radius: 20px;
  color: #c48e0e;
  font-weight: 600;
  cursor: pointer;
  font-size: 16px;
  position: relative;
  box-shadow: 0 5px 5px 0 rgba(0,0,0,0.1), 0 5px 10px 0 rgba(0,0,0,0.1);
}

.btn:hover {   
  box-shadow: 0 8px 16px 0 rgba(0,0,0,0.2), 0 6px 20px 0 rgba(0,0,0,0.19);
  color: #f5d142;
  transition-delay: 0.05s;  
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

.btn-secondary {
    background-color: #7B51F2;
    border: 1px solid #7B51F2;
}

.btn-secondary:hover {
        background-color: #8F6BF6;
        border: 1px solid #8F6BF6;
}

.otherBtn{
    float:right;
}






</style>
</asp:Content>




