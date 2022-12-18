using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace TemplateValidation
{
    public class FileValidator
    {
        private Templates _templates;

        public FileValidator(Templates templates)
        {
            _templates = templates;
        }

        public void ValidateAsset(AssetContext context)
        {
            if (!_templates.TryGetTemplateProperties(context.Template, out var template_properties))
            {
                Log.LogInfo($"{ context.AssetGUID} | { context.AssetName} cannot be verified. The template {context.Template} is missing in the templates file");
                return;
            }
            var mismatch_notintemplate = context.Properties.Where(x => !template_properties.Contains(x));

            if (mismatch_notintemplate.Count() == 0) return;

            Log.LogError($"{context.AssetGUID} | {context.AssetName} contains invalid properties: [" + String.Join(", ", mismatch_notintemplate) + "]");
        }
    }
}
