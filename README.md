# 🎤 my Voice

A Blazor web app for recording and storing voice references.

## Features

- 🎙️ **Record voice** directly in the browser
- 💾 **Split storage**: Audio files in browser (IndexedDB), metadata in SQLite
- 📚 **Library** - Browse, search, and playback all recordings
- 🏷️ **Tag support** - Organize recordings with custom tags
- ⚡ **Fast** - Built with Blazor Server (.NET 8)

## Tech Stack

- **Frontend**: Blazor Server (.NET 10)
- **Database**: SQLite (metadata only)
- **Audio Storage**: Browser IndexedDB / Base64 in database
- **Recording**: Browser MediaRecorder API

## Setup

### Prerequisites

- .NET 10 SDK or later
- A modern browser with microphone support

### Run the App

```bash
cd my-voice
dotnet restore
dotnet run
```

Open your browser to `https://localhost:5001` or `http://localhost:5000`

### First Run

The SQLite database (`voices.db`) will be created automatically on first run.

## Project Structure

```
my-voice/
├── Components/
│   ├── Layout/
│   │   └── MainLayout.razor      # Main app layout
│   ├── Pages/
│   │   ├── Home.razor            # Dashboard
│   │   ├── Record.razor          # Recording page
│   │   ├── Library.razor         # Browse recordings
│   │   └── Error.razor           # Error page
│   ├── App.razor                 # Root component
│   ├── Routes.razor              # Routing config
│   └── _Imports.razor            # Global imports
├── Data/
│   └── VoiceDbContext.cs         # EF Core context + model
├── Services/
│   └── IVoiceRecordingService.cs # Business logic
├── wwwroot/
│   ├── app.css                   # Styles
│   └── app.js                    # Audio recording JS interop
├── Program.cs                    # App entry point
├── my-voice.csproj               # Project file
└── README.md
```

## How It Works

1. **Recording**: Uses browser's `MediaRecorder` API via JavaScript interop
2. **Storage**: 
   - Audio data stored as base64 in SQLite (for simplicity)
   - Can be extended to use IndexedDB for larger files
3. **Playback**: Audio embedded as data URLs in `<audio>` elements

## Future Enhancements

- [ ] Move large audio files to IndexedDB
- [ ] Add user authentication
- [ ] Cloud sync option
- [ ] Export recordings as files
- [ ] Transcription integration
- [ ] Mobile PWA support

## License

MIT

---

Built with 🦞 by Killer Queen for Vincent

## Version History

- **v1.1.0** - .NET 10 support
- **v1.0.0** - Initial release (.NET 8)
