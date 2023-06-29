using Microsoft.Extensions.FileProviders;

namespace Fluid.MailSender;

public interface ITemplateProcessor
{
    Task<string> ProcessAsync(string template, object model);
}

internal class TemplateProcessor : ITemplateProcessor
{
    private readonly TemplateOptions _options;

    public TemplateProcessor(IFileProvider fileProvider)
    {
        _options = new TemplateOptions();
        _options.FileProvider = fileProvider;
        _options.MemberAccessStrategy.Register<Notification>();
        _options.MemberAccessStrategy.Register<Item>();
    }

    public async Task<string> ProcessAsync(string template, object model)
    {
        var parser = new FluidParser();
        if (parser.TryParse(template, out var fluidTemplate, out var error))
        {
            var context = new TemplateContext(model, _options);
            var result = await fluidTemplate.RenderAsync(context);
            return result;
        }
        else
        {
            throw new Exception(error.ToString());
        }
    }
}
