<%@ Page Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="RequestList.aspx.cs" Inherits="ServiceProvidingSystem.Servicer.RequestList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server"> 
       

    <div class="topicName">
        <br />
        <br />
        <b><u>Service Request</u></b>&nbsp&nbsp&nbsp<a href ="RequestList.aspx"  class="btnNew btn-primary btn-lg" > Refresh</a>
    </div>

    
    <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>


    <div class="formBorder">

        <asp:Panel ID="RepeaterPanel" runat="server">

        <asp:Repeater  ID="RepeaterRequest" runat="server" >   
                      
                          
        <ItemTemplate>
            <div class="rowStyle"> 
                <div class="rowMargin">
                    <table class="tblStyle">
                        <tr>
                            <td style="width:130px; text-align:right; padding-right:20px;">
                                Request No.:
                                
                            </td>
                 
                            <td style="width:280px;">
                                <asp:Label ID="lblRequestNo" runat="server" Font-Bold="true" ForeColor="Black" Text='<%# Eval("request_no") %>'></asp:Label>
                            </td>
                            <td style="width:200px;">
                                Category & Type:
                            </td>
                            <td style="width:200px;">
                                Area:
                            </td>

                    
                        </tr>
                        <tr>
                            <td style="width:130px; text-align:right; padding-right:20px;">
                                Name:
                            </td>
                 
                            <td style="width:280px;">
                                <asp:Label ID="lblName" runat="server" Font-Bold="true" ForeColor="Black" Text='<%# Eval("full_name") %>'></asp:Label>
                            </td>
                            <td style="width:200px;">
                                <asp:Label ID="lblCategory" runat="server" Font-Bold="true" ForeColor="Black" Text='<%# Eval("service_category") %>'></asp:Label>
                            </td>
                            <td style="width:200px;">
                                <asp:Label ID="lblArea" runat="server" Font-Bold="true" ForeColor="Black" Text='<%# Eval("district") %>'></asp:Label>,&nbsp<asp:Label ID="lblState" runat="server" Font-Bold="true" ForeColor="Black" Text='<%# Eval("state") %>'></asp:Label>
                            </td>

                    
                        </tr>
                        <tr>
                            <td style="width:130px; text-align:right; padding-right:20px;">
                                Date:
                            </td>    
                            <td style="width:280px;">
                                <asp:Label ID="lblDate" runat="server" Font-Bold="true" ForeColor="Black" Text='<%# Convert.ToDateTime(Eval("date_and_time")).ToString("dd/MM/yyyy") %>'></asp:Label>
                            </td>
                            <td style="width:200px;">
                                <asp:Label ID="lblType" runat="server" Font-Bold="true" ForeColor="Black" Text='<%# Eval("service_type") %>'></asp:Label>
                            </td>
                            <td style="width:200px;">
                                Status:
                            </td>

                    
                        </tr>
                        <tr>
                            <td style="width:130px; text-align:right; padding-right:20px;">
                                Time:
                            </td>
                 
                            <td style="width:280px;">
                                <asp:Label ID="lblTime" runat="server" Font-Bold="true" ForeColor="Black" Text='<%# Convert.ToDateTime(Eval("date_and_time")).ToString("HH:mm") %>'></asp:Label>
                            </td>
                            <td style="width:200px;">
                            </td>

                            <td style="width:200px;">
                                <b>Looking</b>
                            </td>

             
                        </tr>
                    </table>  
               </div>
                <div class="btnClass">
                    <asp:Button class="btnNew btn-secondary btn-lg" ID="btnView" runat="server"  CausesValidation="False" CommandName="Cancel" Text="View" OnClick="btnView_Click" />
                </div>

            </div>
            <br>



         </ItemTemplate>




   
        </asp:Repeater>  

        </asp:Panel>

                    <asp:Panel ID="NonePanel" runat="server" Visible="false">
                       <div class="noItemContainer">
                            <br />
                            <asp:Image ID="noItemLogo" runat="server" ImageUrl="~/Image/—Pngtree—info vector icon_3791375.png" class="noItemLogo" Width="200px" Height="200px" />            
                            <br /><br />
                            <h3><asp:Label ID="noItemText" runat="server" Text="There is currently no service request from client, please try to refresh later."></asp:Label></h3>
                            <br />
                        <asp:LinkButton  ID="noItemLinkText" class="noItemLinkText" runat="server" PostBackUrl="~/Servicer/RequestList.aspx">Refresh</asp:LinkButton>
                       </div> 

                    </asp:Panel>

        </div>

        

                        </ContentTemplate>
        </asp:UpdatePanel>


    <br />
    <br />               
<style>
    body{
        font-family: Arial, Helvetica, sans-serif;   
        background-color: #f2f2f2;      
    }

.images{    
    width: 150px;
    height: 110px;    
    background: #f3f3f3;
    padding: 1px;
    border: 2px solid #161616;
    margin-left: 20px;
    position: relative;
    vertical-align: top;    
    box-shadow: inset -5px -5px 30px 0 #656565;
    margin: 10px 15px 10px 15px;
}

.rowMargin{
    padding-left:5px;
    padding-top:15px;
    float:left;
}



.gridviewcss{
     background-color: white;
    margin-top:20px;
    margin-bottom:20px;
    padding-top:20px;
    padding-bottom:20px;

}


.auto-style2 {
    font-size: medium;
}

.auto-style3 {
    font-size: large;
}
.auto-style4 {
    color: #FF9900;
}


.rowStyle{
    height:140px;
    width:100%;
    background-color: white;
    box-shadow: 3px 3px 10px 0 #b3b3b3;
    border-radius: 5px;
}

.formBorder {
        max-width: 1000px;
        margin: auto;
        text-align: left;
        padding-bottom: 50px;
        margin-top: 50px;
        margin-bottom: 100px;
        margin-left:17%;
        margin-right:5%;
}


.topContainer{
    text-align: center;
    font-size: 25px;
    background-color: white;
    width: 100%;
    height: 220px;
}

.auto-style9 {       
    width: 30px;
    height: 30px;
}

.topicName{
    max-width: 1000px;
    margin: auto;
    font-size: 25px;
}

.noItemForm{
    background-color: white;
    margin: auto;
    box-shadow: 3px 3px 10px 0 #b3b3b3;
    border-radius: 5px;    
    width: 40%;
    display: block;
    margin-top: 30px;
    margin-bottom: 5%;
    margin-left: auto;
    margin-right: auto;
    height: 400px; 
}

    .noItemContainer {                                           
        text-align: center;
    }
    .noItemLogo {
      max-width: 70px;
      max-height: 70px;
    }
    .noItemLinkText{
      background: none;
      border: 3px solid;
      border-radius: 5px;
      color: black;
      font-weight: 500;  
      text-transform: uppercase;
      cursor: pointer;
      font-size: 16px; 
      padding: 10px 10px 10px 10px;
      box-shadow: 0 5px 5px 0 rgba(0,0,0,0.1), 0 5px 10px 0 rgba(0,0,0,0.1);
      text-decoration: none;
    }
    .noItemLinkText:hover{
        color: slategray;
        text-decoration: none;
    }

.nav {
  list-style-type: none;
  display: inline-block;
  text-align: center;
  margin: 0;      
}

.ul {
    display: inline-block;
    font-size: 20px;
    padding: 20px;
    color: #c48e0e;
}

.btnClass {
    float:right;
    margin-top:50px;
    margin-right:20px;
}

        /*---Button --*/
        .btnNew{  
          text-align:center;
          width:100px;
          background: none;
          border: 3px solid;
          border-radius: 10px;
          color: #ffffff;
          font-weight: 600;
          cursor: pointer;
          position: relative;
          box-shadow: 0 5px 5px 0 rgba(0,0,0,0.1), 0 5px 10px 0 rgba(0,0,0,0.1);
        }


        .btn-primary {
            background-color: #EF7E7E;
            border: 1px solid #EF7E7E;
        }

        .btn-primary:hover {
                background-color: #F68888;
                border: 1px solid #F68888;
        }

        .btn-secondary {
            background-color: #7B51F2;
            border: 1px solid #7B51F2;
        }

        .btn-secondary:hover {
                background-color: #8F6BF6;
                border: 1px solid #8F6BF6;
        }



</style>
</asp:Content>


