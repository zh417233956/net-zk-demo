<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="WebApplication.Index" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <%
                string pools =string.Join(",", WebApplication.ZKDemo.zkhelper.pools.Select(m=>m.Addr).ToList());               
            %>
            <%=pools %>
        </div>
    </form>
</body>
</html>
