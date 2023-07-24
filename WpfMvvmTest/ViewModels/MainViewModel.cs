using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using WpfMvvmTest.Bases;
using WpfMvvmTest.Models;

namespace WpfMvvmTest.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        public MainViewModel() 
        {
            Title = "WPF Mvvm Test";
            Init();
        }

        private IList<Member> _member;
        public IList<Member> Members
        {
            get { return _member; }
            set { SetProperty(ref _member, value); }
        }

        private void Init()
        {
            Members = new ObservableCollection<Member>
            {
                new Member
                {
                    Id = 1,
                    Name = "TestName",
                    Phone = "010-1111-2222",
                    RegDate = DateTime.Now,
                    IsUse = true,
                }
            };

            NewCommand = new RelayCommand(() => { IsEditing = true; });
            CancelCommand = new RelayCommand(() => { IsEditing = false; });
        }

        public IRelayCommand NewCommand { get; set; }
        public IRelayCommand CancelCommand { get; set; }

        private bool _isEditing;
        public bool IsEditing
        {
            get { return _isEditing; }
            set { SetProperty(ref _isEditing, value); }
        }

    }
}
