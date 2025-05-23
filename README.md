```
   /$$$$$$$                                   /$$                    
   | $$__  $$                                 |__/                    
   | $$  \ $$ /$$$$$$   /$$$$$$  /$$$$$$/$$$$  /$$  /$$$$$$$  /$$$$$$ 
   | $$$$$$$//$$__  $$ /$$__  $$| $$_  $$_  $$| $$ /$$_____/ /$$__  $$
   | $$____/| $$  \__/| $$  \ $$| $$ \ $$ \ $$| $$|  $$$$$$ | $$$$$$$$
   | $$     | $$      | $$  | $$| $$ | $$ | $$| $$ \____  $$| $$_____/
   | $$     | $$      |  $$$$$$/| $$ | $$ | $$| $$ /$$$$$$$/|  $$$$$$$
   |__/     |__/       \______/ |__/ |__/ |__/|__/|_______/  \_______/

                      UI Application (Cross platform)
                                Not Released
```

<p align="center">
    <img src="https://img.shields.io/badge/C%23-black?style=flat-square&logo=sharp&logoColor=lightblue&logoSize=auto&label=LANGUAGE&labelColor=gray&color=purple"/>
    <img src="https://img.shields.io/badge/CROSS-black?style=flat-square&logo=appveyor&logoColor=yellow&logoSize=auto&label=PLATFORM&labelColor=gray&color=blue"/>
    <img src="https://img.shields.io/badge/CLEAN-black?style=flat-square&logo=appveyor&logoColor=red&logoSize=auto&label=ARCHITECTURE&labelColor=gray&color=green"/>
</p>

## 📖 Promise Project Overview

this project presents a cross-platform desktop application. It's for taking notes, keeping track of your life and generally trying to help your productivity!

## Navigation

- [Project Architecture](#project-architecture)
- [Nuget Packages](#nuget-packages)
- [Usage](#usage)

## Project Architecture

- **Promise.Domain** - includes models, enumerations, and service interfaces. It is publicly available to other projects

- **Promise.Infrastructure** - implements almost all interfaces from Promise.Domain and can also add extensions to existing models. Available for Promise.UI only

- **Promise.Application** - includes the use and implementation of even more abstraction over interface implementations from Promise.Domain. The project is only referenced in Promise.UI

- **Promise.UI** - executable project, represents the entire User-Interface component. It has references to all projects and can also implement several interfaces from Promise.Domain

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
