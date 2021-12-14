
<%@ Page Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="EditService.aspx.cs" Inherits="ServiceProvidingSystem.Servicer.EditService" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server"> 


    
  <asp:ScriptManager ID="ScriptManager1" runat="server">  
   </asp:ScriptManager>  

        <div class="editForm">
        <div class="topicName">
       <b><u>Edit Service</u></b>
        </div>
        <br/>
        <br/>
            <div class="uploadImage">
                    <asp:Label ID="lblReferPic" runat="server" Text="Reference Picture"></asp:Label>
                    <br/>
                    <div><asp:image height="140px" Width="140px" ToolTip = "Reference Picture" ID="ReferencePic" runat="server" ImageUrl ="~/Image/noImage.png" BorderStyle="Dotted" BorderColor="Black" BorderWidth="1px" ></asp:image></div>
                    <br/>
                    <asp:FileUpload ID="ImageUpload" runat="server"/>
            </div>

        <div class="tableClass">
        <table class="tbDetails">
            <tr>
                <td>
                    <asp:Label ID="lblFormServiceTitle" runat="server" Text="Service Title"></asp:Label>
                    <br />
                </td>
                <td>
                    <asp:TextBox ID="txtServiceTitle" runat="server" MaxLength="30"></asp:TextBox>
                    <asp:RequiredFieldValidator style="color:red" ID="ServiceTitleRequired" runat="server" ControlToValidate="txtServiceTitle" ErrorMessage="Service Title is required." ToolTip="Service Title is required." ValidationGroup="PostService">*</asp:RequiredFieldValidator>
                </td>

                <td>
                    <asp:Label ID="lblFormState" runat="server" Text="State"></asp:Label>
                </td>
                <td>
                  <asp:UpdatePanel ID="statepanel" runat="server">  
                          <ContentTemplate>
                              <asp:DropDownList ID="ddlstate" runat="server" AppendDataBoundItems="true" AutoPostBack="true" OnSelectedIndexChanged="ddlstate_SelectedIndexChanged" Width="200px" Height="30px"></asp:DropDownList>
                           </ContentTemplate>  
                     <Triggers>  
                      <asp:AsyncPostBackTrigger ControlID ="ddlstate" />  
                     </Triggers>  
                  </asp:UpdatePanel>  
                </td>
            </tr>

            <tr style="padding-bottom:20px;">
                <td>
                    Service Category
                </td>
                <td>
                     <asp:DropDownList ID="ddlCategory" runat="server" AppendDataBoundItems="true" AutoPostBack="true" OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged" Width="200px" Height="30px">
                        <asp:ListItem Value="Installation">Installation</asp:ListItem>

                        <asp:ListItem Value="Repairing">Repairing</asp:ListItem>

                        <asp:ListItem Value="Others">Others</asp:ListItem>

                     </asp:DropDownList>
                </td>
                <td>
                    City
                </td>
                <td>
                     <asp:UpdatePanel ID="districtpanel" runat="server">      
                        <ContentTemplate>   
                            <asp:DropDownList ID="ddldistrict" runat="server" AutoPostBack="true" AppendDataBoundItems="true" Width="200px" Height="30px">
                                  <asp:ListItem Value="--Please Select District--">--Please Select District--</asp:ListItem>

                            </asp:DropDownList>
                        </ContentTemplate>  
                        <Triggers>  
                             <asp:AsyncPostBackTrigger ControlID ="ddldistrict"/>  
                        </Triggers>  
                     </asp:UpdatePanel>  
                   
                </td>
            </tr>
            
            

            <tr>
                <td>
                    <asp:Label ID="lblServiceType" runat="server" Text="Service Type"></asp:Label>
                    <br />
                </td>
                <td>
                    <asp:DropDownList ID="ddlInstallType" runat="server" Width="200px" Height="30px">
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

                    <asp:DropDownList ID="ddlRepairType" runat="server" Width="200px" Height="30px" Visible="false">
                        <asp:ListItem Value="Vehicles">Vehicles</asp:ListItem>

                        <asp:ListItem Value="Home appliances">Home appliances</asp:ListItem>

                        <asp:ListItem Value="Mobile & Gadget">Mobile & Gadget</asp:ListItem>

                        <asp:ListItem Value="Computer & Accessories">Computer & Accessories</asp:ListItem>

                        <asp:ListItem Value="Musical instrument">Musical instrument</asp:ListItem>

                        <asp:ListItem Value="Industrial Machine">Industrial Machine</asp:ListItem>

                        <asp:ListItem Value="Clothing">Clothing</asp:ListItem>

                        <asp:ListItem Value="Shoes">Shoes</asp:ListItem>

                        <asp:ListItem Value="Watches">Watches</asp:ListItem>

                        <asp:ListItem Value="Game Console">Game Console</asp:ListItem>

                        <asp:ListItem Value="Camera & Drones">Camera & Drones</asp:ListItem>

                        <asp:ListItem Value="Network Infrastructure">Network Infrastructure</asp:ListItem>

                        <asp:ListItem Value="Others">Others</asp:ListItem>

                    </asp:DropDownList>

                    <asp:DropDownList ID="ddlOtherType" runat="server" Width="200px" Height="30px" Visible="false">
                        <asp:ListItem Value="Insecticide">Insecticide</asp:ListItem>

                        <asp:ListItem Value="Porter services">Porter services</asp:ListItem>

                        <asp:ListItem Value="Data entry">Data entry</asp:ListItem>

                        <asp:ListItem Value="Distribute flyers">Distribute flyers</asp:ListItem>

                        <asp:ListItem Value="Transportation">Transportation</asp:ListItem>

                        <asp:ListItem Value="Others">Others</asp:ListItem>

                        <asp:ListItem Value="Clothing">Clothing</asp:ListItem>

                        <asp:ListItem Value="Shoes">Shoes</asp:ListItem>

                        <asp:ListItem Value="Watches">Watches</asp:ListItem>

                        <asp:ListItem Value="Game Console">Game Console</asp:ListItem>

                        <asp:ListItem Value="Camera & Drones">Camera & Drones</asp:ListItem>

                        <asp:ListItem Value="Network Infrastructure">Network Infrastructure</asp:ListItem>

                    </asp:DropDownList>

                </td>

                <td>
                    <asp:Label ID="lblTransport" runat="server" Text="Transport Charges"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTransport" runat="server" MaxLength="50" ></asp:TextBox>
                    <asp:RequiredFieldValidator style="color:red" ID="TransportRequired" runat="server" ControlToValidate="txtTransport" ErrorMessage="Transport Charges is required." ToolTip="Transport Charges is required." ValidationGroup="PostService">*</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr style="padding-bottom:30px;">
                <td colspan="2">
                </td>
                <td>
                    <asp:Label ID="lblFees" runat="server" Text="Fees(RM)"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtFees" runat="server" MaxLength="50" ></asp:TextBox>
                    <asp:RequiredFieldValidator style="color:red" ID="RequiredFees" runat="server" ControlToValidate="txtFees" ErrorMessage="Fees is required." ToolTip="Fees is required." ValidationGroup="PostService">*</asp:RequiredFieldValidator> <br/>
                    <asp:RegularExpressionValidator style="color:red" runat="server" ID="PricePattern" ControlToValidate = "txtFees" ValidationExpression = "^[1-9]\d*(\.\d+)?$" ErrorMessage="Invalid Fees." ValidationGroup="PostService"></asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblRemarks" runat="server" Text="Remarks"></asp:Label>
                </td>
                <td colspan="3">
                </td>
            </tr>
            <tr>
                <td colspan="4" style="height:200px;">
                    <asp:TextBox runat="server" ID="txtRemarks" Width="990px" TextMode="MultiLine" Rows="10" />
                </td>
            </tr>

            </table>
            </div>

            <div class="errorMsg">
                 <asp:Label ID="lblError" runat="server" Text="" ForeColor="Red"></asp:Label>
            </div>

            <div class="btnClass">

            <asp:Button ID="btnSave" runat="server" class="btnNew btn-primary btn-lg" Text="Save" ValidationGroup="PostService" OnClick="btnSave_Click" />
            &nbsp;&nbsp;&nbsp;
            <asp:Button ID="btnCancel" runat="server" class="btnNew btn-secondary btn-lg" Text="Cancel" CausesValidation="false" OnClick="btnCancel_Click" />

            </div>

            <br />


        </div>




 

<style type="text/css">

body{    
    font-family: Arial, Helvetica, sans-serif;   
    background-color: #f2f2f2;      
}

    header{
        box-shadow: 0 .5rem 1rem rgba(0,0,0,.15);
        background: white;
    }

.topicName{
    margin: auto;
    font-size: 25px;
}

.tbDetails{
    border-collapse:separate;
    border-spacing:0 20px;
}


.tbDetails td{
    height:20px;
    width:300px;
}



.btnClass {
    margin-top:20px;
    text-align:center;
}

.tableClass{
    margin-left:5%;
    margin-right:auto;
}

.errorMsg{
    margin-left:5%;
    margin-right:auto;
}

.editForm {
    background-color: white;
    margin: auto;
    box-shadow: 3px 3px 10px 0 #b3b3b3;
    border-radius: 5px;    
    width: 80%;
    display: block;
    margin-top: 140px;
    margin-bottom: 5%;
    margin-left: auto;
    margin-right: auto;
    padding: 3%;
    height: 1050px;  

}

.uploadImage{
    margin-left:5%;
    margin-right:auto;
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

input[type=text]:focus, input[type=password]:focus, input[type=date]:focus{
  outline: none;
  box-shadow: 0 0 0.5pt 0.5pt #900e8c;
  opacity:1;
}



</style>
</asp:Content>

