using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.ComponentModel;
using MvvmTutorials.ToolkitMessages.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkit.Mvvm.Messaging.Messages;

namespace MvvmTutorials.ToolkitMessages.ViewModels;

public partial class WeekTimerViewModel : ObservableObject
{
    public class WeekTimer
    {
        public string dayName { get; set; }
        public int time { get; set; }
    }
    public class WeekTimerModel
    {
        public List<WeekTimer> weeks { get; set; }
    }

}