# BSharp.Core.RazorToPdf
## Convert a Razor Template (cshtml or vbhtml) to a PDF format.
A simple way to generate a PDF using as a template Razor.
Alternatives to use:
1. Using a content template
This is an alternative use when the source template can be stored in a Database or any persistence mechanism other than a physical file; or when the physical file has to be pre-processed before making the transformation. This returns the binary representation of the pdf to customize the presentation technique of the pdf, as for example, with MVC or ASP.NET Web Forms.
    
2. Using a physical path of template
This is an alternative use when the source template is a physical file. This returns the binary representation of the pdf to customize the presentation technique of the pdf, as for example, with MVC or ASP.NET Web Forms.

3. Save in a physical path using a content template
This is an alternative use when the source template can be stored in a Database or any persistence mechanism other than a physical file; or when the physical file has to be pre-processed before making the transformation. This saves the pdf in a physical file. 

4. Save in a physical path using a physical path of template
This is an alternative use when the source template is a physical file. This saves the pdf in a physical file. .
  
## Examples:

### Using a content template
```csharp
using BSharp.Core.RazorToPdf;

string content = "<h3>Hi, @Model.Name</h3>";
var model = new { Name = "John Smith" };
ITransformEngine transformEngine = new TransformEngine();
byte[]  bytes = transformEngine.Transform(content, "Example1", model);
```
### Using a physical path of template
```csharp
using BSharp.Core.RazorToPdf;

string templatePath = @"C:\Template.cshtml";
var model = new { Name = "John Smith" };
ITransformEngine transformEngine = new TransformEngine();
byte[]  bytes = transformEngine.TransformFromFile(templatePath, "Example2", model);
```
### Save in a physical path using a content template
```csharp
using BSharp.Core.RazorToPdf;

string content = "<h3>Hi, @Model.Name</h3>";
var model = new { Name = "John Smith" };
string targetPath = @"C:\Demo.pdf";
ITransformEngine transformEngine = new TransformEngine();
transformEngine.TransformAndSave(content, "Example3", model, targetPath);
```
### Save in a physical path using a physical path of template
```csharp
using BSharp.Core.RazorToPdf;

string templatePath = @"C:\Template.cshtml";
var model = new { Name = "John Smith" };
string targetPath = @"C:\Demo.pdf";
ITransformEngine transformEngine = new TransformEngine();
transformEngine.TransformFromFileAndSave(templatePath, "Example4", model, targetPath);
```
## Temporary files

Please review the RazorEngine documentation [here](https://github.com/Antaris/RazorEngine#temporary-files), for more details.

## Example including embeded images
You can include images embeded in base64 using the following instruction:
```csharp
using BSharp.Core.RazorToPdf;
string base64 = "<IMAGE CODING IN BASE64 >";
string content = "<h3>Hi, @Model.Name</h3><img src=\"@Model.ImageBase64\" />";
var model = new { Name = "John Smith", ImageBase64 = string.Format(CultureInfo.CurrentCulture, "data:image/png;base64,{0}", base64) };
string targetPath = @"C:\Demo.pdf";
ITransformEngine transformEngine = new TransformEngine(new PdfOptions
            {
                ImageTransform = new Base64ImageTransform()
            });
transformEngine.TransformAndSave(content, "Example3", model, targetPath);
