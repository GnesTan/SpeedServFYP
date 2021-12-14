
<%@ Page Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="CurrentServingList.aspx.cs" Inherits="ServiceProvidingSystem.Servicer.CurrentServingList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server"> 
       

    <div class="topicName">
        <br />
        <br />
        <b><u>Current Serving</u></b>
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
                                <asp:Label ID="lblArea" runat="server" Font-Bold="true" ForeColor="Black" Text='<%# Eval("district") %>'></asp:Label><%# (string)Eval("district") =="" ? "" : ", " %><asp:Label ID="lblState" runat="server" Font-Bold="true" ForeColor="Black" Text='<%# Eval("state") %>'></asp:Label>
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
                                <asp:Label ID="lblStatus" runat="server" Font-Bold="true" ForeColor="Black" Text="Serving"></asp:Label>
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
                            <h3><asp:Label ID="noItemText" runat="server" Text="There is currently no service is in progressing."></asp:Label></h3>
                            <br />
                        <asp:LinkButton  ID="noItemLinkText" class="noItemLinkText" runat="server" PostBackUrl="~/Servicer/RequestList.aspx">Look for Service Request</asp:LinkButton>
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

        header{
        box-shadow: 0 .5rem 1rem rgba(0,0,0,.15);
        background: white;
    }

    .rowMargin{
        padding-left:5px;
        padding-top:15px;
        float:left;
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



.topicName{
    max-width: 1000px;
    margin: auto;
    margin-top: 110px;
    font-size: 25px;
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


