/// <summary>Represents an SMS message.</summary>
/// <remarks>
/// This class contains the phone number of the recipient and the content of the message.
/// </remarks>
namespace Notificaciones.Models
{
    public class ModeloSms
    {
        /// <summary>Gets or sets the phone number of the recipient.</summary>
        public string? numeroDestino { set; get; }
        /// <summary>Gets or sets the content of the message.</summary>
        public string? contenidoMensaje { set; get; }
    }
}
