using Common.Command.NotifyPropertyChanged;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayDay
{
    public class RoundRobinCollection : NotifyPropertyChanged
    {
        private readonly List<float> _values;

        public IReadOnlyList<float> Values => _values;



        public RoundRobinCollection(int amount)

        {

            _values = new List<float>();

            for (int i = 0; i < amount; i++)

                _values.Add(0F);

        }

        public void Push(float value)

        {
            _values.RemoveAt(0);
            _values.Add(value);
            OnPropertyChanged(nameof(Values));

        }

    }
}
