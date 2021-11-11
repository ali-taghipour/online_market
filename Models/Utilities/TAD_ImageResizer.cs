using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Threading.Tasks;

namespace TAD_ImageResizer
{
    public class ImageResizer
    {
        private static ImageCodecInfo jpgEncoder;



        //-------------------------------------------//
        //             only resize RESIZE            //
        //-------------------------------------------//
        public async static Task OnlyResizeImage(string inFile, string outFile,
           int ResizeMaxWidth, int ResizeMaxHeight, long level)
        {
            byte[] buffer;
            using (Stream stream = new FileStream(inFile, FileMode.Open))
            {
                buffer = new byte[stream.Length];
                await Task<int>.Factory.FromAsync(stream.BeginRead, stream.EndRead,
                buffer, 0, buffer.Length, null);
            }
            using (MemoryStream memStream = new MemoryStream(buffer))
            {
                using (Image inImage = Image.FromStream(memStream))
                {

                    double width;
                    double height;
                    double InRatio = (double)inImage.Width / inImage.Height;
                    double OutRatio = (double)ResizeMaxWidth / ResizeMaxHeight;

                    //if (inImage.Height < inImage.Width)
                    //{
                    //    width = maxDimension;
                    //    height = (maxDimension / (double)inImage.Width) * inImage.Height;
                    //}
                    //else
                    //{
                    //    height = maxDimension;
                    //    width = (maxDimension / (double)inImage.Height) * inImage.Width;
                    //}

                    if (inImage.Width > ResizeMaxWidth || inImage.Height > ResizeMaxHeight)
                    {
                        if (inImage.Width > ResizeMaxWidth && inImage.Height > ResizeMaxHeight)
                        {
                            if (OutRatio < InRatio)
                            {
                                width = ResizeMaxWidth;
                                height = width / InRatio;
                            }
                            else
                            {
                                height = ResizeMaxHeight;
                                width = height * InRatio;
                            }
                        }
                        else
                        {
                            if (inImage.Width > ResizeMaxWidth)
                            {
                                width = ResizeMaxWidth;
                                height = width / InRatio;
                            }
                            else
                            {
                                height = ResizeMaxHeight;
                                width = height * InRatio;
                            }
                        }


                        using (Bitmap bitmap = new Bitmap((int)width, (int)height))
                        {
                            using (Graphics graphics = Graphics.FromImage(bitmap))
                            {

                                graphics.SmoothingMode = SmoothingMode.HighQuality;
                                graphics.InterpolationMode =
                                InterpolationMode.HighQualityBicubic;
                                graphics.DrawImage(inImage, 0, 0, bitmap.Width, bitmap.Height);

                                if (inImage.RawFormat.Guid == ImageFormat.Jpeg.Guid)
                                {
                                    if (jpgEncoder == null)
                                    {
                                        ImageCodecInfo[] ici =
                                        ImageCodecInfo.GetImageDecoders();
                                        foreach (ImageCodecInfo info in ici)
                                        {
                                            if (info.FormatID == ImageFormat.Jpeg.Guid)
                                            {
                                                jpgEncoder = info;
                                                break;
                                            }
                                        }
                                    }
                                    if (jpgEncoder != null)
                                    {
                                        EncoderParameters ep = new EncoderParameters(1);
                                        ep.Param[0] = new EncoderParameter(Encoder.Quality,
                                        level);
                                        bitmap.Save(outFile, jpgEncoder, ep);
                                    }
                                    else
                                        bitmap.Save(outFile, inImage.RawFormat);
                                }
                                else
                                {
                                    //
                                    // Fill with white for transparent GIFs
                                    //
                                    graphics.DrawImage(inImage, 0, 0, bitmap.Width, bitmap.Height);
                                    bitmap.Save(outFile, inImage.RawFormat);
                                }
                            }
                        }
                    }
                }
            }
        }




        //-------------------------------------------//
        //              resize RESIZE                 //
        //-------------------------------------------//
        public async static Task ResizeImage(string inFile, string outFile,
        double maxDimension, long level)
        {
            byte[] buffer;
            using (Stream stream = new FileStream(inFile, FileMode.Open))
            {
                buffer = new byte[stream.Length];
                await Task<int>.Factory.FromAsync(stream.BeginRead, stream.EndRead,
                buffer, 0, buffer.Length, null);
            }
            using (MemoryStream memStream = new MemoryStream(buffer))
            {
                using (Image inImage = Image.FromStream(memStream))
                {

                    double width;
                    double height;
                    if (inImage.Height < inImage.Width)
                    {
                        width = maxDimension;
                        height = (maxDimension / (double)inImage.Width) * inImage.Height;
                    }
                    else
                    {
                        height = maxDimension;
                        width = (maxDimension / (double)inImage.Height) * inImage.Width;
                    }
                    using (Bitmap bitmap = new Bitmap((int)width, (int)height))
                    {
                        using (Graphics graphics = Graphics.FromImage(bitmap))
                        {

                            graphics.SmoothingMode = SmoothingMode.HighQuality;
                            graphics.InterpolationMode =
                            InterpolationMode.HighQualityBicubic;
                            graphics.DrawImage(inImage, 0, 0, bitmap.Width, bitmap.Height);

                            if (inImage.RawFormat.Guid == ImageFormat.Jpeg.Guid)
                            {
                                if (jpgEncoder == null)
                                {
                                    ImageCodecInfo[] ici =
                                    ImageCodecInfo.GetImageDecoders();
                                    foreach (ImageCodecInfo info in ici)
                                    {
                                        if (info.FormatID == ImageFormat.Jpeg.Guid)
                                        {
                                            jpgEncoder = info;
                                            break;
                                        }
                                    }
                                }
                                if (jpgEncoder != null)
                                {
                                    EncoderParameters ep = new EncoderParameters(1);
                                    ep.Param[0] = new EncoderParameter(Encoder.Quality,
                                    level);
                                    bitmap.Save(outFile, jpgEncoder, ep);
                                }
                                else
                                    bitmap.Save(outFile, inImage.RawFormat);
                            }
                            else
                            {
                                //
                                // Fill with white for transparent GIFs
                                //
                                graphics.DrawImage(inImage, 0, 0, bitmap.Width, bitmap.Height);
                                bitmap.Save(outFile, inImage.RawFormat);
                            }
                        }
                    }
                }
            }
        }



        //-------------------------------------------//
        //              CROP RESIZE                 //
        //-------------------------------------------//

        public async static Task ResizeImage(string inFile, string outFile,
           int CropWidth, int CropHeight, long level)
        {
            byte[] buffer;
            using (Stream stream = new FileStream(inFile, FileMode.Open))
            {
                buffer = new byte[stream.Length];
                await Task<int>.Factory.FromAsync(stream.BeginRead, stream.EndRead,
                buffer, 0, buffer.Length, null);
            }
            using (MemoryStream memStream = new MemoryStream(buffer))
            {
                using (Image inImage = Image.FromStream(memStream))
                {
                    int width = CropWidth;
                    int marginLeft = 0;
                    int marginTop = 0;
                    float ac = (float)inImage.Width / (float)inImage.Height;
                    int height = Convert.ToInt16(width / ac);

                    if (width > height)
                    {
                        height = CropHeight;
                        width = Convert.ToInt16(height * ac);
                        marginLeft = (width - CropWidth) / 2;
                    }
                    else if (height > width)
                    {
                        marginTop = (height - CropHeight) / 2;
                    }
                    using (Bitmap bitmap = new Bitmap((int)width, (int)height))
                    {
                        using (Graphics graphics = Graphics.FromImage(bitmap))
                        {
                            graphics.SmoothingMode = SmoothingMode.HighQuality;
                            graphics.InterpolationMode =
                            InterpolationMode.HighQualityBicubic;

                            Rectangle crop = new Rectangle(marginLeft, marginTop, CropWidth, CropHeight);

                            Graphics gr = Graphics.FromImage(bitmap);

                            gr.DrawImage(inImage, 0, 0, width, height);

                            Bitmap pic2 = new Bitmap(crop.Width, crop.Height);
                            Graphics gr2 = Graphics.FromImage(pic2);
                            gr2.SmoothingMode = SmoothingMode.HighQuality;
                            gr2.InterpolationMode = InterpolationMode.HighQualityBicubic;
                            gr2.DrawImage(bitmap, new Rectangle(0, 0, crop.Width, crop.Height), crop, GraphicsUnit.Pixel);

                            if (inImage.RawFormat.Guid == ImageFormat.Jpeg.Guid)
                            {
                                if (jpgEncoder == null)
                                {
                                    ImageCodecInfo[] ici =
                                    ImageCodecInfo.GetImageDecoders();
                                    foreach (ImageCodecInfo info in ici)
                                    {
                                        if (info.FormatID == ImageFormat.Jpeg.Guid)
                                        {
                                            jpgEncoder = info;
                                            break;
                                        }
                                    }
                                }
                                if (jpgEncoder != null)
                                {
                                    EncoderParameters ep = new EncoderParameters(1);
                                    ep.Param[0] = new EncoderParameter(Encoder.Quality,
                                    level);
                                    pic2.Save(outFile, jpgEncoder, ep);
                                }
                                else
                                    pic2.Save(outFile, inImage.RawFormat);
                            }
                            else
                            {
                                //
                                // Fill with white for transparent GIFs
                                //
                                graphics.DrawImage(inImage, 0, 0, bitmap.Width, bitmap.Height);
                                pic2.Save(outFile, inImage.RawFormat);
                            }
                        }
                    }
                }
            }
        }
    }
}