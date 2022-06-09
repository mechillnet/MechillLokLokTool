using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using PNB.Service.Security;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;

namespace PNB.Web.Framework.Menu
{
    public class XmlSiteMap
    {

        internal static IServiceProvider _serviceProvider;
        public XmlSiteMap(IServiceProvider serviceProvider)
        {
            RootNode = new SiteMapNode();
            _serviceProvider = serviceProvider;
        }

        /// <summary>
        /// Root node
        /// </summary>
        public SiteMapNode RootNode { get; set; }

        /// <summary>
        /// Load sitemap
        /// </summary>
        /// <param name="physicalPath">Filepath to load a sitemap</param>



        public virtual void LoadFrom(string physicalPath)
        {
            string filePath = MapPath(physicalPath);
            string content = File.ReadAllText(filePath);

            if (!string.IsNullOrEmpty(content))
            {
                using (var sr = new StringReader(content))
                {
                    using (var xr = XmlReader.Create(sr,
                            new XmlReaderSettings
                            {
                                CloseInput = true,
                                IgnoreWhitespace = true,
                                IgnoreComments = true,
                                IgnoreProcessingInstructions = true
                            }))
                    {
                        var doc = new XmlDocument();
                        doc.Load(xr);

                        if ((doc.DocumentElement != null) && doc.HasChildNodes)
                        {
                            XmlNode xmlRootNode = doc.DocumentElement.FirstChild;
                            Iterate(RootNode, xmlRootNode);
                        }
                    }
                }
            }
        }

        private static void Iterate(SiteMapNode siteMapNode, XmlNode xmlNode)
        {
            PopulateNode(siteMapNode, xmlNode);

            foreach (XmlNode xmlChildNode in xmlNode.ChildNodes)
            {
                if (xmlChildNode.LocalName.Equals("siteMapNode", StringComparison.InvariantCultureIgnoreCase))
                {
                    var siteMapChildNode = new SiteMapNode();
                    siteMapNode.ChildNodes.Add(siteMapChildNode);

                    Iterate(siteMapChildNode, xmlChildNode);
                }
            }
        }

        private static void PopulateNode(SiteMapNode siteMapNode, XmlNode xmlNode)
        {
            //system name
            siteMapNode.SystemName = GetStringValueFromAttribute(xmlNode, "SystemName");

            //title


            siteMapNode.Title = GetStringValueFromAttribute(xmlNode, "name");
            siteMapNode.name = GetStringValueFromAttribute(xmlNode, "name");
            string resource;
            //switch (GetStringValueFromAttribute(xmlNode, "Resource"))
            //{
            //    case "Master":
            //        resource = StaticResources.ItemMenuMaster.ResourceManager.GetString(GetStringValueFromAttribute(xmlNode, "name"));
            //        siteMapNode.name = resource;
            //        break;
            //    case "Purchase":
            //        resource = StaticResources.ItemMenuPurchase.ResourceManager.GetString(GetStringValueFromAttribute(xmlNode, "name"));
            //        siteMapNode.name = resource;
            //        break;
            //    case "MainMenu":
            //        resource = StaticResources.ItemMenu.ResourceManager.GetString(GetStringValueFromAttribute(xmlNode, "name"));
            //        siteMapNode.name = resource;
            //        break;
            //    case "RawMaterial":
            //        resource = StaticResources.ItemMenuRawMaterial.ResourceManager.GetString(GetStringValueFromAttribute(xmlNode, "name"));
            //        siteMapNode.name = resource;
            //        break;
            //    case "Sales":
            //        resource = StaticResources.ItemMenuSales.ResourceManager.GetString(GetStringValueFromAttribute(xmlNode, "name"));
            //        siteMapNode.name = resource;
            //        break;
            //    default:
            //        siteMapNode.name = GetStringValueFromAttribute(xmlNode, "name");
            //        break;
            //}


            //routes, url
            string controllerName = GetStringValueFromAttribute(xmlNode, "controller");
            string actionName = GetStringValueFromAttribute(xmlNode, "action");
            string url = GetStringValueFromAttribute(xmlNode, "url");
            siteMapNode.Area = GetStringValueFromAttribute(xmlNode, "Area");
            if (!string.IsNullOrEmpty(controllerName) && !string.IsNullOrEmpty(actionName))
            {
                siteMapNode.ControllerName = controllerName;
                siteMapNode.ActionName = actionName;
            }
            else if (!string.IsNullOrEmpty(url))
            {
                siteMapNode.Url = url;
            }

            //image URL
            siteMapNode.IconClass = GetStringValueFromAttribute(xmlNode, "IconClass");

            //permission name
            var permissionNames = GetStringValueFromAttribute(xmlNode, "PermissionNames");
            if (!string.IsNullOrEmpty(permissionNames))
            {
                var permissionService =_serviceProvider.GetService<IPermissionService>();
                siteMapNode.Visible = permissionNames.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                   .Any(permissionName => permissionService.Authorize(permissionName.Trim()));
            }
            else
            {
                siteMapNode.Visible = true;
            }

            // Open URL in new tab
            var openUrlInNewTabValue = GetStringValueFromAttribute(xmlNode, "OpenUrlInNewTab");
            bool booleanResult;
            if (!string.IsNullOrWhiteSpace(openUrlInNewTabValue) && bool.TryParse(openUrlInNewTabValue, out booleanResult))
            {
                siteMapNode.OpenUrlInNewTab = booleanResult;
            }
        }

        private static string GetStringValueFromAttribute(XmlNode node, string attributeName)
        {
            string value = null;

            if (node.Attributes != null && node.Attributes.Count > 0)
            {
                XmlAttribute attribute = node.Attributes[attributeName];

                if (attribute != null)
                {
                    value = attribute.Value;
                }
            }

            return value;
        }
        public static string MapPath(string path)
        {
           

            //not hosted. For example, run in unit tests
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            path = path.Replace("~/", "").TrimStart('/').Replace('/', '\\');
            return Path.Combine(baseDirectory, path);
        }
    }
}
