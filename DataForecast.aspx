<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="DataForecast.aspx.cs" Inherits="DataForecast" %>

<%@ Register assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" namespace="System.Web.UI.DataVisualization.Charting" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="FunctionalGroup" Runat="Server">

    <tr>
    <td>

    <asp:Panel ID="Panel9" runat="server" Width="378px">
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Label ID="Label4" runat="server" Font-Bold="True" Font-Size="X-Large" ForeColor="Black" 
    Text="温度变化曲线"></asp:Label>
    </asp:Panel>

    <asp:Chart ID="Chart1" runat="server" DataSourceID="AccessDataSource1" Width="378px" Height="280px">
        <series>
            <asp:Series Name="Series1" ChartType="Line" XValueMember="tht_tempdata" 
                YValueMembers="tht_number">
            </asp:Series>
        </series>
        <chartareas>
            <asp:ChartArea Name="ChartArea1">
            </asp:ChartArea>
        </chartareas>
    </asp:Chart>
    <asp:AccessDataSource ID="AccessDataSource1" runat="server" 
        DataFile="~/App_Data/Database.accdb" 
        SelectCommand="SELECT [tht_tempdata], [tht_number] FROM [temp_histogram_table]">
    </asp:AccessDataSource>
    <asp:Panel ID="Panel10" runat="server" Width="378px">
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Label ID="Label3" runat="server" Font-Bold="True" Font-Size="X-Large" 
    ForeColor="Black" Text="温度直方图分布"></asp:Label>
    </asp:Panel>
    <asp:Chart ID="Chart2" runat="server" DataSourceID="AccessDataSource1" Width="378px" Height="280px">
        <series>
            <asp:Series Name="Series1" ChartArea="ChartArea1" ChartType="Line" 
                XValueMember="tht_tempdata" YValueMembers="tht_number">
            </asp:Series>
        </series>
        <chartareas>
            <asp:ChartArea Name="ChartArea1">
            </asp:ChartArea>
        </chartareas>
    </asp:Chart>

    </td>
</tr>

</asp:Content>

