# Packaging (MSIX + App Installer)

Fiber Img App uses **single-project MSIX** on `src/Ttu.FiberImgApp` (`EnableMsixTooling`).

## Identity

| Field | Value |
| --- | --- |
| Name | `TTU.FiberImgApp` |
| Publisher | `CN=TTU` |
| Display name | Fiber Img App |

Publisher **must** match the subject of your signing certificate (`CN=TTU`).

## Local package (Visual Studio)

1. Open `Ttu.FiberImgApp.sln`.
2. Right-click `Ttu.FiberImgApp` → **Package and Publish** → **Create App Packages…**
3. Choose **Sideloading** and enable **automatic updates**.
4. Set Installation URL to your Releases download folder, e.g.  
   `https://github.com/<you>/ttu-fiber-img-app/releases/latest/download/`
5. Sign with the self-signed `.pfx` created in SETUP.

## App Installer template

`FiberImgApp.appinstaller.template` is filled by GitHub Actions (or manually) with:

- `{{VERSION}}` — e.g. `0.1.0.0`
- `{{APPINSTALLER_URI}}` — trailing-slash Releases download URL

## Optional classic packaging project

If you prefer a separate Windows Application Packaging Project:

1. In VS: Add → New Project → **Windows Application Packaging Project**
2. Name it `Ttu.FiberImgApp.Package` under `packaging/`
3. Add a project reference to `Ttu.FiberImgApp`
4. Move/align identity to `CN=TTU` / `TTU.FiberImgApp`
