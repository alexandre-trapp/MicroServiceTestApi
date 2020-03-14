# WeatherDBApi
Api rest para consumo do backend em mongodb

Para acessar a api via swagger, basta acessar a url no azure a seguir: 

a api está rodando em cima de um banco de dados mongoDb, conectado via string de conexão, formando um "servidor" na minha máquina
local, onde efetua as chamadas para a api de previsão do tempo e cadastra na base de dados;

Caso queira conectar no mongoDb para verificar a base de dados, basta acessar pelo MongoDb compass pela string de conexão: 
**mongodb+srv://trapp:trapp@cluster0-fcbso.mongodb.net/test** ou linha de comando no mongoShell: **mongo "mongodb+srv://cluster0-fcbso.mongodb.net/test"  --username trapp** (informar senha: trapp)

Infelizmente ainda não estão se comunicando via message broker (rabbitMQ, etc), e sim diretamente, mas futuramente vou implementar essa parte.



