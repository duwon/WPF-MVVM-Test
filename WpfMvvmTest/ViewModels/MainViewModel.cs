using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
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
            SelectionChangedCommand = new RelayCommand<object>(OnSelectionChanged);
            EditCommand = new RelayCommand(() => IsEditing = true, () => EditMember != null);
            DeleteCommand = new RelayCommand(OnDelete, () => EditMember != null);

            PropertyChanged += MainViewModel_PropertyChanged;
        }

        public IRelayCommand NewCommand { get; set; }
        public IRelayCommand CancelCommand { get; set; }
        public IRelayCommand SelectionChangedCommand { get; set; }
        public IRelayCommand EditCommand { get; set; }
        public IRelayCommand DeleteCommand { get; set; }

        private bool _isEditing;
        public bool IsEditing
        {
            get { return _isEditing; }
            set { SetProperty(ref _isEditing, value); }
        }

        private Member _editMember;
        public Member EditMember
        {
            get { return _editMember; }
            set { SetProperty(ref _editMember, value); }
        }

        private void OnSelectionChanged(object para)
        {
            var args = para as SelectionChangedEventArgs;
            if (args == null)
            {
                return;
            }

            if (args.AddedItems.Count == 0)
            {
                EditMember = null;
                return;
            }
            else
            {
                var member = args.AddedItems[0] as Member;
                EditMember = (Member)member.Clone();
            }

        }

        private void MainViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(EditMember):
                    EditCommand.NotifyCanExecuteChanged();
                    DeleteCommand.NotifyCanExecuteChanged();
                    break;
            }
        }

        private void OnDelete()
        {
        }
    }
}
