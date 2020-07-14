using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QVision.Params
{
    class Global
    {
        public const string ToolName1 = "预填参数";
        public const string ToolName2 = "匹配工具";
        public const string ToolName3 = "线检测工具";


        public static string LotNum = "xxx";
        public static string Device = "xxx";
        public static string OperatorID = "xxx";
        public static int TotalFrame = 10;
        public static string RecipeName = "xxx";


        //public static string ConfigPath = @"D:\x_ray\Config";
        public static string TempImagePath = @"D:\x_ray\tempImage";
        public static string SaveImagePath = @"D:\x_ray\save";
        public static string XRayImagePath = @"\\192.168.0.50\Image\test";  //X_Ray机器上图片地址

        public static string RecipePath = @"D:\x_ray\recipe";
        public static string InkPointPath = @"D:\x_ray\inkPoint";   //点墨文件位置

        public static string LotSummaryPath = @"D:\x_ray\lotSummary"; //LotSummary位置，就是csv文件

        public static bool mySwitch1 = false;  //用来在图像处理中当开关用,单步运行还是连续运行
        public static bool mySwitch2 = false;  //开关2 上面一样的作用

        public static int FrameNum = 0; //当前的检测过的frame数量
        public static int FrameImageNum = 42; //每个frame上图像的个数
        public static int FrameNumPerTray = 12; //每个托盘有几个frame
        public static int ImageRegionNum = 6;   //每张图像的region数量

        public static int ImageXNum = 3; //每张图上横向几个产品
        public static int ImageYNum = 2; //每张图纵向有几个产品

        public static int FrameXNum = 6; //每个Frame横向几个产品
        public static int FrameYNum = 42;//每个Frame纵向几个产品


        public static int TrayXNum = 2;  //托盘行数
        public static int TrayYNum = 6;  //托盘列数

        public static string TrayID = "qqq";

        public static MachineState mMState = MachineState.Free;
        public static PauseState mPState = PauseState.Run;



        public static bool ready2Go = false; //判断是否都准备好，可以跑了

        public static List<List<int[]>> AllFrameReslut = new List<List<int[]>>();  //所有Lot的结果，就是把每次TrayResult的list都存到这里面来。


        public static string WorkProgressLabel = "空闲状态";  //工作内容
        public static int WorkProgressNum = 0;         //工作进度

        public static string lotGUID = "";
        public static DateTime startTime;
        public static DateTime endTime;
        public static DateTime endLotTime;
        public static string attrib = null;
        public static string runAttrib = null;
        public static string reportType = null;

        public static int trayIndex = 1;
        public static int frameIndex = 1;
        public string unitContent = null;

    }
}
