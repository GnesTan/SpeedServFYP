<%@ Page Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="ServicerViewProfile.aspx.cs" Inherits="ServiceProvidingSystem.Servicer.ServicerViewProfile" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server"> 
       

    <div class="mainContainer">
        <%--If No Item Section--%>


            <div class="infoContainer">
                <div class="infoContainer2">
                    <div class="profileTitle">
                       <h2><u>Profile</u></h2>
                    </div>
                    <div class="tbDetails">
                    <table>
                        <tr class="rowStyle">
                            <td>
                                <asp:Label ID="lblName" runat="server" Text="Name:"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblDpName" runat="server" Font-Bold="true"></asp:Label>
                            </td>
                        </tr>
                        <tr class="rowStyle">
                            <td>
                                <asp:Label ID="lblIC" runat="server" Text="IC No.:"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblDpIC" runat="server" Font-Bold="true"></asp:Label>
                            </td>
                        </tr>
                        <tr class="rowStyle">
                            <td>
                                <asp:Label ID="lblDob" runat="server" Text="Date of Birth:"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblDpDob" runat="server" Font-Bold="true"></asp:Label>
                            </td>
                        </tr>
                        <tr class="rowStyle">
                            <td>
                                <asp:Label ID="lblGender" runat="server" Text="Gender:"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblDpGender" runat="server" Font-Bold="true"></asp:Label>
                            </td>
                        </tr>
                        <tr class="rowStyle">
                            <td>
                                <asp:Label ID="lblPhoneNo" runat="server" Text="Phone No.:"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblDpPhoneNo" runat="server" Font-Bold="true"></asp:Label>
                            </td>
                        </tr>
                        <tr class="rowStyle">
                            <td>
                                <asp:Label ID="lblEmail" runat="server" Text="Email Address:"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblDpEmail" runat="server" Font-Bold="true"></asp:Label>
                            </td>
                        </tr>
                        <tr class="rowStyle" style="height:150px;">
                            <td>
                                <asp:Label ID="lblAddress" runat="server" Text="Home Address:"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblDpAddress" runat="server" Width="200px" Font-Bold="true"></asp:Label>
                            </td>
                        </tr>
                        <tr class="rowStyle">
                            <td>
                                <asp:Label ID="lblDays" runat="server" Text="Working Days:"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblDpDays" runat="server" Width="200px" Font-Bold="true"></asp:Label>
                            </td>
                        </tr>
                        <tr class="rowStyle">
                            <td>
                                <asp:Label ID="lblTime" runat="server" Text="Available Time:"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblDpTime" runat="server" Font-Bold="true"></asp:Label>
                            </td>
                        </tr>
                    </table>
                


               </div> 


            </div> 

                                <div class="profilePic">
                    <asp:Image ID="imgProfile" runat="server" Height="140px" Width="140px" style="border-radius:100px; box-shadow: 0 0 0 3px #b3b3b3; text-decoration:none;" ImageUrl="~/Image/Foto_Formal.jpg" />
                </div>



    </div>

        <div class="btnClass">
            <div class="firstBtn">
                <a runat="server" href="~/ResetPassword.aspx" class="btnNew btn-primary btn-lg">Reset Password</a>
            </div> 

            <div class="otherBtn">
                <a href="ServicerEditProfile.aspx" class="btnNew btn-primary btn-lg">Edit Profile</a>
                <asp:LinkButton ID="lbDeactivate" runat="server" onClientClick="return fnConfirm();" class="btnNew btn-secondary btn-lg" OnClick="lbDeactivate_Click">Deactivate</asp:LinkButton>
                <script type="text/javascript">
                    function fnConfirm() {
                        return confirm("Are you sure you want to deactivate this account? (After deactivated, your services are not able to view by client.)");
                    }
                </script>
                <asp:LinkButton ID="lbActivate" runat="server" class="btnNew btn-secondary btn-lg" OnClick="lbActivate_Click" Visible="false">Activate</asp:LinkButton>
            </div> 
        </div> 
    <br />
    <br /> 
        
    </div>
    

<style>
    body{
        font-family: Arial, Helvetica, sans-serif;   
        background-color: #f2f2f2;      
    }

        header{
        box-shadow: 0 .5rem 1rem rgba(0,0,0,.15);
        background: white;
    }

    .infoContainer { 
        
        text-align: left;
        height:650px;
        box-shadow: 3px 3px 10px 0 #b3b3b3;
        border-radius: 2px;  
        margin-left:10%;
        margin-right:10%;
    }

    .infoContainer2 {                                           
        margin-top:30px;
        margin-left:30px;
        float:left;
    }

    .profileTitle {                                           
        font-size:xx-large;

    }



    .tbDetails {                                           
        font-size:large;

    }

    .rowStyle > td {
        padding-bottom: 1em;
        padding-right:20px;

    }
    


.mainContainer {
        background-color: white;
        max-width: 1000px;
        margin: auto;
        box-shadow: 3px 3px 10px 0 #b3b3b3;
        border-radius: 5px;
        text-align: left;
        padding-top: 50px;
        padding-bottom: 100px;
        margin-bottom: 100px;
        margin-top:150px;
        height:820px;
}



.profilePic{
    float:right;
    margin-top:40px;
    margin-right:60px;
}

.btnClass{
    text-align:left;
    width:auto;
    margin-top:40px;
    margin-left:10%;
    margin-right:10%;

}


/*---Button --*/
.btnNew{  
  max-width: 100%;
  max-height: 100%;
  background: none;
  border: 3px solid;
  border-radius: 10px;
  color: #ffffff;
  font-weight: 600;
  text-transform: uppercase;
  cursor: pointer;
  font-size: 16px;
  position: relative;
  box-shadow: 0 5px 5px 0 rgba(0,0,0,0.1), 0 5px 10px 0 rgba(0,0,0,0.1);
}


.btn-primary {
    background-color: #898989;
    border: 1px solid #563d7c;
}

.btn-primary:hover {
        background-color: #A19F9F;
        border: 1px solid #A19F9F;
}

.btn-secondary {
    background-color: #F43838;
    border: 1px solid #F43838;
}

.btn-secondary:hover {
        background-color: #F54A4A;
        border: 1px solid #F54A4A;
}

.firstBtn{
    float:left;
}

.otherBtn{
    float:right;
}


</style>
</asp:Content>


