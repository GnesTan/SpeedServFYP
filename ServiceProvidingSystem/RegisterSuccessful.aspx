<%@ Page Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="RegisterSuccessful.aspx.cs" Inherits="ServiceProvidingSystem.RegisterSuccessful" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">


 
    <!-- Login Section -->


         <hr>
             
              <!-- Login Form -->
 
       <div class="loginForm">


        <div>
             <h1>Successful Registered</h1>
        </div>
        <div class="loginChild">
            <div class="inputLayout">
                <asp:Image ID="Image1" runat="server" Height="141px" ImageUrl="~/Image/Success.JPG" Width="157px" />

                <table>
                    <tr>
                        <td style="text-align:center">
                            Your account have created sucessfully, please verify your email to get started.
                           
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align:center">
                            <br>
                        </td>
                    </tr>


                    <tr>
                        <td style="text-align:center">
                            You have been registred successfully, we will redirect you to login page in 5 seconds.
                        </td>             
                    </tr>

                    <tr>
                        <td style="text-align:center">
                            <br>
                        </td>
                    </tr>

                    <tr>
                        <td style="text-align:center">
                            If the web page not redirect automatically, click
                            <asp:HyperLink ID="hlLogin" runat="server" NavigateUrl="Login.aspx">Here</asp:HyperLink>

                        </td>             
                    </tr>


                </table> 

            </div>



        </div>




     </div>

    <style type="text/css">

    h1{
        font-size:larger;
    }

    body{    
        background-color: #f2f2f2;      
    }

    .loginForm {
        background-color: white;
        text-align:center;
        max-width: 680px;
        margin: auto;
        box-shadow: 3px 3px 10px 0 #b3b3b3;
        border-radius: 5px;    
        width: 35%;
        height:500px;
        display: block;
        margin-top: 180px;
        margin-bottom: 5%;
        margin-left: auto;
        margin-right: auto;
        padding: 3%;
        position: relative;
    }

    .loginChild{
      position: absolute;
      top: 10%;
      left: 10%;
      margin: -25px 0 0 -25px; /* apply negative top and left margins to truly center the element */
      margin-top:10%;

    }

    .inputLayout{
      margin-left:auto;
      margin-right:auto;

    }



    </style>

</asp:Content>


