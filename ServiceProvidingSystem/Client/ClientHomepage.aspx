<%@ Page Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="ClientHomepage.aspx.cs" Inherits="ServiceProvidingSystem.Client.ClientHomepage" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server"> 

    <div id="slides" class="carousel slide" data-ride="carousel">
        <ul class="carousel-indicators">
            <li data-target="#slides" data-slide-to="0" class="active"></li>
            <li data-target="#slides" data-slide-to="1" ></li>
            <li data-target="#slides" data-slide-to="2" ></li>
        </ul>

          <div class="carousel-inner">
            <div class="carousel-item active">
                <img src="Image/CustomerSlider2.jpg">

                <div class="carousel-caption">
                <h1 class="display-2">Jojo Artwork</h1>            
                <h3>Find The Art You Are Looking For!</h3>
                <a href="Customer/UserView.aspx"  class="btn btn-outline-light btn-lg">View Art</a>
                </div>
            </div>
            
            <div class="carousel-item ">
                <img src="Image/CustomerSlider2.jpg">
                <div class="carousel-caption">
                <h1 class="display-2">Jojo Artwork</h1>            
                <h3>Find The Art You Are Looking For!</h3>
                <a href="Customer/UserView.aspx"  class="btn btn-outline-light btn-lg">View Art</a>
               </div>
            </div>

            <div class="carousel-item ">
                <img src="Image/CustomerSlider2.jpg">
                <div class="carousel-caption">
                <h1 class="display-2">Jojo Artwork</h1>            
                <h3>Find The Art You Are Looking For!</h3>
                <a href="Customer/UserView.aspx"  class="btn btn-outline-light btn-lg">View Art</a>
               </div>
            </div>   
            </div>
        </div>

<!-- Jumbotron -->

     <div class="jumbotron">
         <div class="col-xs-12 col-sm-12 col-md-9 col-lg-9 col-xl-10">
             <p>Jojo Artwork selects talented artists from all over the world, for you.</p>
         </div>
          <div class="col-xs-12 col-sm-12 col-md-3 col-lg-3 col-xl-2">
              <a href="Customer/UserView.aspx"><button type="button" class="btn btn-outline-secondary btn-lg">Purchase Art</button></a>
          </div>      
     </div>
     

    <!-- Welcome Section -->
     <div class="container-fluid padding">  
     <div class="row welcome text-center">  
         <div class="col-12">
             <h1 class="display-4"> Original Art</h1>
         </div>
         <hr>
         <div class="col-12">
             <p class="lead"> Discover the world through original paintings for sale</p>
         </div>

     </div>
     </div>


   
     <hr class="my-4" />
     <!-- Three Column Section -->
    <!-- 
    <div class="container-fluid padding">
        <div class="row text-center padding">

        </div>
    </div> 
      -->



    <!-- Two Column Section -->
    <div class="container-fluid padding">
        <div class="row padding">
            <div class="col-md-12 col-lg-8">
                <h2>Lorem Ipsum</h2>
                <p>Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris,Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris,Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris</p>
                <p>Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris</p>
                <p>Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris</p>       
                <br />
                <a href="Customer/UserView.aspx" class="btn btn-primary">Learn More</a>
            </div>
            <div class="col-lg-4">
                <img src="LucaImage/Arts/13.jpg" class="img-fluid" id="homepageImage1"/>
            </div>
        </div>
    </div>

       <!-- Fixed BackGround -->
    <figure>
        <div class="fixed-wrap">
            <div id="fixed">
            </div>
        </div>
    </figure>
    <hr class="my-4" />


      <!-- Meet The Artists -->
   <div class="container-fluid padding">
       <div class="row welcome text-center">
           <div class="col-12">
               <h1> Meet The Artist</h1>
           </div>
           <hr>
       </div>
   </div>
    <hr class="my-4" />
    
       <!-- Cards -->
    <div class="container-fluid">
    <div class="container-fluid padding">
    <div class="row padding">

        <div class="col-md-4">
            <div class="card">
                <img class="card-img-top" src="LucaImage/Artist/6.jpg" />
                <div class="card-body">
                    <h4 class="card-title">John Doe</h4>
                    <p class="card-text"> Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco labori</p>
                    <a href ="Customer/UserView.aspx" class="btn btn-outline-secondary"> See Profile</a>
                </div>
            </div>
        </div>

         <div class="col-md-4">
            <div class="card">
                <img class="card-img-top" src="LucaImage/Artist/2.jpg" />
                <div class="card-body">
                    <h4 class="card-title">Alexis Anderson</h4>
                    <p class="card-text"> Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco labori</p>
                    <a href ="Customer/UserView.aspx" class="btn btn-outline-secondary"> See Profile</a>
                </div>
            </div>
        </div>

         <div class="col-md-4">
            <div class="card">
                <img class="card-img-top" src="LucaImage/Artist/3.jpg" />
                <div class="card-body">
                    <h4 class="card-title">Ellie Burnheart</h4>
                    <p class="card-text"> Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco labori</p>
                    <a href ="Customer/UserView.aspx" class="btn btn-outline-secondary"> See Profile</a>
                </div>
            </div>
        </div>
    </div>
     </div>
        <hr class ="my-4" />
        </div>

</asp:Content>
