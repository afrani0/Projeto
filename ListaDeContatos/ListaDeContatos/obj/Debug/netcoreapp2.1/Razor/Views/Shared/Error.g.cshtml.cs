#pragma checksum "D:\Versoes\GIT\REPOSITORIO PUBLICO\Portifolio\Portifolio\ListaDeContatos\ListaDeContatos\Views\Shared\Error.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "86313bce59b3d003559d020a63d4476d0ea39446"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Shared_Error), @"mvc.1.0.view", @"/Views/Shared/Error.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Shared/Error.cshtml", typeof(AspNetCore.Views_Shared_Error))]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#line 1 "D:\Versoes\GIT\REPOSITORIO PUBLICO\Portifolio\Portifolio\ListaDeContatos\ListaDeContatos\Views\_ViewImports.cshtml"
using ListaDeContatos;

#line default
#line hidden
#line 2 "D:\Versoes\GIT\REPOSITORIO PUBLICO\Portifolio\Portifolio\ListaDeContatos\ListaDeContatos\Views\_ViewImports.cshtml"
using ListaDeContatos.Models;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"86313bce59b3d003559d020a63d4476d0ea39446", @"/Views/Shared/Error.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"35a9f05227dd6a80e10ad612604a973f708e66e6", @"/Views/_ViewImports.cshtml")]
    public class Views_Shared_Error : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<ListaDeContatos.ViewModels.ErrorViewModel>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#line 2 "D:\Versoes\GIT\REPOSITORIO PUBLICO\Portifolio\Portifolio\ListaDeContatos\ListaDeContatos\Views\Shared\Error.cshtml"
  
    ViewData["Title"] = "Error";

#line default
#line hidden
            BeginContext(91, 122, true);
            WriteLiteral("\r\n<h1 class=\"text-danger\">Erro.</h1>\r\n<h2 class=\"text-danger\">Um erro ocorreu enquanto processava sua requisição.</h2>\r\n\r\n");
            EndContext();
#line 9 "D:\Versoes\GIT\REPOSITORIO PUBLICO\Portifolio\Portifolio\ListaDeContatos\ListaDeContatos\Views\Shared\Error.cshtml"
 if (Model.ShowRequestId)
{

#line default
#line hidden
            BeginContext(243, 52, true);
            WriteLiteral("    <p>\r\n        <strong>Request ID:</strong> <code>");
            EndContext();
            BeginContext(296, 15, false);
#line 12 "D:\Versoes\GIT\REPOSITORIO PUBLICO\Portifolio\Portifolio\ListaDeContatos\ListaDeContatos\Views\Shared\Error.cshtml"
                                      Write(Model.RequestId);

#line default
#line hidden
            EndContext();
            BeginContext(311, 19, true);
            WriteLiteral("</code>\r\n    </p>\r\n");
            EndContext();
#line 14 "D:\Versoes\GIT\REPOSITORIO PUBLICO\Portifolio\Portifolio\ListaDeContatos\ListaDeContatos\Views\Shared\Error.cshtml"
}

#line default
#line hidden
            BeginContext(333, 562, true);
            WriteLiteral(@"
<h3>Development Mode</h3>
<p>
    Swapping to <strong>Development</strong> environment will display more detailed information about the error that occurred.
</p>
<p>
    <strong>Development environment should not be enabled in deployed applications</strong>, as it can result in sensitive information from exceptions being displayed to end users. For local debugging, development environment can be enabled by setting the <strong>ASPNETCORE_ENVIRONMENT</strong> environment variable to <strong>Development</strong>, and restarting the application.
</p>
");
            EndContext();
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<ListaDeContatos.ViewModels.ErrorViewModel> Html { get; private set; }
    }
}
#pragma warning restore 1591
