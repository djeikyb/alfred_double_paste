using Ardalis.Specification;
using Ardalis.Specification.EntityFrameworkCore;

namespace DoublePaste.Core;

public interface IAggregateRoot
{
}

/// <inheritdoc/>
public interface IRepository<T> : IReadRepositoryBase<T> where T : class, IAggregateRoot
{
}

public class ClipboardRepository<T> : RepositoryBase<T>, IRepository<T> where T : class, IAggregateRoot
{
    private readonly ReadOnlyClipboardContext _context;

    public ClipboardRepository(ReadOnlyClipboardContext context) : base(context)
    {
        _context = context;
    }
}
