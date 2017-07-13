<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        #GridView1 {}
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
            color:Gray;
            }
        #GridView1 td.ttl
        {
            color:Red;
            }
       #GridView1 tr:hover
        {
            background-color:#ddefed;
            }
            
            
    </style>   
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="FunctionalGroup" Runat="Server">
    <tr>
        <td>
    <asp:Panel ID="Panel10" runat="server" Height="300px" Width="378px">
        <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AutoGenerateColumns="False"  
            DataKeyNames="fpt_id" DataSourceID="GridViewDataSource" PageSize="8" 
            Width="378px" Height="300px">
            <Columns>
                <asp:BoundField DataField="fpt_id" HeaderText="序号" InsertVisible="False" 
                    ReadOnly="True" SortExpression="fpt_id" />
                <asp:BoundField DataField="fpt_band" HeaderText="波段" 
                    SortExpression="fpt_band" />
                <asp:BoundField DataField="fpt_chanel" HeaderText="通道" 
                    SortExpression="fpt_chanel" />
                <asp:BoundField DataField="fpt_timedate" HeaderText="日期" 
                    SortExpression="fpt_timedate" />
            </Columns>
        </asp:GridView>
        <asp:AccessDataSource ID="GridViewDataSource" runat="server" 
            DataFile="~/App_Data/Database.accdb" 
            SelectCommand="SELECT [fpt_id], [fpt_band], [fpt_chanel], [fpt_timedate] FROM [file_path_table]">
        </asp:AccessDataSource>
    </asp:Panel>
    <asp:Panel ID="Panel11" runat="server" Height="150px" Width="378px">
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Label ID="Label3" runat="server" Font-Size="X-Large" Text="温度直方图分布"></asp:Label>
        <asp:Chart ID="Chart1" runat="server" 
    DataSourceID="HistogramDataSource" Width="378px" Height="130px" >
            <Series>
                <asp:Series Name="Series1" XValueMember="tht_number" 
                    YValueMembers="tht_tempdata" ChartType="Line">
                </asp:Series>
            </Series>
            <ChartAreas>
                <asp:ChartArea Name="ChartArea1">
                </asp:ChartArea>
            </ChartAreas>
        </asp:Chart>
        <asp:AccessDataSource ID="HistogramDataSource" runat="server" DataFile="~/App_Data/Database.accdb" 
    SelectCommand="SELECT [tht_tempdata], [tht_number] FROM [temp_histogram_table]">
        </asp:AccessDataSource>
    </asp:Panel>
    <asp:Panel ID="Panel12" runat="server" Height="150px" Width="378px">
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Label ID="Label4" runat="server" Text="画线温度均匀性"></asp:Label>
        <asp:Chart ID="Chart2" runat="server" DataSourceID="TemChangeAllDataSource" 
            Width="378px" Height="130px" >
            <Series>
                <asp:Series Name="Series1" ChartType="Line" XValueMember="tht_number" 
                    YValueMembers="tht_tempdata">
                </asp:Series>
            </Series>
            <ChartAreas>
                <asp:ChartArea Name="ChartArea1">
                </asp:ChartArea>
            </ChartAreas>
        </asp:Chart>
        <asp:AccessDataSource ID="TemChangeAllDataSource" runat="server" 
            DataFile="~/App_Data/Database.accdb" 
            SelectCommand="SELECT [tht_tempdata], [tht_number] FROM [temp_histogram_table]">
        </asp:AccessDataSource>
    </asp:Panel> 
        </td>
    </tr>
</asp:Content>