## Documentación Técnica Web Api Neoris
___
Para el correcto funcionamiento del web api se deben ejecutar los siguientes pasos:

**Migración de Base de datos (EF Code First):** Se debe ejecutar los siguientes comandos en la consola de Nugets de Visual Studio para la creación de la base de datos con su respectivas tablas:

~~~CMD

Add-Migration Initial -p Neoris.Repositories -s Neoris.Repositories

Update-database -p Neoris.Repositories -s Neoris.Repositories
~~~

**NOTA:**

1.1. Sino se desea realizar la migración se adjunta script de base datos para su ejecución en Sql Server (nombre de archivo: NeorisDB.sql). 

1.2 Si se genera el siguiente error en la migración:

~~~CMD
"The name 'Initial' is used by an existing migration."
~~~

Se debe eliminar el contenido de la carpeta Migrations en la biblioteca de clases Neoris.Repositories para su correcto funcionamiento.

**Administración de repositorio GitHub:** Se trabajo en la rama de Developer pero también la rama master está homologada.
Url de repositorio: https://github.com/Ju4nMa24/Neoris.Api.git

**Postman:** Se crea la siguiente colección en Postman para validación de los Endpoints: https://www.postman.com/speeding-crater-141527/workspace/neoris/collection/8167258-fd0fd981-dc3f-49b7-b116-43ef159e1d5d?action=share&creator=8167258

Adicional se crea colección de Environments para un uso más sencillo: https://www.postman.com/speeding-crater-141527/workspace/neoris/environment/8167258-f6a66d77-05e1-405c-ba1f-514e7d0326ba

