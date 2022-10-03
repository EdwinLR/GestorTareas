using System.Collections.Generic;
using Xamarin.Forms;

namespace GestorTareas.Prism.Views
{
    public partial class AreaManagersScholarsRequests : ContentPage
    {
        public AreaManagersScholarsRequests()
        {
            InitializeComponent();
            list.ItemsSource = new List<Request>()
            {
                new Request()
                {
                    Manager = "Sánchez Nava Rosa María",
                    State = "Solicitud del 27 sept. 2022\nEstado: " + "En proceso de autorización"
                },
                new Request()
                {
                    Manager = "Sánchez Nava Rosa María",
                    State = "Solicitud del 13 jul. 2022\nEstado: " + "En proceso de autorización"
                },
            };
        }

        public class Request
        {
            public string Manager { get; set; }
            public string State { get; set; }
        }
    }
}
