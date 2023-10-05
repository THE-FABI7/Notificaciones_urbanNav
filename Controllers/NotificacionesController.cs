using Microsoft.AspNetCore.Mvc;

using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Threading.Tasks;
using Notificaciones.Models;
namespace Notificaciones.Controllers;
using DotNetEnv;
using Amazon;
using Amazon.SimpleNotificationService;
using Amazon.SimpleNotificationService.Model;


[ApiController]
[Route("[controller]")]
public class NotificacionesController : ControllerBase
{
    [Route("correo-bienvenida")]
    [HttpPost]
    /// <summary>Sends a welcome email to a recipient using SendGrid.</summary>
    /// <param name="datos">The data needed to send the email.</param>
    /// <returns>An ActionResult indicating whether the email was sent successfully or not.</returns>
    /// <remarks>
    /// This method uses SendGrid to send a welcome email to a recipient. It retrieves the SendGrid API key and the ID of the
    /// SendGrid template to use from environment variables. It then creates a SendGridMessage object and sets its template ID
    /// to the retrieved ID. It then sends the email using the SendGridClient object and the SendEmailAsync method. If the email
    /// is sent successfully, it returns an Ok ActionResult with a message indicating the
    public async Task<ActionResult> EnviarCorreoBienvenida(ModeloCorreo datos)
    {
        SendGridMessage msg = this.crearMensajeBase(datos);
        var apiKey = Environment.GetEnvironmentVariable("SEND_API_KEY");
        var client = new SendGridClient(apiKey);
        var templateId = Environment.GetEnvironmentVariable("WELCOME_SENDGRID_TEMPLATE_ID");



        msg.SetTemplateId(templateId);
        var response = await client.SendEmailAsync(msg);
        msg.SetTemplateData(new
        {
            name = datos.nombreDestino,
            message = datos.contenidoCorreo,
            asunto = datos.asuntoCorreo
        });

        if (response.StatusCode == System.Net.HttpStatusCode.Accepted)
        {
            return Ok("Correo sent successfully to: " + datos.correoDestino);

        }
        else
        {
            return BadRequest("Correo  sent failed" + datos.correoDestino);
        }
    }

    [Route("correo-recuperacion-clave")]
    [HttpPost]
    /// <summary>Sends a password recovery email to the specified email address.</summary>
    /// <param name="datos">The email data, including the recipient's email address and name.</param>
    /// <returns>An ActionResult indicating whether the email was sent successfully or not.</returns>
    /// <remarks>
    /// This method uses the SendGrid API to send an email to the specified email address. The email is based on a pre-defined template
    /// and includes the recipient's name and a message indicating that it is a password recovery email.
    /// </remarks>
    public async Task<ActionResult> EnviarCorreoRecuperacionClave(ModeloCorreo datos)
    {
        SendGridMessage msg = this.crearMensajeBase(datos);
        var apiKey = Environment.GetEnvironmentVariable("SEND_API_KEY");
        var client = new SendGridClient(apiKey);

        msg.SetTemplateId(Environment.GetEnvironmentVariable("WELCOME_SENDGRID_TEMPLATE_ID"));
        var response = await client.SendEmailAsync(msg);
        msg.SetTemplateData(new
        {
            name = datos.nombreDestino,
            message = "password recovery"
        });

        if (response.StatusCode == System.Net.HttpStatusCode.Accepted)
        {
            return Ok("Correo sent successfully to: " + datos.correoDestino);

        }
        else
        {
            return BadRequest("Correo  sent failed" + datos.correoDestino);
        }
    }

[Route("asignar-solicitud")]
[HttpPost]
public async Task<ActionResult> EnviarCorreoAsignacionSolicitud(ModeloAsignacionSolicitud datos)
{
    // Obtener los datos necesarios para enviar el correo
    var apiKey = Environment.GetEnvironmentVariable("SEND_API_KEY");
    var templateId = Environment.GetEnvironmentVariable("ASIGNACION_SOLICITUD_SENDGRID_TEMPLATE_ID");
    var from = new EmailAddress("correo@dominio.com", "Nombre del remitente");
    var to = new EmailAddress(datos.CorreoAsesor, datos.NombreAsesor);

    // Crear el mensaje base
    SendGridMessage msg = MailHelper.CreateSingleTemplateEmail(from, to, templateId, new
    {
        nombreUsuario = datos.NombreUsuario,
        nombreAsesor = datos.NombreAsesor,
        idSolicitud = datos.IdSolicitud
    });

    // Configurar el cliente de SendGrid y enviar el correo
    var client = new SendGridClient(apiKey);
    var response = await client.SendEmailAsync(msg);

    // Devolver una respuesta adecuada según el resultado del envío del correo
    if (response.StatusCode == System.Net.HttpStatusCode.Accepted)
    {
        return Ok("Correo enviado a " + datos.CorreoAsesor);
    }
    else
    {
        return BadRequest("Error enviando correo a " + datos.CorreoAsesor);
    }
}

    [Route("enviar-correo-2fa")]
    [HttpPost]
    /// <summary>Sends a two-factor authentication email to the specified recipient.</summary>
    /// <param name="datos">The email data, including recipient name, email address, subject, and message content.</param>
    /// <returns>An ActionResult indicating whether the email was successfully sent or not.</returns>
    /// <exception cref="System.ArgumentNullException">Thrown when the apiKey or templateId environment variables are null.</exception>
    public async Task<ActionResult> EnviarCorreo2fa(ModeloCorreo datos)
    {
        var apiKey = Environment.GetEnvironmentVariable("SEND_API_KEY");
        var templateId = Environment.GetEnvironmentVariable("TwoFA_SENDGRID_TEMPLATE_ID");
        var client = new SendGridClient(apiKey);

        SendGridMessage msg = this.crearMensajeBase(datos);
        msg.SetTemplateId(Environment.GetEnvironmentVariable("TwoFA_SENDGRID_TEMPLATE_ID"));
        msg.SetTemplateData(new
        {
            nombre = datos.nombreDestino,
            mensaje = datos.contenidoCorreo,
            asunto = datos.asuntoCorreo
        });
        var response = await client.SendEmailAsync(msg);
        if (response.StatusCode == System.Net.HttpStatusCode.Accepted)
        {
            return Ok("Correo enviado a la dirección " + datos.correoDestino);
        }
        else
        {
            return BadRequest("Error enviando el mensaje a la dirección: " + datos.correoDestino);
        }
    }

    /// <summary>Creates a base SendGrid message with the given email data.</summary>
    /// <param name="datos">The email data to use for the message.</param>
    /// <returns>A SendGrid message with the given email data.</returns>
    /// <exception cref="Exception">Thrown when the SendGrid API key is not configured correctly.</exception>
    private SendGridMessage crearMensajeBase(ModeloCorreo datos)
    {
        var apiKey = Environment.GetEnvironmentVariable("SEND_API_KEY");

        if (string.IsNullOrEmpty(apiKey))
        {
            throw new Exception("La clave de API de SendGrid no se ha configurado correctamente.");
        }

        var client = new SendGridClient(apiKey);
        var from = new EmailAddress("jesus.1701912773@ucaldas.edu.co", "jesus salazar");
        var subject = datos.asuntoCorreo;
        var to = new EmailAddress(datos.correoDestino, datos.nombreDestino);
        var plainTextContent = "plain text content";
        var htmlContent = datos.contenidoCorreo;
        var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
        return msg;
    }

    [Route("enviar-sms")]
    [HttpPost]
    /// <summary>Sends an SMS message with a new password to a specified phone number.</summary>
    /// <param name="datos">The SMS message data, including the message content and destination phone number.</param>
    /// <returns>An ActionResult indicating whether the message was successfully sent or not.</returns>
    /// <exception cref="AmazonSimpleNotificationServiceException">Thrown when there is an error publishing the message.</exception>
    public async Task<ActionResult> EnviarSMSNuevaClave(ModeloSms datos)
    {
        var accessKey = Environment.GetEnvironmentVariable("ACCESS_KEY_AWS");
        var secretKey = Environment.GetEnvironmentVariable("SECRET_KEY_AWS");
        var client = new AmazonSimpleNotificationServiceClient(accessKey, secretKey, RegionEndpoint.USEast1);
        var messageAttributes = new Dictionary<string, MessageAttributeValue>();
        var smsType = new MessageAttributeValue
        {
            DataType = "String",
            StringValue = "Transactional"
        };

        messageAttributes.Add("AWS.SNS.SMS.SMSType", smsType);

        PublishRequest request = new PublishRequest
        {
            Message = datos.contenidoMensaje,
            PhoneNumber = datos.numeroDestino,
            MessageAttributes = messageAttributes
        };
        try
        {
            await client.PublishAsync(request);
            return Ok("Mensaje enviado");
        }
        catch
        {
            return BadRequest("Error enviando el sms");
        }
    }
}
