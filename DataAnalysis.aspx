<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="DataAnalysis.aspx.cs" Inherits="DataAnalysis" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="FunctionalGroup" Runat="Server">
    <tr>
        <td>
            <asp:Panel ID="Panel11" runat="server">
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Label ID="Label4" runat="server" Text="温度变化曲线" Font-Size="X-Large"></asp:Label>
            </asp:Panel>
            <asp:Chart ID="Chart1" runat="server" DataSourceID="AccessDataSource2" width="378px">
                <Series>
                    <asp:Series ChartType="Line" Name="Series1" XValueMember="tht_tempdata" 
                        YValueMembers="tht_number">
                    </asp:Series>
                </Series>
                <ChartAreas>
                    <asp:ChartArea Name="ChartArea1">
                    </asp:ChartArea>
                </ChartAreas>
            </asp:Chart>
            <asp:AccessDataSource ID="AccessDataSource2" runat="server" 
                DataFile="~/App_Data/Database.accdb" 
                SelectCommand="SELECT [tht_tempdata], [tht_number] FROM [temp_histogram_table]">
            </asp:AccessDataSource>
            <asp:Panel ID="Panel10" runat="server">
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Label ID="Label3" runat="server" Text="画线温度均匀性" Font-Size="X-Large"></asp:Label>
            </asp:Panel>
            <asp:Chart ID="Chart2" runat="server" DataSourceID="AccessDataSource1" Width="378px">
                <Series>
                    <asp:Series ChartType="Line" Name="Series1" XValueMember="tht_tempdata" 
                        YValueMembers="tht_number">
                    </asp:Series>
                </Series>
                <ChartAreas>
                    <asp:ChartArea Name="ChartArea1">
                    </asp:ChartArea>
                </ChartAreas>
            </asp:Chart>
            <asp:AccessDataSource ID="AccessDataSource1" runat="server" 
                DataFile="~/App_Data/Database.accdb" 
                SelectCommand="SELECT [tht_tempdata], [tht_number] FROM [temp_histogram_table]">
            </asp:AccessDataSource>
        </td>
    </tr>
</asp:Content>

