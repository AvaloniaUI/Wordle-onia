using Avalonia.Web.Blazor;

namespace Wordle.Web;

public partial class App
{
    protected override void OnParametersSet()
    {
        base.OnParametersSet();
        
        WebAppBuilder.Configure<Wordle.App>()
            .SetupWithSingleViewLifetime();
    }
}