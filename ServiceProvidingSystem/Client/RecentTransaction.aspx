<%@ Page Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="RecentTransaction.aspx.cs" Inherits="ServiceProvidingSystem.Client.RecentTransaction" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server"> 
    <%--Plugins--%>
    <link rel="stylesheet" href="https://unpkg.com/aos@next/dist/aos.css" />
    <link rel="preconnect" href="https://fonts.googleapis.com">
    <link rel="preconnect" href="https://fonts.gstatic.com" crossorigin>
    <link href="https://fonts.googleapis.com/css2?family=Lato&family=PT+Sans&family=Roboto:wght@300&display=swap" rel="stylesheet"> 
    <script src="https://code.jquery.com/jquery-3.3.1.slim.min.js" integrity="sha384-q8i/X+965DzO0rT7abK41JStQIAqVgRVzpbzo5smXKp4YfRvH+8abtTE1Pi6jizo" crossorigin="anonymous"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.14.3/umd/popper.min.js" integrity="sha384-ZMP7rVo3mIykV+2+9J3UJ46jBk0WLaUAdn689aCwoqbBJiSnjAK/l8WvCWPIPm49" crossorigin="anonymous"></script>
    <script src="https://stackpath.bootstrapcdn.com/bootstrap/4.1.3/js/bootstrap.min.js" integrity="sha384-ChfqqxuZUCnJSK3+MXmPNIyE6ZbWh2IMqE241rYiqJxyMiZ6OW/JmZQ5stwEULTy" crossorigin="anonymous"></script>  
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.0.2/dist/js/bootstrap.bundle.min.js" integrity="sha384-MrcW6ZMFYlzcLA8Nl+NtUVF0sA7MsXsP1UyJoMp4YLEuNSfAP+JcXn/tWtIaxVXM" crossorigin="anonymous"></script>
    <%--End Plugins--%>

    <%--CSS File Plugins--%>
    <link href="../General.css" rel="stylesheet" />   
    <link href="../ClientHomePage.css" rel="stylesheet" />      
    <link href="../RecentTransaction.css" rel="stylesheet" />
       
    <asp:ScriptManager ID="ScriptManager1" runat="server">  
    </asp:ScriptManager> 

    <%-- Request Recent Transaction--%>
    <section id="#" class="recentTransact d-flex align-items-center">
    <div class="container">
        <div class="row mb-3 px-2">
            <h1 data-aos="fade-right"><span style="">R</span>ecent Transaction</h1>
        </div>
    <div data-aos="zoom-out" data-aos-delay="100">  
        <div class="container shadow p-4 mb-5 bg-white rounded" data-aos="zoom-out" data-aos-delay="50" >                    
            <%--Recent Transaction--%>
                <div class="reqStatusMargin col-sm px-5 py-3">      
                    <div class="row">
                        <div class="col-md-12">
                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">  
                                <ContentTemplate>                                        
                                    <div class="row px-2 py-2">
                                            <div class="reqStatusMargin col-sm px-2 py-1">      
                                                <asp:Panel ID="RepeaterPanel" runat="server">
                                                <asp:Repeater ID="RecentTransactPanel" runat="server">                                                   
                                                <ItemTemplate>
                                                    <div class="container shadow p-4 mb-5 bg-white rounded"> 
                                                        <div class="row">
                                                           <div class="col-md-7 py-3">
                                                                <div class="row py-3">
                                                                    <asp:Label ID="lblServiceTypeCat" class="reqDetailsLabel serviceCatType align-middle" runat="server" Text='<%# Eval("date_and_time") %>'></asp:Label>
                                                                    <br />                                                            
                                                                    <asp:Label ID="lblServiceTitle" runat="server" class="reqDetailsLabel serviceName align-middle" Text='<%# Eval("title") %>'></asp:Label>
                                                                </div>
                                                                <div class="row">
                                                                    <div class="col md-6 mx-1">
                                                                        <h4 for="name" class="form-label align-middle">Paid Amount</h4>
                                                                        <asp:Label ID="lblPrice" runat="server" class="reqDetailsLabel status align-middle" Text='<%# "RM " + Eval("payment_amount") %>'></asp:Label>                                                                        
                                                                       <asp:HiddenField ID="HiddenField1" runat="server" Value='<%# Eval("request_no") %>'></asp:HiddenField>       
                                                                        <asp:HiddenField ID="HiddenField2" runat="server" Value='<%# Eval("payment_id") %>'></asp:HiddenField>  
                                                                    </div> 
                                                                    <div class="col md-5">
                                                                        <h4 for="name" class="form-label align-middle">Review Status</h4>
                                                                        <asp:Label ID="Label1" runat="server" class="reqDetailsLabel status align-middle" Text='<%# (string)Eval("doneReview") == "Y" ? "Review Completed" : "Pending Review" %>'></asp:Label>                                                                                                                                                                                                                                                                                             
                                                                    </div>  
                                                                </div>                                                                                                            
                                                            </div>
                                                     <div class="col-md-4 px-3 py-3">       
                                                        <div class="row">
                                                            <div class="col-md-12 mb-3">                                                                
                                                                <div class="text-end text-lg-flex justify-content-center">  
                                                                    <asp:LinkButton ID="lbReview" class="btn-find-now btnReview d-inline-flex align-items-center justify-content-center align-self-center" runat="server" OnClick="btnReview_Click" Visible='<%# (string)Eval("doneReview") == "Y" ? false : true %>'>  
                                                                    <span>Review Servicer</span>                                
                                                                    </asp:LinkButton> 
                                                                </div>    
                                                            </div> 
                                                            <div class="col-md-12">
                                                                <div class="text-end text-lg-flex justify-content-center">  
                                                                    <asp:LinkButton ID="lbViewPay" class="btn-find-now btnViewMore d-inline-flex align-items-center justify-content-center align-self-center" runat="server" OnClick="btnViewPayment_Click">  
                                                                    <span>View Transaction</span>                                
                                                                    </asp:LinkButton> 
                                                                </div>                      
                                                            </div>
                                                        </div>                                                                                                        
                                                    </div>
                                                        </div>
                                                    </div>
                                                 </ItemTemplate>   
                                                </asp:Repeater>  
                                                </asp:Panel>
                                            </div>   
                                                    <div style="text-align:center;">
                                                    <asp:Repeater ID="rptPaging" runat="server" OnItemCommand="rptPaging_ItemCommand">
                                                        <ItemTemplate>
                                                        <asp:LinkButton ID="btnPage"
                                                        style="padding: 8px; margin: 7px; border: solid 2px #FE5959; color: black; font-weight: bold; border-radius: 25px;"
                                                        CommandName="Page" CommandArgument="<%# Container.DataItem %>"
                                                        runat="server" ForeColor="White" Font-Bold="True">
                                                        <%# Container.DataItem %>
                                                        </asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:Repeater>

                                                    </div>
                                    </div>                                 
                                    <div class="noItemContainer">
                                                <br />
                                            <asp:Image ID="noItemLogo" runat="server" ImageUrl="~/Image/—Pngtree—info vector icon_3791375.png" class="noItemLogo" />            
                                                <br /><br />
                                            <h3><asp:Label ID="noItemText" runat="server" Text="Currently no any transaction" style="font-size: 30px;"></asp:Label></h3>
                                                <br />
                                            <asp:LinkButton ID="noItemLinkText" class="noservicelink p-2" runat="server" PostBackUrl="ClientHomePage.aspx">Back to Main</asp:LinkButton>
                                        </div>  
                    </ContentTemplate>
                    <Triggers>  
                    <asp:AsyncPostBackTrigger ControlID ="RecentTransactPanel" />  
                    </Triggers>  
            </asp:UpdatePanel>
                        </div>

                    </div>
                </div>       
            </div>
        </div>

    <%--End Recent Transaction--%>
    </div>
    </section>  
    <%-- AOS script plugin--%>
<script src="https://unpkg.com/aos@next/dist/aos.js"></script>
<script>
    /* AOS plugin script*/
    AOS.init();
    /*Display tooltip*/
    var tooltipTriggerList = [].slice.call(document.querySelectorAll('[data-bs-toggle="tooltip"]'))
    var tooltipList = tooltipTriggerList.map(function (tooltipTriggerEl) {
        return new bootstrap.Tooltip(tooltipTriggerEl)
    })
</script>
</asp:Content>
