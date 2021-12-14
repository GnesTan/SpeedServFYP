<%@ Page Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="AccountMaintenance.aspx.cs" Inherits="ServiceProvidingSystem.BackendUser.AccountMaintenance" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server"> 
       

    <div class="topicName">
        <br />
        <br />
        <b><u>Account Maintenance</u></b>&nbsp&nbsp&nbsp<a href ="CreateAccount.aspx"  class="btnNew btn-primary btn-lg" > Add</a>
    </div>


    <div class="formBorder">
        <asp:Repeater  ID="Repeater1" runat="server" >   
                      
                          
        <ItemTemplate>
            <div class="rowStyle"> 
                <div class="rowMargin">
                    <table class="tblStyle">
                        <tr>
                            <td style="width:370px;">
                                <span>Username:</span>&nbsp
                                <asp:Label ID="lblUsername" runat="server" Font-Bold="true" ForeColor="Black" Text='<%# Eval("USERNAME") %>'></asp:Label>
                            </td>
                 
                            <td style="width:370px;">
                                <span>Name:</span>&nbsp
                                <asp:Label ID="lblFullname" runat="server" Font-Bold="true" ForeColor="Black" Text='<%# Eval("FULL_NAME") %>'></asp:Label>
                            </td>
                            <td style="width:130px;">
                                <asp:Button class="btnNew btn-primary btn-lg" ID="btnEdit" runat="server" CommandName="Confirm" Text="Edit" ValidationGroup="ChangePassword1" OnClick="btnEdit_Click" />
                            </td>
                            <td style="width:130px;">
                                <asp:Button class="btnNew btn-secondary btn-lg" ID="btnDelete" runat="server" onClientClick="return fnConfirmDelete();" CausesValidation="False" CommandName="Cancel" Text="Delete" OnClick="btnDelete_Click" />
                                <script type="text/javascript">
                                    function fnConfirmDelete() {
                                      return confirm("Are you sure you want to delete this account?");
                                    }
                                </script>
                            </td>
                    
                        </tr>
                    </table>  
               </div>
            </div>
            <br>



         </ItemTemplate>




   
        </asp:Repeater>  
        </div>


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
    padding-left:30px;
    padding-top:15px;
}







.rowStyle{
    height:80px;
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
    font-size: 25px;
    margin-top:110px;
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

