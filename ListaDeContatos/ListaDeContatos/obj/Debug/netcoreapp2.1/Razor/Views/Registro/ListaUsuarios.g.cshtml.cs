#pragma checksum "D:\Versoes\GIT\REPOSITORIO PUBLICO\Portifolio\Portifolio\ListaDeContatos\ListaDeContatos\Views\Registro\ListaUsuarios.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "11437af2ffafa59f82dc7cc04723921191eb8111"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Registro_ListaUsuarios), @"mvc.1.0.view", @"/Views/Registro/ListaUsuarios.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Registro/ListaUsuarios.cshtml", typeof(AspNetCore.Views_Registro_ListaUsuarios))]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"11437af2ffafa59f82dc7cc04723921191eb8111", @"/Views/Registro/ListaUsuarios.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"35a9f05227dd6a80e10ad612604a973f708e66e6", @"/Views/_ViewImports.cshtml")]
    public class Views_Registro_ListaUsuarios : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<ListaDeContatos.ViewModels.UsuarioEditarViewModel>>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("id", new global::Microsoft.AspNetCore.Html.HtmlString("idFormDelete"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("action", new global::Microsoft.AspNetCore.Html.HtmlString("/registro/DeletarUsuario"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("method", "post", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("btn btn-info"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_4 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "CriarUsuario", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_5 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("btn panel-info"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_6 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("title", new global::Microsoft.AspNetCore.Html.HtmlString("Editar"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_7 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "EditarUsuario", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_8 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("btn alert-info"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_9 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("title", new global::Microsoft.AspNetCore.Html.HtmlString("Detalhes"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_10 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "VisualizarUsuario", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            BeginContext(71, 2, true);
            WriteLiteral("\r\n");
            EndContext();
#line 3 "D:\Versoes\GIT\REPOSITORIO PUBLICO\Portifolio\Portifolio\ListaDeContatos\ListaDeContatos\Views\Registro\ListaUsuarios.cshtml"
  
    ViewData["Title"] = "Lista de Usuários";

#line default
#line hidden
            BeginContext(126, 677, true);
            WriteLiteral(@"<div>   
    <div class=""modal fade"">
        <div class=""modal-dialog modal-dialog-centered"">
            <div class=""modal-content"">
                <div class=""modal-header alert-info"">
                    <button type=""button"" class=""close"" data-dismiss=""modal""><span aria-hidden=""true"">x</span><span class=""sr-only"">Close</span></button>
                    <h4 class=""modal-title"">Atenção!</h4>
                </div>
                <div class=""modal-body"">
                    <p>O usuário será excluído permanentemente junto com sua permissão. Deseja mesmo excluir?</p>
                </div>
                <div class=""modal-footer"">
                    ");
            EndContext();
            BeginContext(803, 450, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "d1af1321f0834284b0f414175aa3031b", async() => {
                BeginContext(875, 52, true);
                WriteLiteral("\r\n                        \r\n                        ");
                EndContext();
                BeginContext(928, 23, false);
#line 20 "D:\Versoes\GIT\REPOSITORIO PUBLICO\Portifolio\Portifolio\ListaDeContatos\ListaDeContatos\Views\Registro\ListaUsuarios.cshtml"
                   Write(Html.AntiForgeryToken());

#line default
#line hidden
                EndContext();
                BeginContext(951, 295, true);
                WriteLiteral(@"
                        <input type=""hidden"" id=""id"" name=""id"" value="""" />
                        <button type=""submit"" class=""btn btn-danger"">Excluir</button>
                        <button type=""button"" class=""btn btn-default"" data-dismiss=""modal"">Cancelar</button>
                    ");
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Method = (string)__tagHelperAttribute_2.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(1253, 192, true);
            WriteLiteral("\r\n                </div>\r\n            </div><!-- /.modal-content -->\r\n        </div><!-- /.modal-dialog -->\r\n    </div><!-- /.modal -->\r\n    <h2>Lista de Usuários</h2>\r\n\r\n\r\n\r\n    <p>\r\n        ");
            EndContext();
            BeginContext(1445, 66, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "2a14a3a358a443509f92c2dd72deae6f", async() => {
                BeginContext(1495, 12, true);
                WriteLiteral("Novo Usuário");
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_3);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_4.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_4);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(1511, 120, true);
            WriteLiteral("\r\n    </p>\r\n\r\n    <table class=\"table\">\r\n\r\n        <thead>\r\n            <tr>\r\n                <th>\r\n                    ");
            EndContext();
            BeginContext(1632, 40, false);
#line 42 "D:\Versoes\GIT\REPOSITORIO PUBLICO\Portifolio\Portifolio\ListaDeContatos\ListaDeContatos\Views\Registro\ListaUsuarios.cshtml"
               Write(Html.DisplayNameFor(model => model.Nome));

#line default
#line hidden
            EndContext();
            BeginContext(1672, 67, true);
            WriteLiteral("\r\n                </th>\r\n                <th>\r\n                    ");
            EndContext();
            BeginContext(1740, 45, false);
#line 45 "D:\Versoes\GIT\REPOSITORIO PUBLICO\Portifolio\Portifolio\ListaDeContatos\ListaDeContatos\Views\Registro\ListaUsuarios.cshtml"
               Write(Html.DisplayNameFor(model => model.SobreNome));

#line default
#line hidden
            EndContext();
            BeginContext(1785, 67, true);
            WriteLiteral("\r\n                </th>\r\n                <th>\r\n                    ");
            EndContext();
            BeginContext(1853, 47, false);
#line 48 "D:\Versoes\GIT\REPOSITORIO PUBLICO\Portifolio\Portifolio\ListaDeContatos\ListaDeContatos\Views\Registro\ListaUsuarios.cshtml"
               Write(Html.DisplayNameFor(model => model.NomeUsuario));

#line default
#line hidden
            EndContext();
            BeginContext(1900, 124, true);
            WriteLiteral("\r\n                </th>\r\n                \r\n                <th></th>\r\n            </tr>\r\n        </thead>\r\n        <tbody>\r\n");
            EndContext();
#line 55 "D:\Versoes\GIT\REPOSITORIO PUBLICO\Portifolio\Portifolio\ListaDeContatos\ListaDeContatos\Views\Registro\ListaUsuarios.cshtml"
             foreach (var item in Model)
            {

#line default
#line hidden
            BeginContext(2081, 72, true);
            WriteLiteral("                <tr>\r\n                    <td>\r\n                        ");
            EndContext();
            BeginContext(2154, 39, false);
#line 59 "D:\Versoes\GIT\REPOSITORIO PUBLICO\Portifolio\Portifolio\ListaDeContatos\ListaDeContatos\Views\Registro\ListaUsuarios.cshtml"
                   Write(Html.DisplayFor(modelItem => item.Nome));

#line default
#line hidden
            EndContext();
            BeginContext(2193, 79, true);
            WriteLiteral("\r\n                    </td>\r\n                    <td>\r\n                        ");
            EndContext();
            BeginContext(2273, 44, false);
#line 62 "D:\Versoes\GIT\REPOSITORIO PUBLICO\Portifolio\Portifolio\ListaDeContatos\ListaDeContatos\Views\Registro\ListaUsuarios.cshtml"
                   Write(Html.DisplayFor(modelItem => item.SobreNome));

#line default
#line hidden
            EndContext();
            BeginContext(2317, 79, true);
            WriteLiteral("\r\n                    </td>\r\n                    <td>\r\n                        ");
            EndContext();
            BeginContext(2397, 46, false);
#line 65 "D:\Versoes\GIT\REPOSITORIO PUBLICO\Portifolio\Portifolio\ListaDeContatos\ListaDeContatos\Views\Registro\ListaUsuarios.cshtml"
                   Write(Html.DisplayFor(modelItem => item.NomeUsuario));

#line default
#line hidden
            EndContext();
            BeginContext(2443, 100, true);
            WriteLiteral("\r\n                    </td>\r\n                   \r\n                    <td>\r\n                        ");
            EndContext();
            BeginContext(2543, 138, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "c02c54a9eaa74e3c987c8fd811b491c8", async() => {
                BeginContext(2635, 42, true);
                WriteLiteral("<i class=\"glyphicon glyphicon-pencil\"></i>");
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_5);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_6);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_7.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_7);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-id", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#line 69 "D:\Versoes\GIT\REPOSITORIO PUBLICO\Portifolio\Portifolio\ListaDeContatos\ListaDeContatos\Views\Registro\ListaUsuarios.cshtml"
                                                                                              WriteLiteral(item.Id);

#line default
#line hidden
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-id", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(2681, 28, true);
            WriteLiteral(" |\r\n                        ");
            EndContext();
            BeginContext(2709, 144, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "10ee94d911d84e6db2db372b5b80e2db", async() => {
                BeginContext(2807, 42, true);
                WriteLiteral("<i class=\"glyphicon glyphicon-search\"></i>");
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_8);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_9);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_10.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_10);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-id", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#line 70 "D:\Versoes\GIT\REPOSITORIO PUBLICO\Portifolio\Portifolio\ListaDeContatos\ListaDeContatos\Views\Registro\ListaUsuarios.cshtml"
                                                                                                    WriteLiteral(item.Id);

#line default
#line hidden
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-id", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(2853, 85, true);
            WriteLiteral(" |\r\n                        <a class=\"btn btn-info excluir\" title=\"Excluir\" data-id=\"");
            EndContext();
            BeginContext(2939, 7, false);
#line 71 "D:\Versoes\GIT\REPOSITORIO PUBLICO\Portifolio\Portifolio\ListaDeContatos\ListaDeContatos\Views\Registro\ListaUsuarios.cshtml"
                                                                            Write(item.Id);

#line default
#line hidden
            EndContext();
            BeginContext(2946, 99, true);
            WriteLiteral("\"><i class=\"glyphicon glyphicon-trash\"></i></a>\r\n                    </td>\r\n                </tr>\r\n");
            EndContext();
#line 74 "D:\Versoes\GIT\REPOSITORIO PUBLICO\Portifolio\Portifolio\ListaDeContatos\ListaDeContatos\Views\Registro\ListaUsuarios.cshtml"
            }

#line default
#line hidden
            BeginContext(3060, 46, true);
            WriteLiteral("        </tbody>\r\n    </table>\r\n\r\n</div>\r\n\r\n\r\n");
            EndContext();
            DefineSection("scripts", async() => {
                BeginContext(3123, 624, true);
                WriteLiteral(@"
    <script>
        $(document).ready(function () {

            $(function () {
                $("".excluir"").click(function () {
                    var id = $(this).attr(""data-id"");
                    
                    $(""#idFormDelete input#id"").val(id)
                    $("".modal"").modal();

                });
            })

            var interval;
            function fecharAlertas() {

                $("".alerta"").slideToggle(1000);
                clearInterval(interval);
            }

            var interval = setInterval(fecharAlertas, 20000);
        });
    </script>
");
                EndContext();
            }
            );
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<ListaDeContatos.ViewModels.UsuarioEditarViewModel>> Html { get; private set; }
    }
}
#pragma warning restore 1591