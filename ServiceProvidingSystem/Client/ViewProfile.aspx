<%@ Page Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="ViewProfile.aspx.cs" Inherits="ServiceProvidingSystem.Client.ClientManagement.ViewProfile" %>

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
    <link href="../DisplayService.css" rel="stylesheet" />         
    <link href="../ViewProfile.css" rel="stylesheet" />

    <asp:ScriptManager ID="ScriptManager1" runat="server">  
    </asp:ScriptManager> 

    <section id="#" class="viewProfile d-flex align-items-center">
    <div class="container">
        <div class="row mb-3">
            <div class="col">
                <h1 data-aos="fade-right"><span style="">My</span> Profile</h1>
            </div>
        </div>
    
        <div data-aos="zoom-out" data-aos-delay="100">  
        <div class="container shadow p-4 mb-5 bg-white rounded" data-aos="zoom-out" data-aos-delay="50" >       
             
            <%--Servicer profile--%>
            <div class="reqStatusMargin col-sm px-5 py-3">      
                <div class="row mb-5">
                    <div class="col-md-4 py-2">
                        <asp:Image ID="imgProfile" runat="server" Height="160px" Width="160px" style="border-radius:100px; box-shadow: 0 0 0 3px #b3b3b3; text-decoration:none;" ImageUrl="~/Image/Foto_Formal.jpg" />
                    </div>
                    <div class="col-md-7 py-5">
                        <div class="row mb-3">
                            <div class="col-md-12 mx-2">
                                <h4 for="name" class="form-label align-middle">Name</h4>
                                <asp:Label ID="lblDpName" class="reqDetailsLabel clientName align-middle" runat="server"></asp:Label>	
                            </div>
                        </div>            
                    </div>
                </div>
                <div class="row mb-5 mt-3">
                    <div class="col-md-4">
                        <h4 for="name" class="form-label align-middle">Identity No.</h4>
                        <asp:Label ID="lblDpIC" class="reqDetailsLabel title align-middle" runat="server"></asp:Label>	
                    </div>
                    <div class="col-md-4">
                        <h4 for="name" class="form-label align-middle">Date of Birth</h4>
                       <asp:Label ID="lblDpDob" class="reqDetailsLabel title align-middle" runat="server"></asp:Label>		
                    </div>
                    <div class="col-md-4">
                        <h4 for="name" class="form-label align-middle">Gender</h4>
                       <asp:Label ID="lblDpGender" class="reqDetailsLabel title align-middle" runat="server"></asp:Label>		
                    </div>
                </div>
                <div class="row mb-5 mt-3">
                    <div class="col-md-4">
                        <h4 for="name" class="form-label align-middle">Phone No.</h4>
                        <asp:Label ID="lblDpPhoneNo" class="reqDetailsLabel title align-middle" runat="server"></asp:Label>	
                    </div>
                    <div class="col-md-6">
                        <h4 for="name" class="form-label align-middle">Email Address</h4>
                       <asp:Label ID="lblDpEmail" class="reqDetailsLabel title align-middle" runat="server"></asp:Label>		
                    </div>
                </div>
                <div class="row mb-5 mt-3">
                    <div class="col-md-8">
                        <h4 for="name" class="form-label align-middle">Home Address</h4>
                        <asp:Label ID="lblDpAddress" class="reqDetailsLabel title align-middle" runat="server"></asp:Label>	
                    </div>
                </div> 
            </div>   
            <hr />
            <div class="row g-3">     
                <div class="text-end text-lg-flex justify-content-center">  
                    <asp:LinkButton ID="btnResetPassword" class="btn-find-now btnChangePassword d-inline-flex align-items-center justify-content-center align-self-center" runat="server" OnClick="btnResetPassword_Click">  
                    <span>Reset Password</span>                                
                    </asp:LinkButton>     

                    <asp:LinkButton ID="btnEditProfile" class="btn-find-now btnCancel d-inline-flex align-items-center justify-content-center align-self-center" runat="server" OnClick="btnEditProfile_Click">  
                    <span>Edit Profile</span>                                
                    </asp:LinkButton>                        
                </div>
            </div>  
            </div>               
        </div> 

        <div data-aos="zoom-out" data-aos-delay="100">  
            <div class="container shadow p-4 mb-5 bg-white rounded" data-aos="zoom-out" data-aos-delay="50" >       
             <h3 data-aos="fade-right"><span style="">M</span>embership Details</h3>
                <%--Membership Details--%>
                <div class="reqStatusMargin col-sm px-5 py-3">      
                    <div class="row mb-5">
                        <div class="col-md-8 py-2">
                            <h4 for="name" class="form-label align-middle">Current Rank</h4>                            
                            <asp:Image ID="imgRank" runat="server" ImageUrl="../Image/bronze-rank.png"/><asp:Label ID="lblDpRank" class="reqDetailsLabel currentRank" runat="server" style="margin-left: 20px;" ForeColor="#996633"></asp:Label>
                        </div>
                        <div class="col-md-4 py-2">
                            <h4 for="name" class="form-label align-middle">Current Reward Points</h4>                            
                            <asp:Label ID="lblDpCurrentPoints" class="reqDetailsLabel title align-middle" runat="server"></asp:Label>
                        </div>   
                    </div>
                    <div class="row mb-5">
                        <div class="col-md-4 py-2">
                            <h4 for="name" class="form-label align-middle">Max Service able to request</h4>                            
                            <asp:Label ID="lblDpMaxService" class="reqDetailsLabel title align-middle" runat="server"></asp:Label>
                        </div>
                        <div class="col-md-4 py-2">
                            <h4 for="name" class="form-label align-middle" runat="server" ID="lblDisText">Request needed for Next Rank</h4>                            
                            <asp:Label ID="lblDpRequestNeeded" class="reqDetailsLabel title align-middle" runat="server"></asp:Label>
                        </div>
                         <div class="col-md-4 py-2">
                            <h4 for="name" class="form-label align-middle">Total Completed Services Requested</h4>                            
                            <asp:Label ID="lblDpServComplete" class="reqDetailsLabel title align-middle" runat="server"></asp:Label>	
                        </div> 
                    </div>
                   
                </div>               
            </div> 
        </div>

    <%--End View Profile--%>
          
    </div>
    </section>         
    
    <%-- AOS script plugin--%>
<script src="https://unpkg.com/aos@next/dist/aos.js"></script>
<script>
    /* AOS plugin script*/
    AOS.init();
</script>
</asp:Content>


