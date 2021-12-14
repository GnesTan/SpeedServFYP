
<%@ Page Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="MyRanking.aspx.cs" Inherits="ServiceProvidingSystem.Servicer.MyRanking" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server"> 
       

    <div class="topicName">
        <br />
        <br />
        <b><u>My Ranking</u></b>
    </div>

    <div class="mainContainer">
        <%--If No Item Section--%>


            <div class="infoContainer">
                <div class="infoContainer2">
                    <div class="tbDetails">
                    <table>
                        <tr class="rowStyle">
                            <td>
                                <asp:Label ID="lblId" runat="server" Text="Your ID:"></asp:Label>
                            </td>
                            <td style="padding-right:40px;">
                                <b><asp:Label ID="lblRetreId" runat="server" Text="S000152142"></asp:Label></b>
                            </td>
                            <td>
                                <asp:Label ID="lblCollectedPoint" runat="server" Text="Current collected points:"></asp:Label>
                            </td>
                        </tr>
                        <tr class="rowStyle">
                            <td colspan="2">
               
                            </td>
                            <td style="padding-right:40px;">
                                <b><asp:Label ID="lblRetreCollectedPoint" runat="server" Text="1100"></asp:Label></b>
                            </td>
                        </tr>
                        <tr class="rowStyle">
                            <td>
                               
                            </td>
                            <td style="padding-right:20px;">
                                
                            </td>
                            <td>
                                <asp:Label ID="lblNextRank" runat="server" Text="Points needed to next rank:"></asp:Label>
                            </td>
                        </tr>
                        <tr class="rowStyle">
                            <td>
                                 <asp:Label ID="lblRanking" runat="server" Text="Current Ranking:"></asp:Label>
                            </td>
                            <td style="padding-right:20px;">
                                
                            </td>
                            <td>
                                <b><asp:Label ID="lblRetreNextRank" runat="server" Text="0"></asp:Label></b>
                            </td>
                        </tr>
                        <tr class="rowStyle">
                            <td>
                                <asp:Image ID="imgIcon" runat="server" ImageUrl="/Image/Bronze.png" Width="80px" Height="80px" />
                            </td>
                            <td colspan="2">
                                <b><asp:Label ID="lblRank" runat="server" Text="Bronze" ForeColor="Brown" Font-Size="Larger"></asp:Label></b>

                            </td>
                            
                        </tr>
                        
                    </table>

                


               </div> 

                <div class="displayText">
                    <b>
                    <asp:Label ID="lblPoint" runat="server" Text="Available points:"></asp:Label>
                    <asp:Label ID="lblRetrePoint" runat="server" Text="200"></asp:Label>
                    </b>
                </div>


            </div> 




    </div>

        <div class="btnClass">
            <div class="otherBtn">
                <asp:LinkButton ID="lbCollect" runat="server" class="btnNew btn-primary btn-lg" OnClick="lbCollect_Click">Collect</asp:LinkButton>
            </div> 
        </div> 
    <br />
    <br />
    <br /> 
        <asp:Label ID="lblNote" runat="server" Text="*The higher the ranking, the higher priority to be sorted and displayed in the service searching performed by client."></asp:Label>
        <br />
        <asp:Label ID="lblNote2" runat="server" Text="The ranking will also able to be viewed by client."></asp:Label>
        
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
        height:300px;
        box-shadow: 3px 3px 10px 0 #b3b3b3;
        border-radius: 2px;  
        margin-left:5%;
        margin-right:5%;
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
        margin-right:80px;
        float:left;
    }

    .rowStyle > td {
        padding-bottom: 1em;

    }
    


.mainContainer {
        background-color: white;
        max-width: 1100px;
        margin: auto;
        box-shadow: 3px 3px 10px 0 #b3b3b3;
        border-radius: 5px;
        text-align: left;
        padding-top: 50px;
        padding-bottom: 100px;
        margin-bottom: 100px;
        height:450px;
}


.topicName{
    max-width: 1100px;
    margin: auto;
    margin-top: 110px;
    font-size: 25px;
}

.displayText{
    float:right;
    margin-top:40px;
    margin-right:10px;
    color:darkblue;
    font-size:x-large;

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

.otherBtn{
    float:right;
}






</style>
</asp:Content>


