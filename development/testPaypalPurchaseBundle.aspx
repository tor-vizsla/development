<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="testPaypalPurchaseBundle.aspx.cs" Inherits="development.testPaypal" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
        <script src="js/jquery-1.10.2.min.js"></script>
    <script type="text/javascript">
        function send() {
            $("#dvResults").html('please wait...');
            PageMethods.PurchaseBundle(function(retVal){
                alert(retVal);
                $("#dvResults").html(retVal);
            },pageMethodFail);
        }

    
        function pageMethodFail(error, userContext, methodName) {
            alert("Error:" + error + "\n\n" + userContext + "\n\n" + "PageMethod:" + methodName);
        }
    
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" EnablePageMethods="true" runat="server"></asp:ScriptManager>
    <a href="javascript:send();">test api</a>
        <div id="dvResults">not set</div>
    </form>
</body>
</html>
