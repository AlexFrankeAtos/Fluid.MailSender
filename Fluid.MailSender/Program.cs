using Fluid;
using Fluid.MailSender;
using Microsoft.Extensions.FileProviders;
using System.Reflection;

var notification = new Notification("This is the Title", "This is the Description", new Item[] {
    new Item("Item 1", "Item 1 Description"),
    new Item("Item 2", "Item 2 Description")
});

var assemblyPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
var filePath = Path.Combine(assemblyPath!, "Templates");
var fileProvider = new PhysicalFileProvider(filePath);

var processor = new TemplateProcessor(fileProvider);

var source = await File.ReadAllTextAsync(@".\Templates\Mail_Body.liquid");
var result = await processor.ProcessAsync(source, notification);
Console.WriteLine(result);
