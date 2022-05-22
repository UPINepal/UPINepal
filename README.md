

![alt text](https://static.upinepal.com/logo/ReadMeLogo.jpg)

## UPI Nepal
(all about UPI in Nepal)

UPI Nepal consist of two repository 

-----------------
1. [UPINepal](https://github.com/UPINepal/UPINepal) : (Built on ASP.NET core and Blazor (.NET 7))
2. [Static](https://github.com/UPINepal/Static) : (Contains Static data and Json data for Apps and Banks)

## Getting started

----------------------
### Prerequisites
* [.NET 7 Preview 4](https://dotnet.microsoft.com/en-us/download/dotnet/7.0)
* [Visual Studio 2022 Preview](https://visualstudio.microsoft.com/vs/preview/) or [VS Code](https://code.visualstudio.com/) or [Rider](https://www.jetbrains.com/rider/)
  (prefer VS 2022 preview due to preview version of .NET 7)

### Setting up project locally
1. In VS studio or Rider select `Server` project as startup project and run
2. In VS code can naviate to `src/Server` and then  type  ```dotnet run``` and hit enter
3. if you want to reference staicai content locally from [Static](https://github.com/UPINepal/Static) then in that case also have to run static project locally and then need to update
```csharp
namespace Shared;

public static class StaticContent
{
    public const string StaticUrl = "https://static.upinepal.com"; //(Update to local server)
}  
```
_to run static [Static](https://github.com/UPINepal/Static) project locally can use any tools or server
for exmaple
[dotnet-serve](https://github.com/natemcmaster/dotnet-serve)_



## Contributing
1. To improve and optimize code side of things create pull request or open issue on [UPINepal](https://github.com/UPINepal/UPINepal) 
1. To add/update data and logos Create pull request or open issue on [Static](https://github.com/UPINepal/Static) 
