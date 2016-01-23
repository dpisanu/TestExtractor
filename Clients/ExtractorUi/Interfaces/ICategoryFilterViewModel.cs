namespace TestExtractor.ExtractorUi.Interfaces
{
    /// <summary>
    ///     Interface specifying the API of a Category Filter View Model
    ///     Implements Interface : <see cref="IViewModel" />
    /// </summary>
    internal interface ICategoryFilterViewModel : IViewModel
    {
        /// <summary>
        ///     Category Value
        /// </summary>
        string Category { get; }

        /// <summary>
        ///     Is Filter Enabled
        /// </summary>
        bool Enabled { get; set; }
    }
}