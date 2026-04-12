using Microsoft.EntityFrameworkCore;

namespace my_voice.Data;

public class VoiceDbContext : DbContext
{
    public VoiceDbContext(DbContextOptions<VoiceDbContext> options)
        : base(options)
    {
    }

    public DbSet<VoiceRecording> VoiceRecordings => Set<VoiceRecording>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<VoiceRecording>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Title).IsRequired().HasMaxLength(200);
            entity.Property(e => e.Description).HasMaxLength(2000);
            entity.Property(e => e.Tags).HasMaxLength(500);
            entity.Property(e => e.AudioData).IsRequired();
            entity.Property(e => e.DurationSeconds).IsRequired();
            entity.Property(e => e.CreatedAt).IsRequired();
            entity.HasIndex(e => e.CreatedAt);
            entity.HasIndex(e => e.Title);
        });
    }
}

public class VoiceRecording
{
    public int Id { get; set; }
    public string Title { get; set; } = "";
    public string? Description { get; set; }
    public string? Tags { get; set; }
    public byte[] AudioData { get; set; } = Array.Empty<byte>();
    public int DurationSeconds { get; set; }
    public DateTime CreatedAt { get; set; }
}
