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
    public class CustomerViewModel : BaseViewModel
    {
        private ObservableCollection<Customer> _List;
        public ObservableCollection<Customer> List { get => _List; set { _List = value; OnPropertyChanged(); } }

        private Customer _SelectedItem;
        public Customer SelectedItem
        {
            get => _SelectedItem;
            set
            {
                _SelectedItem = value;
                OnPropertyChanged();
                if (SelectedItem != null)
                {
                    DisplayName = SelectedItem.DisplayName;
                    Phone = SelectedItem.Phone;
                    Address = SelectedItem.Address;
                    Email = SelectedItem.Email;
                    MoreInfo = SelectedItem.MoreInfo;
                    ContractDate = SelectedItem.ContractDate;
                }
            }
        }

        private String _DisplayName;
        public String DisplayName { get => _DisplayName; set { _DisplayName = value; OnPropertyChanged(); } }

        private String _Phone;
        public String Phone { get => _Phone; set { _Phone = value; OnPropertyChanged(); } }

        private String _Address;
        public String Address { get => _Address; set { _Address = value; OnPropertyChanged(); } }

        private String _Email;
        public String Email { get => _Email; set { _Email = value; OnPropertyChanged(); } }

        private String _MoreInfo;
        public String MoreInfo { get => _MoreInfo; set { _MoreInfo = value; OnPropertyChanged(); } }

        private DateTime? _ContractDate;
        public DateTime? ContractDate { get => _ContractDate; set { _ContractDate = value; OnPropertyChanged(); } }

        public ICommand AddCommand { get; set; }
        public ICommand EditCommand { get; set; }
        public ICommand DeleteCommand { get; set; }

        public CustomerViewModel()
        {
            List = new ObservableCollection<Customer>(DataProvider.Ins.DB.Customers);

            AddCommand = new RelayCommand<object>((p) =>
            {
                if (string.IsNullOrEmpty(DisplayName))
                    return false;

                var displayList = DataProvider.Ins.DB.Customers.Where(x => x.DisplayName == DisplayName);
                if (displayList == null || displayList.Count() != 0)
                    return false;
                return true;
            }, (p) =>
            {
                var Customer = new Customer() { DisplayName = DisplayName, Address = Address, Email = Email, Phone = Phone, ContractDate = ContractDate, MoreInfo = MoreInfo };

                DataProvider.Ins.DB.Customers.Add(Customer);
                DataProvider.Ins.DB.SaveChanges();
                List.Add(Customer);
            });

            EditCommand = new RelayCommand<object>((p) =>
            {
                if (string.IsNullOrEmpty(DisplayName) || SelectedItem == null)
                    return false;

                var displayList = DataProvider.Ins.DB.Customers.Where(x => x.Id == SelectedItem.Id);
                if (displayList != null && displayList.Count() != 0)
                    return true;
                return false;
            }, (p) =>
            {
                var Customer = DataProvider.Ins.DB.Customers.Where(x => x.Id == SelectedItem.Id).SingleOrDefault();

                Customer.DisplayName = DisplayName;
                Customer.Address = Address;
                Customer.Email = Email;
                Customer.Phone = Phone;
                Customer.ContractDate = ContractDate;
                Customer.MoreInfo = MoreInfo;
                DataProvider.Ins.DB.SaveChanges();

            });

            DeleteCommand = new RelayCommand<object>((p) =>
            {
                var Customer = DataProvider.Ins.DB.OutputInfoes.Where(x => x.IdCustomer == SelectedItem.Id).Count();
                

                if (Customer == 0)
                    return true;
                return false;

            }, (p) =>
            {
                var Customer = DataProvider.Ins.DB.Customers.Where(x => x.Id == SelectedItem.Id).SingleOrDefault();

                DataProvider.Ins.DB.Customers.Remove(Customer);
                DataProvider.Ins.DB.SaveChanges();
                List.Remove(Customer);
            });
        }

    }
}