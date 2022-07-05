namespace DoublePaste.Core;

public interface IClipboardService
{
    Task<ScriptFilterDto> LastTwoForTwitterThreads(CancellationToken ct);
}

public class ClipboardService : IClipboardService
{
    private readonly IRepository<Clipboard> _repo;

    public ClipboardService(IRepository<Clipboard> repo)
    {
        _repo = repo;
    }

    public async Task<ScriptFilterDto> LastTwoForTwitterThreads(CancellationToken ct)
    {
        var found = await _repo.ListAsync(new LastTwoSpec(), ct);
        if (found == null)
        {
            throw new Exception("Nothing in clipboard?");
        }

        var formatted = Format(found);

        var text = new ScriptFilterItemText();
        text.Copy = formatted;

        var item = new ScriptFilterItem("some title?");
        item.Arg = formatted;
        item.Text = text;

        var dto = new ScriptFilterDto();
        dto.Items.Add(item);
        return dto;
    }

    internal static string Format(List<string> lastTwo)
    {
        if (lastTwo.Count != 2)
        {
            return $"🚨🐙🚨🐙 Expected two clips, found: {lastTwo.Count}";
        }

        var message = lastTwo[0];
        var link = lastTwo[1];
        return $"{link}\n\n{message}\n\n\n";
    }
}
