using Controls.Core;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntryPoint.Model
{
    public class ProofreadingItem:BindableBase
    {
        public string Title { get; set; }
        public string Description { get; set; }

        private bool _isSelected;
        public bool IsSelected
        {
            get => _isSelected;
            set
            {

                _isSelected = value;
                RaisePropertyChanged();
            }
        }

        public ObservableCollection<ProofreadingContent> Contents { get; set; } = new ObservableCollection<ProofreadingContent>();
    }
}
