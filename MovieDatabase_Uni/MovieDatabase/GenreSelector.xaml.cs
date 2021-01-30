using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using MovieDatabase.Models;

namespace MovieDatabase
{
    /// <summary>
    /// Interaction logic for Genre.xaml
    /// </summary>
    public partial class GenreSelector : UserControl
    {
        public GenreSelector()
        {
            InitializeComponent();
        }
        public List<Genre> Selected
        {
            get
            {
                var SelectGenres = new List<Genre>();
                if (CbComedy.IsChecked.Value) SelectGenres.Add(Genre.comedy);
                if (CbRomance.IsChecked.Value) SelectGenres.Add(Genre.romance);
                if (CbAction.IsChecked.Value) SelectGenres.Add(Genre.action);
                if (CbThriller.IsChecked.Value) SelectGenres.Add(Genre.thriller);
                if (CbHorror.IsChecked.Value) SelectGenres.Add(Genre.horror);
                if (CbWestern.IsChecked.Value) SelectGenres.Add(Genre.western);
                if (CbScifi.IsChecked.Value) SelectGenres.Add(Genre.scifi);
                if (CbWar.IsChecked.Value) SelectGenres.Add(Genre.war);

                return SelectGenres;
            }
            set
            {
                CbComedy.IsChecked = value.Contains(Genre.comedy) ? true : false;
                CbRomance.IsChecked = value.Contains(Genre.romance) ? true : false;
                CbAction.IsChecked = value.Contains(Genre.action) ? true : false;
                CbThriller.IsChecked = value.Contains(Genre.thriller) ? true : false;
                CbHorror.IsChecked = value.Contains(Genre.horror) ? true : false;
                CbWestern.IsChecked = value.Contains(Genre.western) ? true : false;
                CbScifi.IsChecked = value.Contains(Genre.scifi) ? true : false;
                CbWar.IsChecked = value.Contains(Genre.war) ? true : false;
            }
        }
            public void GenresClear()
        {
            CbComedy.IsChecked = false;
            CbRomance.IsChecked = false;
            CbAction.IsChecked = false;
            CbThriller.IsChecked = false;
            CbHorror.IsChecked = false;
            CbWestern.IsChecked = false;
            CbScifi.IsChecked = false;
            CbWar.IsChecked = false;
        }
        }
    }
