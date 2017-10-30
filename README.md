<h1>BSharp.Core.RazorToPdf</h1>
<h3>Convert a Razor Template (cshtml or vbhtml) to a PDF format.</h3>
<p>A simple way to generate a PDF using as a template Razor.</p>
<p>Alternatives to use:</p>
<ol>
  <li>
    <b>Using a content template</b><br/>
    This is an alternative use when the source template can be stored in a Database or any persistence mechanism other than a physical file; or when the physical file has to be pre-processed before making the transformation. This returns the binary representation of the pdf to customize the presentation technique of the pdf, as for example, with MVC or ASP.NET Web Forms. 
  </li>
  <li>
    <b>Using a physical path of template</b><br/>
    This is an alternative use when the source template is a physical file. This returns the binary representation of the pdf to customize the presentation technique of the pdf, as for example, with MVC or ASP.NET Web Forms.
  </li>
  <li>
    <b>Save in a physical path using a content template</b><br/>
    This is an alternative use when the source template can be stored in a Database or any persistence mechanism other than a physical file; or when the physical file has to be pre-processed before making the transformation. This saves the pdf in a physical file. 
  </li>
  <li>
    <b>Save in a physical path using a physical path of template</b><br/>
    This is an alternative use when the source template is a physical file. This saves the pdf in a physical file. .
  </li>
</ol>
<h2>Examples:</h2>
<h3>Using a content template</h3>
<pre>
  string content = "&lt;h3&gt;Hi, @Model.Name&lt;/h3&gt";
  var model = new { Name = "John Smith" };
  ITransformEngine transformEngine = new TransformEngine();
  byte[]  bytes = transformEngine.Transform(content, "Example1", model);
</pre>
<h3>Using a physical path of template</h3>
<pre>
  string templatePath = @"C:\Template.cshtml";
  var model = new { Name = "John Smith" };
  ITransformEngine transformEngine = new TransformEngine();
  byte[]  bytes = transformEngine.TransformFromFile(templatePath, "Example2", model);
</pre>
<h3>Save in a physical path using a content template</h3>
<pre>
  string content = "&lt;h3&gt;Hi, @Model.Name&lt;/h3&gt";
  var model = new { Name = "John Smith" };
  string targetPath = @"C:\Demo.pdf";
  ITransformEngine transformEngine = new TransformEngine();
  transformEngine.TransformAndSave(content, "Example3", model, targetPath);
</pre>
<h3>Save in a physical path using a physical path of template</h3>
<pre>
  string templatePath = @"C:\Template.cshtml";
  var model = new { Name = "John Smith" };
  string targetPath = @"C:\Demo.pdf";
  ITransformEngine transformEngine = new TransformEngine();
  transformEngine.TransformFromFileAndSave(templatePath, "Example4", model, targetPath);
</pre>
