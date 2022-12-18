using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace TemplateValidation
{
    internal class Assets
    {
        private XmlDocument? AssetsFile;

        public void BindTo(String Filename)
        {
            AssetsFile = new XmlDocument();
            AssetsFile.Load(Filename);
        }

        public AssetContext[] GetAssets()
        {
            if (AssetsFile is null)
                throw new InvalidOperationException("Initialize Assets first!");

            return AssetsFile
                .SelectNodes("//Asset[Values]")?
                .Cast<XmlNode>()
                .Select(x => AssetContext.FromNode(x))
                .Where(x => x is not null)
                .ToArray() 
                ?? Array.Empty<AssetContext>();
        }
    }
}
