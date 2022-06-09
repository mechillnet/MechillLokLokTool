using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Routing;
using System.Text;
using PNB.Service.sSeo;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace PNB.Web.Framework.Routing
{
    public class SlugRouteTransformer : DynamicRouteValueTransformer
    {
        #region Fields

     
        private readonly IUrlRecordService _urlRecordService;
      

        #endregion

        #region Ctor

        public SlugRouteTransformer(
            IUrlRecordService urlRecordService
            )
        {
           
            _urlRecordService = urlRecordService;
          
        }

        #endregion

        #region Methods

        public override ValueTask<RouteValueDictionary> TransformAsync(HttpContext httpContext, RouteValueDictionary values)
        {
            if (values == null)
                return new ValueTask<RouteValueDictionary>(values);

            if (!values.TryGetValue("SeName", out var slugValue) || string.IsNullOrEmpty(slugValue as string))
                return new ValueTask<RouteValueDictionary>(values);

            var slug = slugValue as string;
            var urlRecord = _urlRecordService.GetBySlug(slug);

            //no URL record found
            if (urlRecord == null)
                return new ValueTask<RouteValueDictionary>(values);

            //virtual directory path
            var pathBase = httpContext.Request.PathBase;

            ////if URL record is not active let's find the latest one
            //if (!urlRecord.IsActive)
            //{
            //    var activeSlug = _urlRecordService.GetActiveSlug(urlRecord.EntityId, urlRecord.EntityName, urlRecord.LanguageId);
            //    if (string.IsNullOrEmpty(activeSlug))
            //        return new ValueTask<RouteValueDictionary>(values);

            //    //redirect to active slug if found
            //    values[NopPathRouteDefaults.ControllerFieldKey] = "Common";
            //    values[NopPathRouteDefaults.ActionFieldKey] = "InternalRedirect";
            //    values[NopPathRouteDefaults.UrlFieldKey] = $"{pathBase}/{activeSlug}{httpContext.Request.QueryString}";
            //    values[NopPathRouteDefaults.PermanentRedirectFieldKey] = true;
            //    httpContext.Items["PTB.RedirectFromGenericPathRoute"] = true;

            //    return new ValueTask<RouteValueDictionary>(values);
            //}

            //Ensure that the slug is the same for the current language, 
            //otherwise it can cause some issues when customers choose a new language but a slug stays the same
         
            //since we are here, all is ok with the slug, so process URL
            switch (urlRecord.EntityName.ToLowerInvariant())
            {
                case "topic":
                    values[NopPathRouteDefaults.ControllerFieldKey] = "Topic";
                    values[NopPathRouteDefaults.ActionFieldKey] = "TopicDetails";
                    values[NopPathRouteDefaults.TopicIdFieldKey] = urlRecord.EntityId;
                    values[NopPathRouteDefaults.SeNameFieldKey] = urlRecord.Slug;
                    break;
                //case "producttag":
                //    values[NopPathRouteDefaults.ControllerFieldKey] = "Catalog";
                //    values[NopPathRouteDefaults.ActionFieldKey] = "ProductsByTag";
                //    values[NopPathRouteDefaults.ProducttagIdFieldKey] = urlRecord.EntityId;
                //    values[NopPathRouteDefaults.SeNameFieldKey] = urlRecord.Slug;
                //    break;
                //case "category":
                //    values[NopPathRouteDefaults.ControllerFieldKey] = "Catalog";
                //    values[NopPathRouteDefaults.ActionFieldKey] = "Category";
                //    values[NopPathRouteDefaults.CategoryIdFieldKey] = urlRecord.EntityId;
                //    values[NopPathRouteDefaults.SeNameFieldKey] = urlRecord.Slug;
                //    break;
                //case "manufacturer":
                //    values[NopPathRouteDefaults.ControllerFieldKey] = "Catalog";
                //    values[NopPathRouteDefaults.ActionFieldKey] = "Manufacturer";
                //    values[NopPathRouteDefaults.ManufacturerIdFieldKey] = urlRecord.EntityId;
                //    values[NopPathRouteDefaults.SeNameFieldKey] = urlRecord.Slug;
                //    break;
                //case "vendor":
                //    values[NopPathRouteDefaults.ControllerFieldKey] = "Catalog";
                //    values[NopPathRouteDefaults.ActionFieldKey] = "Vendor";
                //    values[NopPathRouteDefaults.VendorIdFieldKey] = urlRecord.EntityId;
                //    values[NopPathRouteDefaults.SeNameFieldKey] = urlRecord.Slug;
                //    break;
                //case "newsitem":
                //    values[NopPathRouteDefaults.ControllerFieldKey] = "News";
                //    values[NopPathRouteDefaults.ActionFieldKey] = "NewsItem";
                //    values[NopPathRouteDefaults.NewsItemIdFieldKey] = urlRecord.EntityId;
                //    values[NopPathRouteDefaults.SeNameFieldKey] = urlRecord.Slug;
                //    break;
                //case "blogpost":
                //    values[NopPathRouteDefaults.ControllerFieldKey] = "Blog";
                //    values[NopPathRouteDefaults.ActionFieldKey] = "BlogPost";
                //    values[NopPathRouteDefaults.BlogPostIdFieldKey] = urlRecord.EntityId;
                //    values[NopPathRouteDefaults.SeNameFieldKey] = urlRecord.Slug;
                //    break;
              
                default:
               
                    break;
            }

            return new ValueTask<RouteValueDictionary>(values);
        }

        #endregion
    }
}
