<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="digitalGoods.aspx.cs" Inherits="development.paypal.digitalGoods" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
<form action='checkout' method='post'>
	<input type='image' name='paypal_submit' id='paypal_submit'  src='https://www.paypal.com/en_US/i/btn/btn_dg_pay_w_paypal.gif' border='0' align='top' alt='Pay with PayPal'/>
</form>
<script src='https://www.paypalobjects.com/js/external/dg.js' type='text/javascript'></script>
<script>
	var dg = new PAYPAL.apps.DGFlow(
	{
		trigger: 'paypal_submit',
		expType: 'instant'
	});
</script>

</body>
</html>
