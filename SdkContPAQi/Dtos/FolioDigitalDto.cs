
namespace SdkContPAQi
{
    public class FolioDigitalDto
    {
        /// <summary>
        ///     Id del folio digital.
        /// </summary>
        public int CIDFOLDIG { get; set; }

        /// <summary>
        ///     Guarda el UUID del documento timbrado.
        /// </summary>
        public string CUUID { get; set; } = string.Empty;
    }
}