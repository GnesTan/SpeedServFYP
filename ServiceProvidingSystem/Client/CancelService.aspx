<%@ Page Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="CancelService.aspx.cs" Inherits="ServiceProvidingSystem.Client.CancelService" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server"> 
    <%--Plugins--%>
    <link rel="stylesheet" href="https://unpkg.com/aos@next/dist/aos.css" />
    <link rel="preconnect" href="https://fonts.googleapis.com">
    <link rel="preconnect" href="https://fonts.gstatic.com" crossorigin>
    <link href="https://fonts.googleapis.com/css2?family=Lato&family=PT+Sans&family=Roboto:wght@300&display=swap" rel="stylesheet"> 
    <script src="https://code.jquery.com/jquery-3.3.1.slim.min.js" integrity="sha384-q8i/X+965DzO0rT7abK41JStQIAqVgRVzpbzo5smXKp4YfRvH+8abtTE1Pi6jizo" crossorigin="anonymous"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.14.3/umd/popper.min.js" integrity="sha384-ZMP7rVo3mIykV+2+9J3UJ46jBk0WLaUAdn689aCwoqbBJiSnjAK/l8WvCWPIPm49" crossorigin="anonymous"></script>
    <script src="https://stackpath.bootstrapcdn.com/bootstrap/4.1.3/js/bootstrap.min.js" integrity="sha384-ChfqqxuZUCnJSK3+MXmPNIyE6ZbWh2IMqE241rYiqJxyMiZ6OW/JmZQ5stwEULTy" crossorigin="anonymous"></script> 

    <%--End Plugins--%>

    <%--CSS File Plugins--%>
    <link href="../General.css" rel="stylesheet" />   
    <link href="../ClientHomePage.css" rel="stylesheet" />            
    <link href="../CancelService.css" rel="stylesheet" />

    <asp:ScriptManager ID="ScriptManager1" runat="server">  
    </asp:ScriptManager> 

<%--Cancel Service--%>   
    <section id="#" class="cancelServ d-flex align-items-center">
        <div class="container">
            <div data-aos="zoom-out" data-aos-delay="100">  
                <div class="cancelserv shadow p-5 mb-5 bg-white rounded" data-aos="zoom-out" data-aos-delay="50" >       
                     <h3 data-aos="fade-right"><span style="">C</span>ancel Service</h3>
                    <div class="row" data-aos="fade-right" data-aos-delay="200">                                       
                        <div class=" col justify-content-center" data-aos="zoom-out" data-aos-delay="300">
                            <div class="row mb-4 mt-3">
                                <div class="col-md-12">
                                    <h5 for="name" class="form-label">Cancel Reason</h5>
				                    <asp:UpdatePanel ID="cancelpanel" runat="server">  
                                        <ContentTemplate>
                                            <asp:DropDownList class="form-select" ID="ddlReason" runat="server" AppendDataBoundItems="true" OnSelectedIndexChanged="ddlReason_SelectedIndexChanged" AutoPostBack="True">
                                                <asp:ListItem Value="waitlong">I had waited too long</asp:ListItem>
                                                <asp:ListItem Value="keyinwrong">I had key in incorrect request details</asp:ListItem>
                                                <asp:ListItem Value="busy">I am currently busy</asp:ListItem>                                                
                                                <asp:ListItem Value="others">Others</asp:ListItem>                        
                                            </asp:DropDownList>
                                        </ContentTemplate>
                                      <Triggers>  
                                      <asp:AsyncPostBackTrigger ControlID ="ddlReason" />  
                                      </Triggers>  
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                            <div class="row mb-3">
                                <div class="col-md-12">
                                <asp:UpdatePanel ID="otherreasonpanel" runat="server">  
                                    <ContentTemplate>
				                        <asp:TextBox type="text" ID="txtOtherReason" class="form-control" runat="server" placeholder="Type Your Reason Here" Visible="False" TextMode="MultiLine" Rows="5" AutoPostBack="True"></asp:TextBox>
                                         <asp:RequiredFieldValidator style="color:red" ID="ReasonRequired" runat="server" ControlToValidate="txtOtherReason" ErrorMessage="Reason is required." ToolTip="Reason is required." ValidationGroup="CancelService">Reason is required.</asp:RequiredFieldValidator>
                                    </ContentTemplate>
                                      <Triggers>  
                                      <asp:AsyncPostBackTrigger ControlID ="txtOtherReason" />  
                                      </Triggers>  
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                        </div>                                 
                    </div>
                     <hr class="topline"/>
                    <hr />
                    <div class="row g-3">     
                        <div class="text-end text-lg-flex justify-content-center">  
                            <asp:LinkButton ID="btnBack" class="btn-find-now btnDismiss d-inline-flex align-items-center justify-content-center align-self-center" runat="server" OnClick="btnBack_Click">  
                            <span>Back</span>                                
                            </asp:LinkButton>     

                            <asp:LinkButton ID="btnCancelService" class="btn-find-now btnCancel d-inline-flex align-items-center justify-content-center align-self-center" runat="server" OnClick="btnCancelService_Click" ValidationGroup="CancelService">  
                            <span>Confirm Cancel</span>                                
                            </asp:LinkButton>                                         
                        </div>
                        </div>        
                   </div>                
             </div>
            
        




<%--End Cancel Service--%>
           </div>
    </section>
     
    
    <%-- AOS script plugin--%>
<script src="https://unpkg.com/aos@next/dist/aos.js"></script>
<script>
    /* AOS plugin script*/
    AOS.init();
</script>
</asp:Content>

