<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="BackendHeader.ascx.cs" Inherits="ServiceProvidingSystem.BackendHeader" %>

            <nav class="navbar navbar-expand-md navbar-dark bg-dark sticky-top">
                <div class ="container-fluid">         
                <a class="navbar-brand" href="~/Homepage.aspx" runat="server"><img src="~/Image/JojoLogoArtist.PNG" width="50" height="50" runat="server"/></a>
                 <button class="navbar-toggler" type="button" data-toggle="collapse" data-target="#navbarResponsive">
                    <span class="navbar-toggler-icon"></span>
                </button>
                <div class="collapse navbar-collapse" id="navbarResponsive">
                    <ul class="navbar-nav ml-auto">       
                          <li class="nav-item active">
                          <asp:Label class="nav-link" ID="welcomeName" runat="server" Text=""></asp:Label>
                              &nbsp;&nbsp;&nbsp;&nbsp;
                        </li>  
                          <li class="nav-item active">
                            <asp:LinkButton class="nav-link" ID="LinkButton4" PostBackUrl="~/Artist/ArtistMenu.aspx" runat="server" CausesValidation="false">Menu</asp:LinkButton>
                        </li>                                           
                      
                        <li class="nav-item active">
                            <asp:LinkButton class="nav-link color-white" ID="LinkButton2" PostBackUrl="~/Servicer/ServicerViewProfile.aspx" runat="server" CausesValidation="false"><i class="fa fa-user" aria-hidden="true"></i><span class="tooltiptext">Edit Profile</span></asp:LinkButton>
                        </li>


                    </ul>              
                </div>
               </div> 
            </nav>