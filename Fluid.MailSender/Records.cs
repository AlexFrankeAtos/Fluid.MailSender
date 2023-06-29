namespace Fluid.MailSender;

public record Notification(string Title, string Description, Item[] Items);
public record Item(string Title, string Description);