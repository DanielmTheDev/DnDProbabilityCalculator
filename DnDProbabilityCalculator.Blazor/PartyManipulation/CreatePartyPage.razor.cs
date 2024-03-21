﻿using DnDProbabilityCalculator.Shared.PartyCreation;
using FluentResults;
using Microsoft.AspNetCore.Components;
using Microsoft.FluentUI.AspNetCore.Components;

namespace DnDProbabilityCalculator.Blazor.PartyManipulation;

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


    private async Task Submit(CreatePartyDto createPartyDto)
    {
        _isFormDisabled = true;
        var result = await PartyClient.Save(createPartyDto);
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

    private void ShowSuccessToast(string partyId, string partyName)
        => ToastService.ShowToast(ToastIntent.Success, $"\"{partyName}\" successfully created",
            topAction: "Go", callback: new EventCallback<ToastResult>(this, () => NavigationManager.NavigateTo($"/probability-calculator/{partyId}")));

    private void ShowErrorToast(IError result)
        => ToastService.ShowToast(ToastIntent.Error, $"Error: {result.Message}");
}