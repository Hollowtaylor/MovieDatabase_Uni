using Microsoft.Win32;
using MovieDatabase.Models;
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
using System.Text.RegularExpressions;

namespace MovieDatabase
{
    /// <summary>
    /// Interaction logic for MovieView.xaml
    /// </summary>
    public partial class MovieView : Window
    {
        private Database db;
        private Movie movie;
        private BitmapImage bitmap;
        public MovieView()
        {
            InitializeComponent();
            db = new Database();
            movie = new Movie();
            
            ViewMode();

        }

        private void EditMode()
        {
            NextButton.Visibility = Visibility.Hidden;
            PreviousButton.Visibility = Visibility.Hidden;
            FirstButton.Visibility = Visibility.Hidden;
            LastButton.Visibility = Visibility.Hidden;
            SaveButton.Visibility = Visibility.Visible;
            CancelButton.Visibility = Visibility.Visible;
            CastAddButton.Visibility = Visibility.Visible;
            CastDelButton.Visibility = Visibility.Visible;


            EditMenu.IsEnabled = false;
            FileMenu.IsEnabled = false;
            ViewMenu.IsEnabled = false;

            title.IsEnabled = true;
            duration.IsEnabled = true;
            year.IsEnabled = true;
            budget.IsEnabled = true;
            director.IsEnabled = true;
            Rating.IsEnabled = true;
            URLtxt.IsEnabled = true;
            Casttxt.IsEnabled = true;

            
            URLGb.Visibility = Visibility.Visible;
            URLtxt.Visibility = Visibility.Visible;
            CastGb.Visibility = Visibility.Visible;
            Casttxt.Visibility = Visibility.Visible;
            
            Poster.Visibility = Visibility.Visible;

        }
        private void ViewMode()
        {
            FirstButton.Visibility = Visibility.Visible;
            PreviousButton.Visibility = Visibility.Visible;
            NextButton.Visibility = Visibility.Visible;
            LastButton.Visibility = Visibility.Visible;
            SaveButton.Visibility = Visibility.Hidden;
            CancelButton.Visibility = Visibility.Hidden;
            CastAddButton.Visibility = Visibility.Hidden;
            CastDelButton.Visibility = Visibility.Hidden;

            EditMenu.IsEnabled = true;
            FileMenu.IsEnabled = true;
            FileNewMenu.IsEnabled = true;
            FileSaveMenu.IsEnabled = true;
            ViewMenu.IsEnabled = true;
            AboutMenu.IsEnabled = true;

            title.IsEnabled = false;
            duration.IsEnabled = false;
            year.IsEnabled = false;
            budget.IsEnabled = false;
            director.IsEnabled = false;
            Rating.IsEnabled = false;
            URLtxt.IsEnabled = false;
            Casttxt.IsEnabled = false;

            URLGb.Visibility = Visibility.Hidden;
            URLtxt.Visibility = Visibility.Hidden;
            CastGb.Visibility = Visibility.Hidden;
            Casttxt.Visibility = Visibility.Hidden;
          
            Poster.Visibility = Visibility.Visible;
        }
        private Movie UpdateModelfromUI(Movie movie)
        {
            movie.Title = title.Text;
            movie.Duration = int.Parse(duration.Text);
            movie.Year = int.Parse(year.Text);
            movie.Budget = int.Parse(budget.Text);
            movie.Director = director.Text;
            movie.URL = URLtxt.Text;
            movie.Genres = Genre.Selected;
            movie.Rating = Rating.Selected;
            movie.Actors = CastList.Items.Cast<string>().ToList();
            return movie;

            
        }
        private void UpdateUIfromModel(Movie m)
        {
            // db.Update(movie);
            //   title.Text = movie.Title;
            //   duration.Text = Convert.ToString(movie.Duration);
            //   year.Text = Convert.ToString(movie.Year);
            //   budget.Text = Convert.ToString(movie.Budget);
            //  director.Text = director.Text;
            //   URLtxt.Text = URLtxt.Text;
            //   Genre.Selected = movie.Genres;
            //  Rating.Selected = movie.Rating;
            //  Poster.Source = bitmap;
            CastList.Items.Clear();
            title.Text = m != null ? m.Title : "";
            duration.Text = m != null ? m.Duration.ToString() : "";
            year.Text = m != null ? m.Year.ToString() : "";
            budget.Text = m != null ? m.Budget.ToString() : "";
            Rating.Selected = m != null ? m.Rating : 0;
            director.Text = m != null ? m.Director : "";
            Genre.Selected = m != null ? m.Genres : new List<Genre>();
            URLtxt.Text = m != null ? m.URL : "";
            if (m != null)
            {
                foreach (var i in m.Actors)
                    CastList.Items.Add(i);
                string path = (m.URL);
                var uri = new Uri(path, UriKind.Absolute);
                Poster.Source = new BitmapImage(uri);

            }
            else
            {
                CastList.Items.Clear();
                Poster.Source = new BitmapImage();
            }


            UpdateNavigation();
        }

    
    private void UpdateNavigation()
        {
            if (db.Index == 0)
            {
                FirstButton.Visibility = Visibility.Hidden;
                PreviousButton.Visibility = Visibility.Hidden;
                NextButton.Visibility = Visibility.Visible;
                LastButton.Visibility = Visibility.Visible;
            }
            else if (db.Index == db.Count - 1)
            {
                FirstButton.Visibility = Visibility.Visible;
                PreviousButton.Visibility = Visibility.Visible;
                NextButton.Visibility = Visibility.Hidden;
                LastButton.Visibility = Visibility.Hidden;
            }
            else
            {
                FirstButton.Visibility = Visibility.Visible;
                PreviousButton.Visibility = Visibility.Visible;
                NextButton.Visibility = Visibility.Visible;
                LastButton.Visibility = Visibility.Visible;
            }
        }
        private void MainMenuEdit_Click(object sender, RoutedEventArgs e)
        {
            EditMode();
        }
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (IsNumber() == true && IsValidURL() == true)
            {
                Movie m = new Movie();
                m = UpdateModelfromUI(m);
                db.Add(m);
                ImageConvert();
                UpdateUIfromModel(m);
                ViewMode();
                db.First();
                UpdateNavigation();
            }
            else
            {
                MessageBox.Show("Invalid Input");
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            UpdateUIfromModel(db.Get());
            ViewMode();
        }
        private void FirstButton_Click(object sender, RoutedEventArgs e)
        {
            db.First();
            UpdateUIfromModel(db.Get());
            UpdateNavigation();
        }

        private void PreviousButton_Click(object sender, RoutedEventArgs e)
        {
            db.Prev();
            UpdateUIfromModel(db.Get());
            UpdateNavigation();
        }

        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            db.Next();
            UpdateUIfromModel(db.Get());
            UpdateNavigation();
        }

        private void LastButton_Click(object sender, RoutedEventArgs e)
        {
            db.Last();
            UpdateUIfromModel(db.Get());
            UpdateNavigation();
        }


        private void AboutMenu_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Created By:\n" +
                            "Dean Stewart       B00719125\n" +
                            "Ricky Taylor       B00706372\n" + 
                            "James Rodinson     B00695309\n");
        }

  



        private void FileNewMenu_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog()
            {
                Filter = "json files|*.json",
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                Title = "Enter name of order file to save"
            };
            if (dialog.ShowDialog() == true)
            {
                var file = dialog.FileName;
                db.Save(file);
                EditMode();
            }
        }

        private void FileSaveMenu_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog()
            {
                Filter = "json files|*.json",
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                Title = "Enter name of order file to save"
            };
            if (dialog.ShowDialog() == true)
            {
                var file = dialog.FileName;
                db.Save(file);
            }
        }

  
        private void ExitMenu_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Are you sure you want to exit the program?", "Exit the program?", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                this.Close();
            }
        }

        private void FileLoadMenu_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog()
            {
                Filter = "json files|*.json",
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                Title = "Enter name of order file to save"
            };
            if (dialog.ShowDialog() == true)
            {
                var file = dialog.FileName;
                db.Load(file);
            }
        }

        private void CastAddButton_Click(object sender, RoutedEventArgs e)
        {
            if (Casttxt.Text != "")
                CastList.Items.Add(Casttxt.Text);
            Casttxt.Clear();
        }

        private void CastDelButton_Click(object sender, RoutedEventArgs e)
        {
            if (CastList.SelectedItem != null)
                CastList.Items.RemoveAt(CastList.SelectedIndex);
            Casttxt.Clear();
        }

        private void EditCreateMenu_Click(object sender, RoutedEventArgs e)
        {
            EditMode();
            title.Clear();
            year.Clear();
            duration.Clear();
            budget.Clear();
            director.Clear();
            Casttxt.Clear();
            URLtxt.Clear();
            Genre.Selected = new List<Genre>();
            Poster.Source = new BitmapImage();
            Rating.Selected = 0;
        }

        private void EditEditMenu_Click(object sender, RoutedEventArgs e)
        {
            EditMode();
        }

        private void EditDeleteMenu_Click(object sender, RoutedEventArgs e)
        {
            db.Delete();
            UpdateUIfromModel(db.Get());
            UpdateNavigation();
        }

        public bool IsNumber()
        {
            int random;
            if (int.TryParse(duration.Text, out random) && int.TryParse(year.Text, out random) && int.TryParse(budget.Text, out random))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool IsValidURL()
        {
            string URL = URLtxt.Text;
            string pattern = @"^(((ht|f)tp(s?)\://)|(www))?[^.](.)[^.](([-.\w]*[0-9a-zA-Z])+(:(0-9)*)*(\/?)([a-zA-Z0-9\-\.\?\,\'\/\\\+&amp;%\$#_]*))[^.](.)[^.]([a-zA-Z0-9]+)(\.jpg)$";
            Regex regex = new Regex(pattern);
            Match regexresult = Regex.Match(URL, pattern);

            if (regexresult.Success)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        private void ImageConvert()
        {

            var path = URLtxt.Text;
            var uri = new Uri(path, UriKind.Absolute);
            bitmap = new BitmapImage(uri);
        }

        private void OrderByYear_Click(object sender, RoutedEventArgs e)
        {
            db.First();
            UpdateNavigation();
            db.OrderByTitle();
            UpdateUIfromModel(db.Get());
        }

        private void OrderByTitle_Click(object sender, RoutedEventArgs e)
        {
            db.First();
            UpdateNavigation();
            db.OrderByYear();
            UpdateUIfromModel(db.Get());
        }

        private void OrderByDuration_Click(object sender, RoutedEventArgs e)
        {
            db.First();
            UpdateNavigation();
            db.OrderByDuration();
            UpdateUIfromModel(db.Get());
        }
        // Create what ever methods you deem necessary to provide a functioning UI
    }
}