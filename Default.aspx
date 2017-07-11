<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="FunctionalGroup" Runat="Server">
    
    <asp:GridView ID="GridView1" runat="server" AllowPaging="True" 
        AutoGenerateColumns="False" 
    DataSourceID="AccessDataSource2" PageSize="8" Width="378px" Height="300px" 
        DataKeyNames="fpt_id">
        <Columns>
            <asp:BoundField DataField="fpt_id" HeaderText="序号" 
                SortExpression="fpt_id" InsertVisible="False" ReadOnly="True" />
            <asp:BoundField DataField="fpt_band" HeaderText="波段" 
                SortExpression="fpt_band" />
            <asp:BoundField DataField="fpt_chanel" HeaderText="通道" 
                SortExpression="fpt_chanel" />
            <asp:BoundField DataField="fpt_timedate" HeaderText="日期" 
                SortExpression="fpt_timedate" />
        </Columns>
        <SelectedRowStyle BackColor="#999999" />
    </asp:GridView>
    <asp:AccessDataSource ID="AccessDataSource2" runat="server" 
    DataFile="~/App_Data/Database.accdb" 
    
        SelectCommand="SELECT [fpt_id], [fpt_band], [fpt_chanel], [fpt_timedate] FROM [file_path_table]">
</asp:AccessDataSource>
    <asp:AccessDataSource ID="AccessDataSource1" runat="server" 
        DataFile="~/App_Data/Database.accdb" 
        
    SelectCommand="SELECT [tht_number], [tht_tempdata] FROM [temp_histogram_table]">
    </asp:AccessDataSource>
    <asp:Panel ID="Panel9" runat="server" Width="378px">
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Label ID="Label5" runat="server" Text="温度直方图分布" Font-Bold="True" Font-Size="X-Large"></asp:Label>
    </asp:Panel>
<br />
    <asp:Chart ID="Chart1" runat="server" Width="378px" Height="130px" 
    DataSourceID="AccessDataSource1">
        <Series>
            <asp:Series Name="Series1" ChartType="Line" XValueMember="tht_tempdata" 
                YValueMembers="tht_number">
            </asp:Series>
        </Series>
        <ChartAreas>
            <asp:ChartArea Name="ChartArea1">
            </asp:ChartArea>
        </ChartAreas>
    </asp:Chart>
<br />
    <asp:Panel ID="Panel10" runat="server" Width="378px">
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Label ID="Label4" runat="server" Font-Bold="True" Font-Size="X-Large" Text="画线温度均匀性"></asp:Label>
    </asp:Panel>
    <asp:Chart ID="Chart2" runat="server" Width="378px" Height="130px" 
    DataSourceID="AccessDataSource3">
        <Series>
            <asp:Series Name="Series1" ChartType="Line" XValueMember="tht_tempdata" 
                YValueMembers="tht_number">
            </asp:Series>
        </Series>
        <ChartAreas>
            <asp:ChartArea Name="ChartArea1">
            </asp:ChartArea>
        </ChartAreas>
    </asp:Chart>
    <asp:AccessDataSource ID="AccessDataSource3" runat="server" 
    DataFile="~/App_Data/Database.accdb" 
    SelectCommand="SELECT [tht_tempdata], [tht_number] FROM [temp_histogram_table]">
</asp:AccessDataSource>
    
</asp:Content>