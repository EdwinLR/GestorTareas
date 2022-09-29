using System.Collections.Generic;
using Xamarin.Forms;

namespace GestorTareas.Prism.Views
{
    public partial class AdminHomePage : ContentPage
    {
        public AdminHomePage()
        {
            InitializeComponent();

            MenuItems.ItemsSource = new List<MenuItem>()
            {
                new MenuItem()
                {
                    Item = "Jefes de Área",
                    Detail = "",
                    Image = "ic_person.png"
                },
                new MenuItem()
                {
                    Item = "Becarios",
                    Detail = "",
                    Image = "ic_person.png"
                },
                new MenuItem()
                {
                    Item = "Revisión de Solicitudes",
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
