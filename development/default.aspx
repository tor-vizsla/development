<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="development._default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="/js/jquery-1.10.2.min.js"></script>
    <script type="text/javascript">
        function testApi() {
            PageMethods.Test('hello', function (retVal) {
                alert("here");
                $("#divResults").html(retVal);
            },pageMethodFail);
        }

        function pageMethodFail(err)
        {
            alert("error: "+err.message);
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" enablepagemethods="true" runat="server"></asp:ScriptManager>


        <a href="javascript:testApi();">test api</a>
    <div id="divResults">
        [results]
    </div>
    </form>
</body>
</html>
