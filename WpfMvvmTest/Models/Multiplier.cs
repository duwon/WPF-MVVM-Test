using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfMvvmTest.Models
{
    public class Multiplier : ObservableObject
    {
        private int num1 = 1;

        public int Num1
        {
            set
            {
                SetProperty(ref num1, value);
                Num2 = value * 2;

            }
            get
            {
                return num1;
            }
        }

        private int num2 = 2;

        public int Num2
        {
            get
            {
                return num2;
            }
            set
            {
                SetProperty(ref num2, value); 
            }
        }
    }
}
