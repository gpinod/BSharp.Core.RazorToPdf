using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;
using RazorEngine;
using RazorEngine.Templating;
using System.IO;

namespace BSharp.Core.RazorToPdf
{
    public class TransformEngine : ITransformEngine
    {
        public TransformEngine()
        {
        }

        public byte[] Transform<TModel>(string templateContent, string key, TModel model)
        {
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

        public byte[] TransformFromFile<TModel>(string templatePath, string key, TModel model)
        {
            string templateContent = File.ReadAllText(templatePath);
            return Transform(templateContent, key, model);
        }

        public void TransformAndSave<TModel>(string templateContent, string key, TModel model, string targetPath)
        {
            byte[] bytes = Transform(templateContent, key, model);
            File.WriteAllBytes(targetPath, bytes);
        }

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
