using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

public partial class DataForecast : System.Web.UI.Page
{
    int g_nWidth = 768;
	int g_nHeight = 576;
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    /************************************************************************/
    /* 读取温度数据                                                               
    /************************************************************************/
    public void ReadTempData(string fileName)
    {
        int[] pTempData = new int[g_nWidth * g_nHeight];   //温度矩阵
        int num = 0;
        if (File.Exists(fileName))
        {
            BinaryReader binReader = new BinaryReader(File.Open(fileName, FileMode.Open));
            try
            {
                for (int i = 0; i < g_nWidth; i++)   //读取温度矩阵
                {
                    for (int j = 0; j < g_nHeight; j++)
                    {
                        binReader.BaseStream.Seek(sizeof(int) * num, SeekOrigin.Begin);
                        pTempData[i * g_nHeight + j] = binReader.ReadInt32();
                        num++;
                    }
                }
            }
            catch (EndOfStreamException e)
            {
                Console.WriteLine("{0} caught and ignored. " +
                    "Using default values.", e.GetType().Name);
            }
            finally
            {
                binReader.Close();
                Response.Write("<script>alert('读取完成！')</script>");
            }
        }
    }

    //public void ReadDatCamer(string fileName)
    //{
    //    int[] pTempData = new int[g_nWidth * g_nHeight];   //温度矩阵
    //    byte[] TubMask = new byte[g_nWidth * g_nHeight];   //炉管掩膜
    //    int[] pVirtualThermTempData = new int[g_nVirtualThermColNum * g_nVirtualThermRowNum];    //虚拟热电偶对应的矩阵
    //    int[] pVirtualThermPos = new int[g_nVirtualThermColNum * g_nVirtualThermRowNum * 2];     //虚拟热电偶对应的坐标
    //    char[] pTempFileNL = new char[38];     //温度文件名长度
    //    int pTubNum = 0;     //加热炉编号
    //    int pOverallTemperature = 0;    //整体温度值（Tm）
    //    int pMaximumTemperature = 0;    // 最高温度值（Th）
    //    int pHistogramBinNum = 0;       //直方图Bin数
    //    int[] pHistogramCount = new int[512];   //直方图Count
    //    int[] pHistogramBin = new int[512];     //直方图Bin
    //    int num = 0;
    //    if (File.Exists(fileName))
    //    {
    //        BinaryReader binReader = new BinaryReader(File.Open(fileName, FileMode.Open));
    //        try
    //        {
    //            for (int i = 0; i < g_nWidth; i++)   //读取温度矩阵
    //            {
    //                for (int j = 0; j < g_nHeight; j++)
    //                {
    //                    binReader.BaseStream.Seek(sizeof(int) * num, SeekOrigin.Begin);
    //                    pTempData[i * g_nHeight + j] = binReader.ReadInt32();
    //                    //Response.Write(pTempData[i*g_nHeight+j]);
    //                    //Response.Write("<br>");
    //                    num++;
    //                }
    //            }
    //            //读取炉管掩膜
    //            binReader.BaseStream.Seek(0, SeekOrigin.Current);
    //            binReader.Read(TubMask, 0, g_nWidth * g_nHeight);
    //            //读取虚拟热电偶的位置
    //            for (int i = 0; i < g_nVirtualThermRowNum; i++)
    //            {
    //                for (int j = 0; j < g_nVirtualThermColNum * 2; j++)
    //                {
    //                    binReader.BaseStream.Seek(0, SeekOrigin.Current);
    //                    pVirtualThermPos[i * g_nVirtualThermColNum * 2 + j] = binReader.ReadInt32();
    //                    //Response.Write(pVirtualThermPos[i * g_nVirtualThermColNum * 2 + j]);
    //                    //Response.Write("<br>");
    //                }
    //            }
    //            //读取温度文件名长度
    //            for (int i = 0; i < 38; i++)
    //            {
    //                binReader.BaseStream.Seek(0, SeekOrigin.Current);
    //                pTempFileNL[i] = binReader.ReadChar();
    //                Response.Write(pTempFileNL[i]);
    //            }
    //            Response.Write("<br>");
    //            //加热炉编号
    //            binReader.BaseStream.Seek(0, SeekOrigin.Current);
    //            pTubNum = binReader.ReadInt32();
    //            Response.Write(pTubNum);
    //            Response.Write("<br>");
    //            //整体温度值
    //            binReader.BaseStream.Seek(0, SeekOrigin.Current);
    //            pOverallTemperature = binReader.ReadInt32();
    //            Response.Write(pOverallTemperature);
    //            Response.Write("<br>");
    //            //最高温度值
    //            binReader.BaseStream.Seek(0, SeekOrigin.Current);
    //            pMaximumTemperature = binReader.ReadInt32();
    //            Response.Write(pMaximumTemperature);
    //            Response.Write("<br>");
    //            //直方图Bin数
    //            binReader.BaseStream.Seek(0, SeekOrigin.Current);
    //            pHistogramBinNum = binReader.ReadInt32();
    //            Response.Write(pHistogramBinNum);
    //            Response.Write("<hr>");
    //            //直方图Count
    //            for (int i = 0; i < pHistogramBinNum; i++)
    //            {
    //                Int64 streamNum = binReader.BaseStream.Seek(0, SeekOrigin.Current);
    //                pHistogramCount[i] = binReader.ReadInt32();
    //                Response.Write(pHistogramCount[i]);
    //                Response.Write("<br>");
    //            }
    //            Response.Write("<hr>");
    //            for (int j = 0; j < 512; j++)
    //            {
    //                Int64 streamNum2 = binReader.BaseStream.Seek(0, SeekOrigin.Current);
    //                pHistogramBin[j] = binReader.ReadInt32();
    //                Response.Write(pHistogramBin[j]);
    //                Response.Write("<br>");
    //            }


    //        }

    //        catch (EndOfStreamException e)
    //        {
    //            Console.WriteLine("{0} caught and ignored. " +
    //                "Using default values.", e.GetType().Name);
    //        }
    //        finally
    //        {
    //            binReader.Close();
    //            Response.Write("<script>alert('读取完成！')</script>");
    //        }
    //    }
    //}

}