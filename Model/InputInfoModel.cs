using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.ViewModel;

namespace WpfApp1.Model
{
    public class InputInfoModel : BaseViewModel
    {
        private Object _Object;
        public Object Object { get => _Object; set { _Object = value; OnPropertyChanged(); } }

        private InputInfo _InputInfo;
        public InputInfo InputInfo { get => _InputInfo; set { _InputInfo = value; OnPropertyChanged(); } }

        private Input _Input;
        public Input Input { get => _Input; set { _Input = value; OnPropertyChanged(); } }
    }
}
