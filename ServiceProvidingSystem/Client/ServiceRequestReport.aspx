﻿<%@ Page Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="ServiceRequestReport.aspx.cs" Inherits="ServiceProvidingSystem.Client.ServiceRequestReport" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.4000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server"> 
       

     <cr:crystalreportviewer runat="server" ID="crystalreportviewer1" autodatabind="true"></cr:crystalreportviewer>





    

</asp:Content>
