#pragma checksum "C:\Users\Alexander\source\repos\Group6_Assignment4\Views\Home\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "be9f5784f2840aa1c6beb2c5f179ef6a9fd85478"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Home_Index), @"mvc.1.0.view", @"/Views/Home/Index.cshtml")]
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
#nullable restore
#line 1 "C:\Users\Alexander\source\repos\Group6_Assignment4\Views\_ViewImports.cshtml"
using Assignment4;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\Alexander\source\repos\Group6_Assignment4\Views\_ViewImports.cshtml"
using Assignment4.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"be9f5784f2840aa1c6beb2c5f179ef6a9fd85478", @"/Views/Home/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"6d5f0f2cf78de94e78bec100bcb9010e144748b3", @"/Views/_ViewImports.cshtml")]
    public class Views_Home_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 1 "C:\Users\Alexander\source\repos\Group6_Assignment4\Views\Home\Index.cshtml"
  
    ViewData["Title"] = "Home";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n");
            DefineSection("custom_js_css", async() => {
                WriteLiteral("\r\n    <link href=\"/CSS/BannerStyle.css\" rel=\"stylesheet\" type=\"text/css\">\r\n    <script src=\"/js/BannerScript.js\"></script>\r\n");
            }
            );
            WriteLiteral(@"
<div id=""slider"">
    <img src=""Images/Banner/1.jpg"" id=""image"" />
    <div class=""slider_intro"">
        <p class=""slider_intro_text"">
            Welcome to the NPS Campgrounds reservation service. Although we have had to undergo new sanitization protocol,
            there may be no better place to socially distance than in some of our great parks.
        </p>
    </div>
    <div class=""left_hold"">
        <img onClick=""rotateOnce(-1)"" class=""left"" src=""Images/Banner/LeftArrow.png"" />
    </div>
    <div class=""right_hold"">
        <img onClick=""rotateOnce(1)"" class=""right"" src=""Images/Banner/RightArrow.png"" />
    </div>
    <div class=""slider_description"">
        <p class=""slider_title"" id=""park_name"">Everglades National Park</p>
        <p class=""slider_title"" id=""img_descr"">Entrace Sign for the Everglades National Park</p>
    </div>
</div>


");
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
