using Microsoft.Practices.Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace PayDay.ViewModels
{
    public class GameViewModel : ViewModelBase
    {
        #region ------------------------- Fields, Constants, Delegates, Events --------------------------------------------
        private DispatcherTimer time;
        private DateTime datetime;
        private string timerText;
        #endregion



        #region ------------------------- Constructors, Destructors, Dispose, Clone ---------------------------------------
        public GameViewModel(IEventAggregator eventAggregator) : base(eventAggregator) 
        {
            TimerText = "00:00";
            time = new DispatcherTimer();
            datetime= new DateTime();
            time.Interval = TimeSpan.FromMinutes(1);
            time.Tick += TimerTick;
            time.Start();
        }
        #endregion



        #region ------------------------- Properties, Indexers ------------------------------------------------------------
        public string TimerText
        {
            get { return timerText; }
            set { 
                if(timerText!= value)
                {
                    timerText = value;
                    this.OnPropertyChanged(nameof(TimerText));
                }
                
            }
        }
        #endregion



        #region ------------------------- Private helper ------------------------------------------------------------------
        private void TimerTick(object sender, EventArgs e)
        {
            datetime = datetime.AddMinutes(1);
            TimerText = datetime.ToString("HH:mm");
        }
        #endregion



        #region ------------------------- Commands ------------------------------------------------------------------------

        #endregion
    }
}
