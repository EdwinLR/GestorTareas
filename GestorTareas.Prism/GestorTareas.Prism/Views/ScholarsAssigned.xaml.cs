using System.Collections.Generic;
using Xamarin.Forms;

namespace GestorTareas.Prism.Views
{
    public partial class ScholarsAssigned : ContentPage
    {
        public ScholarsAssigned()
        {
            InitializeComponent();
            list.ItemsSource = new List<Person>()
            {
                new Person()
                {
                    Name = "Lozada Ramos Edwin",
                    StudentNumber = 20078390,
                    Image = "ic_person.png"
                },
                new Person()
                {
                    Name = "Barroeta Martinez Alejandro",
                    StudentNumber = 20071361,
                    Image = "ic_person.png"
                },
            };
        }

        public class Person
        {
            public string Name { get; set; }
            public int StudentNumber { get; set; }
            public string Image { get; set; }
        }
    }
}
