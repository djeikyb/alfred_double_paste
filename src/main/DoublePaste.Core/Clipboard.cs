namespace DoublePaste.Core;

public class Clipboard : IAggregateRoot

{
    public Clipboard(string item, long ts, string app, string apppath, int dataType, string dataHash)
    {
        Item = item;
        Ts = ts;
        App = app;
        Apppath = apppath;
        DataType = dataType;
        DataHash = dataHash;
    }

    public string Item { get; init; }
    public long Ts { get; init; }
    public string App { get; init; }
    public string Apppath { get; init; }
    public int DataType { get; init; }
    public string DataHash { get; init; }
}
