using System;
using iTextSharp.tool.xml.pipeline.html;

namespace BSharp.Core.RazorToPdf
{
    public abstract class ImageTransform
    {
        internal abstract IImageProvider GetImageProvider();
    }
}