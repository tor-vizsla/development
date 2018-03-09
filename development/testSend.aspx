<%@ Page Language="C#" AutoEventWireup="true" debug="true" CodeBehind="testSend.aspx.cs" Inherits="development.testSend" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="js/jquery-1.10.2.min.js"></script>
    <script type="text/javascript">
        function send() {
            var mob = '07873777444';
            PageMethods.TestSend(mob, function(retVal){
                alert(retVal);
            },pageMethodFail);
        }

    
        function pageMethodFail(error, userContext, methodName) {
            alert("Error:" + error + "\n\n" + userContext + "\n\n" + "PageMethod:" + methodName);
        }
    
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" enablepagemethods="true" runat="server"></asp:ScriptManager>
        <a href="javascript:send();">test send</a>
    <div id="dvBody">
    </div>
    </form>
</body>
</html>
