<%@ Page Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="DisplayService.aspx.cs" Inherits="ServiceProvidingSystem.Client.DisplayService" %>

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
    <link href="../DisplayService.css" rel="stylesheet" />
       
    <asp:ScriptManager ID="ScriptManager1" runat="server">  
    </asp:ScriptManager> 

    <section id="#" class="displayserv d-flex align-items-center">
    <div class="container">
            <div class="disServForm" data-aos="zoom-out" data-aos-delay="100">
       
        <div>
            <h1 data-aos="fade-right"><span style="">S</span>ervices</h1>
        </div>
        

        <%--Sorting--%>
            <div class="servicesddl col-sm" data-aos="zoom-out" data-aos-delay="300">        
            <div class="row">
                <div class="col-md-6">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">  
                        <ContentTemplate>
                            <div class="search_wrap search_wrap_6">
			                    <div class="search_box">
				                        <asp:TextBox type="search" ID="tbSearch" class="form-control" runat="server" AutoPostBack="True" OnTextChanged="tbSearch_TextChanged" placeholder="Search"></asp:TextBox>
<%--				                    <div class="btn btn-primary">
					                    <p>Search</p>
				                    </div>--%>
			                    </div>
		                    </div>                    
                        </ContentTemplate>
                        <Triggers>  
                        <asp:AsyncPostBackTrigger ControlID ="tbSearch" />  
                        </Triggers>  
                    </asp:UpdatePanel> 
                </div>
                <div class="col-md-2">

                </div>
                <div class="col-md-4">
                    <asp:UpdatePanel ID="sortservpanel" runat="server">  
                        <ContentTemplate>
                            <asp:DropDownList class="form-select" ID="ddlSort" runat="server" AppendDataBoundItems="true" AutoPostBack="True" OnSelectedIndexChanged="ddlSort_SelectedIndexChanged">
                               <asp:ListItem Value="Default">Default</asp:ListItem>                             
                                <asp:ListItem Value="LowToHighPrice">Price - Low to High</asp:ListItem>
                                <asp:ListItem Value="HighToLowPrice">Price - High to Low</asp:ListItem>                                            
                            </asp:DropDownList>
                        </ContentTemplate>
                        <Triggers>  
                        <asp:AsyncPostBackTrigger ControlID ="ddlSort" />  
                        </Triggers>  
                    </asp:UpdatePanel> 
                </div>

                </div>  
            </div>
           
        <%--End Sorting--%>

<%--DisplayService--%>
        <div class="servicegv">
            <asp:UpdatePanel ID="UpdatePanel2" runat="server">  
            <ContentTemplate>
                                        
                            <div class="row px-2 py-2">
                                    <div class="reqStatusMargin col-sm px-2 py-1">      
                                        <asp:Panel ID="RepeaterPanel" runat="server">
                                        <asp:Repeater ID="displayServRepeater" runat="server" >                                                   
                                        <ItemTemplate>
                                            <div class="container shadow p-4 mb-5 bg-white rounded"> 
                                                <div class="row">
                                                    <div class="col-md-3">
                                                        
                                                            <asp:Image class="imgServicePic" runat="server" ImageUrl='<%# Eval("service_picture") %>'/>  
                                                        
                                                        
                                                    </div>
                                                    <div class="col-md-5 py-3">
                                                        <div class="row py-3">
                                                            <asp:Label ID="lblServiceTitle" runat="server" Font-Bold="True" Font-Size="X-Large" Font-Strikeout="False" Text='<%# Eval("service_title") %>'></asp:Label>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col">
                                                                <asp:Label ID="lblFees" runat="server" Font-Size="Large" Text='<%#"RM " + String.Format("{0:#.00}",Eval("fees")) %>' ></asp:Label>
                                                                <asp:HiddenField ID="HiddenField1" runat="server" Value='<%# Eval("offer_id") %>'></asp:HiddenField>
                                                            </div>
                                                            
                                                        </div>                                                                                                            
                                                    </div>
                                                    <div class="col-md-3 px-3 py-5">                                                         
                                                        <div class="text-end text-lg-flex justify-content-center">  
                                                            <asp:LinkButton ID="btnViewMore" class="btn-find-now btnViewMore d-inline-flex align-items-center justify-content-center align-self-center" runat="server" OnClick="btnViewMore_Click">  
                                                            <span>View More</span>                                
                                                            </asp:LinkButton>                        
                                                        </div>
                                                    </div>

                                                </div>
                                            </div>
                                         </ItemTemplate>   
                                        </asp:Repeater>  
                                        </asp:Panel>
                                    </div>   
                            </div>             
            
   
           




                    <div class="noItemContainer">
                                <br />
                            <asp:Image ID="noItemLogo" runat="server" ImageUrl="~/Image/—Pngtree—info vector icon_3791375.png" class="noItemLogo" />            
                                <br /><br />
                            <h3><asp:Label ID="noItemText" runat="server" Text="Currently no services in this area" style="font-size: 30px;"></asp:Label></h3>
                                <br />
                            <asp:LinkButton ID="noItemLinkText" class="noservicelink p-2" runat="server" PostBackUrl="ClientHomePage.aspx">Back to Main</asp:LinkButton>
                        </div>  
                    </ContentTemplate>
                    <Triggers>  
                    <asp:AsyncPostBackTrigger ControlID ="displayServRepeater" />  
                    </Triggers>  
            </asp:UpdatePanel>
        </div>
                </div>
<%--End DisplayService--%>
    </div>
    </section>  
    <%-- AOS script plugin--%>
<script src="https://unpkg.com/aos@next/dist/aos.js"></script>
<script>
    /* AOS plugin script*/
    AOS.init();    
</script>
</asp:Content>
