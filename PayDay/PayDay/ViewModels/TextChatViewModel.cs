using Data;
using Microsoft.Practices.Prism.Events;
using Services.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PayDay.ViewModels
{
    public class TextChatViewModel : ViewModelBase
    {
        #region ------------------------- Fields, Constants, Delegates, Events --------------------------------------------
        private List<Chat> chat;
        private Game game;
        #endregion

        #region ------------------------- Constructors, Destructors, Dispose, Clone ---------------------------------------
        public TextChatViewModel(IEventAggregator eventAggregator, Game game):base(eventAggregator) 
        {
            this.game = game;
            this.Chat= new List<Chat>();
            BackgroundWorker worker = new BackgroundWorker();
            worker.DoWork += GetMessages;
            worker.RunWorkerAsync();
        }
        #endregion

        #region ------------------------- Properties, Indexers ------------------------------------------------------------
        public List<Chat> Chat
        {
            get { return chat; }
            set 
            {
                if(this.chat!= value)
                {
                    this.chat= value;
                    this.OnPropertyChanged(nameof(Chat));
                }
            }
        }
        #endregion

        #region ------------------------- Private helper ------------------------------------------------------------------
        private void GetMessages(object sender, DoWorkEventArgs e)
        {
            do
            {
                this.Chat = TextChatService.GetTextChatMessages(this.game.GameId,this.game.UserId,this.game.Username);
            } while (true);
            
        }
        #endregion

        #region ------------------------- Commands ------------------------------------------------------------------------
        #endregion
    }
}
