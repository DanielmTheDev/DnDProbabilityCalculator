using DnDProbabilityCalculator.Shared.PartyCreation;
using FluentResults;
using Microsoft.AspNetCore.Components;
using Microsoft.FluentUI.AspNetCore.Components;

namespace DnDProbabilityCalculator.Blazor.PartyCreation;

public partial class CreatePartyPage
{
    [Inject]
    public IPartyClient PartyClient { get; set; } = null!;

    [Inject]
    public IToastService ToastService { get; set; } = null!;

    [Inject]
    public NavigationManager NavigationManager { get; set; } = null!;

    private bool _isFormDisabled;
    private CreatePartyDto _party = new() { Characters = [new()] };

    private async Task Submit()
    {
        _isFormDisabled = true;
        var result = await PartyClient.Save(_party);
        if (result.IsSuccess)
        {
            ShowSuccessToast(result.Value, _party.Name!);
            ResetForm();
            _isFormDisabled = false;
        }
        else
        {
            ShowErrorToast(result.Errors.First());
            _isFormDisabled = false;
        }
    }

    private void ShowErrorToast(IError result)
        => ToastService.ShowToast(ToastIntent.Error, $"Error: {result.Message}");

    private void ResetForm()
        => _party = new() { Characters = [new()] };

    private void ShowSuccessToast(string partyId, string partyName)
        => ToastService.ShowToast(ToastIntent.Success, $"\"{partyName}\" successfully created",
            topAction: "Go", callback: new EventCallback<ToastResult>(this, () => NavigationManager.NavigateTo($"/probability-calculator/{partyId}")));
}