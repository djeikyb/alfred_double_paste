using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace DoublePaste.Core;

public class ReadOnlyClipboardContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var dbpath = PathToClipboardDb();
        var cb = new SqliteConnectionStringBuilder
        {
            Mode = SqliteOpenMode.ReadOnly,
            DataSource = dbpath,
        };
        optionsBuilder.UseSqlite(cb.ConnectionString);
    }

    /// <inheritdoc />
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Clipboard>().HasNoKey();
    }

    private static string PathToClipboardDb()
    {
        var homedir = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
        const string filename = "clipboard.alfdb";
        var dbpath = Path.Combine(homedir, "Library/Application Support/Alfred/Databases", filename);
        return dbpath;
    }
}
