using System;
using System.Collections.Generic;
using System.Text;

namespace Twins.Models
{
    class DefaultParameters
    {
        private static DefaultParameters _instance = null;

        public int Colum { get; set; }

        public int Row { get; set; }

        public string Desk { get; set; }

        private DefaultParameters()
        {
            Colum = 6;
            Row = 4;
            Desk = "Animales";
        }

        public static DefaultParameters Instance
        {
            get
            {
                // The first call will create the one and only instance.
                if (_instance == null)
                {
                    _instance = new DefaultParameters();
                }

                // Every call afterwards will return the single instance created above.
                return _instance;
            }
        }
    }
}
