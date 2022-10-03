using System.Collections.Generic;
using Xamarin.Forms;

namespace GestorTareas.Prism.Views
{
    public partial class ScholarsRequests : ContentPage
    {
        public ScholarsRequests()
        {
            InitializeComponent();
            list.ItemsSource = new List<Request>()
            {
                new Request()
                {
                    RequestName = "Solicitud del 27 sept. 2022",
                    State = "Estado: " + "En proceso de autorización"
                },
                new Request()
                {
                    RequestName = "Solicitud del 13 jul. 2022",
                    State = "Estado: " + "En proceso de autorización"
                },
            };
        }

        public class Request
        {
            public string RequestName { get; set; }
            public string State { get; set; }
        }
    }
}
