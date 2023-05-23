using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Services.Services
{
    public class ErrorServices
    {
        public static void ShowError(ErrorCodes errorCodes)
        {
           if(errorCodes != ErrorCodes.NoError)
            {
                MessageBox.Show("There has been an error: " + errorCodes, errorCodes.ToString(), MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
