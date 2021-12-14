<%@ Page Language="C#"  MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="ViewServicerReviews.aspx.cs" Inherits="ServiceProvidingSystem.Client.ViewServicerReviews" %>

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
    <link href="../ViewServicerReviews.css" rel="stylesheet" />
       
    <asp:ScriptManager ID="ScriptManager1" runat="server">  
    </asp:ScriptManager> 
    <%-- Favourite--%>
    <section id="#" class="viewServRev d-flex align-items-center">
    <div class="container">
        <div class="disServForm" data-aos="zoom-out" data-aos-delay="100">
        <div class="row mb-3 px-2">
            <div class="col-7">
                <h1 data-aos="fade-right"><span style="">C</span>omment About Servicer</h1>
            </div>
            <div class="col-5">

                        <div class="search_wrap search_wrap_6">
			                <div class="search_box">
				                <asp:TextBox type="search" ID="tbSearch" class="form-control" runat="server" AutoPostBack="True" OnTextChanged="tbSearch_TextChanged" placeholder="Search"></asp:TextBox>
			                </div>
		                </div>                    

            </div>
            
        </div>
        <div class="servicegv">        

            <asp:UpdatePanel ID="UpdatePanel2" runat="server">  
            <ContentTemplate>                                        
                            <div class="row px-5 py-3">
                                    <div class="reqStatusMargin col-sm px-5 py-3">      
                                        <asp:Panel ID="RepeaterPanel" runat="server">
                                        <asp:Repeater  ID="reviewpanel" runat="server" >                                                   
                                        <ItemTemplate>
                                            <div class="rowStyle"> 
                                                <div class="row">
                                                    <div class="col-md-2">
                                                         <div class="row">
                                                             <div class="col">
                                                                  <asp:Image ID="imgProfile" runat="server" Height="70px" Width="70px" style="border-radius:100px; box-shadow: 0 0 0 3px #b3b3b3; text-decoration:none;" ImageUrl='<%# (string)Eval("IS_ANONYMOUS") =="Y" ? "~/Image/generaluser.png" : Eval("profile_picture") %>'/>
                                                             </div>                                       
                                                        </div>
                                                    </div>
                                                    <div class="col-md-10">
                                                        <div class="row">
                                                            <asp:Label ID="lblDateTime" runat="server" ForeColor="gray" Text='<%# Eval("date_and_time") %>'></asp:Label>
                                                        </div>
                                                        <div class="row">
                                                            <asp:Label ID="lblName" runat="server" Font-Bold="true" ForeColor="Black" Text='<%# (string)Eval("IS_ANONYMOUS") =="Y" ? "Anonymous" : Eval("full_name") %>'></asp:Label>
                                                        </div>
                                                        <div class="row">
                                                            <asp:Label ID="lblComment" runat="server" ForeColor="Black" Text='<%# Eval("comment") %>'></asp:Label>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <br />
                                            <hr />
                                            <br>
                                         </ItemTemplate>   
                                        </asp:Repeater>  
                                        </asp:Panel>
                                    </div>   
                            </div>                                
                    <div class="noItemContainer">
                                <br />
                            <asp:Image ID="noItemLogo" runat="server" ImageUrl="~/Image/—Pngtree—info vector icon_3791375.png" class="noItemLogo" />            
                                <br /><br />
                            <h3><asp:Label ID="noItemText" runat="server" Text="Currently servicer does not receive any review" style="font-size: 30px;"></asp:Label></h3>
                                <br />
                            <asp:LinkButton ID="noItemLinkText" class="noservicelink p-2" runat="server" PostBackUrl="DisplayServiceDetails.aspx">Back</asp:LinkButton>
                        </div>  
                    </ContentTemplate>
                    <Triggers>  
                    <asp:AsyncPostBackTrigger ControlID ="reviewpanel" />  
                    </Triggers>  
            </asp:UpdatePanel>
        </div>
            </div>
<%--End Favourite--%>
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
