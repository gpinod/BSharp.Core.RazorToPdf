namespace BSharp.Core.RazorToPdf
{
    /// <summary>
    /// Interface that contains the methods to transform a razor template (cshtml or vbhtml) to a pdf format with a model.
    /// </summary>
    public interface ITransformEngine
    {
        /// <summary>
        /// Transform a razor template (cshtml or vbhtml) to a binary data that represents a pdf format.
        /// </summary>
        /// <typeparam name="TModel">The type of the model using to mix with the template to generate the pdf.</typeparam>
        /// <param name="templateContent">The content of the template.</param>
        /// <param name="key">A name that is used to generate a cache of the template</param>
        /// <param name="model">The model that is used to mix with the template and generate the pdf.</param>
        /// <returns>The representation of the pdf in binary.</returns>
        byte[] Transform<TModel>(string templateContent, string key, TModel model);
        /// <summary>
        /// Transform a razor template (cshtml or vbhtml) to a binary data that represents a pdf format.
        /// </summary>
        /// <typeparam name="TModel">The type of the model using to mix with the template to generate the pdf.</typeparam>
        /// <param name="templatepPath">The physical path of the template.</param>
        /// <param name="key">A name that is used to generate a cache of the template</param>
        /// <param name="model">The model that is used to mix with the template and generate the pdf.</param>
        /// <returns>The representation of the pdf in binary.</returns>
        byte[] TransformFromFile<TModel>(string templatePath, string key, TModel model);
        /// <summary>
        /// Transform a razor template (cshtml or vbhtml) to a binary data that represents a pdf format and save the pdf physically.
        /// </summary>
        /// <typeparam name="TModel">The type of the model using to mix with the template to generate the pdf.</typeparam>
        /// <param name="templateContent">The content of the template.</param>
        /// <param name="key">A name that is used to generate a cache of the template</param>
        /// <param name="model">The model that is used to mix with the template and generate the pdf.</param>
        /// <param name="targetPath">The path where the pdf will be saved.</param>
        void TransformAndSave<TModel>(string templateContent, string key, TModel model, string targetPath);
        /// <summary>
        /// Transform a razor template (cshtml or vbhtml) to a binary data that represents a pdf format and save the pdf physically.
        /// </summary>
        /// <typeparam name="TModel">The type of the model using to mix with the template to generate the pdf.</typeparam>
        /// <param name="templatepPath">The physical path of the template.</param>
        /// <param name="key">A name that is used to generate a cache of the template</param>
        /// <param name="model">The model that is used to mix with the template and generate the pdf.</param>
        /// <param name="targetPath">The path where the pdf will be saved.</param>
        void TransformFromFileAndSave<TModel>(string templatePath, string key, TModel model, string targetPath);
    }
}