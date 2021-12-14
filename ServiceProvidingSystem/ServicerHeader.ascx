<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ServicerHeader.ascx.cs" Inherits="ServiceProvidingSystem.ServicerHeader" %>

    <header id="header" class="header fixed-top">
        <div class="container-fluid container-xl d-flex align-items-center justify-content-between">

            <asp:LinkButton runat="server" class="logo d-flex align-items-center" PostBackUrl="~/Servicer/RequestList.aspx">
            <i class="bi bi-lightning"></i>
            <span><span class="spec" style="color: #FE5959;">S</span>peedServ</span>
            </asp:LinkButton>

            <%--Nav Bar--%>
                <nav id="navbar" class="navbar">
                <ul>          
                    <li><a class="navname" id="displayName" runat="server"></a></li>
                    <li><asp:LinkButton class="nav-link" runat="server" PostBackUrl="~/Servicer/RequestList.aspx">Request</asp:LinkButton></li>
                    <li class="dropdown"><asp:LinkButton runat="server" PostBackUrl="~/Servicer/CurrentServingList.aspx"><span>My Services</span> <i class="bi bi-chevron-down"></i></asp:LinkButton>
                    <ul>
                    <li><asp:LinkButton runat="server" PostBackUrl="~/Servicer/CurrentServingList.aspx">Current Serving</asp:LinkButton></li>
                    <li><asp:LinkButton runat="server" PostBackUrl="~/Servicer/ServiceHistory.aspx">Service History</asp:LinkButton></li>
                    <li><asp:LinkButton runat="server" PostBackUrl="~/Servicer/MyService.aspx">Posted Services</asp:LinkButton></li>
                    </ul>
                    </li>

                    <li class="dropdown"><asp:LinkButton class="nav-link" runat="server" PostBackUrl="~/Servicer/ServicerViewProfile.aspx"><i class="bi bi-person-circle" style="margin-right: 5px;"></i><i class="bi bi-chevron-down"></i></asp:LinkButton>
                    <ul style="margin-top: 10px;">
                    <li><asp:LinkButton runat="server" PostBackUrl="~/Servicer/ServicerViewProfile.aspx">My Profile</asp:LinkButton></li>
                    <li><asp:LinkButton runat="server" PostBackUrl="~/Servicer/ServicerWallet.aspx">My Wallet</asp:LinkButton></li>
                    <li><asp:LinkButton runat="server" PostBackUrl="~/Servicer/MyRanking.aspx">My Ranking</asp:LinkButton></li>
                    <li><asp:LinkButton runat="server" PostBackUrl="~/Servicer/MySubscription.aspx">My Subscription</asp:LinkButton></li>
                    <li><asp:LinkButton runat="server" PostBackUrl="~/Servicer/ViewReport.aspx">Report</asp:LinkButton></li>
                    <li><asp:LinkButton ID="lbSignOut" runat="server" OnClick="lbSignOut_Click">Sign Out</asp:LinkButton></li>
                    </ul>
                    </li>
                </ul>        
                </nav>
            <%--End Nav Bar--%>
        </div>
    </header>
