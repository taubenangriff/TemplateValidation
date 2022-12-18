using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace TemplateValidation
{
    public class Templates
    {
        private XmlDocument? BindingDocument;

        private Dictionary<String, String[]> StoredValues; 

        public Templates() {
            StoredValues = new(); 
        }

        public void Bind(String Filename)
        {
            BindingDocument = new XmlDocument();
            BindingDocument.Load(Filename);
        }

        public bool TryGetTemplateProperties(String TemplateName, out String[] properties)
        {
            properties = null;
            if (StoredValues.ContainsKey(TemplateName))
            {
                properties = StoredValues[TemplateName];
                return true;
            }
            if (TryLoadFromTemplateFile(TemplateName, out var props))
            {
                StoredValues.Add(TemplateName, props);
                properties = props;
                return true;
            }
            return false;
        }

        private bool TryLoadFromTemplateFile(String TemplateName, out String[] properties)
        {
            properties = null;
            if (BindingDocument is null)
                throw new InvalidOperationException("Templates not bound to the templates.xml file");

            var node = BindingDocument.SelectSingleNode($"//Template[Name='{TemplateName}']/Properties");
            if (node is null) 
                return false;

            properties = node!
                .ChildNodes
                .OfType<XmlNode>()
                .Select(x => x.Name)
                .ToArray();
            return true;
        }
    }
}
