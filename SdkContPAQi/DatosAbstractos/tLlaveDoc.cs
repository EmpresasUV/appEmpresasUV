using System.Runtime.InteropServices;

namespace SdkContPAQi
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 4)]
    public struct tLlaveDoc
    {
        /// <summary>
        ///     Código del concepto del documento.
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = SdkConstantes.kLongCodigo)]
        public string aCodConcepto;

        /// <summary>
        ///     Serie del documento.
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = SdkConstantes.kLongSerie)]
        public string aSerie;

        /// <summary>
        ///     Folio del documento.
        /// </summary>
        public double aFolio;
    }
}