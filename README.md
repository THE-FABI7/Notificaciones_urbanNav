# Notificaciones UrbanNav

Microservicio de notificaciones para la aplicación web de servicios de transporte UrbanNav.

## Requisitos Previos

- [.NET Core SDK](https://dotnet.microsoft.com/download/dotnet-core) instalado en tu máquina (se recomienda la versión 6.0).

## Configuración

Sigue estos pasos para configurar el proyecto en tu entorno local:

1. Clona este repositorio en tu máquina local:

    ```bash
    git clone https://github.com/tu-usuario/tu-repositorio.git
    ```

2. Abre una terminal en la carpeta raíz del proyecto.

3. Ejecuta el siguiente comando para restaurar las dependencias del proyecto:

    ```bash
    dotnet restore
    ```

4. Crea un archivo `.env` en la carpeta raíz del proyecto y agrega las siguientes variables de entorno:

    ```plaintext
    SENDGRID_API_KEY=<tu clave de API de SendGrid>
    AWS_ACCESS_KEY_ID=<tu ID de clave de acceso de AWS>
    AWS_SECRET_ACCESS_KEY=<tu clave de acceso secreta de AWS>
    AWS_REGION=<la región de AWS que deseas utilizar>
    AWS_SNS_TOPIC_ARN=<el ARN del tema de SNS que deseas utilizar>
    ```

   Asegúrate de reemplazar los valores `<...>` con tus propias claves y valores.

## Ejecución

En la misma terminal, ejecuta el siguiente comando para compilar y ejecutar la aplicación:

```bash
dotnet run


5. **Sección de Pruebas:** Al igual que con la sección de configuración, formatea esta sección para que los comandos de prueba sean más visibles.

```markdown
## Pruebas

Sigue estos pasos para ejecutar las pruebas unitarias:

1. Abre una terminal en la carpeta raíz del proyecto.

2. Ejecuta el siguiente comando para ejecutar las pruebas unitarias:

    ```bash
    dotnet test
    ```

Deberías ver los resultados de las pruebas en la terminal.
