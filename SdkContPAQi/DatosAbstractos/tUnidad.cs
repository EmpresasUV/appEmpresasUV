using System.Runtime.InteropServices;

namespace SdkContPAQi
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 4)]
    public struct tUnidad
    {
        /// <summary>
        ///     Nombre de la unidad.
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = SdkConstantes.kLongNombre)]
        public string cNombreUnidad;

        /// <summary>
        ///     Abreviatura.
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = SdkConstantes.kLongAbreviatura)]
        public string cAbreviatura;

        /// <summary>
        ///     Valor de despliegue.
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = SdkConstantes.kLongAbreviatura)]
        public string cDespliegue;
    }
}