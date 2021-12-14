<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ErrorPage.aspx.cs" Inherits="ServiceProvidingSystem.ErrorPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

</head>
<body>
    <form id="form1" runat="server">
        <div class="errorPageContainer">
            <img alt="WarningIcon" class="warningPage" src="Image/warningIcon.png"/>
        <h1>Error
            <asp:Label ID="ErrorCode" runat="server" ForeColor="#D92024" CssClass="auto-style2" style="font-size: 40px;"></asp:Label>
            Encountered
        </h1>

        <br />
        <b>What is this error?<br />
        </b><br />
        <asp:Label ID="ErrorMessage" runat="server" ForeColor="#EE0D4A" CssClass="auto-style1"></asp:Label>
        <br /><br />
        <b>Here is an option for you :<br />
        </b><br /><br/>
        <asp:LinkButton class="BackToHomeBtn" runat="server" OnClick="btnHome_Click" >Visit SpeedServ Homepage</asp:LinkButton>
        </div>
    </form>
</body>
</html>

<style type="text/css">
    body {
    font-family: Arial, Helvetica, sans-serif;   
    background-color: #f2f2f2;      
    }
    .errorPageContainer {       
        max-width: 1000px;
        margin: auto;      
        border-radius: 5px;        
        margin-top: 5%;
        margin-bottom: 100px;
        text-align: center;
    }
    .warningPage {
      max-width: 200px;
      max-height: 200px;
    }
    .BackToHomeBtn {
      max-width: 100%;
      max-height: 100%;
      background: none;
      margin: 30px 30px 30px 30px;
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
    .auto-style1 {
        font-size: large;
    }
    .auto-style2 {
        font-size: xx-large;
    }
    .BackToHomeBtn:hover {
        color: slategray;
          text-decoration: none;
    }
</style>
