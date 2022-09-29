using System.Collections.Generic;
using Xamarin.Forms;

namespace GestorTareas.Prism.Views
{
    public partial class ScholarHomePage : ContentPage
    {
        public ScholarHomePage()
        {
            InitializeComponent();

            MenuItems.ItemsSource = new List<MenuItem>()
            {
                new MenuItem()
                {
                    Item = "Tareas Asignadas",
                    Detail = "",
                    Image = "ic_person.png"
                },
                new MenuItem()
                {
                    Item = "Horas retribuidas",
                    Detail = "",
                    Image = "ic_person.png"
                }
            };
        }

        public class MenuItem
        {
            public string Item { get; set; }
            public string Detail { get; set; }
            public string Image { get; set; }
        }
    }
}

