using System.Runtime.InteropServices;

namespace SdkContPAQi
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 4)]
    public struct tTipoProducto
    {
        /// <summary>
        ///     Tipo de dato abstracto: tSeriesCapas.
        /// </summary>
        public tSeriesCapas aSeriesCapas;

        /// <summary>
        ///     Tipo de dato abstracto: Caracteristicas.
        /// </summary>
        public tCaracteristicas aCaracteristicas;
    }
}