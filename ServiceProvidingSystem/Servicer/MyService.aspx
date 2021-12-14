
<%@ Page Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="MyService.aspx.cs" Inherits="ServiceProvidingSystem.Servicer.MyService" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server"> 
       

    <div class="topicName">
        <br />
        <br />
        <b><u>Service Posted</u></b>&nbsp&nbsp&nbsp</b>&nbsp&nbsp&nbsp<asp:Button class="btnNew btn-primary btn-lg" ID="btnEdit" runat="server" Text="New" OnClick="btnNew_Click" />
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
                <div class="rowImage"><asp:image height="130px" Width="130px" ToolTip = "Reference Picture" ID="ReferencePic" runat="server" ImageUrl ='<%# Eval("service_picture") %>' BorderStyle="Dotted" BorderColor="Black" BorderWidth="1px" ></asp:image></div>
                <div class="rowMargin">
                    <table class="tblStyle">
                        <tr>
                            <td style="width:160px; text-align:right; padding-right:20px;">
                                Offer No.:
                                
                            </td>
                 
                            <td style="width:230px;">
                                <asp:Label ID="lblOfferNo" runat="server" Font-Bold="true" ForeColor="Black" Text='<%# Eval("offer_id") %>'></asp:Label>
                            </td>
                            <td style="width:230px;">
                                Category & Type:
                            </td>

                    
                        </tr>
                        <tr>
                            <td style="width:160px; text-align:right; padding-right:20px;">
                                Title:
                            </td>
                 
                            <td style="width:230px;">
                                <asp:Label ID="lblTitle" runat="server" Font-Bold="true" ForeColor="Black" Text='<%# Eval("service_title") %>'></asp:Label>
                            </td>
                            <td style="width:230px;">
                                <asp:Label ID="lblCategory" runat="server" Font-Bold="true" ForeColor="Black" Text='<%# Eval("service_category") %>'></asp:Label>
                            </td>

                    
                        </tr>
                        <tr>
                            <td style="width:160px; text-align:right; padding-right:20px;">
                                Transport Charges:
                            </td>    
                            <td style="width:230px;">
                                <asp:Label ID="lblTransport" runat="server" Font-Bold="true" ForeColor="Black" Text='<%# Eval("delivery_fee") %>'></asp:Label>
                            </td>
                            <td style="width:230px;">
                                <asp:Label ID="lblType" runat="server" Font-Bold="true" ForeColor="Black" Text='<%# Eval("service_type") %>'></asp:Label>
                            </td>

                    
                        </tr>
                        <tr>
                            <td style="width:160px; text-align:right; padding-right:20px;">
                                Fees(RM):
                            </td>
                 
                            <td style="width:230px;">
                                <asp:Label ID="lblFees" runat="server" Font-Bold="true" ForeColor="Black" Text='<%# Eval("fees") %>'></asp:Label>
                            </td>
                            <td style="width:230px;">
                            </td>


             
                        </tr>
                        <tr>
                            <td style="width:160px; text-align:right; padding-right:20px;">
                                Area:
                            </td>
                 
                            <td style="width:230px;">
                                <asp:Label ID="lblArea" runat="server" Font-Bold="true" ForeColor="Black" Text='<%# Eval("district") %>'></asp:Label><%# (string)Eval("district") =="" ? "" : ", " %><asp:Label ID="lblState" runat="server" Font-Bold="true" ForeColor="Black" Text='<%# Eval("state") %>'></asp:Label>
                            </td>
                            <td style="width:230px;">
                            </td>

             
                        </tr>
                    </table>  
               </div>
                <div class="btnClass">
                    <asp:Button class="btnNew btn-primary btn-lg" ID="btnEdit" runat="server" CommandName="Confirm" Text="Edit" ValidationGroup="ChangePassword1" OnClick="btnEdit_Click" />
                    <br/>
                    <br/>
                    <asp:Button class="btnNew btn-secondary btn-lg" ID="btnDelete" runat="server" onClientClick="return fnConfirmDelete();" CausesValidation="False" CommandName="Cancel" Text="Delete" OnClick="btnDelete_Click" />
                    <script type="text/javascript">
                        function fnConfirmDelete() {
                            return confirm("Are you sure you want to delete this service?");
                        }
                    </script>
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
                            <h3><asp:Label ID="noItemText" runat="server" Text="There is currently no service offered, add new service to enable client viewing."></asp:Label></h3>
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


.rowStyle{
    height:160px;
    width:100%;
    background-color: white;
    box-shadow: 3px 3px 10px 0 #b3b3b3;
    border-radius: 5px;
}

.rowImage{
    padding-left:5px;
    padding-top:15px;
    float:left;
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
    margin-top:20px;
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



