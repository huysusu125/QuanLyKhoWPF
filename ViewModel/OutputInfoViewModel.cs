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
    class OutputInfoViewModel : BaseViewModel
    {
        private ObservableCollection<OutputInfo> _List;
        public ObservableCollection<OutputInfo> List { get => _List; set { _List = value; OnPropertyChanged(); } }

        private ObservableCollection<Model.Object> _ListObject;
        public ObservableCollection<Model.Object> ListObject { get => _ListObject; set { _ListObject = value; OnPropertyChanged(); } }

        private ObservableCollection<Customer> _ListCustomer;
        public ObservableCollection<Customer> ListCustomer { get => _ListCustomer; set { _ListCustomer = value; OnPropertyChanged(); } }

        private ObservableCollection<Output> _ListOutput;
        public ObservableCollection<Output> ListOutput { get => _ListOutput; set { _ListOutput = value; OnPropertyChanged(); } }

        private OutputInfo _SelectedItem;
        public OutputInfo SelectedItem
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
                    OutputPrice = (double)SelectedItem.OutputPrice;
                    Status = SelectedItem.Status;
                    Output = SelectedItem.Output;
                    DateOutput = SelectedItem.Output.DateOutput;
                    SelectedCustomer = SelectedItem.Customer;
                }
            }
        }



        private int _Count;
        public int Count { get => _Count; set { _Count = value; OnPropertyChanged(); } }

        private double _OutputPrice;
        public double OutputPrice { get => _OutputPrice; set { _OutputPrice = value; OnPropertyChanged(); } }

        private string _Status { get; set; }
        public string Status { get => _Status; set { _Status = value; OnPropertyChanged(); } }

        private DateTime? _DateOutput = new Output().DateOutput;
        public DateTime? DateOutput { get => _DateOutput; set { _DateOutput = value; OnPropertyChanged(); } }

        

        private Customer _SelectedCustomer;
        public Customer SelectedCustomer
        {
            get => _SelectedCustomer;
            set
            {
                _SelectedCustomer = value;
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

        private Output _Output;
        public Output Output
        {
            get => _Output;
            set
            {
                _Output = value;
                OnPropertyChanged();
            }
        }

        public ICommand AddCommand { get; set; }
        public ICommand EditCommand { get; set; }
        public ICommand DeleteCommand { get; set; }

        public OutputInfoViewModel()
        {
            List = new ObservableCollection<OutputInfo>(DataProvider.Ins.DB.OutputInfoes);
            ListObject = new ObservableCollection<Model.Object>(DataProvider.Ins.DB.Objects);
            ListCustomer = new ObservableCollection<Customer>(DataProvider.Ins.DB.Customers);
            ListOutput = new ObservableCollection<Output>(DataProvider.Ins.DB.Outputs);

            AddCommand = new RelayCommand<object>((p) =>
            {
                if (SelectedObject == null || SelectedCustomer == null)
                    return false;
                return true;

            }, (p) =>
            {
                var Output = new Output() { Id = Guid.NewGuid().ToString(), DateOutput = DateOutput };
                var OutputInfo = new OutputInfo() { Id = Guid.NewGuid().ToString(), IdObject = SelectedObject.Id, IdOutput = Output.Id, Count = Count, OutputPrice = OutputPrice, IdCustomer = SelectedCustomer.Id, Status = Status  };

                DataProvider.Ins.DB.Outputs.Add(Output);
                DataProvider.Ins.DB.OutputInfoes.Add(OutputInfo);
                DataProvider.Ins.DB.SaveChanges();
                List.Add(OutputInfo);
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

                var OutputInfo = DataProvider.Ins.DB.OutputInfoes.Where(x => x.Id == SelectedItem.Id).SingleOrDefault();
                var Output = DataProvider.Ins.DB.Outputs.Where(x => x.Id == SelectedItem.Output.Id).SingleOrDefault();

                OutputInfo.IdObject = SelectedObject.Id;
                OutputInfo.Count = Count;
                OutputInfo.OutputPrice = OutputPrice;
                OutputInfo.IdCustomer = SelectedCustomer.Id;
                Output.DateOutput = DateOutput;
                OutputInfo.Status = Status;
                DataProvider.Ins.DB.SaveChanges();
            });

            DeleteCommand = new RelayCommand<object>((p) =>
            {
                return true;

            }, (p) =>
            {
                var Output = DataProvider.Ins.DB.Outputs.Where(x => x.Id == SelectedItem.Output.Id).SingleOrDefault();
                var OutputInfo = DataProvider.Ins.DB.OutputInfoes.Where(x => x.Id == SelectedItem.Id).SingleOrDefault();

                DataProvider.Ins.DB.Outputs.Remove(Output);
                DataProvider.Ins.DB.OutputInfoes.Remove(OutputInfo);
                DataProvider.Ins.DB.SaveChanges();
                List.Remove(OutputInfo);
            });
        }

    }
}