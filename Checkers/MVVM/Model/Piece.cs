using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Checkers.MVVM.Model
{
    [Serializable]
    public class Piece:Core.ObservableObject
    {
        private int _x;
        private int _y;
        private bool _isKing;
        private string _imagePath;

        [XmlAttribute("X")]
        public int X
        {
            get => _x;
            set
            {
                _x = value;
                OnPropertyChanged();
            }
        }

        [XmlAttribute("Y")]
        public int Y
        {
            get => _y;
            set
            {
                _y = value;
                OnPropertyChanged();
            }
        }

        [XmlAttribute("IsKing")]
        public bool IsKing
        {
            get => _isKing;
            set
            {
                _isKing = value;
                OnPropertyChanged();
            }
        }

        [XmlAttribute("ImagePath")]
        public string ImagePath
        {
            get => _imagePath;
            set
            {
                _imagePath = value;
                OnPropertyChanged();
            }
        }
        public Piece() {}
        public Piece(int x,int y,bool isKing,string image)
        {
            X = x;
            Y = y;
            IsKing = isKing;
            ImagePath = image;

        }
    }
   
}
