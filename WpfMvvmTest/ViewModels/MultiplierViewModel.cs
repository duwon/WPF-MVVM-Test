using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfMvvmTest.Models;

namespace WpfMvvmTest.ViewModels
{
    public class MultiplierViewModel : ObservableObject
    {
        private Multiplier? _multiModel = null;
        public Multiplier? MultiModel
        {
            get { return _multiModel; }
            set { SetProperty(ref _multiModel, value); }
        }

        public MultiplierViewModel()
        {
            MultiModel = new Multiplier();

            PropertyChanged += MultiplierViewModel_PropertyChanged;
        }

        private void MultiplierViewModel_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(MultiModel):
                    Console.WriteLine(e.PropertyName);
                    break;
            }
        }
    }
}
