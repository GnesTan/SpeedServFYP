<%@ Page Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="AccountMaintenance.aspx.cs" Inherits="ServiceProvidingSystem.BackendUser.AccountMaintenance" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server"> 
       

          <div class="topContainer"> 
       <!-- Welcome Column Section -->
         <div class="row welcome text-center">  
         <div class="col-12">  
            <div class="nav">                 
            <div class="ul"><asp:LinkButton class="btn" runat="server" PostBackUrl="~/AddCloth.aspx" CausesValidation="false">Add Cloth</asp:LinkButton></div>
            <div class="ul"><asp:LinkButton class="btn" runat="server" PostBackUrl="~/ViewCloth.aspx" CausesValidation="false">View Cloth</asp:LinkButton></div>
            <div class="ul"><asp:LinkButton class="btn" runat="server" PostBackUrl="~/AccountMaintenance.aspx" CausesValidation="false">Account Maintenance</asp:LinkButton></div>             
           </div>         
         </div>
         </div>  
    </div>

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
    padding-left:30px;
    padding-top:15px;
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

