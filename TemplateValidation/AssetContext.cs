using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Linq;

namespace TemplateValidation
{
    public class AssetContext
    {
        public String AssetGUID { get; init; }
        public String? AssetName { get; init; }
        public String[] Properties { get; init; }
        public String Template { get; init; }

        private AssetContext() {
        
        }

        public static AssetContext? FromNode(XmlNode node)
        {
            var assetGuid = node.SelectSingleNode("./Values/Standard/GUID")?.InnerText;
            var assetName = node.SelectSingleNode("./Values/Standard/Name")?.InnerText;
            var template = node.SelectSingleNode("./Template")?.InnerText;

            var properties = node
                .SelectSingleNode("./Values")?
                .ChildNodes
                .OfType<XmlNode>()
                .Select(x => x.Name)
                .ToArray();

            if (assetGuid is null || properties is null || template is null)
                return null;

            return new AssetContext()
            {
                AssetGUID = assetGuid,
                AssetName = assetName,
                Properties = properties,
                Template = template
            };
        }
    }
}
