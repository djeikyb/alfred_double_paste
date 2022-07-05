using System.Text.Json.Serialization;

namespace DoublePaste.Core;

/// <summary>
/// https://www.alfredapp.com/help/workflows/inputs/script-filter/json/
/// </summary>
public class ScriptFilterDto
{
    [JsonPropertyName("items")]
    public List<ScriptFilterItem> Items { get; } = new();
}

public class ScriptFilterItem
{
    // /// <para>
    // /// uid : STRING (optional)
    // /// </para>
    // ///
    // /// <para>
    // /// This is a unique identifier for the item which allows help Alfred to
    // /// learn about this item for subsequent sorting and ordering of the user's
    // /// actioned results.
    // /// </para>
    // ///
    // /// <para>
    // /// It is important that you use the same UID throughout subsequent
    // /// executions of your script to take advantage of Alfred's knowledge and
    // /// sorting. If you would like Alfred to always show the results in the order
    // /// you return them from your script, exclude the UID field.
    // /// </para>
    // [JsonPropertyName("uid")]
    // public string? Uid { get; set; }

    public ScriptFilterItem(string title)
    {
        Title = title;
    }

    /// <para>
    /// title
    /// </para>
    ///
    /// <para>
    /// The title displayed in the result row. There are no options for this
    /// element and it is essential that this element is populated.
    /// </para>
    ///
    /// <para>
    /// "title": "Desktop"
    /// </para>
    [JsonPropertyName("title")]
    public string Title { get; init; }

    /// <summary>
    /// <para>
    /// text : OBJECT (optional)
    /// </para>
    /// </summary>
    [JsonPropertyName("text")]
    public ScriptFilterItemText? Text { get; set; }

    [JsonPropertyName("arg")]
    public List<string>? Args { get; set; }

    [JsonIgnore]
    public string Arg
    {
        set
        {
            Args ??= new List<string>();
            Args.Add(value);
        }
    }
}

public class ScriptFilterItemText
{
    [JsonPropertyName("copy")]
    public string? Copy { get; set; }
}
