<%@ Page Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="BackEndHomepage.aspx.cs" Inherits="ServiceProvidingSystem.BackendUser.BackEndHomepage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server"> 

    <br/>
    <br/>
    <br/>
    <br/>
    <br/>


    <!-- Three Column Section -->


         <div class="row welcome text-center">  
         <div class="col-12">
             <h1 class="display-4">Menu</h1>
         </div>
     
         <div class="col-12">
             
         </div></div>  

        <hr>
    <!-- Cards -->
    <div class="container-fluid">  

    <div class="container-fluid">
    <div class="container-fluid padding">
    <div class="row padding">

        <div class="col-md-4">
            <div class="card">
                <img class="card-img-top" height="500" src="~/Image/view_request.jpg" runat="server" />
                <div class="card-body">               
                    <a href ="ServicerWithdrawal.aspx" class="btn btn-outline-secondary" > Withdrawal</a>   
                    <a href ="SubscriptionRequest.aspx" class="btn btn-outline-secondary" > Subscription</a> 
                    <a href ="ClientPayment.aspx" class="btn btn-outline-secondary" > Client Payment</a>
                </div>
            </div>
        </div>

         <div class="col-md-4">
            <div class="card">
                <img class="card-img-top" height="500"  src="~/Image/UserMaintenance.png" runat="server"/>
                <div class="card-body">
                    <a href ="BackendReport.aspx" class="btn btn-outline-secondary"> Reports</a>                 
                </div>
            </div>
        </div>

         <div class="col-md-4">
            <div class="card">
                <img class="card-img-top" height="500" src="~/Image/view_report.jpg" runat="server"/>
                <div class="card-body">                 
                    <a href ="AccountMaintenance.aspx"  class="btn btn-outline-secondary" > User Maintenance</a>
                </div>
            </div>
        </div>
    </div>
     </div>
        <hr class ="my-4" />
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
</style>

</asp:Content>
  
