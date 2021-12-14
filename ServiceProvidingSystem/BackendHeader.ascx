<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="BackendHeader.ascx.cs" Inherits="ServiceProvidingSystem.BackendHeader" %>


    <header id="header" class="header fixed-top">
        <div class="container-fluid container-xl d-flex align-items-center justify-content-between">

            <asp:LinkButton runat="server" class="logo d-flex align-items-center" PostBackUrl="~/BackendUser/BackEndHomepage.aspx">
            <i class="bi bi-lightning"></i>
            <span><span class="spec" style="color: #FE5959;">S</span>peedServ</span>
            </asp:LinkButton>

            <%--Nav Bar--%>
                <nav id="navbar" class="navbar">
                <ul>                                              
                    <li class="dropdown"><asp:LinkButton runat="server" PostBackUrl="#"><span>Request</span> <i class="bi bi-chevron-down"></i></asp:LinkButton>
                    <ul>
                    <li><asp:LinkButton runat="server" PostBackUrl="~/BackendUser/ServicerWithdrawal.aspx">Servicer Withdrawal</asp:LinkButton></li>
                    <li><asp:LinkButton runat="server" PostBackUrl="~/BackendUser/SubscriptionRequest.aspx">Subscription</asp:LinkButton></li>
                    <li><asp:LinkButton runat="server" PostBackUrl="~/BackendUser/ClientPayment.aspx">Client Payment</asp:LinkButton></li>
                    </ul>
                    </li>

                    <li class="dropdown"><asp:LinkButton class="nav-link" runat="server" PostBackUrl="#"><i class="bi bi-person-circle" style="margin-right: 5px;"></i><i class="bi bi-chevron-down"></i></asp:LinkButton>
                    <ul style="margin-top: 10px;">
                    <li><asp:LinkButton runat="server" PostBackUrl="~/BackendUser/AccountMaintenance.aspx">Account Maintenance</asp:LinkButton></li>
                    <li><asp:LinkButton runat="server" PostBackUrl="~/BackendUser/BackendReport.aspx">Report</asp:LinkButton></li>
                    <li><asp:LinkButton ID="lbSignOut" runat="server" OnClick="lbSignOut_Click">Sign Out</asp:LinkButton></li>
                    </ul>
                    </li>
                </ul>        
                </nav>
            <%--End Nav Bar--%>
        </div>
    </header>