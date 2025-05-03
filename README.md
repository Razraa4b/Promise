# Promise Project

this project presents a cross-platform desktop application. It's for taking notes, keeping track of your life and generally trying to help your productivity!

## Navigation

- [Project Architecture](#project-architecture)
- [Nuget Packages](#nuget-packages)
- [Usage](#usage)

## Project Architecture

The project includes a `Clean Architecture` that is broken down into several layers. 

```
├───Promise.Application
│
├───Promise.Domain
│
├───Promise.Infrastructure
│
└───Promise.UI
```

- **Promise.Domain** - Promise.Application includes models, enumerations, and service interfaces. It is publicly available to other projects

- **Promise.Infrastructure** - implements almost all interfaces from Promise.Domain and can also add extensions to existing models. Available for Promise.UI only

- **Promise.Application** - includes the use and implementation of even more abstraction over interface implementations from Promise.Domain. The project is only referenced in Promise.UI

- **Promise.UI** - running project, represents the entire User-Interface component. It has references to all projects and can also implement several interfaces from Promise.Domain

## Nuget Packages

Used packages sorted by project:
- Promise.Domain:
    - Autofac
- Promise.Infrastructure:
    - Autofac
    - Microsoft.EntityFrameworkCore
    - Microsoft.EntityFrameworkCore.Sqlite
- Promise.Application:
    - Autofac
    - Avalonia.ReactiveUI
- Promise.UI:
    - Autofac
    - Avalonia
    - Avalonia.Xaml.Behaviors
    - Avalonia.Svg.Skia
    - Splat.Autofac

## Usage

> [!WARNING]
> The application is still in development. Some things may not work

Select `Promise.UI` directory, and execute this command:
```
dotnet run
```
