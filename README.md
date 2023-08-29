ChallengeN5

Este repositorio contiene una configuración de Docker Compose para facilitar el despliegue y la configuración de un entorno de desarrollo para el proyecto ChallengeN5. A continuación, se proporciona información sobre los servicios configurados y cómo usar este archivo de Docker Compose.

Servicios Configurados
sql-server-db
Imagen: mcr.microsoft.com/mssql/server:latest
Puerto: 1433
Variables de entorno:
SA_PASSWORD: Contraseña para el usuario "sa"
ACCEPT_EULA: "Y" para aceptar el acuerdo de licencia
Volúmenes: ./database/initDb.sql:/docker-entrypoint-initdb.d/initDb.sql

challenge-api
Build: Construye la imagen a partir del Dockerfile en ChallengeN5.Api
Puerto: 8080

elasticsearch
Imagen: docker.elastic.co/elasticsearch/elasticsearch:7.11.0
Puerto: 9200
Variables de entorno:
discovery.type=single-node

kafka
Imagen: confluentinc/cp-kafka
Puerto: 9092
Variables de entorno:
KAFKA_BROKER_ID: 1
KAFKA_ZOOKEEPER_CONNECT: "zookeeper:2181"
KAFKA_ADVERTISED_LISTENERS: "PLAINTEXT://kafka:9092"
KAFKA_OFFSETS_TOPIC_REPLICATION_FACTOR: 1

kibana
Imagen: docker.elastic.co/kibana/kibana:7.11.0
Puerto: 5601
Variables de entorno:
ELASTICSEARCH_URL: "http://elasticsearch:9200"

Instrucciones de Uso
Asegúrate de tener Docker y Docker Compose instalados en tu sistema.

Clona este repositorio en tu máquina local.

Desde la raíz del repositorio, ejecuta el siguiente comando para levantar los servicios:
docker-compose up -d

Espera a que los contenedores se inicien. Puedes verificar su estado con:
docker-compose ps

Si el servicio sql-server-db falla al crearse inicialmente debido a problemas de inicialización de la base de datos, asegúrate de que el archivo de inicialización initDb.sql se encuentre en la carpeta ./database/ y ejecútalo manualmente en el contenedor sql-server-db para configurar la base de datos.

Una vez que todos los contenedores estén en funcionamiento, podrás acceder a los servicios a través de los puertos especificados en el archivo docker-compose.yml. Por ejemplo:

La API estará disponible en http://localhost:8080
Elasticsearch en http://localhost:9200
Kibana en http://localhost:5601
Kafka en http://localhost:9092

Notas Adicionales
Este archivo de Docker Compose está diseñado para crear un entorno de desarrollo local.
