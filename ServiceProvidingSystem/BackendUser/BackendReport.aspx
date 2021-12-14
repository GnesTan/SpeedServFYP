
<%@ Page Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="BackendReport.aspx.cs" Inherits="ServiceProvidingSystem.BackendUser.BackendReport" %>
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
                    <asp:ImageButton ID="ib1" runat="server" ImageUrl="/Image/SubscriptionVolume.JPG" Width="120px" Height="120px" BorderStyle="Solid" BorderColor="Black" BorderWidth="1px" CssClass="btn1Style" OnClick="ib1_Click" />
                    <br/>
                    <a href="SubscriptionVolumeReportSetting.aspx" runat="server" style="font-size:small;">Subsciption Volume Report</a>

               </div> 

               <div class="btn1">
                    <asp:ImageButton ID="ib2" runat="server" ImageUrl="/Image/AnnualService.JPG" Width="120px" Height="120px" BorderStyle="Solid" BorderColor="Black" BorderWidth="1px" CssClass="btn1Style" OnClick="ib2_Click" />
                    <br/>
                    <a href="SalesTransactionReportSetting.aspx" runat="server" style="font-size:small;">Sales Transaction Report</a>

               </div> 

               <div class="btn1">
                    <asp:ImageButton ID="ib3" runat="server" ImageUrl="/Image/UserTypeRatio.JPG" Width="120px" Height="120px" BorderStyle="Solid" BorderColor="Black" BorderWidth="1px" CssClass="btn1Style" OnClick="ib3_Click" />
                    <br/>
                    <a href="UserTypeRatioAnalysisReport.aspx" runat="server" style="font-size:small;" target="_blank">User Type Ratio Analysis Report</a>

               </div> 



            </div> 




    </div>


    

<style>
    body{
        font-family: Arial, Helvetica, sans-serif;   
        background-color: #f2f2f2;      
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

    .btn1 a{
   
    }

    .btn1Style{
        box-shadow: 3px 3px 10px 0 #b3b3b3;
        border-radius: 10px;

    }


        header{
        box-shadow: 0 .5rem 1rem rgba(0,0,0,.15);
        background: white;
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
    font-size: 25px;
    margin-top:110px;
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








</style>
</asp:Content>



