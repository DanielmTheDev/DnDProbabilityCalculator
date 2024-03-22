using DnDProbabilityCalculator.Core.Adventuring;
using DnDProbabilityCalculator.Shared.PartyCreation;
using FluentResults;
using Microsoft.AspNetCore.Components;
using Microsoft.FluentUI.AspNetCore.Components;

namespace DnDProbabilityCalculator.Blazor.PartyManipulation;

public partial class UpdatePartyPage
{
    [Inject]
    public IPartyClient PartyClient { get; set; } = null!;

    [Inject]
    public IToastService ToastService { get; set; } = null!;

    [Inject]
    public NavigationManager NavigationManager { get; set; } = null!;

    [Parameter]
    public string PartyId { get; set; } = string.Empty;

    private bool _isFormDisabled;
    private bool _isFormLoading;
    private CreatePartyDto _party = new() { Characters = [new()] };

    protected override async Task OnInitializedAsync()
    {
        if (!string.IsNullOrEmpty(PartyId))
        {
            _isFormLoading = true;
            var result = await PartyClient.Get(PartyId);
            if (result.IsFailed)
            {
                ToastService.ShowToast(ToastIntent.Error, result.Errors.First().Message);
            }

            _party = new(result.Value);
            _isFormLoading = false;
        }
    }

    private async Task Submit(CreatePartyDto createPartyDto)
    {
        _isFormDisabled = true;
        var result = await PartyClient.Update(Guid.Parse(PartyId), createPartyDto);
        if (result.IsSuccess)
        {
            ShowSuccessToast(result.Value, createPartyDto.Name!);
            ResetForm();
            _isFormDisabled = false;
        }
        else
        {
            ShowErrorToast(result.Errors.First());
            _isFormDisabled = false;
        }
    }

    private void ResetForm()
        => _party = new() { Characters = [new()] };

    private void ShowSuccessToast(Party party, string partyName)
        => ToastService.ShowToast(ToastIntent.Success, $"\"{partyName}\" successfully updated",
            topAction: "Go", callback: new EventCallback<ToastResult>(this, () => NavigationManager.NavigateTo($"/probability-calculator/{party.Id}")));

    private void ShowErrorToast(IError result)
        => ToastService.ShowToast(ToastIntent.Error, $"Error: {result.Message}");
}