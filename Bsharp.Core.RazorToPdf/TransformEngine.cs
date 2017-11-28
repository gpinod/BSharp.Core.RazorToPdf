using BSharp.Core.RazorToPdf.Utils;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;
using iTextSharp.tool.xml.html;
using iTextSharp.tool.xml.parser;
using iTextSharp.tool.xml.pipeline.css;
using iTextSharp.tool.xml.pipeline.end;
using iTextSharp.tool.xml.pipeline.html;
using RazorEngine;
using RazorEngine.Templating;
using System.IO;

namespace BSharp.Core.RazorToPdf
{
    /// <summary>
    /// Implementation of <see cref="ITransformEngine"/>
    /// </summary>
    public class TransformEngine : ITransformEngine
    {
        private PdfOptions _options;

        /// <summary>
        /// Create an instance of <see cref="TransformEngine"/>
        /// </summary>
        public TransformEngine()
            : this(new PdfOptions())
        {
        }

        /// <summary>
        /// Create an instance of <see cref="TransformEngine"/> customizing the transformation of pdf
        /// </summary>
        /// <param name="options">Represents the customization options to transform pdf</param>
        public TransformEngine(PdfOptions options)
        {
            _options = options;
        }

        /// <summary>
        /// <see cref="ITransformEngine"/>
        /// </summary>
        /// <typeparam name="TModel"><see cref="ITransformEngine"/></typeparam>
        /// <param name="templateContent"><see cref="ITransformEngine"/></param>
        /// <param name="key"><see cref="ITransformEngine"/></param>
        /// <param name="model"><see cref="ITransformEngine"/></param>
        /// <returns><see cref="ITransformEngine"/></returns>
        public byte[] Transform<TModel>(string templateContent, string key, TModel model)
        {
            Guard.CheckIsNull(model, nameof(model));
            string html;
            if (!Engine.Razor.IsTemplateCached(key, typeof(TModel)))
            {
                html = Engine.Razor.RunCompile(templateContent, key, typeof(TModel), model);
            }
            else
            {
                html = Engine.Razor.Run(key, typeof(TModel), model);
            }
            return ConvertToPdf(html);
        }

        /// <summary>
        /// <see cref="ITransformEngine"/>
        /// </summary>
        /// <typeparam name="TModel"><see cref="ITransformEngine"/></typeparam>
        /// <param name="templatePath"><see cref="ITransformEngine"/></param>
        /// <param name="key"><see cref="ITransformEngine"/></param>
        /// <param name="model"><see cref="ITransformEngine"/></param>
        /// <returns><see cref="ITransformEngine"/></returns>

        public byte[] TransformFromFile<TModel>(string templatePath, string key, TModel model)
        {
            string templateContent = File.ReadAllText(templatePath);
            return Transform(templateContent, key, model);
        }

        /// <summary>
        /// <see cref="ITransformEngine"/>
        /// </summary>
        /// <typeparam name="TModel"><see cref="ITransformEngine"/></typeparam>
        /// <param name="templateContent"><see cref="ITransformEngine"/></param>
        /// <param name="key"><see cref="ITransformEngine"/></param>
        /// <param name="model"><see cref="ITransformEngine"/></param>
        /// <param name="targetPath"><see cref="ITransformEngine"/></param>
        public void TransformAndSave<TModel>(string templateContent, string key, TModel model, string targetPath)
        {
            byte[] bytes = Transform(templateContent, key, model);
            File.WriteAllBytes(targetPath, bytes);
        }

        /// <summary>
        /// <see cref="ITransformEngine"/>
        /// </summary>
        /// <typeparam name="TModel"><see cref="ITransformEngine"/></typeparam>
        /// <param name="templatePath"><see cref="ITransformEngine"/></param>
        /// <param name="key"><see cref="ITransformEngine"/></param>
        /// <param name="model"><see cref="ITransformEngine"/></param>
        /// <param name="targetPath"><see cref="ITransformEngine"/></param>
        public void TransformFromFileAndSave<TModel>(string templatePath, string key, TModel model, string targetPath)
        {
            byte[] bytes = TransformFromFile(templatePath, key, model);
            File.WriteAllBytes(targetPath, bytes);
        }

        private byte[] ConvertToPdf(string html)
        {
            byte[] bytes;
            using (MemoryStream ms = new MemoryStream())
            {
                CreateDocumentAndTransform(html, ms);
                bytes = ms.ToArray();
            }
            return bytes;
        }

        private void CreateDocumentAndTransform(string html, MemoryStream ms)
        {
            using (Document doc = new Document())
            {
                WriteToDocument(html, ms, doc);
            }
        }

        private void WriteToDocument(string html, MemoryStream ms, Document doc)
        {
            using (var writer = PdfWriter.GetInstance(doc, ms))
            {
                doc.Open();
                ParseXHtml(html, doc, writer);

                doc.Close();
            }
        }

        private void ParseXHtml(string content, Document doc, PdfWriter writer)
        {
            using (var srHtml = new StringReader(content))
            {
                XMLWorkerHelper xmlWorkerHelper = XMLWorkerHelper.GetInstance();

                ICSSResolver cssResolver = xmlWorkerHelper.GetDefaultCssResolver(true);

                HtmlPipelineContext htmlContext = new HtmlPipelineContext(null);
                htmlContext.SetTagFactory(Tags.GetHtmlTagProcessorFactory());
                if (_options.ImageTransform != null)
                {
                    htmlContext.SetImageProvider(_options.ImageTransform.GetImageProvider());
                }

                PdfWriterPipeline pdf = new PdfWriterPipeline(doc, writer);
                HtmlPipeline html = new HtmlPipeline(htmlContext, pdf);
                CssResolverPipeline css = new CssResolverPipeline(cssResolver, html);

                XMLWorker worker = new XMLWorker(css, true);
                XMLParser p = new XMLParser(worker);
                p.Parse(srHtml);
            }
        }
    }
}
