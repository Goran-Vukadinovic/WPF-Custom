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
using System.Windows.Input;
using System.Windows.Media;

namespace EntryPoint.ViewModel
{
    public class MyToolBarViewModel : BindableBase
    {
        public static MyToolBarViewModel Instance;
        public MyToolBarViewModel()
        {
            Instance = this;

            popupMenuItems = new ObservableCollection<PopupMenuItem>();

            _popupMenuCommand = new RelayCommand(ExecutePopupMenuCommand);
            _backlinksCommand = new RelayCommand(ExecuteBacklinksCommand);
            _closeBacklinksCommand = new RelayCommand(ExecuteCloseBacklinksCommand);
            InitPopupMenu();
            InitBacklinks();
        }

        void InitPopupMenu()
        {
            PopupMenuItem item = new PopupMenuItem();
            item.iconType = IconType.Icon_Sistrix_brands;
            item.itemText = "Search";
            item.itemCommandText = "Ctrl+F";
            item.itemCommand = new RelayCommand(ExecuteSearchCommand);
            popupMenuItems.Add(item);

            item = new PopupMenuItem();
            item.iconType = IconType.Icon_Align_Right_solid;
            item.itemText = "Outline";
            item.itemCommandText = "Ctrl+O";
            item.itemCommand = new RelayCommand(ExecuteOutlineCommand);
            popupMenuItems.Add(item);

            item = new PopupMenuItem();
            item.iconType = IconType.Icon_Arrow_Left_solid;
            item.itemText = "Mavigate backward";
            item.itemCommandText = "Ctrl+<";
            item.itemCommand = new RelayCommand(ExecuteNavigatebackwardCommand);
            popupMenuItems.Add(item);

            item = new PopupMenuItem();
            item.iconType = IconType.Icon_Arrow_Right_solid;
            item.itemText = "Mavigate forward";
            item.itemCommandText = "Ctrl+>";
            item.itemCommand = new RelayCommand(ExecuteNavigateforwardCommand);
            item.visibleSeparator = Visibility.Visible;
            popupMenuItems.Add(item);

            ////////////////////////////////////
            item = new PopupMenuItem();
            item.iconType = IconType.Icon_List_Alt_solid;
            item.itemText = "Proofreading";
            item.itemCommandText = "Ctrl+P";
            item.itemCommand = new RelayCommand(ExecuteProofreadingCommand);
            popupMenuItems.Add(item);

            item = new PopupMenuItem();
            item.iconType = IconType.Icon_Comments_regular;
            item.itemText = "Comments";
            item.itemCommandText = "";
            item.itemCommand = new RelayCommand(ExecuteCommentsCommand);
            popupMenuItems.Add(item);

            item = new PopupMenuItem();
            item.iconType = IconType.Icon_User_regular;
            item.itemText = "Change authors";
            item.itemCommandText = "";
            item.itemCommand = new RelayCommand(ExecuteChangeauthorsCommand);
            item.visibleSeparator = Visibility.Visible;
            popupMenuItems.Add(item);


            ////////////////////////////////////
            item = new PopupMenuItem();
            item.iconType = IconType.Icon_Sliders_H_solid;
            item.itemText = "Highlight settings";
            item.itemCommandText = "";
            item.itemCommand = new RelayCommand(ExecuteHighlightsettingsCommand);
            popupMenuItems.Add(item);

            item = new PopupMenuItem();
            item.iconType = IconType.Icon_Project_Diagram_solid;
            item.itemText = "Related documents";
            item.itemCommandText = "";
            item.itemCommand = new RelayCommand(ExecuteRelateddocumentsCommand);
            item.visibleSeparator = Visibility.Visible;
            popupMenuItems.Add(item);


            ////////////////////////////////////
            item = new PopupMenuItem();
            item.iconType = IconType.Icon_Fan_solid;
            item.itemText = "Reboot add-in";
            item.itemCommandText = "Ctrl+R";
            item.itemCommand = new RelayCommand(ExecuteRebootaddinCommand);
            popupMenuItems.Add(item);

            item = new PopupMenuItem();
            item.iconType = IconType.Icon_Comment_solid;
            item.itemText = "Send feedback";
            item.itemCommandText = "";
            item.itemCommand = new RelayCommand(ExecuteSendfeedbackCommand);
            popupMenuItems.Add(item);

            item = new PopupMenuItem();
            item.iconType = IconType.Icon_Rocket_solid;
            item.itemText = "Upgrade";
            item.itemCommandText = "";
            item.itemCommand = new RelayCommand(ExecuteUpgradeCommand);
            popupMenuItems.Add(item);

            item = new PopupMenuItem();
            item.iconType = IconType.Icon_Info_Circle_solid;
            item.itemText = "About";
            item.itemCommandText = "";
            item.itemCommand = new RelayCommand(ExecuteAboutCommand);
            popupMenuItems.Add(item);

            item = new PopupMenuItem();
            item.iconType = IconType.Icon_Window_Close_solid;
            item.itemText = "Close add-in";
            item.itemCommandText = "";
            item.itemCommand = new RelayCommand(ExecuteCloseaddinCommand);
            popupMenuItems.Add(item);
        }

        void InitBacklinks()
        {
            Groups = new ObservableCollection<ProofreadingGroup>();

            var item1 = new ProofreadingItem() { Title = "Wrong Index", Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Leo vel fringilla est ullamcorper eget nulla facilisi etiam. Id cursus metus aliquam eleifend mi in nulla posuere. Arcu vitae elementum curabitur vitae nunc sed. Est placerat in egestas erat imperdiet sed. Dictum at tempor commodo ullamcorper a lacus vestibulum sed. In hendrerit gravida rutrum quisque non. Habitasse platea dictumst quisque sagittis. Libero justo laoreet sit amet cursus sit amet dictum sit. Et malesuada fames ac turpis egestas integer eget aliquet." };
            item1.Contents.Add(new ProofreadingContent
            {
                Subject = "1 Definations",
                FormattedText = "[d](1.35)[/d][t#6357fc](1.36)[/t] Lorem ipsum dolor sit amet, consectetur"
            });

            var item2 = new ProofreadingItem() { Title = "Wrong Index", Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Leo vel fringilla est ullamcorper eget nulla facilisi etiam. Id cursus metus aliquam eleifend mi in nulla posuere. Arcu vitae elementum curabitur vitae nunc sed. Est placerat in egestas erat imperdiet sed. Dictum at tempor commodo ullamcorper a lacus vestibulum sed. In hendrerit gravida rutrum quisque non. Habitasse platea dictumst quisque sagittis. Libero justo laoreet sit amet cursus sit amet dictum sit. Et malesuada fames ac turpis egestas integer eget aliquet." };
            item2.Contents.Add(new ProofreadingContent
            {
                Subject = "2.1 Demand Registration",
                FormattedText = "[d](d)[/d][t#6357fc](c)[/t] Lorem ipsum dolor sit amet, consectetur"
            });

            var item3 = new ProofreadingItem() { Title = "Wrong Index", Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Leo vel fringilla est ullamcorper eget nulla facilisi etiam. Id cursus metus aliquam eleifend mi in nulla posuere. Arcu vitae elementum curabitur vitae nunc sed. Est placerat in egestas erat imperdiet sed. Dictum at tempor commodo ullamcorper a lacus vestibulum sed. In hendrerit gravida rutrum quisque non. Habitasse platea dictumst quisque sagittis. Libero justo laoreet sit amet cursus sit amet dictum sit. Et malesuada fames ac turpis egestas integer eget aliquet." };
            item3.Contents.Add(new ProofreadingContent
            {
                Subject = "3.1 Demand Registration",
                FormattedText = "[d](d)[/d][t#6357fc](c)[/t] Lorem ipsum dolor sit amet, consectetur"
            });

            item3.Contents.Add(new ProofreadingContent
            {
                Subject = "3.2 Demand Registration",
                FormattedText = "[d](d)[/d][t#6357fc](c)[/t] Lorem ipsum dolor sit amet, consectetur"
            });

            var group1 = new ProofreadingGroup() { GroupTitle = "ISSUES" };
            group1.Items.Add(item1);
            group1.Items.Add(item2);
            group1.Items.Add(item3);
            Groups.Add(group1);

            var item4 = new ProofreadingItem() { Title = "Wrong Index", Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Leo vel fringilla est ullamcorper eget nulla facilisi etiam. Id cursus metus aliquam eleifend mi in nulla posuere. Arcu vitae elementum curabitur vitae nunc sed. Est placerat in egestas erat imperdiet sed. Dictum at tempor commodo ullamcorper a lacus vestibulum sed. In hendrerit gravida rutrum quisque non. Habitasse platea dictumst quisque sagittis. Libero justo laoreet sit amet cursus sit amet dictum sit. Et malesuada fames ac turpis egestas integer eget aliquet." };
            item4.Contents.Add(new ProofreadingContent
            {
                Subject = "2.2 Demand Registration",
                FormattedText = "[d](d)[/d][t#6357fc](c)[/t] Lorem ipsum dolor sit amet, consectetur"
            });

            var item5 = new ProofreadingItem() { Title = "Wrong Index", Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Leo vel fringilla est ullamcorper eget nulla facilisi etiam. Id cursus metus aliquam eleifend mi in nulla posuere. Arcu vitae elementum curabitur vitae nunc sed. Est placerat in egestas erat imperdiet sed. Dictum at tempor commodo ullamcorper a lacus vestibulum sed. In hendrerit gravida rutrum quisque non. Habitasse platea dictumst quisque sagittis. Libero justo laoreet sit amet cursus sit amet dictum sit. Et malesuada fames ac turpis egestas integer eget aliquet." };
            item5.Contents.Add(new ProofreadingContent
            {
                Subject = "3.5 Demand Registration",
                FormattedText = "[d](d)[/d][t#6357fc](c)[/t] Lorem ipsum dolor sit amet, consectetur"
            });
            var group2 = new ProofreadingGroup() { GroupTitle = "Group 2" };
            group2.Items.Add(item4);
            group2.Items.Add(item5);
            Groups.Add(group2);
        }

        public void KeyDown(KeyEventArgs e)
        {
            if (e.KeyboardDevice.Modifiers == ModifierKeys.Control)
            {
                switch (e.Key)
                {
                    case Key.F:
                        ExecuteSearchCommand(null);
                        break;
                    case Key.O:
                        ExecuteOutlineCommand(null);
                        break;
                    case Key.OemComma: // "<"
                        ExecuteNavigatebackwardCommand(null);
                        break;
                    case Key.OemPeriod: // ">"
                        ExecuteNavigateforwardCommand(null);
                        break;
                    case Key.P:
                        ExecuteProofreadingCommand(null);
                        break;
                    case Key.R:
                        ExecuteRebootaddinCommand(null);
                        break;
                    default:
                        break;
                }
            }
        }

        public virtual void ExecutePopupMenuCommand(object obj)
        {
            if(isOpenPopup)
                this.isOpenPopup = false;
            else
                this.isOpenPopup = true;
        }

        public virtual void ExecuteBacklinksCommand(object obj)
        {
            if (isOpenBacklinks)
                this.isOpenBacklinks = false;
            else
                this.isOpenBacklinks = true;
        }

        public virtual void ExecuteCloseBacklinksCommand(object obj)
        {
            this.isOpenBacklinks = false;
        }

        




        public virtual void ExecuteSearchCommand(object obj)
        {
            this.isOpenPopup = false;
            MessageBox.Show("Search Command");
        }

        public virtual void ExecuteOutlineCommand(object obj)
        {
            this.isOpenPopup = false;
            MessageBox.Show("Outline Command");
        }
        public virtual void ExecuteNavigatebackwardCommand(object obj)
        {
            this.isOpenPopup = false;
            MessageBox.Show("Navigate backward Command");
        }
        public virtual void ExecuteNavigateforwardCommand(object obj)
        {
            this.isOpenPopup = false;
            MessageBox.Show("Navigate forward Command");
        }
        public virtual void ExecuteProofreadingCommand(object obj)
        {
            this.isOpenPopup = false;
            MessageBox.Show("Proof reading Command");
        }
        public virtual void ExecuteCommentsCommand(object obj)
        {
            this.isOpenPopup = false;
            MessageBox.Show("Comments Command");
        }
        public virtual void ExecuteChangeauthorsCommand(object obj)
        {
            this.isOpenPopup = false;
            MessageBox.Show("Change authors Command");
        }
        public virtual void ExecuteHighlightsettingsCommand(object obj)
        {
            this.isOpenPopup = false;
            MessageBox.Show("Highlight settings Command");
        }
        public virtual void ExecuteRelateddocumentsCommand(object obj)
        {
            this.isOpenPopup = false;
            MessageBox.Show("Related documents Command");
        }
        public virtual void ExecuteRebootaddinCommand(object obj)
        {
            this.isOpenPopup = false;
            MessageBox.Show("Reboot addin Command");
        }
        public virtual void ExecuteSendfeedbackCommand(object obj)
        {
            this.isOpenPopup = false;
            MessageBox.Show("Send feedback Command");
        }
        public virtual void ExecuteUpgradeCommand(object obj)
        {
            this.isOpenPopup = false;
            MessageBox.Show("Upgrade Command");
        }
        public virtual void ExecuteAboutCommand(object obj)
        {
            this.isOpenPopup = false;
            MessageBox.Show("About Command");
        }
        public virtual void ExecuteCloseaddinCommand(object obj)
        {
            this.isOpenPopup = false;
            MessageBox.Show("Close addin Command");
        }



        private RelayCommand _popupMenuCommand;
        public RelayCommand popupMenuCommand
        {
            get
            {
                return _popupMenuCommand;
            }
            set
            {
                _popupMenuCommand = value;
            }
        }

        private RelayCommand _backlinksCommand;
        public RelayCommand backlinksCommand
        {
            get
            {
                return _backlinksCommand;
            }
            set
            {
                _backlinksCommand = value;
            }
        }

        private RelayCommand _closeBacklinksCommand;
        public RelayCommand closeBacklinksCommand
        {
            get
            {
                return _closeBacklinksCommand;
            }
            set
            {
                _closeBacklinksCommand = value;
            }
        }
        

        private ObservableCollection<PopupMenuItem> popupMenuItems;
        public ObservableCollection<PopupMenuItem> PopupMenuItems
        {
            get => this.popupMenuItems;
        }

        private bool _isOpenPopup = false;
        public bool isOpenPopup
        {
            get
            {
                return _isOpenPopup;
            }
            set
            {
                if (_isOpenPopup != value)
                {
                    _isOpenPopup = value;
                    this.RaisePropertyChanged();
                }
            }
        }

        private ObservableCollection<ProofreadingGroup> groups;
        public ObservableCollection<ProofreadingGroup> Groups 
        {
            get 
            {
                return groups;
            } 
            set 
            { 
                groups = value; 
            }
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
        private bool _isOpenBacklinks = false;
        public bool isOpenBacklinks
        {
            get
            {
                return _isOpenBacklinks;
            }
            set
            {
                if (_isOpenBacklinks != value)
                {
                    _isOpenBacklinks = value;

                    if (_isOpenBacklinks)
                        downArrowIcon = IconType.Icon_Angle_Up_solid;
                    else
                        downArrowIcon = IconType.Icon_Angle_Down_solid;

                    this.RaisePropertyChanged();
                }
            }
        }

        private IconType _downArrowIcon = IconType.Icon_Angle_Down_solid;
        public IconType downArrowIcon
        {
            get
            {
                return _downArrowIcon;
            }
            set
            {
                _downArrowIcon = value;
                this.RaisePropertyChanged();
            }
        }
    }
}
