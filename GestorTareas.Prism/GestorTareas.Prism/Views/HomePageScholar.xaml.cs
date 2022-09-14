using System.Collections.Generic;
using Xamarin.Forms;
using static GestorTareas.Prism.Views.RegisteredUsersList;

namespace GestorTareas.Prism.Views
{
    public partial class HomePageScholar : ContentPage
    {
        public HomePageScholar()
        {
            InitializeComponent();

            Lunes.ItemsSource = new List<Task>()
            {
                new Task()
                {
                    Name = "Limpiar anaqueles de Soporte Técnico",
                    Detail = "Fecha Limite: 09/11/2022 - 12:00 am"
                },
                new Task()
                {
                    Name = "Limpiar anaqueles de Soporte Técnico",
                    Detail = "Fecha Limite: 09/11/2022 - 12:00 am"
                },
                new Task()
                {
                    Name = "Limpiar anaqueles de Soporte Técnico",
                    Detail = "Fecha Limite: 09/11/2022 - 12:00 am"
                }
            };

            Martes.ItemsSource = new List<Task>()
            {
                new Task()
                {
                    Name = "Limpiar anaqueles de Soporte Técnico",
                    Detail = "Fecha Limite: 09/11/2022 - 12:00 am"
                }
            };

            Miercoles.ItemsSource = new List<Task>()
            {
                new Task()
                {
                    Name = "No tienes actividades asignadas para este día.",
                    Detail = ""
                }
            };

            Jueves.ItemsSource = new List<Task>()
            {
                new Task()
                {
                    Name = "Limpiar anaqueles de Soporte Técnico",
                    Detail = "Fecha Limite: 09/11/2022 - 12:00 am"
                },
                new Task()
                {
                    Name = "Limpiar anaqueles de Soporte Técnico",
                    Detail = "Fecha Limite: 09/11/2022 - 12:00 am"
                }
            };

            Viernes.ItemsSource = new List<Task>()
            {
                new Task()
                {
                    Name = "No tienes actividades asignadas para este día.",
                    Detail = ""
                }
            };
        }

        public class Task
        {
            public string Name { get; set; }
            public string Detail { get; set; }
        }
    }
}
