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
    public class AskAIViewModel : BindableBase
    {
        public static AskAIViewModel Instance;
        public AskAIViewModel()
        {
            Instance = this;

            popupMenuItems = new ObservableCollection<PopupMenuItem>();

            InitPopupMenu();
        }

        void InitPopupMenu()
        {
            PopupMenuItem item = new PopupMenuItem();
            item.itemSectionText = "EDIT SELECTION";
            item.visibleSection = Visibility.Visible;
            popupMenuItems.Add(item);

            item = new PopupMenuItem();
            item.iconType = IconType.Icon_Lightbulb_regular;
            item.itemText = "Improve writing";
            item.itemCommand = new RelayCommand(ExecuteImprovewritingCommand);
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
            item.itemSectionText = "WRITE WITH AI";
            item.visibleSection = Visibility.Visible;
            popupMenuItems.Add(item);

            item = new PopupMenuItem();
            item.iconType = IconType.Icon_Pen_solid;
            item.itemText = "Continue writing";
            item.itemCommand = new RelayCommand(ExecuteContinuewritingCommand);
            popupMenuItems.Add(item);

            item = new PopupMenuItem();
            item.iconType = IconType.Icon_Align_Center_solid;
            item.itemText = "Section...";
            item.itemCommand = new RelayCommand(ExecuteSectionCommand);
            popupMenuItems.Add(item);

            item = new PopupMenuItem();
            item.iconType = IconType.Icon_Book_solid;
            item.itemText = "Full contract...";
            item.itemCommand = new RelayCommand(ExecuteFullcontractCommand);
            popupMenuItems.Add(item);
        }

   

        public virtual void ExecuteImprovewritingCommand(object obj)
        {
            MessageBox.Show("Improve writing Command");
        }

        public virtual void ExecuteMakeshorterCommand(object obj)
        {
            MessageBox.Show("Make shorter Command");
        }
        public virtual void ExecuteMakelongerCommand(object obj)
        {
            MessageBox.Show("Make longer Command");
        }
        public virtual void ExecuteContinuewritingCommand(object obj)
        {
            MessageBox.Show("Continue writing Command");
        }
        public virtual void ExecuteSectionCommand(object obj)
        {
            MessageBox.Show("Section Command");
        }
        public virtual void ExecuteFullcontractCommand(object obj)
        {
            MessageBox.Show("Full contract Command");
        }


        private ObservableCollection<PopupMenuItem> popupMenuItems;
        public ObservableCollection<PopupMenuItem> PopupMenuItems
        {
            get => this.popupMenuItems;
        }

        private ProofreadingContent _selectedContent;
        public ProofreadingContent SelectedContent
        {
            get => _selectedContent;
            set
            {
                if (_selectedContent != value)
                {
                    _selectedContent = value;
                    RaisePropertyChanged();
                }
            }
        }
    }
}
