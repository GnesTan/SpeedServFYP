<%@ Page Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="ClientHomePage.aspx.cs" Inherits="ServiceProvidingSystem.Client.ClientHomePage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server"> 
       <%--Plugins--%>
    <link rel="stylesheet" href="https://unpkg.com/aos@next/dist/aos.css" />
    <link rel="preconnect" href="https://fonts.googleapis.com">
    <link rel="preconnect" href="https://fonts.gstatic.com" crossorigin>
    <link href="https://fonts.googleapis.com/css2?family=Lato&family=PT+Sans&family=Roboto:wght@300&display=swap" rel="stylesheet"> 
    <script src="https://code.jquery.com/jquery-3.3.1.slim.min.js" integrity="sha384-q8i/X+965DzO0rT7abK41JStQIAqVgRVzpbzo5smXKp4YfRvH+8abtTE1Pi6jizo" crossorigin="anonymous"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.14.3/umd/popper.min.js" integrity="sha384-ZMP7rVo3mIykV+2+9J3UJ46jBk0WLaUAdn689aCwoqbBJiSnjAK/l8WvCWPIPm49" crossorigin="anonymous"></script>
    <script src="https://stackpath.bootstrapcdn.com/bootstrap/4.1.3/js/bootstrap.min.js" integrity="sha384-ChfqqxuZUCnJSK3+MXmPNIyE6ZbWh2IMqE241rYiqJxyMiZ6OW/JmZQ5stwEULTy" crossorigin="anonymous"></script>    
    <%--End Plugins--%>

    <%--CSS File Plugins--%>
    <link href="../General.css" rel="stylesheet" />   
    <link href="../ClientHomePage.css" rel="stylesheet" />  
    <%--End CSS File Plugins--%>
   <asp:ScriptManager ID="ScriptManager1" runat="server">  
   </asp:ScriptManager> 
    
    <%--Top About--%>
    <section id="about" class="topAbout d-flex align-items-center">
    <div class="container">
        <div class="row">
            <div class="col-lg-6 topAbout-img" data-aos="zoom-out" data-aos-delay="250">
                <img src="../Image/homepagelogo.png" class="img-fluid"/>      
            </div>
            <div class="col-lg-6 d-flex flex-column justify-content-center">
                <h1 data-aos="fade-right"><span style="">S</span>peedServ</h1>
                <h2 data-aos="fade-up" data-aos-delay="450">From now on, the Service Industries is going to be changed</h2>
                <div data-aos="fade-up" data-aos-delay="650">                    
                    <div class=" text-lg-flex justify-content-center">
                        <a href="#services" class="btn-get-started scrollto d-inline-flex align-items-center justify-content-center align-self-center">
                            <span>Explore Our Services</span>
                            <i class="bi bi-arrow-right"></i>
                        </a>                                                                       
                        <a runat="server" onserverclick="liRequest_Click" class="btn-get-started scrollto d-inline-flex align-items-center justify-content-center align-self-center">
                            <span>Request Services</span>
                            <i class="bi bi-arrow-right"></i>
                        </a>
                       </div>                  
                    </div>
                </div>
            </div>
        </div>
    </section>
    <!-- End TopAbout -->

    <%--Find Service--%>
    <section id="services" class="services col-lg-12 d-flex flex-column justify-content-center ">
        <div class="container shadow p-5 mb-5 bg-white rounded" data-aos="zoom-out" data-aos-delay="50" >       
            <div class="row" data-aos="fade-right" data-aos-delay="200">      
                <h1 class="mb-3" ><span>Find</span> Your Service Now</h1>   
                <hr class="topline"/>
                <div class="servicesddl col-sm" data-aos="zoom-out" data-aos-delay="300">
                    <h5 for="service-cat" class="form-label">Service Category</h5>
                    <asp:UpdatePanel ID="servcatpanel" runat="server">  
                        <ContentTemplate>
                            <asp:DropDownList class="form-select" ID="ddlCategory" runat="server" AppendDataBoundItems="true" OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged" AutoPostBack="True">
                                <asp:ListItem Value="Installation">Installation</asp:ListItem>
                                <asp:ListItem Value="Repairing">Repairing</asp:ListItem>
                                <asp:ListItem Value="Others">Others</asp:ListItem>                        
                            </asp:DropDownList>
                        </ContentTemplate>
                      <Triggers>  
                      <asp:AsyncPostBackTrigger ControlID ="ddlCategory" />  
                      </Triggers>  
                    </asp:UpdatePanel>

                </div>                  
                <div class="servicesddl col-sm" data-aos="zoom-out" data-aos-delay="300">       
                    <h5 for="service-type" class="form-label">Service Type</h5>
                        <asp:UpdatePanel ID="installservpanel" runat="server">  
                        <ContentTemplate>
                            <asp:DropDownList class="form-select" ID="ddlInstallType" runat="server">
                                <asp:ListItem Value="Vehicles">Vehicles</asp:ListItem>
                                <asp:ListItem Value="Home appliances">Home appliances</asp:ListItem>
                                <asp:ListItem Value="Mobile & Gadget">Mobile & Gadget</asp:ListItem>
                                <asp:ListItem Value="Computer & Accessories">Computer & Accessories</asp:ListItem>
                                <asp:ListItem Value="Musical instrument">Musical instrument</asp:ListItem>
                                <asp:ListItem Value="Industrial Machine">Industrial Machine</asp:ListItem>
                                <asp:ListItem Value="Watches">Watches</asp:ListItem>
                                <asp:ListItem Value="Gaming PC">Gaming PC</asp:ListItem>
                                <asp:ListItem Value="Camera & Drones">Camera & Drones</asp:ListItem>
                                <asp:ListItem Value="Network Infrastructure">Network Infrastructure</asp:ListItem>
                                <asp:ListItem Value="Others">Others</asp:ListItem>
                            </asp:DropDownList>
                          </ContentTemplate>
                          <Triggers>  
                          <asp:AsyncPostBackTrigger ControlID ="ddlInstallType" />  
                          </Triggers>  
                     </asp:UpdatePanel>
                    <asp:UpdatePanel ID="repairservpanel" runat="server">  
                        <ContentTemplate>
                            <asp:DropDownList class="form-select" ID="ddlRepairType" runat="server" Visible="False">
                                <asp:ListItem Value="Vehicles">Vehicles</asp:ListItem>
                                <asp:ListItem Value="Home appliances">Home appliances</asp:ListItem>
                                <asp:ListItem Value="Mobile & Gadget">Mobile & Gadget</asp:ListItem>
                                <asp:ListItem Value="Computer & Accessories">Computer & Accessories</asp:ListItem>
                                <asp:ListItem Value="Musical instrument">Musical instrument</asp:ListItem>
                                <asp:ListItem Value="Industrial Machine">Industrial Machine</asp:ListItem>
                                <asp:ListItem Value="Clothing">Clothing</asp:ListItem>
                                <asp:ListItem Value="Shoes">Shoes</asp:ListItem>
                                <asp:ListItem Value="Watches">Watches</asp:ListItem>
                                <asp:ListItem Value="Game Console">Game Console</asp:ListItem>
                                <asp:ListItem Value="Camera & Drones">Camera & Drones</asp:ListItem>
                                <asp:ListItem Value="Network Infrastructure">Network Infrastructure</asp:ListItem>
                                <asp:ListItem Value="Others">Others</asp:ListItem>
                            </asp:DropDownList>
                        </ContentTemplate>
                      <Triggers>  
                      <asp:AsyncPostBackTrigger ControlID ="ddlRepairType" />  
                      </Triggers>  
                    </asp:UpdatePanel>
                    <asp:UpdatePanel ID="otherservpanel" runat="server">  
                    <ContentTemplate>
                        <asp:DropDownList class="form-select" ID="ddlOtherType" runat="server" Visible="False">
                            <asp:ListItem Value="Insecticide">Insecticide</asp:ListItem>

                            <asp:ListItem Value="Porter services">Porter services</asp:ListItem>

                            <asp:ListItem Value="Data entry">Data entry</asp:ListItem>

                            <asp:ListItem Value="Distribute flyers">Distribute flyers</asp:ListItem>

                            <asp:ListItem Value="Transportation">Transportation</asp:ListItem>

                            <asp:ListItem Value="Others">Others</asp:ListItem>

                        </asp:DropDownList>
                    </ContentTemplate>
                      <Triggers>  
                      <asp:AsyncPostBackTrigger ControlID ="ddlOtherType" />  
                      </Triggers>  
                    </asp:UpdatePanel>
                </div>
                                                         
                <div class="col-sm" data-aos="zoom-out" data-aos-delay="300">
                    <h5 for="state" class="form-label">State</h5>
                    <asp:UpdatePanel ID="statepanel" runat="server">  
                        <ContentTemplate>
                   <asp:DropDownList class="form-select" ID="ddlState" runat="server" AppendDataBoundItems="true" AutoPostBack="true" OnSelectedIndexChanged="ddlState_SelectedIndexChanged">
                    </asp:DropDownList>                              
                    </ContentTemplate>
                        <Triggers>  
                        <asp:AsyncPostBackTrigger ControlID ="ddlState" />  
                        </Triggers>  
                    </asp:UpdatePanel>
                </div>
                  
                <div class="col-sm" data-aos="zoom-out" data-aos-delay="300">   
                    <h5 for="district" class="form-label">City</h5>
                        <asp:UpdatePanel ID="districtpanel" runat="server">  
                        <ContentTemplate>
                    <asp:DropDownList class="form-select" ID="ddlDistrict" runat="server" AutoPostBack="true" AppendDataBoundItems="true" ValidationGroup="FindNow">   
                        <asp:ListItem Value="--Please Select District--">--Please Select District--</asp:ListItem>
                    </asp:DropDownList>
                        <asp:Label ID="lblError" runat="server" Text="" ForeColor="Red"></asp:Label>
                    </ContentTemplate>  
                        <Triggers>  
                        <asp:AsyncPostBackTrigger ControlID ="ddlDistrict" />  
                        </Triggers>  
                    </asp:UpdatePanel>                      
                </div>
            </div>
            <hr />
            <div class="row g-3">     
                    <div class="text-end text-lg-flex justify-content-center" data-aos="zoom-out" data-aos-delay="400">                        
                        <asp:LinkButton ID="btnFindNow" class="btn-find-now d-inline-flex align-items-center justify-content-center align-self-center" runat="server" ValidationGroup="FindNow" OnClick="btnFindNow_Click">  
                        <span>Find Now</span>
                        <i class="bi bi-arrow-right"></i>
                        </asp:LinkButton>                                                              
                    </div>
            </div>        
        </div>
       </section>

    <%--End Service--%>

    <%--Features--%>
    <section id="values" class="features">

      <div class="container" data-aos="fade-up">

        <header class="section-header">                  
          <p>More Than <span>Exceptional</span></p>
        </header>
          <hr />
        <div class="row">
          <div class="col-lg-3">
            <div class="box" data-aos="fade-up" data-aos-delay="100">
                <img src="../Image/feat1.png" class="img-fluid" />        
                <hr />
              <h3>Contactless Services</h3>
              <p>SpeedServ provides nearly zero contact services to our clients to ensure hygieneness and follow pandemic precautions</p>
            </div>
          </div>

          <div class="col-lg-3 mt-4 mt-lg-0">
            <div class="box" data-aos="fade-up" data-aos-delay="200">
              <img src="../Image/feat2.png" class="img-fluid" alt="">
                 <hr />
              <h3>Comphrensive</h3>
              <p>SpeedServ provides varieties of desired services to our clients without need to make any extensive research from internet</p>
            </div>
          </div>

          <div class="col-lg-3 mt-4 mt-lg-0">
            <div class="box" data-aos="fade-up" data-aos-delay="300">
              <img src="../Image/feat3.png" class="img-fluid" alt="">
                 <hr />
              <h3>Transparent & Security</h3>
              <p>SpeedServ servicesrs had been through a series of basic verification and qualification to avoid any unethical behaviour to clients</p>
            </div>
          </div>
          
          <div class="col-lg-3">
            <div class="box" data-aos="fade-up" data-aos-delay="400">
              <img src="../Image/feat4.png" class="img-fluid" alt="">
                 <hr />
              <h3>Support Startups</h3>
              <p>SpeedServ provides platform to every ambitious entrepreneur and freelancer to start their business without having any affordance issues</p>
            </div>
          </div>
        </div>

      </div>

    </section>
    <%--End Features--%>

   <%-- Review--%>
    <section class="review">
    <div class="container" data-aos="fade-left">
            <header class="section-header">                  
              <p><span>Review</span> From Customers</p>
            </header>
            <hr />
            <div class="row">
          <div class="col-lg-4" data-aos="fade-left" data-aos-delay="100" >
            <asp:Image ID="Image1" runat="server" class="bd-placeholder-img rounded-circle mb-3" width="140" height="140" role="img" ImageUrl= "../Image/generaluser.png"/>            
            <h2>Chia Kiat</h2>
            <p>SpeedServ provides excellent services and the servicer are friendly</p>        
          </div>
          <div class="col-lg-4" data-aos="fade-left" data-aos-delay="200">   
            <asp:Image ID="Image2" runat="server" class="bd-placeholder-img rounded-circle mb-3" width="140" height="140" role="img" ImageUrl= "../Image/generaluser.png"/>  
            <h2>Afiq Danial</h2>
            <p>SpeedServ provides many essential services on their websites</p>
        
          </div>
          <div class="col-lg-4" data-aos="fade-left" data-aos-delay="300">
            <asp:Image ID="Image3" runat="server" class="bd-placeholder-img rounded-circle mb-3" width="140" height="140" role="img" ImageUrl= "../Image/generaluser.png"/>  
            <h2>Aarush</h2>
            <p>SpeedServ servicers are well-performed and look professional</p>        
          </div>
        </div>
        </div>
    </section>
    <!-- End Review Section -->
  
 
  <%--Join--%>
  <div class="d-md-flex flex-md-equal w-100">
        <div class="signup pt-3 px-3 pt-md-5 px-md-5 text-center overflow-hidden" data-aos="flip-left" data-aos-delay="200">
          <div class="my-3 py-3" data-aos="zoom-out" data-aos-delay="400">
            <h2 class="display-5">Sign Up Now</h2>
            <p class="lead">Sign Up as SpeedServ Member Now</p>
              <asp:Button ID="btnClientSignUp" class="btn" runat="server" Text="Sign Up" OnClick="btnClientSignUp_Click" />              
          </div>
          <div class="bg-white shadow-sm mx-auto" style="width: 80%; height: 400px; border-radius: 21px 21px 0 0;"></div>
        </div>
        <div class="joinserv pt-3 px-3 pt-md-5 px-md-5 text-center overflow-hidden" data-aos="flip-right" data-aos-delay="200">
          <div class="my-3 p-3" data-aos="zoom-out" data-aos-delay="400">
            <h2 class="display-5">Join As Servicer</h2>
            <p class="lead">Join As One of our SpeedServ Servicer Now</p>
              <asp:Button ID="btnServicerSignUp" class="btn" runat="server" Text="Join Now" OnClick="btnServicerSignUp_Click" />                 
          </div>
          <div class="bg-white shadow-sm mx-auto" style="width: 80%; height: 400px; border-radius: 21px 21px 0 0;"></div>
        </div>  
    </div>
    <!-- End Join Section --> 

    <style>
        .banner-image {
        background-image: url('banner-img.jpg');
        background-size: cover;
        }

        hr{
        margin-top: 1.2rem;
        margin-bottom: 1.2rem;
        border: 0;
        border-top: 1px solid white;
        color: white;
        }

    </style>

   <%-- AOS script plugin--%>
  <script src="https://unpkg.com/aos@next/dist/aos.js"></script>
  <script>
     /* AOS plugin script*/
      AOS.init();    
  </script>
</asp:Content>