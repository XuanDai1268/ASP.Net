using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

//图像绘制
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

public partial class ShowImage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //string str_ImagePath = Request.Cookies["myCookie"].Value;
        //Application.Lock();
        //string str_ImagePath = Application["name"].ToString();
        //Application.UnLock();
        inPutDatToShowBmp("D:\\DataBase\\F01C02\\Image\\2016-09-18 00-39-36-F01C02-B.dat");
    }
    public void inPutDatToShowBmp(String filename)
    {
        int width = 768;
        int height = 576;
        byte temp = 0;
        FileStream fs = new FileStream(filename, FileMode.Open);
        byte[] array = new byte[fs.Length];
        fs.Read(array, 0, array.Length);
        fs.Close();
        if (array == null || array.Length == 0)
        {
            //当没有图片数据时显示默认的图片nophoto.gif
            FileStream fse = new FileStream(Server.MapPath("~/image/Show.bmp"), FileMode.Open, FileAccess.Read);
            byte[] mydata = new byte[fs.Length];
            int Length = (int)(fse.Length);
            fs.Read(mydata, 0, Length);
            fs.Close();
            this.Response.OutputStream.Write(mydata, 0, Length);
            this.Response.End();
        }
        else
        {
            for (int i = 0; i < height / 2; i++)
            {
                for (int j = 0; j < width * 2; j++)
                {
                    temp = array[i * width * 2 + j];
                    array[i * width * 2 + j] = array[(height - 1 - i) * width * 2 + j];
                    array[(height - 1 - i) * width * 2 + j] = temp;
                }
            }
            System.Drawing.Bitmap img = Convert(array, width, height, 10);
            System.Drawing.Graphics graphics = System.Drawing.Graphics.FromImage(img);

            System.Drawing.Font font = new System.Drawing.Font("宋体", 36); //字体与大小
            System.Drawing.Brush brush = new System.Drawing.SolidBrush(System.Drawing.Color.Red);
            graphics.DrawString("代轩", font, brush, 150, 150); //写字，最后两个参数表示位置



            //将图片保存到内存流中
            System.IO.MemoryStream stream = new System.IO.MemoryStream();
            img.Save(stream, System.Drawing.Imaging.ImageFormat.Bmp);

            //在页面上输出
            Response.Clear();
            Response.ContentType = "image/bmp";
            Response.BinaryWrite(stream.ToArray());

            stream.Close();
            stream.Dispose();

            graphics.Dispose();
            img.Dispose();

            Response.End();
        }
    }
    static Bitmap Convert(byte[] input, int width, int height, int bits)
    {
        // Convert byte buffer (2 bytes per pixel) to 32-bit ARGB bitmap
        var bitmap = new Bitmap(width, height, PixelFormat.Format32bppArgb);
        var rect = new Rectangle(0, 0, width, height);

        var lut = CreateLut(bits);
        var bitmap_data = bitmap.LockBits(rect, ImageLockMode.WriteOnly, bitmap.PixelFormat);
        ConvertCore(width, height, bits, input, bitmap_data, lut);
        bitmap.UnlockBits(bitmap_data);
        return bitmap;
    }
    static unsafe void ConvertCore(int width, int height, int bits, byte[] input, BitmapData output, uint[] lut)
    {
        // Copy pixels from input to output, applying LUT
        ushort mask = (ushort)((1 << bits) - 1);
        int in_stride = output.Stride;
        int out_stride = width * 2;
        byte* out_data = (byte*)output.Scan0;
        fixed (byte* in_data = input)
        {
            for (int y = 0; y < height; y++)
            {
                uint* out_row = (uint*)(out_data + (y * in_stride));
                ushort* in_row = (ushort*)(in_data + (y * out_stride));
                for (int x = 0; x < width; x++)
                {
                    ushort in_pixel = (ushort)(in_row[x] & mask);
                    out_row[x] = lut[in_pixel];
                }
            }
        }
    }
    static uint[] CreateLut(int bits)
    {
        // Create a linear LUT to convert from grayscale to ARGB
        int max_input = 1 << bits;
        uint[] lut = new uint[max_input];
        for (int i = 0; i < max_input; i++)
        {
            // map input value to 8-bit range
            byte intensity = (byte)((i * 0xFF) / max_input);
            // create ARGB output value A=255, R=G=B=intensity
            lut[i] = (uint)(0xFF000000L | (intensity * 0x00010101L));
        }
        return lut;
    }
    static byte[] Wedge(int width, int height, int bits)
    {
        // horizontal wedge
        int max = 1 << bits;
        byte[] pixels = new byte[width * height * 2];
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                int pixel = x % max;
                int addr = ((y * width) + x) * 2;
                pixels[addr + 1] = (byte)((pixel & 0xFF00) >> 8);
                pixels[addr + 0] = (byte)((pixel & 0x00FF));
            }
        }
        return pixels;
    }
    
}