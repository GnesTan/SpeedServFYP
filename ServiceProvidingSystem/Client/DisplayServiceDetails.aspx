<%@ Page Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="DisplayServiceDetails.aspx.cs" Inherits="ServiceProvidingSystem.Client.DisplayServiceDetails" %>

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
    <link href="../DisplayService.css" rel="stylesheet" />         
    <link href="../DisplayServiceDetails.css" rel="stylesheet" />

    <asp:ScriptManager ID="ScriptManager1" runat="server">  
    </asp:ScriptManager> 

    <section id="#" class="disServDet d-flex align-items-center">
    <div class="container">

        <div class="row">
                <div class="col-md-12">
                    <div data-aos="zoom-out" data-aos-delay="100">  
                        <div class="container shadow p-4 mb-5 bg-white rounded" data-aos="zoom-out" data-aos-delay="50" >       
                             <h3 data-aos="fade-right"><span style="">S</span>ervicer Profile</h3>
                            <%--Servicer profile--%>
                        <div class="reqStatusMargin col-sm px-3 py-3">      
                            <div class="row">
                                <div class="col-md-3 py-3">
                                    <asp:Image ID="imgProfile" runat="server" Height="100px" Width="100px" style="border-radius:100px; box-shadow: 0 0 0 3px #b3b3b3; text-decoration:none;" ImageUrl="~/Image/Foto_Formal.jpg"/>
                                </div>
                                <div class="col-md-9">
                                    <div class="row mb-3">
                                        <div class="col-md-6 mx-2">
                                            <h4 for="name" class="form-label align-middle">Name</h4>
                                            <asp:Label ID="lblServicerName" class="reqDetailsLabel title align-middle" runat="server"></asp:Label>	
                                        </div>
                                        <div class="col-md-5">
                                            <h4 for="name" class="form-label align-middle">Completed Service</h4>
                                            <asp:Label ID="lblTotalCompleteService" class="reqDetailsLabel title align-middle" runat="server"></asp:Label>	
                                        </div>
                                    </div>
                                    <div class="row mb-3">
                                        <div class="col-md-6 mx-2">
                                            <h4 for="name" class="form-label align-middle">Phone No.</h4>
                                            <asp:Label ID="lblPhoneNo" class="reqDetailsLabel title align-middle" runat="server"></asp:Label>	
                                        </div>
                                        <div class="col-md-5">
                                            <h4 for="name" class="form-label align-middle">Rating</h4>
                                            <i class="bi bi-star-fill rateStar" style="padding-bottom: 12px;"></i><asp:Label ID="lblRating" class="reqDetailsLabel title" runat="server" style="margin-left: 20px;"></asp:Label>	
                                        </div>
                                    </div>              
                                </div>
                            </div>
                        </div>   
                        <hr />
                                <div class="row g-3">     
                                    <div class="text-end text-lg-flex justify-content-center" data-aos="zoom-out" data-aos-delay="400">                        
                                        <asp:LinkButton ID="btnContact" class="btn-find-now btnContact d-inline-flex align-items-center justify-content-center align-self-center" runat="server" OnClick="btnContact_Click">  
                                        <span>Contact Servicer</span>
                                        <i class="bi bi-whatsapp"></i>
                                        </asp:LinkButton>                                                              
                                    </div>
                                </div>        
                            </div>
                        </div>
                </div>
            </div>
        <div class="row">
                <div class="col-md-12">
                    <div data-aos="zoom-out" data-aos-delay="100">  
                        <div class="container shadow p-4 mb-5 bg-white rounded" data-aos="zoom-out" data-aos-delay="50" >       
                             <h3 data-aos="fade-right"><span style="">S</span>ervice Details</h3>
                            <%--Service Details--%>
                        <div class="reqStatusMargin col-sm px-2 py-3">      
                            <div class="row">
                                <div class="col-md-3 py-4">
                                    <asp:Image ID="imgServicePicture" runat="server" class="imgServicePic" ImageUrl="~/Image/Foto_Formal.jpg" />
                                </div>
                                <div class="col-md-9 py-1">
                                    <div class="row mb-3">
                                        <div class="col-md-8 mx-2">   
                                            <asp:Label ID="lblServiceTypeCat" class="reqDetailsLabel serviceCatType align-middle" runat="server"></asp:Label>
                                            <span style="font-size: 14px;">(</span><asp:Hyperlink ID="lblLocation" class="reqDetailsLabel location align-middle" runat="server"></asp:Hyperlink><span style="font-size: 14px;">)</span>                          
                                            <br />
                                            <asp:Label ID="lblServiceName" class="reqDetailsLabel serviceName align-middle" runat="server"></asp:Label>                                            
                                        </div>
                                        <div class="col-md-3 py-3 d-flex flex-row-reverse">                                                                                       	
                                            <asp:ImageButton ID="btnAddFav" class="addFav" runat="server" ImageUrl="../Image/heart-regular-40.png" data-bs-toggle="tooltip" data-bs-placement="bottom" title="Add To Favourite" onClick="addToFav_Click"/>
                                        </div>
                                    </div>
                                    <div class="row mb-4 mt-2">                                        
                                        <div class="col-md-4 mx-2">
                                           <h4 for="name" class="form-label align-middle">Estimated Service Fee</h4>
                                          <asp:Label ID="lblPrice" class="reqDetailsLabel price" runat="server"></asp:Label>	
                                        </div>
                                        <div class="col-md-7 mx-2">
                                           <h4 for="name" class="form-label align-middle">Transport Fee</h4>
                                          <asp:Label ID="lblTransportFee" class="reqDetailsLabel price" runat="server"></asp:Label>	
                                        </div>
                                    </div>                             
                                    <div class="row">
                                    <div class="col-md-8 mx-2">                                    
                                        <h5 for="name" class="form-label align-middle">Remark</h5>
                                          <asp:Label ID="lblRemark" class="reqDetailsLabel remark" runat="server"></asp:Label>	
                                    </div>
                            </div>                                   
                                </div>
                            </div>

                        </div>   
                        <hr />
                                <div class="row g-3">     
                                    <div class="text-end text-lg-flex justify-content-center" data-aos="zoom-out" data-aos-delay="400">                        
                                        <asp:LinkButton ID="btnRequestNow" class="btn-find-now btnView d-inline-flex align-items-center justify-content-center align-self-center" runat="server" OnClick="btnRequestNow_Click">  
                                        <span>Request Now</span>
                                       <i class="bi bi-arrow-right"></i>
                                        </asp:LinkButton>                                                              
                                    </div>
                                </div>        
                            </div>
                    </div>
                </div>
        </div>

        



        <%--Display Review About Servicer--%>
        <div class="row">
            <div data-aos="zoom-out" data-aos-delay="100">  
            <div class="container shadow p-4 mb-5 bg-white rounded" data-aos="zoom-out" data-aos-delay="50" >       
                 <h3 data-aos="fade-right"><span style="">C</span>omment About Servicer</h3>
                <div class="row px-5 py-3">
                        <div class="reqStatusMargin col-sm px-5 py-3">      
                            <asp:Panel ID="RepeaterPanel" runat="server">
                            <asp:Repeater  ID="RepeaterRequest" runat="server" >                                                   
                            <ItemTemplate>
                                <div class="rowStyle"> 
                                    <div class="row">
                                        <div class="col-md-2">
                                             <div class="row">
                                                 <div class="col">
                                                      <asp:Image ID="imgProfile" runat="server" Height="60px" Width="60px" style="border-radius:100px; box-shadow: 0 0 0 3px #b3b3b3; text-decoration:none;" ImageUrl='<%# (string)Eval("IS_ANONYMOUS") =="Y" ? "~/Image/generaluser.png" : Eval("profile_picture") %>'/>
                                                 </div>                                       
                                            </div>
                                        </div>
                                        <div class="col-md-10">
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
                <hr />
                    <div class="row g-3">     
                        <div class="text-end text-lg-flex justify-content-center" data-aos="zoom-out" data-aos-delay="50" >                        
                            <asp:LinkButton ID="LinkButton1" class="btn-find-now btnView d-inline-flex align-items-center justify-content-center align-self-center" runat="server" OnClick="lbViewMoreDetails_Click">  
                            <span>View More</span>
                            <i class="bi bi-arrow-right"></i>
                            </asp:LinkButton>                                                              
                        </div>
                    </div>        
                </div>
            </div>

        </div>



    <%--End Post Service--%>
          
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
