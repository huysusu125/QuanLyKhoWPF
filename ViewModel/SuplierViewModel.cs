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
    public class SuplierViewModel : BaseViewModel
    {
        private ObservableCollection<Suplier> _List;
        public ObservableCollection<Suplier> List { get => _List; set { _List = value; OnPropertyChanged(); } }

        private Suplier _SelectedItem;
        public Suplier SelectedItem
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

        public SuplierViewModel()
        {
            List = new ObservableCollection<Suplier>(DataProvider.Ins.DB.Supliers);

            AddCommand = new RelayCommand<object>((p) =>
            {
                if (string.IsNullOrEmpty(DisplayName))
                    return false;

                var displayList = DataProvider.Ins.DB.Supliers.Where(x => x.DisplayName == DisplayName);
                if (displayList == null || displayList.Count() != 0)
                    return false;
                return true;
            }, (p) =>
            {
                var Suplier = new Suplier() { DisplayName = DisplayName, Address = Address, Email = Email, Phone = Phone, ContractDate = ContractDate, MoreInfo = MoreInfo };

                DataProvider.Ins.DB.Supliers.Add(Suplier);
                DataProvider.Ins.DB.SaveChanges();
                List.Add(Suplier);
            });

            EditCommand = new RelayCommand<object>((p) =>
            {
                if (string.IsNullOrEmpty(DisplayName) || SelectedItem == null)
                    return false;

                var displayList = DataProvider.Ins.DB.Supliers.Where(x => x.Id == SelectedItem.Id);
                if (displayList != null && displayList.Count() != 0)
                    return true;
                return false;
            }, (p) =>
            {
                var Suplier = DataProvider.Ins.DB.Supliers.Where(x => x.Id == SelectedItem.Id).SingleOrDefault();

                Suplier.DisplayName = DisplayName;
                Suplier.Address = Address;
                Suplier.Email = Email;
                Suplier.Phone = Phone;
                Suplier.ContractDate = ContractDate;
                Suplier.MoreInfo = MoreInfo;
                DataProvider.Ins.DB.SaveChanges();

            });

            DeleteCommand = new RelayCommand<object>((p) =>
            {
                var Suplier = DataProvider.Ins.DB.Objects.Where(x => x.IdSuplier == SelectedItem.Id).Count();
                if (Suplier == 0)
                    return true;
                return false;
            }, (p) =>
            {
                var Suplier = DataProvider.Ins.DB.Supliers.Where(x => x.Id == SelectedItem.Id).SingleOrDefault();

                DataProvider.Ins.DB.Supliers.Remove(Suplier);
                DataProvider.Ins.DB.SaveChanges();
                List.Remove(Suplier);
            });
        }

    }
}

