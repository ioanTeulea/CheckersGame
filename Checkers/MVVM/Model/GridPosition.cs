using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Checkers.MVVM.Model
{
    [Serializable]
    public class GridPosition : Core.ObservableObject
    {
        [XmlAttribute("Row")]
        public int Row { get; set; }

        [XmlAttribute("Column")]
        public int Column { get; set; }

        public GridPosition()
        {
        }

        public GridPosition(int row, int column)
        {
            Row = row;
            Column = column;
        }
    }
}
