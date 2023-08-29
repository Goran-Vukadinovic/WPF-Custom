using BoostDraft.Icons;
using Controls.Core;
using EntryPoint.Control;
using EntryPoint.Model;
using SvgResourceGenerator;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;

namespace EntryPoint.ViewModel
{
    public class MainWindowViewModel : BindableBase
    {
        public ICommand KeyCommand { get; private set; }
        public MainWindowViewModel()
        {
            KeyCommand = new RelayCommand<KeyEventArgs>(KeyDownExecute);
        }

        public virtual void KeyDownExecute(KeyEventArgs e)
        {
            if(MyToolBarViewModel.Instance != null)
            {
                MyToolBarViewModel.Instance.KeyDown(e);
            }
        }
    }
}
