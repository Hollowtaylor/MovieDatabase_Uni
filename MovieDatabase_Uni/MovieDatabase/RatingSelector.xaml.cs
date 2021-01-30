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
using System.Windows.Shapes;

namespace MovieDatabase
{
    /// <summary>
    /// Interaction logic for Rating.xaml
    /// </summary>
    public partial class RatingSelector : UserControl
    {
        public RatingSelector()
        {
            InitializeComponent();
        }
        public int Selected
        {
            get
            {
                int rating = 0;
                if (Rb1.IsChecked.Value) rating = 1;
                if (Rb2.IsChecked.Value) rating = 2;
                if (Rb3.IsChecked.Value) rating = 3;
                if (Rb4.IsChecked.Value) rating = 4;
                if (Rb5.IsChecked.Value) rating = 5;
                return rating;
            }
            set
            {
                ClearRatings();
                if (value == 1) Rb1.IsChecked = true;
                if (value == 2) Rb2.IsChecked = true;
                if (value == 3) Rb3.IsChecked = true;
                if (value == 4) Rb4.IsChecked = true;
                if (value == 5) Rb5.IsChecked = true;
            }
        }

        public void ClearRatings()
        {
            Rb1.IsChecked = false;
            Rb2.IsChecked = false;
            Rb3.IsChecked = false;
            Rb4.IsChecked = false;
            Rb5.IsChecked = false;

        }
    }
}





/*
var rating = "";
if (Rb1.IsChecked.Value)
{
    rating = "1";
}
else if (Rb2.IsChecked.Value)
{
    rating = "2";
}
else if (Rb3.IsChecked.Value)
{
    rating = "3";
}
else if (Rb4.IsChecked.Value)
{
    rating = "4";
}
else if (Rb5.IsChecked.Value)
{
    rating = "5";
}
MessageBox.Show("User Selected" + rating);
}

}
}
*/
