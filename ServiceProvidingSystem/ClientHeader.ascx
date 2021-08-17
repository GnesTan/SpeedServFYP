<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ClientHeader.ascx.cs" Inherits="ServiceProvidingSystem.ClientHeader" %>

            <nav class="navbar navbar-expand-md navbar-light bg-light sticky-top">
                <div class ="container-fluid">         
                <a class="navbar-brand" href="~/Homepage.aspx" runat="server"><img src="~/LucaImage/BootStrapImage/JojoLogo.PNG" width="50" height="50" runat="server"/></a>
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
                            <asp:LinkButton class="nav-link" ID="LinkButton4" PostBackUrl="~/Customer/UserView.aspx" runat="server" CausesValidation="false">Artwork</asp:LinkButton>
                        </li>                                            
                      
                        <li class="nav-item active">
                            <asp:LinkButton class="nav-link" ID="LinkButton2" PostBackUrl="~/Customer/EditProfile.aspx" runat="server" CausesValidation="false"><i class="fa fa-user" aria-hidden="true"></i><span class="tooltiptext">Edit Profile</span></asp:LinkButton>
                        </li>

                         <li class="nav-item active">
                            <asp:LinkButton class="nav-link" ID="LinkButton7" PostBackUrl="~/Customer/WishList.aspx" runat="server" CausesValidation="false"><i class="fa fa-heart" aria-hidden="true"></i><span class="tooltiptext">Wishlist</span></asp:LinkButton>
                        </li>

                         <li class="nav-item active">
                            <asp:LinkButton class="nav-link" ID="LinkButton13" PostBackUrl="~/Customer/ShoppingCart.aspx" runat="server" CausesValidation="false"><i class="fa fa-shopping-cart" aria-hidden="true"></i><span class="tooltiptext">Shopping Cart</span></asp:LinkButton>
                        </li>

                        <li class="nav-item active">
                            <asp:LoginStatus CssClass="nav-link" ID="LoginStatus1" runat="server" /> 
                        </li> 
                    </ul>              
                </div>
               </div> 
            </nav>