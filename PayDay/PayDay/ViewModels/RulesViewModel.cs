using Microsoft.Practices.Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayDay.ViewModels
{
    public class RulesViewModel : ViewModelBase
    {
        /// <summary>
        /// Initialize a new instance of the <see cref="RulesViewModel"/> class.
        /// </summary>
        public RulesViewModel(IEventAggregator eventAggregator): base(eventAggregator) { }
    }
}
