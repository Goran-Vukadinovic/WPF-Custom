using BoostDraft.Icons;
using Controls.Core;
using EntryPoint.Control;
using EntryPoint.Model;
using SvgResourceGenerator;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;

namespace EntryPoint.ViewModel
{
    public class ProofreadingViewModel : BindableBase
    {
        public static ProofreadingViewModel Instance;
        public ProofreadingViewModel()
        {
            Instance = this;

            popupMenuItems = new ObservableCollection<PopupMenuItem>();

            InitPopupMenu();
            InitProofreading();
        }

        void InitPopupMenu()
        {
            PopupMenuItem item = new PopupMenuItem();
            item = new PopupMenuItem();
            item.iconType = IconType.Icon_Check_solid;
            item.itemText = "Replace selection";
            item.itemCommand = new RelayCommand(ExecuteReplaceselectionCommand);
            popupMenuItems.Add(item);

            item = new PopupMenuItem();
            item.iconType = IconType.Icon_Plug_solid;
            item.itemText = "Insert below";
            item.itemCommand = new RelayCommand(ExecuteInsertbelowCommand);
            popupMenuItems.Add(item);

            item = new PopupMenuItem();
            item.iconType = IconType.Icon_Align_Left_solid;
            item.itemText = "Make shorter";
            item.itemCommand = new RelayCommand(ExecuteMakeshorterCommand);
            popupMenuItems.Add(item);

            item = new PopupMenuItem();
            item.iconType = IconType.Icon_Align_Right_solid;
            item.itemText = "Make longer";
            item.itemCommand = new RelayCommand(ExecuteMakelongerCommand);
            item.visibleSeparator = Visibility.Visible;
            popupMenuItems.Add(item);

            ////////////////////////////////////
            item = new PopupMenuItem();
            item.iconType = IconType.Icon_Redo_Alt_solid;
            item.itemText = "Try again";
            item.itemCommand = new RelayCommand(ExecuteTryagainCommand);
            popupMenuItems.Add(item);

            item = new PopupMenuItem();
            item.iconType = IconType.Icon_Trash_Alt_solid;
            item.itemText = "Discard";
            item.itemCommand = new RelayCommand(ExecuteDiscardCommand);
            popupMenuItems.Add(item);
        }

        void InitProofreading()
        {
            tabsItems = new ObservableCollection<TabItem>();

            var item1 = new TabItem() { Header = "General", FormattedText = "[d](1.35)[/d][t#6357fc](1.36)[/t] Lorem ipsum dolor sit amet, consectetur" };
            tabsItems.Add(item1);

            var item2 = new TabItem() { Header = "Varient 1", FormattedText = "[d](d)[/d][t#6357fc](c)[/t] Lorem ipsum dolor sit amet, consectetur" };
            tabsItems.Add(item2);

            var item3 = new TabItem() { Header = "Varient 2", FormattedText = "2.2 Demand Registration [d](d)[/d][t#6357fc](c)[/t] Lorem ipsum dolor sit amet, consectetur" };
            tabsItems.Add(item3);
        }

        public virtual void ExecuteReplaceselectionCommand(object obj)
        {
            MessageBox.Show("Replace selection Command");
        }

        public virtual void ExecuteInsertbelowCommand(object obj)
        {
            MessageBox.Show("Insert below Command");
        }
        public virtual void ExecuteMakeshorterCommand(object obj)
        {
            MessageBox.Show("Make shorter Command");
        }
        public virtual void ExecuteMakelongerCommand(object obj)
        {
            MessageBox.Show("Make longer Command");
        }
        public virtual void ExecuteTryagainCommand(object obj)
        {
            MessageBox.Show("Try again Command");
        }
        public virtual void ExecuteDiscardCommand(object obj)
        {
            MessageBox.Show("Discard Command");
        }


        private ObservableCollection<PopupMenuItem> popupMenuItems;
        public ObservableCollection<PopupMenuItem> PopupMenuItems
        {
            get => this.popupMenuItems;
        }

        private ObservableCollection<TabItem> tabsItems;
        public ObservableCollection<TabItem> TabsItems
        {
            get
            {
                return tabsItems;
            }
            set
            {
                tabsItems = value;
            }
        }
    }
}
