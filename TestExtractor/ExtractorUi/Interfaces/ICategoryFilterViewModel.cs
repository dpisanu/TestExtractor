namespace TestExtractor.ExtractorUi.Interfaces
{
    internal interface ICategoryFilterViewModel : IViewModel
    {
        string Category { get; }

        bool Enabled { get; set; }
    }
}