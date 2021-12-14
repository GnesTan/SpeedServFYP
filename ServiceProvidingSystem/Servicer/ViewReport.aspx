

<%@ Page Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="ViewReport.aspx.cs" Inherits="ServiceProvidingSystem.Servicer.ViewReport" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server"> 
       

    <div class="topicName">
        <br />
        <br />
        <b><u>View Report</u></b>
    </div>

    <div class="mainContainer">
        <%--If No Item Section--%>


            <div class="infoContainer">
               <div class="btn1">
                    <asp:ImageButton ID="ib1" runat="server" ImageUrl="/Image/ServiceDetails.JPG" Width="120px" Height="120px" BorderStyle="Solid" BorderColor="Black" BorderWidth="1px" CssClass="btn1Style" OnClick="ib1_Click" />
                    <br/>
                    <a href="ServiceDetailsReportSetting.aspx" runat="server" style="font-size:small;">Service Details Report</a>

               </div> 

               <div class="btn1">
                    <asp:ImageButton ID="ib2" runat="server" ImageUrl="/Image/AnnualService.JPG" Width="120px" Height="120px" BorderStyle="Solid" BorderColor="Black" BorderWidth="1px" CssClass="btn1Style" OnClick="ib2_Click" />
                    <br/>
                    <a href="AnnualServiceAnalysisReportSetting.aspx" runat="server" style="font-size:small;">Annual Service Analysis Report</a>

               </div> 

               <div class="btn1">
                    <asp:ImageButton ID="ib3" runat="server" ImageUrl="/Image/CustomerReview.JPG" Width="120px" Height="120px" BorderStyle="Solid" BorderColor="Black" BorderWidth="1px" CssClass="btn1Style" OnClick="ib3_Click" />
                    <br/>
                    <a href="CustomerReviewReport.aspx" runat="server" style="font-size:small;" target="_blank">Customer Review Report</a>

               </div> 

               <div class="btn1">
                    <asp:ImageButton ID="ib4" runat="server" ImageUrl="/Image/WalletTransaction.JPG" Width="120px" Height="120px" BorderStyle="Solid" BorderColor="Black" BorderWidth="1px" CssClass="btn1Style" OnClick="ib4_Click" />
                    <br/>
                    <a href="WalletTransactionReport.aspx" runat="server" style="font-size:small;" target="_blank">Wallet Transaction Report</a>

               </div> 


            </div> 




    </div>


    

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
        margin-top:2%;
        
        margin-right:5%;
    }

    .btn1 {                                           
        float:left;
        margin-left:5%;
        width:20%;
        text-align:center;
    }


    .btn1Style{
        box-shadow: 3px 3px 10px 0 #b3b3b3;
        border-radius: 10px;

    }



    .tbDetails {                                           
        font-size:large;
        margin-right:80px;
        float:left;
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
        height:450px;
}



.topicName{
    max-width: 1100px;
    margin: auto;
    margin-top: 110px;
    font-size: 25px;
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


