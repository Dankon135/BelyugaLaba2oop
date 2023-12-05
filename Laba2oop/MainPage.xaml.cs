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
        private string _selectedXslFilePath; // Додавання шляху до XSL файлу

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
                    DisplayAllXmlData(_selectedXmlFilePath);
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"An error occurred: {ex.Message}", "OK");
            }
        }

        private async void OnPickXslFileClicked(object sender, EventArgs e)
        {
            try
            {
                var pickResult = await FilePicker.Default.PickAsync();
                if (pickResult != null)
                {
                    _selectedXslFilePath = pickResult.FullPath; // Збереження шляху до XSLT файлу
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"An error occurred: {ex.Message}", "OK");
            }
        }

        private void DisplayAllXmlData(string xmlFilePath)
        {
            _context.SetStrategy(new SaxXmlProcessor());
            string result = _context.ProcessXml(xmlFilePath, "", "", "");
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

            string result = _context.ProcessXml(_selectedXmlFilePath, _selectedXslFilePath, searchQuery, searchType);
            ResultEditor.Text = result;
        }

        private void OnClearClicked(object sender, EventArgs e)
        {


            ResultEditor.Text = string.Empty;
            SearchQueryEntry.Text = string.Empty;


            MethodPicker.SelectedIndex = MethodPicker.Items.IndexOf("SAX");


            SearchTypePicker.SelectedIndex = SearchTypePicker.Items.IndexOf("author");


            _selectedXmlFilePath = null;
            _selectedXslFilePath = null;


        }


        private async void OnTransformToHtmlClicked(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(_selectedXmlFilePath) || string.IsNullOrWhiteSpace(_selectedXslFilePath))
            {
                await DisplayAlert("Warning", "Please select both an XML and an XSLT file first", "OK");
                return;
            }

            // Використовуємо безпечний шлях в спеціальній папці для збереження HTML файлу
            string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            string htmlOutputFileName = "outputfile.html";
            string htmlOutputFilePath = Path.Combine(documentsPath, htmlOutputFileName);

            try
            {
                string result = XmlTransformer.TransformXmlToHtml(_selectedXmlFilePath, _selectedXslFilePath, htmlOutputFilePath);
                await DisplayAlert("Transformation Result", result, "OK");
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
                System.Diagnostics.Process.GetCurrentProcess().Kill();
            }
        }

    }
}
