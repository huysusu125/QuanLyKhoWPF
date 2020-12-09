using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WpfApp1.Model;

namespace WpfApp1.ViewModel
{
    public class InputInfoViewModel : BaseViewModel
    {
        private ObservableCollection<InputInfo> _List;
        public ObservableCollection<InputInfo> List { get => _List; set { _List = value; OnPropertyChanged(); } }

        private ObservableCollection<Model.Object> _ListObject;
        public ObservableCollection<Model.Object> ListObject { get => _ListObject; set { _ListObject = value; OnPropertyChanged(); } }

        private ObservableCollection<Input> _ListInput;
        public ObservableCollection<Input> ListInput { get => _ListInput; set { _ListInput = value; OnPropertyChanged(); } }

        private InputInfo _SelectedItem;
        public InputInfo SelectedItem
        {
            get => _SelectedItem;
            set
            {
                _SelectedItem = value;
                OnPropertyChanged();
                if (SelectedItem != null)
                {
                    SelectedObject = SelectedItem.Object;
                    Count = (int)SelectedItem.Count;
                    InputPrice = (double)SelectedItem.InputPrice;                   
                    Status = SelectedItem.Status;                    
                    DateInput = SelectedItem.Input.DateInput;
                    Input = SelectedItem.Input;
                }
            }
        }

        

        private int _Count;
        public int Count { get => _Count; set { _Count = value; OnPropertyChanged(); } }

        private double _InputPrice;
        public double InputPrice { get => _InputPrice; set { _InputPrice = value; OnPropertyChanged(); } }

        private string _Status { get; set; }
        public string Status { get => _Status; set { _Status = value; OnPropertyChanged(); } }

        private DateTime? _DateInput = new Input().DateInput;
        public DateTime? DateInput { get => _DateInput; set { _DateInput = value; OnPropertyChanged(); } }

        private Input _Input;
        public Input Input
        {
            get => _Input;
            set
            {
                _Input = value;
                OnPropertyChanged();
            }
        }

        private Model.Object _SelectedObject;
        public Model.Object SelectedObject
        {
            get => _SelectedObject;
            set
            {
                _SelectedObject = value;
                OnPropertyChanged();
            }
        }

        public ICommand AddCommand { get; set; }
        public ICommand EditCommand { get; set; }
        public ICommand DeleteCommand { get; set; }

        public InputInfoViewModel()
        {
            List = new ObservableCollection<InputInfo>(DataProvider.Ins.DB.InputInfoes);
            ListObject = new ObservableCollection<Model.Object>(DataProvider.Ins.DB.Objects);
            ListInput = new ObservableCollection<Input>(DataProvider.Ins.DB.Inputs);

            AddCommand = new RelayCommand<object>((p) =>
            {
                if (SelectedObject == null)
                    return false;
                return true;

            }, (p) =>
            {
                var Input = new Input() { Id = Guid.NewGuid().ToString(), DateInput = DateInput };
                var InputInfo = new InputInfo() { Id = Guid.NewGuid().ToString(), IdObject = SelectedObject.Id, IdInput = Input.Id, Count = Count, InputPrice = InputPrice, Status =Status};
                DataProvider.Ins.DB.Inputs.Add(Input);
                DataProvider.Ins.DB.InputInfoes.Add(InputInfo);
                DataProvider.Ins.DB.SaveChanges();
                List.Add(InputInfo);
            });

            EditCommand = new RelayCommand<object>((p) =>
            {
                if (SelectedObject == null)
                    return false;

                var displayList = DataProvider.Ins.DB.InputInfoes.Where(x => x.Id == SelectedItem.Id);
                if (displayList != null)
                    return true;

                return false;

            }, (p) =>
            {
                
                var InputInfo = DataProvider.Ins.DB.InputInfoes.Where(x => x.Id == SelectedItem.Id).SingleOrDefault();
                var Input = DataProvider.Ins.DB.Inputs.Where(x => x.Id == SelectedItem.Input.Id).SingleOrDefault();

                InputInfo.IdObject = SelectedObject.Id;
                InputInfo.Count = Count;
                InputInfo.InputPrice = InputPrice;
                InputInfo.Status = Status;
                Input.DateInput = DateInput;
                DataProvider.Ins.DB.SaveChanges();                
            });

            DeleteCommand = new RelayCommand<object>((p) =>
            {
                return true;

            }, (p) =>
            {
                var Input = DataProvider.Ins.DB.Inputs.Where(x => x.Id == SelectedItem.Input.Id).SingleOrDefault();
                var InputInfo = DataProvider.Ins.DB.InputInfoes.Where(x => x.Id == SelectedItem.Id).SingleOrDefault();
                
                DataProvider.Ins.DB.Inputs.Remove(Input);
                DataProvider.Ins.DB.InputInfoes.Remove(InputInfo);
                DataProvider.Ins.DB.SaveChanges();
                List.Remove(InputInfo);
            });
        }

    }
}