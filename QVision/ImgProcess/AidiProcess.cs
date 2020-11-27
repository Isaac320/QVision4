using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using aqrose.aidi_vision;
using Aq.Aidi;
using Google.Protobuf;
using OpenCvSharp;
using System.Threading;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Drawing.Imaging;
using System.Diagnostics;
using System.IO;

namespace QVision.ImgProcess
{
    public struct aidi_result
    {
        public int area;
        public int center_x;   //"cx":1234,
        public int center_y;   //"cy":489,
        public int height;
        public int width;
        public int TopLeft_x;
        public int TopLeft_y;
        public double score;
        public string type_name;
        public int rotate_height;
        public int rotate_width;
    }

    public struct void_result
    {
        public PointF p;   //框框左上角坐标
        public double areaKK;   //框框面积
        public double areaMax;  //最大气泡面积
        public double areaAll;  //所有气泡面积
        public double ratioMax;  //最大气泡占比
        public double ratioAll;  //所有气泡占比
    }
    


    class AidiProcess
    {
        private static string auth_code_;
        public AidiProcess(string auth_code)
        {
            auth_code_ = auth_code;
        }

        Client runner; //快速分割
        Client runner2; //检查气泡

        public bool bInit = false; //是否初始化成功


        //初始化模型
        public void aidi_factory_init2(string root_path)
        {
            //Entry.InitAlgoPlugin(plugin_path);
            bInit = false;
            runner = new Client(auth_code_);
            runner2 = new Client(auth_code_);
            StringVector save_model_path_list = new StringVector();
            StringVector operator_type_list = new StringVector();

            //获取路径下的子模型文件夹名，如Detect_0、Classify_1，将模块名（Detect,Classify）与序号（0、1）分别填入字典model_index
            DirectoryInfo root_dir = new DirectoryInfo(root_path);
            DirectoryInfo[] dir = root_dir.GetDirectories();
            int module_i = 0;
            for (int d_i = 0; d_i < dir.Length; d_i++)
            {
                if (dir[d_i].Name.IndexOf('_') > 0 && dir[d_i].Name.Split('_')[1] == module_i.ToString())
                {
                    save_model_path_list.Add(root_path + "/" + dir[d_i].Name + "/model/V1");///
                    operator_type_list.Add(dir[d_i].Name.Split('_')[0]);   //"Detection" 检测, "Location" 定位, "Classify" 分类, "Segment" 分割, "FaseLocation"快速定位
                    //module_i += 1;
                    //d_i = 0;
                }
            }

            for (int model_i = 0; model_i < save_model_path_list.Count(); model_i++)
            {
                runner.add_model_engine(operator_type_list[model_i], save_model_path_list[model_i]);
            }
            //支持多线程
            runner.add_route_engine();
            Thread.Sleep(100);

            save_model_path_list.Clear();
            operator_type_list.Clear();

            module_i = 1;
            for (int d_i = 0; d_i < dir.Length; d_i++)
            {
                if (dir[d_i].Name.IndexOf('_') > 0 && dir[d_i].Name.Split('_')[1] == module_i.ToString())
                {
                    save_model_path_list.Add(root_path + "/" + dir[d_i].Name + "/model/V1");///
                    operator_type_list.Add(dir[d_i].Name.Split('_')[0]);   //"Detection" 检测, "Location" 定位, "Classify" 分类, "Segment" 分割, "FaseLocation"快速定位
                    module_i += 1;
                    d_i = 0;
                }
            }

            for (int model_i = 0; model_i < save_model_path_list.Count(); model_i++)
            {
                runner2.add_model_engine(operator_type_list[model_i], save_model_path_list[model_i]);
            }
            //支持多线程
            runner2.add_route_engine();
            Thread.Sleep(100);
            bInit = true;
        }

        public List<void_result> aidi_factory_runner2(Bitmap image_bmp, out Bitmap result_bmp)
        {
            result_bmp = null;
            //判断是否是彩色图片
            int stride;
            int channel_number;
            aqrose.aidi_vision.Image image = new aqrose.aidi_vision.Image();

            //Console.WriteLine("channel_number: " + channel_number);

            byte[] image_bytes = GetBGRValues(image_bmp, out stride, out channel_number);
            image.from_chars(image_bytes, image_bmp.Height, image_bmp.Width, channel_number);

            //BatchImage images = new BatchImage();
            //images.Add(image);

            List<void_result> all_result = new List<void_result>();

           // Stopwatch sw = new Stopwatch();
           // sw.Start();

            //ulong id = runner.add_images(images);
            ulong id = runner.add_images(image);
            BatchLabelIO results = new BatchLabelIO();

            results = runner.wait_get_result(ref id);

            //sw.Stop();
            //TimeSpan ts2 = sw.Elapsed;
            //Console.WriteLine("单图推理时间：{0}", ts2.TotalMilliseconds);
           // Console.WriteLine("结果个数：{0}", results.Count);

            aqrose.aidi_vision.BatchImage images2 = new BatchImage();

            LabelIO tempresult = results[0];
            images2 = image.crop(tempresult, true);

            //转换结果字节流到byte[]
            byte[] data_bytes11 = new byte[tempresult.size()];
            tempresult.data(data_bytes11, tempresult.size());
            //解析结果字节流
            Label label11 = Label.Parser.ParseFrom(data_bytes11);
            //打印结果
            //Console.WriteLine("label:" + label11.ToString());
            //Console.WriteLine("kk个数:" + label11.Regions.Count);

            result_bmp = (Bitmap)image_bmp.Clone();

            int numImage = label11.Regions.Count;

            //有几个框循环几次
            for (int k = 0; k < numImage; k++)
            {

                void_result void_re = new void_result();  //其中一个框框的结果

                Aq.Aidi.Region s_region2 = label11.Regions[k];
                Aq.Aidi.Polygon s_poly = s_region2.Polygon;
                Aq.Aidi.Ring outer = s_poly.Outer;

                float offsetX = outer.Points[0].X;
                float offsetY = outer.Points[0].Y;

                float x2 = outer.Points[2].X;
                float y2 = outer.Points[2].Y;

                float areatmp = (x2 - offsetX) * (y2 - offsetY);

                void_re.p = new PointF(offsetX, offsetY);   //kk左上角左边
                void_re.areaAll = areatmp;                  //kk总面积
              

                ulong id2 = runner2.add_images(images2[k]);
                BatchLabelIO results2 = new BatchLabelIO();

                results2 = runner2.wait_get_result(ref id2);
                
                LabelIO result2 = results2[0];
                

                //转换结果字节流到byte[]
                byte[] data_bytes = new byte[result2.size()];
                result2.data(data_bytes, result2.size());
                //解析结果字节流
                Label label = Label.Parser.ParseFrom(data_bytes);
              


                List<double> areaList = new List<double>();

                if (label.ToString().Contains("datasetType") && label.ToString().Contains("regions"))
                {
                   
                    for (int Region_i = 0; Region_i < label.Regions.Count(); Region_i++)
                    {
                        Aq.Aidi.Region s_region = label.Regions[Region_i];
                        Aq.Aidi.Polygon s_poly2 = s_region.Polygon;
                        Aq.Aidi.Ring outer2 = s_poly2.Outer;
                        List<OpenCvSharp.Point> points = new List<OpenCvSharp.Point>();
                        for (int j = 0; j < outer2.Points.Count(); j++)
                        {
                            points.Add(new OpenCvSharp.Point(outer2.Points[j].X, outer2.Points[j].Y));
                        }
                        double area = Cv2.ContourArea(points);
                        areaList.Add(area);//存面积                       
                    }
                }  
                
                //分析获得的面积 找最大面积 总面积




                //将此次结果画在图上 气泡 和占比



                if (all_result.Count() > 0)
                {
                    //result_bmp = rendering(image_bmp, all_result);
                    result_bmp = drawKK(result_bmp, label, offsetX, offsetY);
                    result_bmp = drawFont(result_bmp, "测试", offsetX, offsetY);
                }


                else
                    result_bmp = image_bmp;
            }

            //画外框
            result_bmp = drawKK(result_bmp, label11);

            //分析结果，标上序号


            return all_result;
        }


        private void showPoint(Label label)
        {
            for (int i = 0; i < label.Regions.Count(); i++)
            {
                Aq.Aidi.Region s_region = label.Regions[i];
                Aq.Aidi.Polygon s_poly = s_region.Polygon;
                Aq.Aidi.Ring outer = s_poly.Outer;
                int num = outer.Points.Count();
                for (int j = 0; j < num; j++)
                {
                    Console.WriteLine("X:{0},Y:{1}", outer.Points[j].X, outer.Points[j].Y);
                }
            }
        }

        private Bitmap drawKK(Bitmap image_bmp, Label label, float offsetX = 0, float offsetY = 0)
        {
            Bitmap result_bmp = new Bitmap(image_bmp);
            Pen pen = new Pen(Color.Red);
            pen.Width = 1;
            for (int i = 0; i < label.Regions.Count(); i++)
            {
                Aq.Aidi.Region s_region = label.Regions[i];
                Aq.Aidi.Polygon s_poly = s_region.Polygon;
                Aq.Aidi.Ring outer = s_poly.Outer;
                int num = outer.Points.Count();
                System.Drawing.PointF[] points = new System.Drawing.PointF[num];
                for (int j = 0; j < num; j++)
                {
                    points[j].X = outer.Points[j].X + offsetX;
                    points[j].Y = outer.Points[j].Y + offsetY;
                }
                Graphics g1 = Graphics.FromImage(result_bmp);
                g1.DrawPolygon(pen, points);
            }
            return result_bmp;
        }


        private Bitmap drawFont(Bitmap image_bmp, string s, float offsetX, float offsetY)
        {
            Bitmap result_bmp = new Bitmap(image_bmp);
            Pen pen = new Pen(Color.Red);
            SolidBrush drawBush = new SolidBrush(Color.Red);
            pen.Width = 1;
            Graphics g1 = Graphics.FromImage(result_bmp);
            g1.DrawString(s, new Font("", 12, FontStyle.Bold), drawBush, new PointF(offsetX, offsetY));

            return result_bmp;
        }


        //给输出的图片缺陷处标上外框
        private Bitmap rendering(Bitmap image_bmp, List<aidi_result> all_result)
        {
            Bitmap result_bmp = new Bitmap(image_bmp);
            //边框的长度和颜色

            Pen pen = new Pen(Color.Red);
            pen.Width = 5;
            int expend = 10;
            //循环后将所有缺陷都加上外框
            for (int item = 0; item < all_result.Count; item++)
            {
                //缺陷的宽度
                int defect_width;
                //外框宽度是否膨胀进行判定
                if (all_result[item].center_x + all_result[item].width + expend > image_bmp.Width)
                {
                    defect_width = 2 * (image_bmp.Width - all_result[item].center_x);
                }
                else
                {
                    defect_width = all_result[item].width + expend * 2;
                }
                //缺陷的高度
                int defect_height;
                //外框高度是否膨胀进行判定

                if (all_result[item].center_y + all_result[item].height + expend * 3 > image_bmp.Height)
                {
                    defect_height = 2 * (image_bmp.Height - all_result[item].center_y);
                }
                else
                {
                    defect_height = all_result[item].height + expend * 2;
                }

                Graphics g1 = Graphics.FromImage(result_bmp);

                g1.DrawRectangle(pen, new Rectangle(new System.Drawing.Point(all_result[item].center_x - expend, all_result[item].center_y - expend), new System.Drawing.Size(defect_width, defect_height)));
            }
            //image_bmp.Save(@"D:\ls文件\616\C#示例工程 - 副本 (2)\result.bmp", ImageFormat.Tiff);
            return result_bmp;
        }

        private void analysis_result(Aq.Aidi.Region s_region, out aidi_result aidi_re)
        {
            aidi_result result = new aidi_result();
            //获取轮廓信息
            Polygon s_polygon = s_region.Polygon;
            Ring outer = s_polygon.Outer;
            List<OpenCvSharp.Point> points = new List<OpenCvSharp.Point>();
            for (int point_i = 0; point_i < outer.Points.Count(); point_i++)
            {
                points.Add(new OpenCvSharp.Point(outer.Points[point_i].X, outer.Points[point_i].Y));
            }

            double area = Cv2.ContourArea(points);
            //最小外接矩形
            RotatedRect r_rect = Cv2.MinAreaRect(points);
            //正外接矩形
            Rect rect = Cv2.BoundingRect(points);

            //Console.WriteLine("缺陷类别：{0}, 缺陷面积：{1}, 最小外接矩形宽：{2}，高：{3}，中心点:{4},{5}",
            //s_region.Name, area, r_rect.Size.Width, r_rect.Size.Height, r_rect.Center.X, r_rect.Center.Y);
            result.area = Convert.ToInt32(area);
            result.center_x = rect.X;
            result.center_y = rect.Y;
            result.height = rect.Height;
            result.width = rect.Width;
            result.score = 1;
            result.rotate_height = Convert.ToInt32(r_rect.Size.Height);
            result.rotate_width = Convert.ToInt32(r_rect.Size.Width);
            result.TopLeft_x = rect.TopLeft.X;
            result.TopLeft_y = rect.TopLeft.Y;
            aidi_re = result;
        }

        private static byte[] pbgra_to_bgr(byte[] bgra, int height, int width)
        {
            byte[] bgr = new byte[height * width * 3];
            for (var pbgr_i = 0; pbgr_i < height * width; pbgr_i++)
            {
                var i = pbgr_i / width;
                var j = pbgr_i % width;
                var pos = i * width + j;
                bgr[pos * 3] = bgra[pos * 4];
                bgr[pos * 3 + 1] = bgra[pos * 4 + 1];
                bgr[pos * 3 + 2] = bgra[pos * 4 + 2];
            }
            return bgr;
        }
        private static byte[] bgra_to_bgr(byte[] bgra, int height, int width)
        {
            byte[] bgr = new byte[height * width * 3];
            for (var bgra_i = 0; bgra_i < height * width; bgra_i++)
            {
                var i = bgra_i / width;
                var j = bgra_i % width;
                var pos = i * width + j;
                byte byte_scale = bgra[pos * 4 + 3];
                if (byte_scale == 255)
                {
                    bgr[pos * 3] = bgra[pos * 4];
                    bgr[pos * 3 + 1] = bgra[pos * 4 + 1];
                    bgr[pos * 3 + 2] = bgra[pos * 4 + 2];
                }
                else
                {
                    float scale = (float)bgra[pos * 4 + 3] / 255.0f;
                    if (scale < 0)
                    {
                        scale = 0;
                    }
                    if (scale > 1)
                    {
                        scale = 1;
                    }
                    float b = (float)bgra[pos * 4] * scale;
                    float g = (float)bgra[pos * 4 + 1] * scale;
                    float r = (float)bgra[pos * 4 + 2] * scale;
                    bgr[pos * 3] = (byte)b;
                    bgr[pos * 3 + 1] = (byte)g;
                    bgr[pos * 3 + 2] = (byte)r;
                }
            }
            return bgr;
        }

        private static byte[] GetBGRValues(Bitmap bmp, out int stride, out int channel_number)
        {
            var rect = new Rectangle(0, 0, bmp.Width, bmp.Height);
            var bmpData = bmp.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadOnly, bmp.PixelFormat);
            stride = bmpData.Stride;
            string bit_number = bmp.PixelFormat.ToString();
            if (bit_number.Contains("24") || bmp.PixelFormat == PixelFormat.Format32bppArgb || bmp.PixelFormat == PixelFormat.Format32bppPArgb)
            { //24位即为3通道，8位为1通道
                //Console.WriteLine("bit_number = 3");
                channel_number = 3;
            }
            else
                channel_number = 1;
            //int channel = bmpData.Stride / bmp.Width; 
            var rowBytes = bmpData.Width * System.Drawing.Image.GetPixelFormatSize(bmp.PixelFormat) / 8;
            //Console.WriteLine(rowBytes);
            var imgBytes = bmp.Height * rowBytes;
            byte[] rgbValues = new byte[imgBytes];
            IntPtr ptr = bmpData.Scan0;
            for (var i = 0; i < bmp.Height; i++)
            {
                Marshal.Copy(ptr, rgbValues, i * rowBytes, rowBytes);   // 对齐
                ptr += bmpData.Stride; // next row
            }
            bmp.UnlockBits(bmpData);
            if (bmp.PixelFormat == PixelFormat.Format32bppPArgb)
            {
                rgbValues = pbgra_to_bgr(rgbValues, bmp.Height, bmp.Width);
            }
            else if (bmp.PixelFormat == System.Drawing.Imaging.PixelFormat.Format32bppArgb)
            {
                rgbValues = bgra_to_bgr(rgbValues, bmp.Height, bmp.Width);
            }
            return rgbValues;
        }



        //通过左上角的坐标，以及y行，x列，来给他们排序
        private bool SortPS(PointF[] ps, out int[] outIndex, int x, int y)
        {
            int num = ps.Count();
            outIndex = new int[num];
            if (x * y != num)
                return false;   //排序未成功
            PointF[] tempP = new PointF[num];
            for (int i = 0; i < num; i++)
            {
                outIndex[i] = i;
                tempP[i] = ps[i];
            }


            //冒泡排序，已y坐标排
            for (int i = 1; i < num; i++)
            {
                for (int j = 0; j < num - i; j++)
                {
                    if (tempP[j].Y > tempP[j + 1].Y)
                    {
                        PointF temp = tempP[j];
                        tempP[j] = tempP[j + 1];
                        tempP[j + 1] = temp;

                        int tt = outIndex[j];
                        outIndex[j] = outIndex[j + 1];
                        outIndex[j + 1] = tt;
                    }
                }
            }

            for (int k = 0; k < y; k++)
            {
                for (int i = 1; i < x; i++)
                {
                    for (int j = k * x; j < x - i + k * x; j++)
                    {
                        if (tempP[j].X > tempP[j + 1].X)
                        {
                            PointF temp = tempP[j];
                            tempP[j] = tempP[j + 1];
                            tempP[j + 1] = temp;

                            int tt = outIndex[j];
                            outIndex[j] = outIndex[j + 1];
                            outIndex[j + 1] = tt;
                        }
                    }
                }
            }
            return true;

        }

        //找最大数
        private double FindMax(List<double> list)
        {
            double max = 0;
            int num = list.Count;
            for (int i = 0; i < num; i++)
            {
                if (list[i] > max)
                {
                    max = list[i];
                }
            }
            return max;
        }


        //求和
        private double SumAll(List<double> list)
        {
            double sum = 0;
            int num = list.Count;
            for (int i = 0; i < num; i++)
            {
                sum = sum + list[i];
            }
            return sum;
        }


    }


}
}
