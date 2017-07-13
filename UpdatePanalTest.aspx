<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UpdatePanalTest.aspx.cs" Inherits="UpdatePanalTest" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        #GridView1{}
        #GridView1 th
        {
            color:Black;
            font-size:16px;
            padding:10px;
            }
        #GridView1 td
        {
            padding:10px;
            font-size:12px;
            color:red;
            }
        #GridView1 td.ttl
        {
            color:Gray;
            }
        #GridView1 tr:hover
        {
            background-color:#dedede;
            }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
            <asp:ScriptManager ID="ScriptManager1" runat="server">  
            </asp:ScriptManager>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" ChildrenAsTriggers="true">
                <ContentTemplate>
                    <asp:Calendar ID="Calendar1" runat="server" onselectionchanged="Calendar1_SelectionChanged"></asp:Calendar>
                </ContentTemplate>
            
            </asp:UpdatePanel>
            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                <ContentTemplate>
                
                    <asp:Label ID="Label4" runat="server" Text="Label"></asp:Label>
                    <br />
                    <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                    <br />
                    <br />
                </ContentTemplate>
            </asp:UpdatePanel>
        
        </div>
        <div>
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
                DataKeyNames="fpt_id" DataSourceID="AccessDataSource1">
                <Columns>
                    <asp:BoundField DataField="fpt_id" HeaderText="编号" InsertVisible="False" 
                        ReadOnly="True" SortExpression="fpt_id" />
                    <asp:BoundField DataField="fpt_band" HeaderText="名称" 
                        SortExpression="fpt_band" >
                    <ItemStyle CssClass="ttl" />
                    </asp:BoundField>
                    <asp:BoundField DataField="fpt_chanel" HeaderText="通道号" 
                        SortExpression="fpt_chanel" />
                    <asp:BoundField DataField="fpt_timedate" HeaderText="日期" 
                        SortExpression="fpt_timedate" />
                </Columns>
            </asp:GridView>
            <asp:AccessDataSource ID="AccessDataSource1" runat="server" 
                DataFile="~/App_Data/Database.accdb" 
                SelectCommand="SELECT [fpt_band], [fpt_id], [fpt_chanel], [fpt_timedate] FROM [file_path_table]">
            </asp:AccessDataSource>
        </div>
    </form>
</body>
</html>
