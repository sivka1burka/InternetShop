#pragma checksum "C:\Users\deys1\Desktop\Диплом\WebApplication6\Views\ShopCart\ShopCart.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "4e68f8a90349966f1771e0e67e0f9ef6e6e539e6"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_ShopCart_ShopCart), @"mvc.1.0.view", @"/Views/ShopCart/ShopCart.cshtml")]
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
#line 1 "C:\Users\deys1\Desktop\Диплом\WebApplication6\Views\_ViewImports.cshtml"
using WebApplication6;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\deys1\Desktop\Диплом\WebApplication6\Views\_ViewImports.cshtml"
using WebApplication6.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 1 "C:\Users\deys1\Desktop\Диплом\WebApplication6\Views\ShopCart\ShopCart.cshtml"
using System.Globalization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\deys1\Desktop\Диплом\WebApplication6\Views\ShopCart\ShopCart.cshtml"
using WebApplication6.ViewModels;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"4e68f8a90349966f1771e0e67e0f9ef6e6e539e6", @"/Views/ShopCart/ShopCart.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"865b2ca0dedbd6d16882aa8f14a3148527d098b0", @"/Views/_ViewImports.cshtml")]
    #nullable restore
    public class Views_ShopCart_ShopCart : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<ShopCartViewModel>
    #nullable disable
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 4 "C:\Users\deys1\Desktop\Диплом\WebApplication6\Views\ShopCart\ShopCart.cshtml"
  
    ViewData["Title"] = "Корзина";

#line default
#line hidden
#nullable disable
            WriteLiteral("<div style=\"padding-left:10%\">\r\n    <h4 >Корзина</h4>\r\n");
#nullable restore
#line 9 "C:\Users\deys1\Desktop\Диплом\WebApplication6\Views\ShopCart\ShopCart.cshtml"
       Html.RenderPartial("_Cart", Model);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"</div>

<script type=""text/javascript"">
    //function updatePrice(el, totalPrice, currentAmount) {
    //    console.log(el, totalPrice, currentAmount);
    //    const parentdiv=$(el).parent().parent().find('.totalSum').html(`${totalPrice*currentAmount} ₸`);
    //}
    function reloadAjax(el){
        //let href=
        console.log(el)
    }
</script>



");
        }
        #pragma warning restore 1998
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<ShopCartViewModel> Html { get; private set; } = default!;
        #nullable disable
    }
}
#pragma warning restore 1591
