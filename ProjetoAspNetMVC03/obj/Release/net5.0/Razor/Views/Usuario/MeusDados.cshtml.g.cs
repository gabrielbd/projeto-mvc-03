#pragma checksum "C:\Users\Marcelo\Desktop\ProjetoAspNetMVC03\ProjetoAspNetMVC03\Views\Usuario\MeusDados.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "365aacb10680827531308370d572f2f1ade46746"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Usuario_MeusDados), @"mvc.1.0.view", @"/Views/Usuario/MeusDados.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"365aacb10680827531308370d572f2f1ade46746", @"/Views/Usuario/MeusDados.cshtml")]
    public class Views_Usuario_MeusDados : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 2 "C:\Users\Marcelo\Desktop\ProjetoAspNetMVC03\ProjetoAspNetMVC03\Views\Usuario\MeusDados.cshtml"
  
    ViewData["Title"] = "MeusDados";
    Layout = "~/Views/Shared/Layout.cshtml";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<h5>Dados do Usuário</h5>\r\nInformações do usuário autenticado na aplicação.\r\n<br />\r\n<br />\r\n\r\n<div class=\"row\">\r\n    <div class=\"col-md-3 text-start\">\r\n        Id do Usuário:\r\n    </div>\r\n    <div class=\"col-md-9\">\r\n        <strong>");
#nullable restore
#line 17 "C:\Users\Marcelo\Desktop\ProjetoAspNetMVC03\ProjetoAspNetMVC03\Views\Usuario\MeusDados.cshtml"
           Write(TempData["IdUsuario"]);

#line default
#line hidden
#nullable disable
            WriteLiteral("</strong>\r\n    </div>\r\n</div>\r\n<div class=\"row\">\r\n    <div class=\"col-md-3 text-start\">\r\n        Nome do Usuário:\r\n    </div>\r\n    <div class=\"col-md-9\">\r\n        <strong>");
#nullable restore
#line 25 "C:\Users\Marcelo\Desktop\ProjetoAspNetMVC03\ProjetoAspNetMVC03\Views\Usuario\MeusDados.cshtml"
           Write(TempData["Nome"]);

#line default
#line hidden
#nullable disable
            WriteLiteral("</strong>\r\n    </div>\r\n</div>\r\n<div class=\"row\">\r\n    <div class=\"col-md-3 text-start\">\r\n        Email de acesso:\r\n    </div>\r\n    <div class=\"col-md-9\">\r\n        <strong>");
#nullable restore
#line 33 "C:\Users\Marcelo\Desktop\ProjetoAspNetMVC03\ProjetoAspNetMVC03\Views\Usuario\MeusDados.cshtml"
           Write(TempData["Email"]);

#line default
#line hidden
#nullable disable
            WriteLiteral("</strong>\r\n    </div>\r\n</div>\r\n<div class=\"row\">\r\n    <div class=\"col-md-3 text-start\">\r\n        Data de Cadastro:\r\n    </div>\r\n    <div class=\"col-md-9\">\r\n        <strong>");
#nullable restore
#line 41 "C:\Users\Marcelo\Desktop\ProjetoAspNetMVC03\ProjetoAspNetMVC03\Views\Usuario\MeusDados.cshtml"
           Write(TempData["DataCadastro"]);

#line default
#line hidden
#nullable disable
            WriteLiteral("</strong>\r\n    </div>\r\n</div>\r\n\r\n\r\n\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; }
    }
}
#pragma warning restore 1591