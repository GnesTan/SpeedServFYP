﻿<%@ Page Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="RequestList.aspx.cs" Inherits="ServiceProvidingSystem.Servicer.RequestList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server"> 
       

    <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>

    <div class="topItem">

        <div class="topicName">
           <asp:LinkButton ID="lbDirectRequest" runat="server" class="btnNew btn-direct btn-lg" OnClick="lbDirectRequest_Click"> Direct Request(0)</asp:LinkButton> &nbsp&nbsp&nbsp
            <br />
            <br />
            <br />
            <b><u>Service Request</u></b>&nbsp&nbsp&nbsp<asp:LinkButton ID="lbRefresh" runat="server" class="btnNew btn-primary btn-lg" OnClick="lbRefresh_Click"> Refresh</asp:LinkButton> &nbsp&nbsp&nbsp
        </div>

        <div class="dropdownlist">
            <div class="filterTopic">
            <b><u>Area filter</u></b>
            </div>
            <div class="dropdown1">
                      <asp:UpdatePanel ID="statepanel" runat="server">  
                              <ContentTemplate>
                                  <asp:DropDownList ID="ddlstate" runat="server" AppendDataBoundItems="true" AutoPostBack="true" OnSelectedIndexChanged="ddlstate_SelectedIndexChanged" Width="200px" Height="30px"></asp:DropDownList>
                               </ContentTemplate>  
                         <Triggers>  
                          <asp:AsyncPostBackTrigger ControlID ="ddlstate" />  
                         </Triggers>  
                      </asp:UpdatePanel>
            </div>
            <div class="dropdown2">
        
                      <asp:UpdatePanel ID="districtpanel" runat="server">      
                            <ContentTemplate>   
                                <asp:DropDownList ID="ddldistrict" runat="server" AutoPostBack="true" AppendDataBoundItems="true" OnSelectedIndexChanged="ddldistrict_SelectedIndexChanged" Width="200px" Height="30px">
                                    <asp:ListItem Value="--All District--">--All District--</asp:ListItem>
                                </asp:DropDownList>
                            </ContentTemplate>  
                            <Triggers>  
                                 <asp:AsyncPostBackTrigger ControlID ="ddldistrict"/>  
                            </Triggers>  
                       </asp:UpdatePanel> 

                        &nbsp&nbsp&nbsp

            </div>

            <div class="filterTopic2">
            <b><u>Service filter</u></b>
            </div>

            <div class="dropdown1">
                     <asp:DropDownList ID="ddlCategory" runat="server" AppendDataBoundItems="true" AutoPostBack="true" OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged" Width="200px" Height="30px">
                        <asp:ListItem Value="Installation">Installation</asp:ListItem>

                        <asp:ListItem Value="Repairing">Repairing</asp:ListItem>

                        <asp:ListItem Value="Others">Others</asp:ListItem>

                     </asp:DropDownList>
            </div>

            <div class="dropdown2">
        
                   <asp:DropDownList ID="ddlType" runat="server"  AppendDataBoundItems="true" AutoPostBack="true" OnSelectedIndexChanged="ddlType_SelectedIndexChanged" Width="200px" Height="30px">
                        <asp:ListItem Value="--Any Type--">--Any Type--</asp:ListItem>

                        <asp:ListItem Value="Vehicles">Vehicles</asp:ListItem>

                        <asp:ListItem Value="Home appliances">Home appliances</asp:ListItem>

                        <asp:ListItem Value="Mobile & Gadget">Mobile & Gadget</asp:ListItem>

                        <asp:ListItem Value="Computer & Accessories">Computer & Accessories</asp:ListItem>

                        <asp:ListItem Value="Musical instrument">Musical instrument</asp:ListItem>

                        <asp:ListItem Value="Industrial Machine">Industrial Machine</asp:ListItem>

                        <asp:ListItem Value="Watches">Watches</asp:ListItem>

                        <asp:ListItem Value="Gaming PC">Gaming PC</asp:ListItem>

                        <asp:ListItem Value="Camera & Drones">Camera & Drones</asp:ListItem>

                        <asp:ListItem Value="Network Infrastructure">Network Infrastructure</asp:ListItem>

                        <asp:ListItem Value="Others">Others</asp:ListItem>

                    </asp:DropDownList>
            </div>

            <div class="btnRight">
                <asp:Button ID="btndefault" runat="server" Text="Save As Default" OnClick="btndefault_Click"/>
            </div>

         </div>
    </div>

    

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


        <div style="overflow: hidden; text-align:center;">

        <asp:Repeater ID="rptPaging" runat="server" OnItemCommand="rptPaging_ItemCommand">
         <ItemTemplate>
          <asp:LinkButton ID="btnPage"
           style="padding: 8px; margin: 2px; background: lightgray; border: solid 1px #666; color: black; font-weight: bold"
           CommandName="Page" CommandArgument="<%# Container.DataItem %>"
           runat="server" ForeColor="White" Font-Bold="True">
            <%# Container.DataItem %>
          </asp:LinkButton>
         </ItemTemplate>
        </asp:Repeater>

        </div>

        </asp:Panel>

                    <asp:Panel ID="NonePanel" runat="server" Visible="false">
                       <div class="noItemContainer">
                            <br />
                            <asp:Image ID="noItemLogo" runat="server" ImageUrl="~/Image/—Pngtree—info vector icon_3791375.png" class="noItemLogo" Width="200px" Height="200px" />            
                            <br /><br />
                            <h3><asp:Label ID="noItemText" runat="server" Text="There is currently no service request from client, please try to refresh later."></asp:Label></h3>
                            <br />
                        <asp:LinkButton  ID="noItemLinkText" class="noItemLinkText" runat="server" OnClick="lbRefresh_Click">Refresh</asp:LinkButton>
                       </div> 

                    </asp:Panel>

                    <asp:Panel ID="ExpiredPanel" runat="server" Visible="false">
                       <div class="noItemContainer">
                            <br />
                            <asp:Image ID="Image1" runat="server" ImageUrl="~/Image/—Pngtree—info vector icon_3791375.png" class="noItemLogo" Width="200px" Height="200px" />            
                            <br /><br />
                            <h3><asp:Label ID="Label1" runat="server" Text="Your subscription validity currently is not active, please subscription to view client request."></asp:Label></h3>
                            <br />
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




.dropdownlist{
    min-height:100px;
    width:610px;
    background-color:lightgray;
    float:right;
    margin-top:50px;
}

.dropdown1{
    margin:auto;
    margin-top:10px;
    margin-right:20px;
    margin-left:20px;
    float:left;

}

.dropdown2{
    margin:auto;
    margin-top:10px;
    margin-right:20px;
    float:left;
}


.btnRight{
    margin-top:50px;
    margin-bottom:20px;
    float:left;

}

.topItem{
    max-width: 1000px;
    margin: auto;  
    margin-top: 110px;
    min-height:120px;
    width:100%;
}

.filterTopic{
    width:100%;
    float:left;
    margin-left:20px;
    margin-top:15px;
}

.filterTopic2{
    width:100%;
    float:left;
    margin-left:20px;
    margin-top:5px;
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
        margin-top: 200px;
        margin-bottom: 100px;
        margin-left:17%;
        margin-right:5%;
        height:auto;
}


.topicName{
    margin-top:100px;
    float:left;
    font-size: 25px;
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

.btn-direct {
    background-color: #948540;
    border: 1px solid #948540;
}

.btn-direct:hover {
        background-color: #9E9153;
        border: 1px solid #9E9153;
}





</style>
</asp:Content>


