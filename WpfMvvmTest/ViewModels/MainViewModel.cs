using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using WpfMvvmTest.Bases;

namespace WpfMvvmTest.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        public MainViewModel() 
        {
            Title = "WPF Mvvm Test";
        }
    }
}
