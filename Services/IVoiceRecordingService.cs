namespace my_voice.Services;

public interface IVoiceRecordingService
{
    Task<List<VoiceRecording>> GetAllAsync();
    Task<VoiceRecording?> GetByIdAsync(int id);
    Task<VoiceRecording> CreateAsync(VoiceRecording recording);
    Task DeleteAsync(int id);
    Task<List<VoiceRecording>> SearchAsync(string searchTerm);
}

public class VoiceRecordingService : IVoiceRecordingService
{
    private readonly VoiceDbContext _db;

    public VoiceRecordingService(VoiceDbContext db)
    {
        _db = db;
    }

    public async Task<List<VoiceRecording>> GetAllAsync()
    {
        return await _db.VoiceRecordings
            .OrderByDescending(r => r.CreatedAt)
            .ToListAsync();
    }

    public async Task<VoiceRecording?> GetByIdAsync(int id)
    {
        return await _db.VoiceRecordings.FindAsync(id);
    }

    public async Task<VoiceRecording> CreateAsync(VoiceRecording recording)
    {
        _db.VoiceRecordings.Add(recording);
        await _db.SaveChangesAsync();
        return recording;
    }

    public async Task DeleteAsync(int id)
    {
        var recording = await _db.VoiceRecordings.FindAsync(id);
        if (recording != null)
        {
            _db.VoiceRecordings.Remove(recording);
            await _db.SaveChangesAsync();
        }
    }

    public async Task<List<VoiceRecording>> SearchAsync(string searchTerm)
    {
        return await _db.VoiceRecordings
            .Where(r => r.Title.Contains(searchTerm) || 
                       (r.Tags != null && r.Tags.Contains(searchTerm)))
            .OrderByDescending(r => r.CreatedAt)
            .ToListAsync();
    }
}
