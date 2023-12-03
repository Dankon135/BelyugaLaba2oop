using Microsoft.Maui.Controls;
using Microsoft.Maui.Storage;
using System;
using Laba2oop;

namespace Laba2oop
{
    public partial class MainPage : ContentPage
    {
        private XmlProcessingContext _context;
        private string _selectedXmlFilePath;

        public MainPage()
        {
            InitializeComponent();
            _context = new XmlProcessingContext(new SaxXmlProcessor());
            MethodPicker.SelectedIndex = 0;
            SearchTypePicker.SelectedIndex = 0;
        }

        private async void OnPickXmlFileClicked(object sender, EventArgs e)
        {
            try
            {
                var pickResult = await FilePicker.Default.PickAsync();
                if (pickResult != null)
                {
                    _selectedXmlFilePath = pickResult.FullPath;
                    DisplayAllXmlData(_selectedXmlFilePath);  // Виклик методу для відображення даних
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"An error occurred: {ex.Message}", "OK");
            }
        }

        private void DisplayAllXmlData(string xmlFilePath)
        {
            // Тут припускаємо, що ви використовуєте SAX метод за замовчуванням
            _context.SetStrategy(new SaxXmlProcessor());
            string result = _context.ProcessXml(xmlFilePath, "", "");
            ResultEditor.Text = result;
        }

        private void OnAnalyzeClicked(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(_selectedXmlFilePath))
            {
                DisplayAlert("Warning", "Please select an XML file first", "OK");
                return;
            }

            string searchQuery = SearchQueryEntry.Text;
            string searchType = SearchTypePicker.SelectedItem as string;
            string selectedMethod = MethodPicker.SelectedItem as string;

            switch (selectedMethod)
            {
                case "SAX":
                    _context.SetStrategy(new SaxXmlProcessor());
                    break;
                case "DOM":
                    _context.SetStrategy(new DomXmlProcessor());
                    break;
                case "LINQ to XML":
                    _context.SetStrategy(new LinqXmlProcessor());
                    break;
            }

            string result = _context.ProcessXml(_selectedXmlFilePath, searchQuery, searchType);
            ResultEditor.Text = result;
        }

        private void OnClearClicked(object sender, EventArgs e)
        {
            ResultEditor.Text = string.Empty;
            _selectedXmlFilePath = null;
        }

        private async void OnTransformToHtmlClicked(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(_selectedXmlFilePath))
            {
                await DisplayAlert("Warning", "Please select an XML file first", "OK");
                return;
            }

            string xsltFilePath = "C:\\Users\\Денис\\source\\repos\\Laba2oop\\Laba2oop\\transform.xslt";
            string htmlOutputFilePath = "C:\\Users\\Денис\\source\\repos\\Laba2oop\\Laba2oop\\outputfile.html";

            try
            {
                XmlTransformer.TransformXmlToHtml(_selectedXmlFilePath, xsltFilePath, htmlOutputFilePath);
                await DisplayAlert("Success", "XML was successfully transformed to HTML.", "OK");
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"An error occurred during transformation: {ex.Message}", "OK");
            }
        }
        private async void OnExitButtonClicked(object sender, EventArgs e)
        {
            bool answer = await DisplayAlert("Підтвердження", "Чи дійсно ви хочете завершити роботу з програмою?", "Так", "Ні");
            if (answer)
            {
                // Виконуємо вихід з програми
                System.Diagnostics.Process.GetCurrentProcess().Kill();
            }
        }

    }
}
