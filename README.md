# Notificaciones_urbanNav
Notification microservice of the urbanNav project
Notificaciones UrbanNav
Microservicio del proyecto de una aplicacion web de servicios de transporte

Requisitos previos
.NET Core SDK instalado en tu máquina.
Configuración
Clona este repositorio en tu máquina local.

Abre una terminal en la carpeta raíz del proyecto.

Ejecuta el siguiente comando para restaurar las dependencias del proyecto:

dotnet restore

Crea un archivo .env en la carpeta raíz del proyecto y agrega las siguientes variables de entorno:

SENDGRID_API_KEY=<tu clave de API de SendGrid>
AWS_ACCESS_KEY_ID=<tu ID de clave de acceso de AWS>
AWS_SECRET_ACCESS_KEY=<tu clave de acceso secreta de AWS>
AWS_REGION=<la región de AWS que deseas utilizar>
AWS_SNS_TOPIC_ARN=<el ARN del tema de SNS que deseas utilizar>

Asegúrate de reemplazar los valores <...> con tus propias claves y valores.

Ejecución
En la misma terminal, ejecuta el siguiente comando para compilar y ejecutar la aplicación:

Si todo funciona correctamente, deberías ver la salida de la aplicación en la terminal.

Pruebas
Abre una terminal en la carpeta raíz del proyecto.

Ejecuta el siguiente comando para ejecutar las pruebas unitarias

dotnet test

Deberías ver los resultados de las pruebas en la terminal.
