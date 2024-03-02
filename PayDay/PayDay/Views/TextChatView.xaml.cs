using Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PayDay.Views
{
    /// <summary>
    /// Interaktionslogik für TextChatView.xaml
    /// </summary>
    public partial class TextChatView : UserControl
    {
        private string userId;
        private string gameid;
        public TextChatView(string userid, string gameid)
        {
            this.userId = userid;
            InitializeComponent();
            this.gameid = gameid;
        }

        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                TextChatService.SetTextChat(this.userId,TextInput.Text, this.gameid);
                TextInput.Text = null;
            }
        }
    }
}
