#pragma checksum "D:\Versoes\GIT\REPOSITORIO PUBLICO\Portifolio\Portifolio\ListaDeContatos\ListaDeContatos\Views\Contato\ListarContatos.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "2192ea871c90994ea6566495c65d4b9d47408c82"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Contato_ListarContatos), @"mvc.1.0.view", @"/Views/Contato/ListarContatos.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Contato/ListarContatos.cshtml", typeof(AspNetCore.Views_Contato_ListarContatos))]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"2192ea871c90994ea6566495c65d4b9d47408c82", @"/Views/Contato/ListarContatos.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"35a9f05227dd6a80e10ad612604a973f708e66e6", @"/Views/_ViewImports.cshtml")]
    public class Views_Contato_ListarContatos : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<ListaDeContatos.Models.Contato>>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("id", new global::Microsoft.AspNetCore.Html.HtmlString("idFormDelete"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("action", new global::Microsoft.AspNetCore.Html.HtmlString("/contato/DeletarContato"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("method", "post", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("btn btn-info"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_4 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "CriarContato", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_5 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("btn panel-info"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_6 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("title", new global::Microsoft.AspNetCore.Html.HtmlString("Editar"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_7 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "EditarContato", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_8 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("btn alert-info"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_9 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("title", new global::Microsoft.AspNetCore.Html.HtmlString("Detalhes"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_10 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "VisualizarContato", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
            BeginContext(52, 2, true);
            WriteLiteral("\r\n");
            EndContext();
#line 3 "D:\Versoes\GIT\REPOSITORIO PUBLICO\Portifolio\Portifolio\ListaDeContatos\ListaDeContatos\Views\Contato\ListarContatos.cshtml"
  
    ViewData["Title"] = "Lista de Contatos";

#line default
#line hidden
            BeginContext(107, 650, true);
            WriteLiteral(@"<div>
    <div class=""modal fade"">
        <div class=""modal-dialog modal-dialog-centered"">
            <div class=""modal-content"">
                <div class=""modal-header alert-info"">
                    <button type=""button"" class=""close"" data-dismiss=""modal""><span aria-hidden=""true"">x</span><span class=""sr-only"">Close</span></button>
                    <h4 class=""modal-title"">Atenção!</h4>
                </div>
                <div class=""modal-body"">
                    <p>O Contato será excluído permanentemente. Deseja mesmo excluir?</p>
                </div>
                <div class=""modal-footer"">
                    ");
            EndContext();
            BeginContext(757, 449, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "e87806a68dec43a3a6a8c8171fb66e6b", async() => {
                BeginContext(828, 52, true);
                WriteLiteral("\r\n                        \r\n                        ");
                EndContext();
                BeginContext(881, 23, false);
#line 20 "D:\Versoes\GIT\REPOSITORIO PUBLICO\Portifolio\Portifolio\ListaDeContatos\ListaDeContatos\Views\Contato\ListarContatos.cshtml"
                   Write(Html.AntiForgeryToken());

#line default
#line hidden
                EndContext();
                BeginContext(904, 295, true);
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
            BeginContext(1206, 188, true);
            WriteLiteral("\r\n                </div>\r\n            </div><!-- /.modal-content -->\r\n        </div><!-- /.modal-dialog -->\r\n    </div><!-- /.modal -->\r\n    <h2>Lista de Contatos</h2>\r\n\r\n    <p>\r\n        ");
            EndContext();
            BeginContext(1394, 66, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "758565ebde3c4772965bb594337d47e2", async() => {
                BeginContext(1444, 12, true);
                WriteLiteral("Novo Contato");
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
            BeginContext(1460, 116, true);
            WriteLiteral("\r\n    </p>\r\n    <table class=\"table\">\r\n        <thead>\r\n            <tr>\r\n                <th>\r\n                    ");
            EndContext();
            BeginContext(1577, 40, false);
#line 38 "D:\Versoes\GIT\REPOSITORIO PUBLICO\Portifolio\Portifolio\ListaDeContatos\ListaDeContatos\Views\Contato\ListarContatos.cshtml"
               Write(Html.DisplayNameFor(model => model.Nome));

#line default
#line hidden
            EndContext();
            BeginContext(1617, 67, true);
            WriteLiteral("\r\n                </th>\r\n                <th>\r\n                    ");
            EndContext();
            BeginContext(1685, 44, false);
#line 41 "D:\Versoes\GIT\REPOSITORIO PUBLICO\Portifolio\Portifolio\ListaDeContatos\ListaDeContatos\Views\Contato\ListarContatos.cshtml"
               Write(Html.DisplayNameFor(model => model.Telefone));

#line default
#line hidden
            EndContext();
            BeginContext(1729, 67, true);
            WriteLiteral("\r\n                </th>\r\n                <th>\r\n                    ");
            EndContext();
            BeginContext(1797, 50, false);
#line 44 "D:\Versoes\GIT\REPOSITORIO PUBLICO\Portifolio\Portifolio\ListaDeContatos\ListaDeContatos\Views\Contato\ListarContatos.cshtml"
               Write(Html.DisplayNameFor(model => model.TelefoneRecado));

#line default
#line hidden
            EndContext();
            BeginContext(1847, 67, true);
            WriteLiteral("\r\n                </th>\r\n                <th>\r\n                    ");
            EndContext();
            BeginContext(1915, 50, false);
#line 47 "D:\Versoes\GIT\REPOSITORIO PUBLICO\Portifolio\Portifolio\ListaDeContatos\ListaDeContatos\Views\Contato\ListarContatos.cshtml"
               Write(Html.DisplayNameFor(model => model.DataNascimento));

#line default
#line hidden
            EndContext();
            BeginContext(1965, 67, true);
            WriteLiteral("\r\n                </th>\r\n                <th>\r\n                    ");
            EndContext();
            BeginContext(2033, 40, false);
#line 50 "D:\Versoes\GIT\REPOSITORIO PUBLICO\Portifolio\Portifolio\ListaDeContatos\ListaDeContatos\Views\Contato\ListarContatos.cshtml"
               Write(Html.DisplayNameFor(model => model.Sexo));

#line default
#line hidden
            EndContext();
            BeginContext(2073, 67, true);
            WriteLiteral("\r\n                </th>\r\n                <th>\r\n                    ");
            EndContext();
            BeginContext(2141, 42, false);
#line 53 "D:\Versoes\GIT\REPOSITORIO PUBLICO\Portifolio\Portifolio\ListaDeContatos\ListaDeContatos\Views\Contato\ListarContatos.cshtml"
               Write(Html.DisplayNameFor(model => model.Estado));

#line default
#line hidden
            EndContext();
            BeginContext(2183, 67, true);
            WriteLiteral("\r\n                </th>\r\n                <th>\r\n                    ");
            EndContext();
            BeginContext(2251, 42, false);
#line 56 "D:\Versoes\GIT\REPOSITORIO PUBLICO\Portifolio\Portifolio\ListaDeContatos\ListaDeContatos\Views\Contato\ListarContatos.cshtml"
               Write(Html.DisplayNameFor(model => model.Cidade));

#line default
#line hidden
            EndContext();
            BeginContext(2293, 67, true);
            WriteLiteral("\r\n                </th>\r\n                <th>\r\n                    ");
            EndContext();
            BeginContext(2361, 39, false);
#line 59 "D:\Versoes\GIT\REPOSITORIO PUBLICO\Portifolio\Portifolio\ListaDeContatos\ListaDeContatos\Views\Contato\ListarContatos.cshtml"
               Write(Html.DisplayNameFor(model => model.Cep));

#line default
#line hidden
            EndContext();
            BeginContext(2400, 106, true);
            WriteLiteral("\r\n                </th>\r\n                <th></th>\r\n            </tr>\r\n        </thead>\r\n        <tbody>\r\n");
            EndContext();
#line 65 "D:\Versoes\GIT\REPOSITORIO PUBLICO\Portifolio\Portifolio\ListaDeContatos\ListaDeContatos\Views\Contato\ListarContatos.cshtml"
             foreach (var item in Model)
            {

#line default
#line hidden
            BeginContext(2563, 72, true);
            WriteLiteral("                <tr>\r\n                    <td>\r\n                        ");
            EndContext();
            BeginContext(2636, 39, false);
#line 69 "D:\Versoes\GIT\REPOSITORIO PUBLICO\Portifolio\Portifolio\ListaDeContatos\ListaDeContatos\Views\Contato\ListarContatos.cshtml"
                   Write(Html.DisplayFor(modelItem => item.Nome));

#line default
#line hidden
            EndContext();
            BeginContext(2675, 55, true);
            WriteLiteral("\r\n                    </td>\r\n                    <td>\r\n");
            EndContext();
#line 72 "D:\Versoes\GIT\REPOSITORIO PUBLICO\Portifolio\Portifolio\ListaDeContatos\ListaDeContatos\Views\Contato\ListarContatos.cshtml"
                           var telefone = (@item.Telefone.Length > 10) ?
                            "(" + @item.Telefone.Substring(0, 2) + ") " + @item.Telefone.Substring(2, 5) + "-" + @item.Telefone.Substring(7, 4) :
                            "(" + @item.Telefone.Substring(0, 2) + ") " + @item.Telefone.Substring(2, 4) + "-" + @item.Telefone.Substring(6, 4);

#line default
#line hidden
            BeginContext(3098, 74, true);
            WriteLiteral("\r\n                        <label style=\"width:120px;font-weight: normal;\">");
            EndContext();
            BeginContext(3173, 8, false);
#line 76 "D:\Versoes\GIT\REPOSITORIO PUBLICO\Portifolio\Portifolio\ListaDeContatos\ListaDeContatos\Views\Contato\ListarContatos.cshtml"
                                                                   Write(telefone);

#line default
#line hidden
            EndContext();
            BeginContext(3181, 63, true);
            WriteLiteral("</label>\r\n                    </td>\r\n                    <td>\r\n");
            EndContext();
#line 79 "D:\Versoes\GIT\REPOSITORIO PUBLICO\Portifolio\Portifolio\ListaDeContatos\ListaDeContatos\Views\Contato\ListarContatos.cshtml"
                           var telefoneRecado = "";

#line default
#line hidden
            BeginContext(3298, 2, true);
            WriteLiteral("\r\n");
            EndContext();
#line 81 "D:\Versoes\GIT\REPOSITORIO PUBLICO\Portifolio\Portifolio\ListaDeContatos\ListaDeContatos\Views\Contato\ListarContatos.cshtml"
                         if (@item.TelefoneRecado != null)
                        {
                            telefoneRecado = (@item.TelefoneRecado.Length > 10) ?
                                "(" + @item.TelefoneRecado.Substring(0, 2) + ") " + @item.TelefoneRecado.Substring(2, 5) + "-" + @item.TelefoneRecado.Substring(7, 4) :
                                "(" + @item.TelefoneRecado.Substring(0, 2) + ") " + @item.TelefoneRecado.Substring(2, 4) + "-" + @item.TelefoneRecado.Substring(6, 4);
                        }

#line default
#line hidden
            BeginContext(3834, 74, true);
            WriteLiteral("\r\n                        <label style=\"width:120px;font-weight: normal;\">");
            EndContext();
            BeginContext(3909, 14, false);
#line 88 "D:\Versoes\GIT\REPOSITORIO PUBLICO\Portifolio\Portifolio\ListaDeContatos\ListaDeContatos\Views\Contato\ListarContatos.cshtml"
                                                                   Write(telefoneRecado);

#line default
#line hidden
            EndContext();
            BeginContext(3923, 135, true);
            WriteLiteral("</label>\r\n                    </td>\r\n                    <td>\r\n                        <label style=\"width:120px;font-weight: normal;\">");
            EndContext();
            BeginContext(4059, 71, false);
#line 91 "D:\Versoes\GIT\REPOSITORIO PUBLICO\Portifolio\Portifolio\ListaDeContatos\ListaDeContatos\Views\Contato\ListarContatos.cshtml"
                                                                   Write(Convert.ToString(string.Format("{0:dd/MM/yyyy}", @item.DataNascimento)));

#line default
#line hidden
            EndContext();
            BeginContext(4130, 64, true);
            WriteLiteral(" </label>\r\n                    </td>\r\n                    <td>\r\n");
            EndContext();
#line 94 "D:\Versoes\GIT\REPOSITORIO PUBLICO\Portifolio\Portifolio\ListaDeContatos\ListaDeContatos\Views\Contato\ListarContatos.cshtml"
                         if (@item.Sexo != true)
                        {

#line default
#line hidden
            BeginContext(4271, 46, true);
            WriteLiteral("                            <label>F</label>\r\n");
            EndContext();
#line 97 "D:\Versoes\GIT\REPOSITORIO PUBLICO\Portifolio\Portifolio\ListaDeContatos\ListaDeContatos\Views\Contato\ListarContatos.cshtml"
                        }
                        else
                        {

#line default
#line hidden
            BeginContext(4401, 46, true);
            WriteLiteral("                            <label>M</label>\r\n");
            EndContext();
#line 101 "D:\Versoes\GIT\REPOSITORIO PUBLICO\Portifolio\Portifolio\ListaDeContatos\ListaDeContatos\Views\Contato\ListarContatos.cshtml"
                        }

#line default
#line hidden
            BeginContext(4474, 98, true);
            WriteLiteral("                    </td>\r\n                   \r\n                    <td>\r\n                        ");
            EndContext();
            BeginContext(4573, 41, false);
#line 105 "D:\Versoes\GIT\REPOSITORIO PUBLICO\Portifolio\Portifolio\ListaDeContatos\ListaDeContatos\Views\Contato\ListarContatos.cshtml"
                   Write(Html.DisplayFor(modelItem => item.Estado));

#line default
#line hidden
            EndContext();
            BeginContext(4614, 79, true);
            WriteLiteral("\r\n                    </td>\r\n                    <td>\r\n                        ");
            EndContext();
            BeginContext(4694, 41, false);
#line 108 "D:\Versoes\GIT\REPOSITORIO PUBLICO\Portifolio\Portifolio\ListaDeContatos\ListaDeContatos\Views\Contato\ListarContatos.cshtml"
                   Write(Html.DisplayFor(modelItem => item.Cidade));

#line default
#line hidden
            EndContext();
            BeginContext(4735, 178, true);
            WriteLiteral("\r\n                    </td>\r\n                   \r\n                    <td>\r\n                        <label class=\"CEP\" style=\"font-weight: normal;\">\r\n                            ");
            EndContext();
            BeginContext(4914, 38, false);
#line 113 "D:\Versoes\GIT\REPOSITORIO PUBLICO\Portifolio\Portifolio\ListaDeContatos\ListaDeContatos\Views\Contato\ListarContatos.cshtml"
                       Write(Html.DisplayFor(modelItem => item.Cep));

#line default
#line hidden
            EndContext();
            BeginContext(4952, 135, true);
            WriteLiteral("\r\n                        </label>\r\n                    </td>\r\n                    \r\n                    <td>\r\n                        ");
            EndContext();
            BeginContext(5087, 145, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "d22238a3d8ce41798e3299b26165443a", async() => {
                BeginContext(5186, 42, true);
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
#line 118 "D:\Versoes\GIT\REPOSITORIO PUBLICO\Portifolio\Portifolio\ListaDeContatos\ListaDeContatos\Views\Contato\ListarContatos.cshtml"
                                                                                              WriteLiteral(item.ContatoId);

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
            BeginContext(5232, 28, true);
            WriteLiteral(" |\r\n                        ");
            EndContext();
            BeginContext(5260, 151, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "146cb88ee2294e6ab2c2af34acee98fe", async() => {
                BeginContext(5365, 42, true);
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
#line 119 "D:\Versoes\GIT\REPOSITORIO PUBLICO\Portifolio\Portifolio\ListaDeContatos\ListaDeContatos\Views\Contato\ListarContatos.cshtml"
                                                                                                    WriteLiteral(item.ContatoId);

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
            BeginContext(5411, 85, true);
            WriteLiteral(" |\r\n                        <a class=\"btn btn-info excluir\" title=\"Excluir\" data-id=\"");
            EndContext();
            BeginContext(5497, 14, false);
#line 120 "D:\Versoes\GIT\REPOSITORIO PUBLICO\Portifolio\Portifolio\ListaDeContatos\ListaDeContatos\Views\Contato\ListarContatos.cshtml"
                                                                            Write(item.ContatoId);

#line default
#line hidden
            EndContext();
            BeginContext(5511, 100, true);
            WriteLiteral("\" ><i class=\"glyphicon glyphicon-trash\"></i></a>\r\n                    </td>\r\n                </tr>\r\n");
            EndContext();
#line 123 "D:\Versoes\GIT\REPOSITORIO PUBLICO\Portifolio\Portifolio\ListaDeContatos\ListaDeContatos\Views\Contato\ListarContatos.cshtml"
            }

#line default
#line hidden
            BeginContext(5626, 40, true);
            WriteLiteral("        </tbody>\r\n    </table>\r\n</div>\r\n");
            EndContext();
            DefineSection("scripts", async() => {
                BeginContext(5683, 1018, true);
                WriteLiteral(@"

    <script>

        $(document).ready(function () {

            var total = $("".CEP"").length;

            for (var i = 0; i < total; i++) {
                var cep = $("".CEP"")[i].innerHTML;
                cep = cep.trim();
                var mascara = cep.substring(0, 2) + ""."" + cep.substring(2, 5) + ""."" + cep.substring(5, 8);
                if (cep != """")
                    $("".CEP"")[i].innerHTML = mascara;
            }

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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<ListaDeContatos.Models.Contato>> Html { get; private set; }
    }
}
#pragma warning restore 1591