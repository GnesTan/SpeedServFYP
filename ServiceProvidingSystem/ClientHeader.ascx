<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ClientHeader.ascx.cs" Inherits="ServiceProvidingSystem.ClientHeader" %>

    <header id="header" class="header fixed-top">
        <div class="container-fluid container-xl d-flex align-items-center justify-content-between">

            <asp:LinkButton runat="server" class="logo d-flex align-items-center" PostBackUrl="~/Client/ClientHomePage.aspx">
            <i class="bi bi-lightning"></i>
            <span><span class="spec" style="color: #FE5959;">S</span>peedServ</span>
            </asp:LinkButton>

            <%--Nav Bar--%>
                <nav id="navbar" class="navbar">
                <ul>          
                    <li><a class="navname" id="displayName" runat="server"></a></li>
                    <li><a class="nav-link scrollto" href="#services" runat="server" onserverclick="liService_Click">Services</a></li>
                    <li class="dropdown"><a href="PostServiceRequest.aspx"><span>Request</span> <i class="bi bi-chevron-down"></i></a>
                    <ul>
                        <li><a runat="server" onserverclick="liRequest_Click">Request Service</a></li>
                        <li><a runat="server" onserverclick="liRequestStatus_Click">Request Status</a></li>
                    </ul>
                    </li>
                    <li class="dropdown"><a runat="server" onserverclick="liPay_Click"><span>Payment</span> <i class="bi bi-chevron-down"></i></a>
                    <ul>                       
                        <li><a runat="server" onserverclick="liPay_Click">Pay Service</a></li>    
                         <li><a runat="server" onserverclick="liTransact_Click">Recent Transaction</a></li>  
                    </ul>
                    </li>
                    <li class="dropdown"><a class="getstarted"><i class="bi bi-person-circle" style="margin-right: 5px;"></i><i class="bi bi-chevron-down"></i></a>
                    <ul style="margin-top: 10px;">
                        <li><a runat="server" onserverclick="liView_Click">View Profile</a></li>
                        <li ><a runat="server" onserverclick="liFav_Click">Favourite</a></li>  
                         <li ><a runat="server" onserverclick="liReport_Click">Report</a></li> 
                        <li><a runat="server" id="liLog" onserverclick="liLog_Click"></a></li>
                    </ul>
                    </li>
                </ul>        
                </nav>
            <%--End Nav Bar--%>
        </div>
    </header>