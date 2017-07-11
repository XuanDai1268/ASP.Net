<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SystemDataSet.aspx.cs" Inherits="SystemDataSet" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>系统参数设置</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:Panel ID="Panel1" runat="server">
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Label ID="Label1" runat="server" Text="系统参数设置" Font-Size="XX-Large" 
                ForeColor="#0066CC"></asp:Label>
            <br />
            &nbsp;<asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
                BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" 
                CellPadding="3" DataKeyNames="sdst_id" DataSourceID="AccessDataSource1" 
                Height="199px" Width="484px">
                <Columns>
                    <asp:BoundField DataField="sdst_property" HeaderText="sdst_property" 
                        SortExpression="sdst_property" />
                    <asp:BoundField DataField="adst_value" HeaderText="adst_value" 
                        SortExpression="adst_value" />
                    <asp:CommandField ShowEditButton="True" />
                </Columns>
                <FooterStyle BackColor="White" ForeColor="#000066" />
                <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                <RowStyle ForeColor="#000066" />
                <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                <SortedAscendingCellStyle BackColor="#F1F1F1" />
                <SortedAscendingHeaderStyle BackColor="#007DBB" />
                <SortedDescendingCellStyle BackColor="#CAC9C9" />
                <SortedDescendingHeaderStyle BackColor="#00547E" />
            </asp:GridView>
            <asp:AccessDataSource ID="AccessDataSource1" runat="server" 
                DataFile="~/App_Data/Database.accdb" 
                DeleteCommand="DELETE FROM [system_data_set_table] WHERE [sdst_id] = ?" 
                InsertCommand="INSERT INTO [system_data_set_table] ([sdst_id], [sdst_property], [adst_value]) VALUES (?, ?, ?)" 
                SelectCommand="SELECT * FROM [system_data_set_table]" 
                UpdateCommand="UPDATE [system_data_set_table] SET [sdst_property] = ?, [adst_value] = ? WHERE [sdst_id] = ?">
                <DeleteParameters>
                    <asp:Parameter Name="sdst_id" Type="Int32" />
                </DeleteParameters>
                <InsertParameters>
                    <asp:Parameter Name="sdst_id" Type="Int32" />
                    <asp:Parameter Name="sdst_property" Type="String" />
                    <asp:Parameter Name="adst_value" Type="String" />
                </InsertParameters>
                <UpdateParameters>
                    <asp:Parameter Name="sdst_property" Type="String" />
                    <asp:Parameter Name="adst_value" Type="String" />
                    <asp:Parameter Name="sdst_id" Type="Int32" />
                </UpdateParameters>
            </asp:AccessDataSource>
            <br />
            <br />
            <asp:Button ID="Button1" runat="server" Font-Size="X-Large" 
                PostBackUrl="~/Default.aspx" Text="返 回" />
            <br />
        </asp:Panel>
        <br />
    
    </div>
    </form>
</body>
</html>
