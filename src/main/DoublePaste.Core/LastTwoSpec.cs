using Ardalis.Specification;

namespace DoublePaste.Core;

public sealed class LastTwoSpec : Specification<Clipboard, string>
{
    // https://www.alfredforum.com/topic/18061-accessing-clipboard-history-inside-a-workflow/#comment-93346

    public LastTwoSpec()
    {
        Query.OrderByDescending(x => x.Ts);
        Query.Where(x => x.DataType == 0); // not sure what the datatypes are?
        Query.Take(2);
        Query.Select(x => x.Item);
    }
}
