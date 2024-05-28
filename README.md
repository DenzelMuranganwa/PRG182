# Applicant Tracker Console System

A C# console application for recording applicants and reviewing qualification and activity statistics. The project demonstrates a menu-driven workflow with local file persistence across separate coursework milestones.

## Core features

- Register applicant records with personal and activity fields.
- Save and reload applicant data from a local text file.
- Review token qualification, average pizza counts, and summary statistics.
- Explore two coursework milestones, with the second milestone providing the later application snapshot.

## Technology

![C#](https://img.shields.io/badge/C%23-.NET%20Framework-512BD4?logo=dotnet&logoColor=white)

## Architecture and data flow

The main program coordinates the menu, applicant operations, and persistence. Each applicant is represented by a small record-like class. The current storage format is comma-separated text written beside the running executable; it is intended for coursework demonstrations, not concurrent or sensitive production data.

This was a **team project** completed as module coursework.

## Requirements

- Windows
- Visual Studio or Build Tools for Visual Studio with the .NET Framework 4.7.2 targeting pack

## Build and run

Open **Developer PowerShell for Visual Studio** at the repository root, then build the second-milestone solution:

```powershell
msbuild ".\PRG182 Project.sln" /p:Configuration=Release
```

Run the resulting console application:

```powershell
& ".bin\Release\PRG182 Project.exe"
```

The first run creates the application data file if it does not exist. The earlier milestone is retained separately under `PRG182 Project\`.

## Development notes

- The data file is plain text and has no escaping for commas in field values.
- The application is a learning project and does not provide database transactions, access controls, or protection for personal information.
- Use fictional records when demonstrating the application.

## Roadmap

- Replace comma-separated text persistence with a validated, versioned format.
- Separate applicant, persistence, and console-interaction responsibilities.
- Add automated tests for parsing, qualification rules, and statistics.
- Improve input validation and report malformed records without terminating the session.

