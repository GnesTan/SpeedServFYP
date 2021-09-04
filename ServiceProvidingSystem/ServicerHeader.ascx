<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ServicerHeader.ascx.cs" Inherits="ServiceProvidingSystem.ServicerHeader" %>

<nav class="nav-area">
        <ul>
            <li style="margin-right:20%; margin-top:5px;">
                <a class="navbar-brand" href="RequestList.aspx" runat="server"><img src="~/Image/JojoLogoArtist.PNG" width="50" height="50" runat="server"/></a>

            </li>
            <li><a href="#">About</a></li>
            <li><a href="RequestList.aspx">Request</a></li>
            <li style="margin-right:20%;"><a href="CurrentServing.aspx">My Services</a>
                <ul>
                    <li><a href="CurrentServing.aspx">Current Serving</a></li>
                    <li><a href="ServiceHistory.aspx">Service History</a></li>
                    <li><a href="PostService.aspx">Posted Services</a></li>
                </ul>
            </li>
            <li><asp:LinkButton class="nav-link color-white" ID="LinkButton2" PostBackUrl="~/Artist/EditProfile.aspx" runat="server" CausesValidation="false"><i class="fa fa-user" aria-hidden="true"></i></asp:LinkButton>
                <ul>
                    <li><a href="ServicerViewProfile.aspx">My Profile</a></li>
                    <li><a href="#">My Ranking</a></li>
                    <li><a href="#">My Subscription</a></li>
                    <li><a href="#">Report</a></li>
                    <li><a href="SignOut1.aspx">Sign Out</a></li>
                </ul>
            </li>
        </ul>
    </nav>


<style>


        a{
            font-size: 20px;
        }
        .nav-area{
            background: #262626;
            vertical-align:central;
            height:100px;
        }
        .nav-area:after{
            content: '';
            clear: both;
            display: block;
        }
        .nav-area ul{
            list-style: none;
            margin: 0;
            z-index:100;
        }
        .nav-area > ul > li{
            float: left;
            position: relative;
            margin-top:20px;
            z-index:100;
        }
        .nav-area ul li a{
            text-decoration: none;
            color: #fff;
            padding: 15px 40px;
            display: block;
            z-index:100;
        }
        .nav-area ul li:hover a{
            background: #34495e;
        }

        .nav-area ul ul{
            position: absolute;
            padding: 0;
            min-width: 230px;
            display: none;
            top: 100%;
            left: 0;
        }
        .nav-area ul li:hover > ul{
            display: block;
        }
        .nav-area ul ul li:hover a{
            background: #262626;
        }

        .nav-area ul ul li{
            position: relative;
            z-index:100;
        }
        .nav-area ul ul ul{
            top: 0;
            left: 100%;
        }
        .nav-area ul ul ul li:hover a{
            background: #34495e;
        }




</style>