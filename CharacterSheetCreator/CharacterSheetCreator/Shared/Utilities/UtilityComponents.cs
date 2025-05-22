using CharacterSheetCreator.Shared.Components;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace CharacterSheetCreator.Shared.Utilities;

public class UtilityComponents
{
    [Inject] protected IDialogService DialogService { get; set; } = default!;
    [Inject] private ISnackbar Snackbar { get; set; } = default!;
    
    public async Task<bool> ShowError(string message, string title = "Error")
    {
        return await ShowError(new List<string> { message }, title);
    }

    public async Task<bool> ShowError(List<string> messages, string title = "Error")
    {
        var parameters = new DialogParameters{{"Messages", messages}};
        var options = new DialogOptions{MaxWidth = MaxWidth.Large};

        var dlg = await DialogService.ShowAsync<ErrorDialog>(title, parameters, options);
        var result = await dlg.Result;
        return !result.Canceled;
    }
    
    public void ShowErrorIgnoreReturn(string message, string title = "Error")
    {
        ShowErrorIgnoreReturn(new List<string> { message }, title);
    }

    public void ShowErrorIgnoreReturn(List<string> messages, string title = "Error")
    {
        var parameters = new DialogParameters{{"Messages", messages}};
        var options = new DialogOptions{MaxWidth = MaxWidth.Large};

        DialogService.Show<ErrorDialog>(title, parameters, options);
    }

    public async Task<bool> ShowWarning(string message, string title = "Warning", string okText = "Ok", string cancelText = "Cancel", bool showCancel = false)
    {
        return await ShowWarning(new List<string> { message }, title, okText, cancelText, showCancel);
    }
    public async Task<bool> ShowWarning(List<string> messages, string title = "Warning", string okText = "Ok", string cancelText = "Cancel", bool showCancel = false)
    {
        var parameters = new DialogParameters
        {
            ["Messages"] = messages,
            ["OkText"] = okText,
            ["CancelText"] = cancelText,
            ["ShowCancel"] = showCancel
        
        };
        
        var options = new DialogOptions{MaxWidth = MaxWidth.Large, BackdropClick = false};
        var dlg = await DialogService.ShowAsync<WarningDialog>(title, parameters, options);
        var result = await dlg.Result;
        return !result.Canceled;
    }
    
    public void ShowSuccess(string message)
    {
            Snackbar.Add(message, Severity.Success);
    }
    
    public async Task<bool> Confirm(string message, string title = "Error")
    {
        var parameters = new DialogParameters
        {
            ["Message"] = message
        };
        var options = new DialogOptions{MaxWidth = MaxWidth.Large, BackdropClick = false};

        var dlg = await DialogService.ShowAsync<ConfirmationDialog>(title, parameters, options);
        var result = await dlg.Result;
        return !result.Canceled;
    }
}