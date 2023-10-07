﻿
namespace SdkContPAQi
{
    /// <summary>
    ///     Agente solo con las propiedades mas comunes.
    ///     Este modelo se puede utilizar para consultar el catalogo de agentes cuando solo se necesitan algunas propiedades.
    ///     Las propiedades de este modelo tienen los los mismos nombres que las propiedades del modelo de SQL para facilitar
    ///     la asignacion de valores cuando se utiliza con los repositorios genericos de Agentes, como por ejemplo los que
    ///     implementan <see cref="IAgenteRepository{T}" /> de <see cref="AgenteDto" />.
    /// </summary>
    public class AgenteDto
    {
        /// <summary>
        ///     Id del agente.
        /// </summary>
        public int CIDAGENTE { get; set; }

        /// <summary>
        ///     Codigo del agente.
        /// </summary>
        public string CCODIGOAGENTE { get; set; } = string.Empty;

        /// <summary>
        ///     Nombre del agente.
        /// </summary>
        public string CNOMBREAGENTE { get; set; } = string.Empty;

        /// <summary>
        ///     Tipo del agente: 1 = Agente de Ventas. 2 = Agente Venta / Cobro. 3 = Agente de Cobro. 4 = Figuras de transporte.
        ///     5 = Medios de transporte.
        /// </summary>
        public int CTIPOAGENTE { get; set; }
    }
}