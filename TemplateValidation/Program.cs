using TemplateValidation;

if (args.Length < 1)
{
    Console.WriteLine("No asset filename specified, terminating");
    return;
}

Log.Init();

Templates templates = new Templates();
templates.Bind(Path.Combine(AppContext.BaseDirectory, "templates.xml"));

Assets assets = new Assets();
assets.BindTo(args[0]);

FileValidator validator = new FileValidator(templates);

var all_assetcontexts = assets.GetAssets();

Log.LogInfo("Asset Contexts loaded, starting validation...");
foreach (var context in all_assetcontexts)
{
    validator.ValidateAsset(context);
}
Log.LogInfo("All assets validated!");

Log.Dispose();