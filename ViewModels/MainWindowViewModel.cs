using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkit.Mvvm.Messaging.Messages;
//using MvvmTutorials.ToolkitMessages.Models;

namespace MvvmTutorials.ToolkitMessages.ViewModels;

public partial class MainWindowViewModel: ObservableRecipient
{
    // 测试用
    [ObservableProperty]
    private string title = "hello";

    [RelayCommand]
    private void ClickMe()
    {
        Title = "world";
    }
}