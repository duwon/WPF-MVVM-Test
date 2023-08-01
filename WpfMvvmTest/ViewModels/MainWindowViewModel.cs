using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
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

            NewCommand = new RelayCommand(OnNew);
            CancelCommand = new RelayCommand(OnCancel);
            SelectionChangedCommand = new RelayCommand<object>(OnSelectionChanged);
            EditCommand = new RelayCommand(() => IsEditing = true, () => EditMember != null);
            DeleteCommand = new RelayCommand(OnDelete, () => EditMember != null);
            SaveCommand = new RelayCommand(OnSave);

            PropertyChanged += MainViewModel_PropertyChanged;
        }


        public IRelayCommand NewCommand { get; set; }
        public IRelayCommand CancelCommand { get; set; }
        public IRelayCommand SelectionChangedCommand { get; set; }
        public IRelayCommand EditCommand { get; set; }
        public IRelayCommand DeleteCommand { get; set; }
        public IRelayCommand SaveCommand { get; set; }

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
            var result = MessageBox.Show("선택된 아이템을 삭제하시겠습니까?", "삭제 확인", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.No)
            {
                return;
            }
            IsEditing = false;
            var removeMember = Members.FirstOrDefault(m => m.Id == EditMember.Id);
            if (removeMember != null)
            {
                Members.Remove(removeMember);
            }
            EditMember = null;
        }

        private void OnSave()
        {
            IsEditing = false;
            if (EditMember.Id == 0)
            {
                //new
                var id = Members.Any() ? Members.Max(m => m.Id) + 1 : 1;
                EditMember.Id = id;
                EditMember.RegDate = DateTime.Now;
                Members.Add((Member)EditMember.Clone());
                EditMember = null;
            }
            else
            {
                var sourceMember = Members.FirstOrDefault(m => m.Id == EditMember.Id);
                if (sourceMember != null)
                {
                    sourceMember.Name = EditMember.Name;
                    sourceMember.Phone = EditMember.Phone;
                    sourceMember.IsUse = EditMember.IsUse;
                }
            }
        }

        private void OnNew()
        {
            IsEditing = true;
            EditMember = new Member
            {
                IsUse = true,
            };
        }

        private void OnCancel()
        {
            IsEditing = false;
            EditMember = null;
        }

        private bool _menuToggle;
        public bool MenuToggle
        {
            get { return _menuToggle; }
            set { SetProperty(ref _menuToggle, value); }
        }

    }
}
