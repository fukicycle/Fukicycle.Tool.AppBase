# Fukicycle.Tool.AppBase

[![Nuget](https://img.shields.io/nuget/v/Fukicycle.Tool.AppBase.svg)](https://www.nuget.org/packages/Fukicycle.Tool.AppBase)

[![Build and publish packages](https://github.com/fukicycle/Fukicycle.Tool.AppBase/actions/workflows/dotnet.yml/badge.svg)](https://github.com/fukicycle/Fukicycle.Tool.AppBase/actions/workflows/dotnet.yml)

This library provides blazor app base function. For example, error handling, display loading and display dialog.

Happy coding :)

## Features
1. Provides application base function.

## Installing and Getting started
### 1. install package.
`Fukicycle.Tool.AppBase` is available for download and installation as [NuGet packages](https://www.nuget.org/packages/Fukicycle.Tool.AppBase).
```
dotnet add package Fukicycle.Tool.AppBase --version <version>
```

### 2. Create your app.

1. Add using `Fukicycle.Tool.AppBase` (`_Imports.razor_`)
1. If you use other components, you have to add bellow namespace.
	- `Fukicycle.Tool.AppBase.Components`
	- `Fukicycle.Tool.AppBase.Components.Dialog`
	- `Fukicycle.Tool.AppBase.Store`

1. Register service. (`Program.cs`)
	```cs
	builder.Services.AddAppBase();
	```

1. Change `LayoutComponentBase` to `AppBaseLayoutComponentBase` (`MainLayout.razor`)
	```diff
	- @inherits LayoutComponentBase
	+ @inherits AppBaseLayoutComponentBase
	
	@Body
	```

1. If you want to use default loader and dialog, you have to add bellow. (`MainLayout.razor`)
	```cs
	@inherits AppBaseLayoutComponentBase
	
	@Body
	
	@if (StateContainer.IsLoading)
	{
	    <Loader /> @/* 👈 You can change your loader component */
	}
	@if (StateContainer.DialogContent != null)
	{
	    <Dialog /> @/* 👈 You can change your dialog component */
	}
	```

1. Finally, let each page inherit `ViewBase` and you're done! :)
	```cs
	@page "/"
	@inherits ViewBase
	<h1>Hello, world!</h1>
	```

1. Use `Execute` or `ExecuteAsync` to execute code while handling errors.
	```cs
	@page "/"
	@inherits ViewBase
	<h1>Hello, world!</h1>
	<div>Members</div>
	@foreach (string member in _members)
	{
	    <div>@member</div>
	}
	
	@foreach (string skill in _skills)
	{
	    <div>@skill</div>
	}
	@code {
	    private List<string> _members = new List<string>();
	    private List<string> _skills = new List<string>();
	    protected override async Task OnInitializedAsync()
	    {
	        //no loader
	        _members = Execute(GetMembers);
	
	        //loader
	        _skills = await ExecuteAsync(GetSkillsAsync, true);
	    }
	
	    private List<string> GetMembers()
	    {
	        if (Random.Shared.Next() % 2 == 0) throw new Exception("Something broke!!!");
	        return new List<string>
	        {
	            "fukicycle",
	            "you",
	            "other"
	        };
	    }
	
	    private async Task<List<string>> GetSkillsAsync()
	    {
	        await Task.Delay(3000);
	        return new List<string>
	        {
	            "C#",
	            "Kotlin",
	            "Java",
	            "Blazor"
	        };
	    }
	
	}
	```
## Contributing
Pull requests and stars are always welcome.
Contributions are what make the open source community such an amazing place to be learn, inspire, and create.   
Any contributions you make are greatly appreciated.

1. Fork the Project.
2. Create your Feature Branch(`git checkout -b feature/amazing_feature`).
3. Commit your Changes(`git commit -m 'Add some changes'`).
4. Push to the Branch(`git push origin feature/amazing_feature`).
5. Open a Pull Request.

## Author
- [fukicycle](https://github.com/fukicycle)

## License
MIT. Click [here](./LICENSE) for details.
