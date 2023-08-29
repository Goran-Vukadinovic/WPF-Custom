using Controls.Core;
using SvgResourceGenerator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace EntryPoint.Model
{
    public class PopupMenuItem
    {
        private IconType _iconType;
        public IconType iconType
        {
            get
            {
                return _iconType;
            }
            set
            {
                _iconType = value;
            }
        }

        private string _itemText;
        public string itemText
        {
            get
            {
                return _itemText;
            }
            set
            {
                _itemText = value;
            }
        }

        private string _itemCommandText;
        public string itemCommandText
        {
            get
            {
                return _itemCommandText;
            }
            set
            {
                _itemCommandText = value;
            }
        }

        private string _itemSectionText;
        public string itemSectionText
        {
            get
            {
                return _itemSectionText;
            }
            set
            {
                _itemSectionText = value;
            }
        }

        private RelayCommand _itemCommand;
        public RelayCommand itemCommand
        {
            get
            {
                return _itemCommand;
            }
            set
            {
                _itemCommand = value;
            }
        }

        private Visibility _visibleSeparator = Visibility.Hidden;
        public Visibility visibleSeparator
        {
            get
            {
                return _visibleSeparator;
            }
            set
            {
                _visibleSeparator = value;
            }
        }

        private Visibility _visibleSection = Visibility.Collapsed;
        public Visibility visibleSection
        {
            get
            {
                return _visibleSection;
            }
            set
            {
                _visibleSection = value;
            }
        }
        
    }
}
