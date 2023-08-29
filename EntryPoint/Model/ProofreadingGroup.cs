using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntryPoint.Model
{
	public class ProofreadingGroup
	{
        public string GroupTitle { get; set; }
        public ObservableCollection<ProofreadingItem> Items { get; set; } = new ObservableCollection<ProofreadingItem>();
    }
}
