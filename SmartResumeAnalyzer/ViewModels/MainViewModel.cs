using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Win32;
using SmartResumeAnalyzer.Services;

namespace SmartResumeAnalyzer.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {
        [ObservableProperty] private string _resumePath;
        [ObservableProperty] private string _analysisResult;
        [ObservableProperty] private bool _isBusy;

        private readonly OpenAiService _aiService = new();

        [RelayCommand]
        private void BrowseFile()
        {
            var dialog = new OpenFileDialog
            {
                Filter = "PDF files (*.pdf)|*.pdf",
                Title = "Selecione um currículo PDF"
            };

            if (dialog.ShowDialog() == true)
                ResumePath = dialog.FileName;
        }

        [RelayCommand]
        private async Task AnalyzeResumeAsync()
        {
            if (string.IsNullOrWhiteSpace(ResumePath)) return;
            IsBusy = true;
            var resumeText = PdfReaderService.ExtractText(ResumePath);
            var result = await _aiService.AnalyzeResumeAsync(resumeText);
            AnalysisResult = result;
            IsBusy = false;
        }
    }
}