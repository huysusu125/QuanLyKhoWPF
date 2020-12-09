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
    public class UnitViewModel :BaseViewModel
    {
        private ObservableCollection<Unit> _List;
        public ObservableCollection<Unit> List { get => _List; set { _List = value;OnPropertyChanged(); } }

        private Unit _SelectedItem;
        public Unit SelectedItem { get => _SelectedItem; 
            set 
            { 
                _SelectedItem = value;
                OnPropertyChanged();
                if(SelectedItem != null)
                {
                    DisplayName = SelectedItem.DisplayName;
                }
            } 
        }

        private String _DisplayName;
        public String DisplayName { get => _DisplayName; set { _DisplayName = value; OnPropertyChanged(); } }

        public ICommand AddCommand { get; set; }
        public ICommand EditCommand { get; set; }
        public ICommand DeleteCommand { get; set; }

        public UnitViewModel()
        {
            List = new ObservableCollection<Unit>(DataProvider.Ins.DB.Units);

            AddCommand = new RelayCommand<object>((p) => 
            {
                if (string.IsNullOrEmpty(DisplayName))
                    return false;

                var displayList = DataProvider.Ins.DB.Units.Where(x => x.DisplayName == DisplayName);
                if (displayList == null || displayList.Count() != 0)
                    return false;
                return true;
            }, (p) => 
            {
                var unit = new Unit() {DisplayName = DisplayName };

                DataProvider.Ins.DB.Units.Add(unit);
                DataProvider.Ins.DB.SaveChanges();
                List.Add(unit);
            });

            EditCommand = new RelayCommand<object>((p) =>
            {
                if (string.IsNullOrEmpty(DisplayName) || SelectedItem ==null)
                    return false;

                var displayList = DataProvider.Ins.DB.Units.Where(x => x.DisplayName == DisplayName);
                if (displayList == null || displayList.Count() != 0)
                    return false;
                return true;
            }, (p) =>
            {
                var unit = DataProvider.Ins.DB.Units.Where(x => x.Id == SelectedItem.Id).SingleOrDefault();

                unit.DisplayName = DisplayName;
                DataProvider.Ins.DB.SaveChanges();
                
            });

            DeleteCommand = new RelayCommand<object>((p) =>
            {
                var Unit = DataProvider.Ins.DB.Objects.Where(x => x.IdUnit == SelectedItem.Id).Count();
                if (Unit == 0)
                    return true;
                return false;
            }, (p) =>
            {
                var unit = DataProvider.Ins.DB.Units.Where(x => x.Id == SelectedItem.Id).SingleOrDefault();

                DataProvider.Ins.DB.Units.Remove(unit);
                DataProvider.Ins.DB.SaveChanges();
                List.Remove(unit);
            });
        }

    }
}
