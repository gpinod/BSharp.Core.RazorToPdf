namespace BSharp.Core.RazorToPdf
{
    public interface ITransformEngine
    {
        byte[] Transform<TModel>(string templateContent, string key, TModel model);
        byte[] TransformFromFile<TModel>(string templatePath, string key, TModel model);
        void TransformAndSave<TModel>(string templateContent, string key, TModel model, string targetPath);
        void TransformFromFileAndSave<TModel>(string templatePath, string key, TModel model, string targetPath);
    }
}