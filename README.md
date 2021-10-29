![Banner](https://raw.githubusercontent.com/sharpyr/Aryth/refs/heads/master/media/aryth-banner.svg)

Basic math utils

[![Version](https://img.shields.io/nuget/vpre/Aryth.svg)](https://www.nuget.org/packages/Aryth)
[![Downloads](https://img.shields.io/nuget/dt/Aryth.svg)](https://www.nuget.org/packages/Aryth)
[![Dependent Libraries](https://img.shields.io/librariesio/dependents/nuget/Aryth.svg?label=dependent%20libraries)](https://libraries.io/nuget/Aryth)
[![Language](https://img.shields.io/badge/language-C%23-blueviolet.svg)](https://dotnet.microsoft.com/learn/csharp)
[![Compatibility](https://img.shields.io/badge/compatibility-.NET%20Standard%202.0-blue.svg)]()
[![License](https://img.shields.io/github/license/sharpyr/Aryth.svg)](https://github.com/sharpyr/Aryth/LICENSE)

## Features

- Extend functions for float and double. e.g. Round, AlmostEqual, Limit.
- Compute characteristic of array or 2d-array, e.g. Bound, Scale.
- Methods for polar coordinates, e.g. Rotate, Distance, Near.
- Parse algebraic expression and calculate, e.g. parse "3 + 4 * 2 / ( 1 - 5 ) ^ 2 ^ 3" and return 3.5.

## Content

| Package            | Content                                            |
|--------------------|----------------------------------------------------|
| `Aryth`            | The core library, including all Aryth sub projects |
| `Aryth.Bounds`     | Compute (min, max) for array and 2d-array          |
| `Aryth.Calculator` | Parse algebraic expression and calculate           |
| `Aryth.Comparer`   | Comparer for array sort                            |
| `Aryth.Coord`      | Functions for Descartes coordinates                |
| `Aryth.Flopper`    | Colorize array and 2d-array in terminal            |
| `Aryth.Math`       | Extend functions for float and double              |
| `Aryth.NiceScale`  | Compute (nice) scale for an array                  |
| `Aryth.Polar`      | Functions for polar coordinates                    |
| `Aryth.Types`      | Base types in Aryth series                         |

## Install

Aryth targets .NET Standard 2.0, fits both .NET and .NET Framework.

Install [Aryth package](https://www.nuget.org/packages/Aryth) and sub packages.

NuGet Package Manager:

```powershell
Install-Package Aryth
```

.NET CLI:

```shell
dotnet add package Aryth
```

All versions can be found [on nuget](https://www.nuget.org/packages/Aryth#versions-body-tab).

## Usage

### Convert color

```csharp
using Aryth.Calculator;

var expression = "3 + 4 * 2 / ( 1 - 5 ) ^ 2";
var result = expression.Calculate();
// result = 3.5
```

### Bound of array

```csharp
using Aryth.Bounds;

var samples = new[] { "foo", "bar", "zen", "16", "24", "32", "64" };
var (vector, bound) = vec.SoleBound();
// vector = [ NaN, NaN, NaN, 1, 2, 3 ]
// bound = ( 16, 64 )
```

### Flop an array
```csharp
using Aryth.Flopper;

var samples = new[] { "foo", "bar", "zen", "des" };
var flopper = FiniteFlopper<string>.From(samples);
Console.WriteLine($">> [next] {flopper.MoveNext()} {flopper.Current}"); // >> [next] True bar
Console.WriteLine($">> [next] {flopper.MoveNext()} {flopper.Current}"); // >> [next] True des
Console.WriteLine($">> [next] {flopper.MoveNext()} {flopper.Current}"); // >> [next] True foo
Console.WriteLine($">> [next] {flopper.MoveNext()} {flopper.Current}"); // >> [next] True zen
Console.WriteLine($">> [next] {flopper.MoveNext()} {flopper.Current}"); // >> [next] False zen
```

> 
# Examples
---------------------
Aryth has a test suite in the [test project](https://github.com/sharpyr/Aryth/tree/master/Aryth.Test/Src).

## Feedback

Aryth is licensed under the [MIT](https://github.com/sharpyr/Aryth/LICENSE) license.

Bug report and contribution are welcome at [the GitHub repository](https://github.com/sharpyr/Aryth).