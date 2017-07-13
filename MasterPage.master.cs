using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

//XML数据绑定域
using System.Xml;
//IO
using System.IO;
//ini
using System.Runtime.InteropServices;
using System.Text;
using CreateWebDir;
using System.Collections;    //数组容器
//数据库操作相关
using System.Data;
using System.Data.OleDb;
//图像读取显示相关
using System.Drawing;
using System.Drawing.Imaging;

public partial class MasterPage : System.Web.UI.MasterPage
{
    //XML写入变量
    XmlDocument xmldoc;
    XmlElement xmlelem;
    //常量
    string dirPathSource = null;  //文件路径源
    //根据日期进行文件检索                                                               
    public List<string> imagefInfoPath = new List<string>();    //图像文件名称列表
    public List<string> tempfInfoPath = new List<string>();     //温度文件名称列表
    string stBegin = null, stEnd = null;      //起始时间和结束时间
    //ini文件读取的全局变量
    List<string> CameraList = new List<string>();
    List<string> FurnaceName = new List<string>();
    List<string> CameraName = new List<string>();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ReadInfoFile();
            WriteXmLDate(CameraList,FurnaceName,CameraName);    //第一次执行
            XMLBindTeeView();
            HttpCookie objCookie = new HttpCookie("myCookie",Server.MapPath("~/App_Data/input.dat"));
            Response.Cookies.Add(objCookie);
            Image1.ImageUrl = "~/ShowImage.aspx";
        }
        else
        {
            ReadInfoFile();
            FindTreeViewXZ();
        }
    }
    /************************************************************************/
    /* 读取配置文件                                                               
    /************************************************************************/
    public void ReadInfoFile()
    {
        CreateWebDir.INIFile inifille = new CreateWebDir.INIFile(@"D:\DataBaseWebDX\Infrared.ini");
        string FurnaceNum = inifille.IniReadValue("GlobalParam", "FurnaceNum");    //获取炉子的总数
        string CameraNum = inifille.IniReadValue("GlobalParam", "CameraNum");    //获取探头的总数
        int FurnaceSum = int.Parse(FurnaceNum);
        int num = 0;
        string fcNum = null;
        for (int i = 0; i < FurnaceSum; i++)
        {
            CameraList.Add(CameraNum.Substring(num, 2));
            for (int j = 0; j < int.Parse(CameraNum.Substring(num, 2));j++)
            {
                fcNum = "F" + (i+1).ToString("D2") + "C" + (j+1).ToString("D2");
                CameraName.Add(inifille.IniReadValue(fcNum, "CameraName"));     //获取探头的名称
            }
            FurnaceName.Add(inifille.IniReadValue(fcNum, "FurnaceName"));    //获取炉子的名称
            num = num + 3;
        }
    }
    /************************************************************************/
    /* 绑定XML数据到TeeView控件上                                                               
    /************************************************************************/
    public void XMLBindTeeView()
    {
        this.TreeView1.ShowLines = true;//显示连接子节点和父节点之间的线条
        //根节点
        TreeNodeBinding TeeView = new TreeNodeBinding();
        TeeView.DataMember = "TeeView";//指定绑定的成员
        TeeView.ValueField = "cTeeViewName";//取值的字段
        this.TreeView1.DataBindings.Add(TeeView);
        //选择探测器节点
        TreeNodeBinding SelectDetector = new TreeNodeBinding();
        SelectDetector.DataMember = "SelectDetector";//添加与"炉子"绑定
        SelectDetector.ValueField = "cSelectDetectorName";
        this.TreeView1.DataBindings.Add(SelectDetector);
        //炉子节点
        TreeNodeBinding Furnace = new TreeNodeBinding();
        Furnace.DataMember = "Furnace";//添加与"炉子"绑定
        Furnace.ValueField = "cTeeViewName";
        this.TreeView1.DataBindings.Add(Furnace);
        //探头节点
        TreeNodeBinding Detector = new TreeNodeBinding();
        Detector.DataMember = "Detector";//添加与"探头"绑定
        Detector.ValueField = "cTeeViewName";
        this.TreeView1.DataBindings.Add(Detector);
        //功能模块节点
        TreeNodeBinding FunctionalGroup = new TreeNodeBinding();
        FunctionalGroup.DataMember = "FunctionalGroup";//添加与"数据库"绑定
        FunctionalGroup.ValueField = "cFunctionalGroupName";
        this.TreeView1.DataBindings.Add(FunctionalGroup);
        //数据分析节点
        TreeNodeBinding DataAnalysis = new TreeNodeBinding();
        DataAnalysis.DataMember = "DataAnalysis";//添加与"温度报表"绑定
        DataAnalysis.ValueField = "cDataAnalysisName";
       // DataAnalysis.NavigateUrl = "~/Default2.aspx";   //页面跳转
        this.TreeView1.DataBindings.Add(DataAnalysis);
        //数据分析子节点--点分析
        TreeNodeBinding PointAnalysis = new TreeNodeBinding();
        PointAnalysis.DataMember = "PointAnalysis";//添加与"查看报表"绑定
        PointAnalysis.ValueField = "cPointAnalysisName";
        PointAnalysis.NavigateUrl = "~/DataAnalysis.aspx";   //页面跳转
        this.TreeView1.DataBindings.Add(PointAnalysis);
        //数据分析子节点--线分析
        TreeNodeBinding LineAnalysis = new TreeNodeBinding();
        LineAnalysis.DataMember = "LineAnalysis";//添加与"数据分析"绑定
        LineAnalysis.ValueField = "cLineAnalysisName";
        LineAnalysis.NavigateUrl = "~/DataAnalysis.aspx";   //页面跳转
        this.TreeView1.DataBindings.Add(LineAnalysis);
        //数据分析子节点--面分析
        TreeNodeBinding FaceAnalysis = new TreeNodeBinding();
        FaceAnalysis.DataMember = "FaceAnalysis";//添加与"点分析"绑定
        FaceAnalysis.ValueField = "cFaceAnalysisName";
        FaceAnalysis.NavigateUrl = "~/DataAnalysis.aspx";   //页面跳转
        this.TreeView1.DataBindings.Add(FaceAnalysis);
        //数据预测节点
        TreeNodeBinding DataForecast = new TreeNodeBinding();
        DataForecast.DataMember = "DataForecast";//添加与"线分析"绑定
        DataForecast.ValueField = "cDataForecastName";
        DataForecast.NavigateUrl = "~/DataForecast.aspx";   //页面跳转
        this.TreeView1.DataBindings.Add(DataForecast);
        //数据检索节点
        TreeNodeBinding DataInspect = new TreeNodeBinding();
        DataInspect.DataMember = "DataInspect";//添加与"线分析"绑定
        DataInspect.ValueField = "cDataInspectName";
        DataInspect.NavigateUrl = "~/Default.aspx";   //页面跳转
        this.TreeView1.DataBindings.Add(DataInspect);
        //系统参数设置
        TreeNodeBinding SystemData = new TreeNodeBinding();
        SystemData.DataMember = "SystemData";//添加与"线分析"绑定
        SystemData.ValueField = "cSystemDataName";
        SystemData.NavigateUrl = "~/SystemDataSet.aspx";   //页面跳转
        this.TreeView1.DataBindings.Add(SystemData);
    }
    /************************************************************************/
    /* 把文件写到XMl文件里                                                               
    /************************************************************************/
    public void WriteXmLDate(List<string> DNum,List<string> FNum,List<string> CNum)
    {
        xmldoc = new XmlDocument(); //加入XML的声明段落,<?xml version="1.0" encoding="gb2312"?> 
        XmlDeclaration xmldecl;
        xmldecl = xmldoc.CreateXmlDeclaration("1.0", "utf-8", null);
        xmldoc.AppendChild(xmldecl);   //加入一个根元素 
        xmlelem = xmldoc.CreateElement("", "TeeView", "");
        xmlelem.SetAttribute("iTeeViewID", "0");
        xmlelem.SetAttribute("cTeeViewName", "导航栏");
        xmldoc.AppendChild(xmlelem); //加入另外一个元素 
        XmlNode root = xmldoc.SelectSingleNode("TeeView");//查找<Employees>  
        string num = null;
        int notes = 0;
        //探测器选择节点
        XmlElement SelectDetector = xmldoc.CreateElement("SelectDetector");//创建一个<Node>节点  
        SelectDetector.SetAttribute("iSelectDetectorID", "100");//设置该节点genre属性  
        SelectDetector.SetAttribute("cSelectDetectorName", "选择探测器");//设置该节点ISBN属性
        for (int i = 0; i < DNum.Count; i++)
        {
            num = (i+1).ToString();
            //FurnaceName = num + "# 炉";

            XmlElement xe1 = xmldoc.CreateElement("Furnace");//创建一个<Node>节点  
            xe1.SetAttribute("iTeeViewID", num);//设置该节点genre属性  
            xe1.SetAttribute("cTeeViewName", FNum[i].ToString());//设置该节点ISBN属性  
            int x = int.Parse(DNum[i].ToString());
            for (int j = 0; j < x; j++)
            {
                num = (i + 101).ToString() + (j+1).ToString();
                XmlElement xesub1 = xmldoc.CreateElement("Detector");
                xesub1.SetAttribute("iTeeViewID", num);//设置该节点genre属性  
                xesub1.SetAttribute("cTeeViewName",CNum[notes].ToString());//设置该节点ISBN属性   
                xe1.AppendChild(xesub1);//添加到<Node>节点中 
                notes++;
            }
            SelectDetector.AppendChild(xe1);//添加到<Employees>节点中  

        } //保存创建好的XML文档   
        root.AppendChild(SelectDetector);
        //功能模块节点
        XmlElement FunctionalGroup = xmldoc.CreateElement("FunctionalGroup");//创建一个<Node>节点  
        FunctionalGroup.SetAttribute("iFunctionalGroupID", "20");//设置该节点genre属性  
        FunctionalGroup.SetAttribute("cFunctionalGroupName", "功能模块");//设置该节点ISBN属性
        //功能模块子节点
        //数据分析
        XmlElement DataAnalysis = xmldoc.CreateElement("DataAnalysis");//创建一个<Node>节点  
        DataAnalysis.SetAttribute("iDataAnalysisID", "21");//设置该节点genre属性  
        DataAnalysis.SetAttribute("cDataAnalysisName", "数据分析");//设置该节点ISBN属性
        //点分析
        XmlElement PointAnalysis = xmldoc.CreateElement("PointAnalysis");
        PointAnalysis.SetAttribute("iPointAnalysisID", "22");//设置该节点genre属性  
        PointAnalysis.SetAttribute("cPointAnalysisName", "点分析");//设置该节点ISBN属性   
        DataAnalysis.AppendChild(PointAnalysis);//添加到<Node>节点中  
        //点分析
        XmlElement LineAnalysis = xmldoc.CreateElement("LineAnalysis");
        LineAnalysis.SetAttribute("iLineAnalysisID", "23");//设置该节点genre属性  
        LineAnalysis.SetAttribute("cLineAnalysisName", "线分析");//设置该节点ISBN属性   
        DataAnalysis.AppendChild(LineAnalysis);//添加到<Node>节点中  
        //面分析
        XmlElement FaceAnalysis = xmldoc.CreateElement("FaceAnalysis");
        FaceAnalysis.SetAttribute("iFaceAnalysisID", "24");//设置该节点genre属性  
        FaceAnalysis.SetAttribute("cFaceAnalysisName", "面分析");//设置该节点ISBN属性   
        DataAnalysis.AppendChild(FaceAnalysis);//添加到<Node>节点中  
        FunctionalGroup.AppendChild(DataAnalysis);//将数据分析添加为功能模块的子模块
        //数据预测
        XmlElement DataForecast = xmldoc.CreateElement("DataForecast");
        DataForecast.SetAttribute("iDataForecastID", "25");//设置该节点genre属性  
        DataForecast.SetAttribute("cDataForecastName", "数据预测");//设置该节点ISBN属性  
        FunctionalGroup.AppendChild(DataForecast);//添加到<Node>节点中  
        //数据检索
        XmlElement DataInspect = xmldoc.CreateElement("DataInspect");
        DataInspect.SetAttribute("iDataInspectID", "26");//设置该节点genre属性  
        DataInspect.SetAttribute("cDataInspectName", "数据检索");//设置该节点ISBN属性   
        FunctionalGroup.AppendChild(DataInspect);//添加到<Node>节点中  
        root.AppendChild(FunctionalGroup);
        //系统参数
        XmlElement SystemData = xmldoc.CreateElement("SystemData");//创建一个<Node>节点  
        SystemData.SetAttribute("iSystemDataID", "30");//设置该节点genre属性  
        SystemData.SetAttribute("cSystemDataName", "系统参数设置");//设置该节点ISBN属性
        root.AppendChild(SystemData);
        //保存XML文件
        xmldoc.Save(Server.MapPath("TeeView.xml"));
    }
    /************************************************************************/
    /* 检索TreeView中选中的节点                                                               
    /************************************************************************/
    public void FindTreeViewXZ()
    {
        TreeNode _tnode = TreeView1.SelectedNode;
        if (_tnode == null)
        {
            Response.Write("<script>window.confirm('请选择探测器！');</script>");
            return;
        }
        string stNodeValuePath = _tnode.ValuePath;
        if (stNodeValuePath.Substring(0,9).CompareTo("导航栏/选择探测器") == 0)
        {
            dirPathSource = "D:\\DataBase\\F"+int.Parse(stNodeValuePath.Substring(10,1)).ToString("D2")+"C"+int.Parse(stNodeValuePath.Substring(15, 1)).ToString("D2")+"\\";             
        } 
    }
    /************************************************************************/
    /* Treeview控件函数                                                               
    /************************************************************************/
    protected void treeT_SelectedNodeChanged(object sender, EventArgs e)
    {

    }
    /************************************************************************/
    /* 遍历并筛选符合条件的文件                                                               
    /************************************************************************/
    public void GetFileName(string dirPath, string dtBegin, string dtEnd,List<string> fInfoPath)
    {
        DirectoryInfo dInfo = new DirectoryInfo(dirPath);    //实例化DirectoryInfo对象
        FileSystemInfo[] fsInfos = dInfo.GetFileSystemInfos();
        foreach (FileSystemInfo fsInfo in fsInfos)
        {
            string nu = fsInfo.Name.Substring(0, 10);
            if (string.Compare(fsInfo.Name.Substring(0, 10), dtBegin) > 0 && string.Compare(fsInfo.Name.Substring(0, 10), dtEnd) < 0)
            {
                fInfoPath.Add(fsInfo.Name);
            }
        }
    }
    /************************************************************************/
    /* 日历控件一进行日期选择                                                               
    /************************************************************************/
    protected void Calendar1_SelectionChanged(object sender, EventArgs e)
    {
        string stDataTime = Calendar1.SelectedDate.ToString();
        if (stDataTime.Length == 16)
        {
            stDataTime = stDataTime.Substring(0, 8);
        }
        else if (stDataTime.Length == 17)
        {
            stDataTime = stDataTime.Substring(0, 9);
        }
        else
        {
            stDataTime = stDataTime.Substring(0, 10);
        }
        string[] split = stDataTime.Split(new char[] { '/' });
        if (split[1].Length == 1)
        {
            split[1] = split[1].Insert(0, "0");
        }
        if (split[2].Length == 1)
        {
            split[2] = split[2].Insert(0, "0");
        }
        stBegin = split[0] + "-" + split[1] + "-" + split[2];
        Label1.Text = "起始日期：" + stBegin;
        //string _script = "$('#TextBox1.Text').val('"+stBegin+"')";
        //ScriptManager.RegisterStartupScript(this, this.GetType(), "", _script, true);
    }
    /************************************************************************/
    /* 日历控件二进行日期选择                                                               
    /************************************************************************/
    protected void Calendar2_SelectionChanged(object sender, EventArgs e)
    {
        string stDataTime = Calendar2.SelectedDate.ToString();
        if (stDataTime.Length == 16)
        {
            stDataTime = stDataTime.Substring(0, 8);
        }
        else if (stDataTime.Length == 17)
        {
            stDataTime = stDataTime.Substring(0, 9);
        }
        else
        {
            stDataTime = stDataTime.Substring(0, 10);
        }
        string[] split = stDataTime.Split(new char[] { '/' });
        if (split[1].Length == 1)
        {
            split[1] = split[1].Insert(0, "0");
        }
        if (split[2].Length == 1)
        {
            split[2] = split[2].Insert(0, "0");
        }
        stEnd = split[0] + "-" + split[1] + "-" + split[2];
        Label2.Text = "截止日期：" + stEnd;
        //string _script = "$('#TextBox2.Text').val('" + stBegin + "')";
        //ScriptManager.RegisterStartupScript(this, this.GetType(), "", _script, true);
    }
    /************************************************************************/
    /* 查询按钮控件                                                               
    /************************************************************************/
    protected void Button1_Click(object sender, EventArgs e)
    {
        //int n = string.Compare(TextBox1.Text, TextBox2.Text);    //-1: 1<2  0: 1=2  1: 1>2
        
        if (Label1.Text.Length <= 5 || Label2.Text.Length <= 5)
        {
            Response.Write("<script>window.confirm('请选择正确的日期区间！');</script>");
            return;
        }
        int n = string.Compare(Label1.Text.Substring(5, 10), Label2.Text.Substring(5, 10));
        if (n == 1)
        {
            Response.Write("<script>window.confirm('请选择正确的日期区间！');</script>");
            return;
        }
        GetFileName(dirPathSource + "Image\\", Label1.Text.Substring(5, 10), Label2.Text.Substring(5, 10), imagefInfoPath);   //读取符合条件的图像文件名称
        GetFileName(dirPathSource + "Temp\\", Label1.Text.Substring(5, 10), Label2.Text.Substring(5, 10), tempfInfoPath);   //读取符合条件的温度文件名称
        //检索到文件写入数据库里
        DataCommand(@"DELETE FROM file_path_table");
        for (int i = 1; i < imagefInfoPath.Count + 1; i++)
        {
            string st_Band = imagefInfoPath[i - 1].Substring(27, 1);
            string st_Chanel = imagefInfoPath[i - 1].Substring(20, 6);
            string st_TimeDate = imagefInfoPath[i - 1].Substring(0, 18);
            string SQLCommand = String.Format(@"INSERT INTO file_path_table(fpt_band, fpt_id, fpt_chanel,fpt_timedate) VALUES (""{0}"",""{1}"",""{2}"",""{3}"")", st_Band, i, st_Chanel,st_TimeDate);
            DataCommand(SQLCommand);
        }
        Response.Write("<script>window.location.href=window.location.href;</script>");    //刷新一下 
       // this.Button1.BackColor = System.Drawing.Color.Red;
    }
    /*#######################################################################/
    /# 数据库服务                                                               
    /#######################################################################*/
    //数据库的路径
    public string DataFile = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=F:\OfflineAnalysisSoftware\ASP.Net\App_Data\Database.accdb";
    /************************************************************************/
    /* 用于插入，删除，更新指令的执行
    /************************************************************************/
    void DataCommand(string stCommand)
    {
        OleDbConnection conn = new OleDbConnection(DataFile);
        try
        {
            conn.Open();
            OleDbCommand cmd = new OleDbCommand(stCommand, conn);
            cmd.ExecuteReader();
            conn.Close();
        }
        catch (System.Exception ee)
        {
            string error = "<script>window.confirm(" + ee.ToString() + ");</script>";
            Response.Write(error);
        }
    }
    /************************************************************************/
    /* 播放按钮                                                               
    /************************************************************************/
    protected void Button4_Click(object sender, EventArgs e)
    {
        TreeNode _tnode = TreeView1.SelectedNode;
        if (_tnode == null)
        {
            Response.Write("<script>window.confirm('请选择探测器！');</script>");
            return;
        }
        GetFileName(dirPathSource + "Image\\", "2016-07-16", "2016-07-18", imagefInfoPath);   //读取符合条件的图像文件名称
        foreach (string str in imagefInfoPath)
        {
          //HttpCookie objCookie = new HttpCookie("myCookie",dirPathSource + "Image\\" + str);
          //  Response.Cookies.Add(objCookie);
            Application["name"] = dirPathSource + "Image\\" + str;
            Image1.ImageUrl = "~/ShowImage.aspx";
        }
    }
}

/************************************************************************/
/* ini文件读取类                                                               
/************************************************************************/
namespace CreateWebDir
{

    /// <summary>
    /// INIFile 的摘要说明
    /// </summary>
    public class INIFile
    {
        public string path;
        public INIFile(string INIPath)
        {
            path = INIPath;
        }
        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section,
        string key, string val, string filePath);
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section,
        string key, string def, StringBuilder retVal, int size, string filePath);
        public void IniWriteValue(string Section, string Key, string Value)
        {
            WritePrivateProfileString(Section, Key, Value, this.path);
        }
        public string IniReadValue(string Section, string Key)
        {
            StringBuilder temp = new StringBuilder(255);
            int i = GetPrivateProfileString(Section, Key, "", temp, 255, this.path);
            return temp.ToString();
        }
    }
}
