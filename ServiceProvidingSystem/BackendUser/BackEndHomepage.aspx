<%@ Page Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="BackEndHomepage.aspx.cs" Inherits="ServiceProvidingSystem.BackendUser.BackEndHomepage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server"> 


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
                <img class="card-img-top" height="500" src="~/LucaImage/Arts/6.jpg" runat="server" />
                <div class="card-body">               
                    <a href ="AddCloth.aspx" class="btn btn-outline-secondary" > Post Art</a>               
                </div>
            </div>
        </div>

         <div class="col-md-4">
            <div class="card">
                <img class="card-img-top" height="500"  src="~/LucaImage/Arts/9.jpg" runat="server"/>
                <div class="card-body">
                   

                    <a href ="ViewCloth.aspx" class="btn btn-outline-secondary"> View Art</a>                 
                </div>
            </div>
        </div>

         <div class="col-md-4">
            <div class="card">
                <img class="card-img-top" height="500" src="~/LucaImage/Artist/6.jpg" runat="server"/>
                <div class="card-body">
                                
                    <a href ="AccountMaintenance.aspx"  class="btn btn-outline-secondary" > Edit Profile</a>
                </div>
            </div>
        </div>
    </div>
     </div>
        <hr class ="my-4" />
        </div>      
  </div>  
</asp:Content>
  
