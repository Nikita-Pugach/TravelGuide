# TravelGuide installer

Builds a Windows installer for the web app: the user installs it, it starts the
app on a local port and opens the browser. No .NET, no terminal, no SDK.

## Build

```powershell
.\Installer\build.ps1
```

The script does two things:

1. `dotnet publish -c Release -r win-x64 --self-contained true` — the app is
   published **self-contained**, so the runtime travels with it and the target
   machine does not need .NET installed.
2. Compiles `TravelGuideSetup.iss` with Inno Setup.

Result:

```
Installer\Output\TravelGuideSetup.exe   (~80 MB)
```

`Output\` is ignored by git. A built installer belongs in
[GitHub Releases](https://github.com/Nikita-Pugach/TravelGuide/releases), not in
the repository — binaries in git history make cloning slow forever.

## Requirements to build

- [.NET 9 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
- [Inno Setup 6](https://jrsoftware.org/isdl.php)

Neither is needed by the person who runs the installer.

## What the installer does

- installs the app into `Program Files\TravelGuide` (or the per-user equivalent
  when the account cannot write there);
- creates Start Menu entries and an optional desktop shortcut;
- registers an uninstaller in "Add or remove programs";
- offers to launch the app right away.

## Demo accounts

The database is created and seeded on the first run.

| Role | Email | Password |
|---|---|---|
| Administrator | `admin@travelguide.com` | `Admin123!` |
| Manager | `manager@travelguide.com` | `Manager123!` |
| Tourist | `tourist@travelguide.com` | `Tourist123!` |
