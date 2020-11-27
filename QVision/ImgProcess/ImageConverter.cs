using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using aqrose.aidi_vision;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace aqrose.aidi_vision
{
    class ImageConverter
    {
        /**
        * @brief example for transform pbgra to bgr
        */
        public byte[] pbgra_to_bgr(byte[] bgra, int height, int width)
        {
            byte[] bgr = new byte[height * width * 3];
            for (var pos = 0; pos < height * width; pos++)
            {
                bgr[pos * 3] = bgra[pos * 4];
                bgr[pos * 3 + 1] = bgra[pos * 4 + 1];
                bgr[pos * 3 + 2] = bgra[pos * 4 + 2];
            }
            return bgr;
        }

        /**
        * @brief example for transform pbgra to bgr
        */
        public byte[] bgra_to_bgr(byte[] bgra, int height, int width)
        {
            byte[] bgr = new byte[height * width * 3];
            for (var pos = 0; pos < height * width; pos++)
            {
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

        /**
        * @brief example for transform Bitmap to byte[]
        */
        public byte[] bitmap_to_byte(ref Bitmap bmp, out int stride, out int channel_number)
        {
            var rect = new Rectangle(0, 0, bmp.Width, bmp.Height);
            var bmpData = bmp.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadOnly, bmp.PixelFormat);
            stride = bmpData.Stride;
            string bit_number = bmp.PixelFormat.ToString();
            if (bit_number.Contains("24") || bmp.PixelFormat == PixelFormat.Format32bppArgb || bmp.PixelFormat == PixelFormat.Format32bppPArgb)
            { //24位即为3通道，8位为1通道
                channel_number = 3;
            }
            else
            {
                channel_number = 1;
            }
            var rowBytes = bmpData.Width * System.Drawing.Image.GetPixelFormatSize(bmp.PixelFormat) / 8;
            var imgBytes = bmp.Height * rowBytes;
            byte[] rgbValues = new byte[imgBytes];
            IntPtr ptr = bmpData.Scan0;
            if (stride == bmpData.Width)
            {
                Marshal.Copy(ptr, rgbValues, 0, imgBytes);
            }
            else
            {
                for (var i = 0; i < bmp.Height; i++)
                {
                    Marshal.Copy(ptr, rgbValues, i * rowBytes, rowBytes);   // 对齐
                    ptr += stride; // next row
                }
            }
            if (channel_number == 1)// 8bit gray
            {
                for (var i = 0; i < imgBytes; i++)
                {
                    rgbValues[i] = bmp.Palette.Entries[rgbValues[i]].R;
                }
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

        /**
        * @brief example for transform Bitmap to aqrose.aidi_vision.Image
        */
        public void bitmap_to_aqimg(ref Bitmap bitmap, ref aqrose.aidi_vision.Image aidi_image)
        {
            int stride;
            int channel_number;
            byte[] image_bytes = bitmap_to_byte(ref bitmap, out stride, out channel_number);
            aidi_image.from_chars(image_bytes, bitmap.Height, bitmap.Width, channel_number);
        }

    }
}
