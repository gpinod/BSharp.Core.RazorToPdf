using BSharp.Core.RazorToPdf.Utils;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;
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
        /// <summary>
        /// Create an instance of <see cref="TransformEngine"/>
        /// </summary>
        public TransformEngine()
        {
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

        private static void ParseXHtml(string html, Document doc, PdfWriter writer)
        {
            using (var srHtml = new StringReader(html))
            {
                XMLWorkerHelper.GetInstance().ParseXHtml(writer, doc, srHtml);
            }
        }
    }
}
