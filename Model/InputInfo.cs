//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WpfApp1.Model
{
    using System;
    using System.Collections.Generic;
    using WpfApp1.ViewModel;

    public partial class InputInfo : BaseViewModel
    {
        private string _Id;
        public string Id { get => _Id; set { _Id = value; OnPropertyChanged(); } }

        private string _IdObject;
        public string IdObject { get => _IdObject; set { _IdObject = value; OnPropertyChanged(); } }

        private string _IdInput;
        public string IdInput { get => _IdInput; set { _IdInput = value; OnPropertyChanged(); } }

        private Nullable<int> _Count;
        public Nullable<int> Count { get => _Count; set { _Count = value; OnPropertyChanged(); } }

        private Nullable<double> _InputPrice;
        public Nullable<double> InputPrice { get => _InputPrice; set { _InputPrice = value; OnPropertyChanged(); } }

        private string _Status;
        public string Status { get => _Status; set { _Status = value; OnPropertyChanged(); } }


        private Input _Input;
        public virtual Input Input { get => _Input; set { _Input = value; OnPropertyChanged(); } }

        private Object _Object;
        public virtual Object Object { get => _Object; set { _Object = value; OnPropertyChanged(); } }
    }
}
