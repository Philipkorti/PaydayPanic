using Common.Command.NotifyPropertyChanged;
using Microsoft.Practices.Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayDay.ViewModels
{
    public class ViewModelBase : NotifyPropertyChanged
    {
        #region ------------------------- Constructors, Destructors, Dispose, Clone ---------------------------------------
        public ViewModelBase(IEventAggregator eventAggregator) 
        {
            this.EventAggregator= eventAggregator;
        }
        #endregion

        #region ------------------------- Properties, Indexers ------------------------------------------------------------
        protected IEventAggregator EventAggregator { get; set; }
        #endregion
    }
}
