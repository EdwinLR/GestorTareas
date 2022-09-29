using System.Collections.Generic;
using Xamarin.Forms;

namespace GestorTareas.Prism.Views
{
    public partial class AssignedScholarInfo : ContentPage
    {
        public AssignedScholarInfo()
        {
            InitializeComponent();
            Tasks.ItemsSource = new List<Task>()
            {
                new Task()
                {
                    Name = "Limpiar anaqueles de Soporte Técnico",
                    Detail = "Fecha Limite: 09/11/2022 - 12:00 am"
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
