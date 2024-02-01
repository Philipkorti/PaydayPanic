using Microsoft.Practices.Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace PayDay.ViewModels
{
    public class RankedMultiplayerWaitingViewModel : ViewModelBase
    {
        #region ------------------------- Fields, Constants, Delegates, Events --------------------------------------------
        private DateTime timeSpan;
        DispatcherTimer timer;
        private string timertext;
        #endregion

        #region ------------------------- Constructors, Destructors, Dispose, Clone ---------------------------------------
        public RankedMultiplayerWaitingViewModel(IEventAggregator eventAggregator) : base(eventAggregator) 
        {
            this.timer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(1) };
            timer.Tick += this.Timer;
            timer.Start();
        }
        #endregion

        #region ------------------------- Properties, Indexers ------------------------------------------------------------
        public string Timertext
        {
            get { return this.timertext; }
            set 
            {
                if (this.Timertext != value)
                {
                    this.timertext = value;
                    this.OnPropertyChanged(nameof(this.Timertext));
                }
            }
        }

        #endregion

        #region ------------------------- Private helper ------------------------------------------------------------------
        private void Timer(object sender, EventArgs e)
        {
            this.timeSpan = this.timeSpan.AddSeconds(1);
            this.Timertext = this.timeSpan.ToString("mm:ss");
        }
        #endregion

        #region ------------------------- Commands ------------------------------------------------------------------------
        #endregion
    }
}
