using Data;
using Microsoft.Practices.Prism.Events;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayDay.Events
{
    public class ShopItemsDataChangeEvent : CompositePresentationEvent<ObservableCollection<ShopItems>>
    {
    }
}
