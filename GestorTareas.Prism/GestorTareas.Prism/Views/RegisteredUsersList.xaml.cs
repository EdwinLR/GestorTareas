using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace GestorTareas.Prism.Views
{
    public partial class RegisteredUsersList : ContentPage
    {
        public RegisteredUsersList()
        {
            InitializeComponent();
            list.ItemsSource = new List<Person>()
            {
                new Person()
                {
                    Name = "Lozada Ramos Edwin",
                    StudentNumber = 20078390,
                    Image = "image.png"
                },
                new Person()
                {
                    Name = "Barroeta Martinez Alejandro",
                    StudentNumber = 20071361,
                    Image = "filter.png"
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
