using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfMvvmTest.Models
{
    public class Member : ObservableObject, ICloneable
    {
        public int Id { get; set; }
        private string _name;
        public string Name
        {
            get { return _name; }
            set { SetProperty(ref _name, value); }
        }
        private string _phone;
        public string Phone
        {
            get { return _phone; }
            set { SetProperty(ref _phone, value); }
        }
        public DateTime RegDate { get; set; }
        private bool _isUse;
        public bool IsUse
        {
            get { return _isUse; }
            set { SetProperty(ref _isUse, value); }
        }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
