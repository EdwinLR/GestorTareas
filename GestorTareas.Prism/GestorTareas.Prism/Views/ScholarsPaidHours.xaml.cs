using System.Collections.Generic;
using Xamarin.Forms;

namespace GestorTareas.Prism.Views
{
    public partial class ScholarsPaidHours : ContentPage
    {
        public ScholarsPaidHours()
        {
            InitializeComponent();
            list.ItemsSource = new List<Person>()
            {
                new Person()
                {
                    Name = "Lozada Ramos Edwin",
                    HoursDetail = "Horas Totales: 375\nHoras Registradas: 100\nHoras Restantes: 275",
                    Image = "ic_person.png"
                },
                new Person()
                {
                    Name = "Barroeta Martinez Alejandro",
                    HoursDetail = "Horas Totales: 275\nHoras Registradas: 75\nHoras Restantes: 200",
                    Image = "ic_person.png"
                },
            };
        }

        public class Person
        {
            public string Name { get; set; }
            public string HoursDetail { get; set; }
            public string Image { get; set; }
        }
    }
}
