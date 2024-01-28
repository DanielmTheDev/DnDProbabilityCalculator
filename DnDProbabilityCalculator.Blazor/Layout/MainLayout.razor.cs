using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;

namespace DnDProbabilityCalculator.Blazor.Layout;

public partial class MainLayout
{
    [Inject]
    private NavigationManager Navigation { get; set; } = null!;

    private void BeginLogOut()
        => Navigation.NavigateToLogout("authentication/logout");

    private void BeginLogIn()
        => Navigation.NavigateToLogin("authentication/login");

    private void NavigateHome()
        => Navigation.NavigateTo("/");
}