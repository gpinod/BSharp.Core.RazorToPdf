using BSharp.Core.RazorToPdf;
using iTextSharp.text;
using iTextSharp.tool.xml.pipeline.html;
using System;
using System.IO;

namespace BSharp.Core.RazorToPdf
{
    /// <summary>
    /// Represents the option to transform a image with an embeded image in base64 to pdf
    /// </summary>
    public class Base64ImageTransform : ImageTransform
    {
        internal override IImageProvider GetImageProvider()
        {
            return new Base64ImageProvider();
        }

        private class Base64ImageProvider : AbstractImageProvider
        {
            public override string GetImageRootPath()
            {
                return null;
            }

            public override Image Retrieve(string src)
            {
                int pos = src.IndexOf("base64,");
                try
                {
                    if (src.StartsWith("data") && pos > 0)
                    {
                        byte[] img = Convert.FromBase64String(src.Substring(pos + 7));
                        return Image.GetInstance(img);
                    }
                    else
                    {
                        return Image.GetInstance(src);
                    }
                }
                catch (BadElementException)
                {
                    return null;
                }
                catch (IOException)
                {
                    return null;
                }
            }
        }
    }
}
