# BlazorWithRedux
Implementing [Redux](https://redux.js.org/introduction/three-principles) pattern in Blazor WebAssembly using C#9/C#10 and [Fluxor](https://github.com/mrpmorris/Fluxor) on [.NET 6](https://dotnet.microsoft.com/download/dotnet/6.0).

## Dependencies added
- `Fluxor.Blazor.Web` [v5.2.0](https://www.nuget.org/packages/Fluxor.Blazor.Web/5.2.0)
- `Fluxor.Blazor.Web.ReduxDevTools` [v5.2.0](https://www.nuget.org/packages/Fluxor.Blazor.Web.ReduxDevTools/5.2.0)

## F5 Experience
1. Install .NET 6 https://dotnet.microsoft.com/download/dotnet/6.0
2. Clone this repository
    ```cmd
    git clone https://github.com/pRob3/BlazorWithRedux
    ```
3. Restore dependencies using
    ```cmd
    dotnet restore
    ```
4. Run Blazor WebAssembly in watch mode.
    ```cmd
    dotnet watch run
    ```
5. Your default browser should automatically opened for you.

## Notes for working with Fluxor/Redux
Install Redux DevTools
https://chrome.google.com/webstore/detail/redux-devtools/lmhkpmbekcpmknklioeibfkpmmfibljd

