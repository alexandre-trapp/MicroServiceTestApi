# WeatherDBApi
Api rest para consumo do backend em mongodb

Ocorreu problemas com o projeto para publicação no azure, de referencias com as packages do nuget, que não consegui arrumar a tempo
Para acessar a api via swagger, será necessário fazer checkout do fonte, abrir a solution em um visual studio 2019, rebuildar, e disparar no IIS a api.

a api está rodando em cima de um banco de dados mongoDb, conectado via string de conexão, formando um "servidor" na minha máquina
local, onde efetua as chamadas para a api de previsão do tempo e cadastra na base de dados;

Caso queira conectar no mongoDb para verificar a base de dados, ou se a mesma estiver fora, basta acessar pelo MongoDb compass pela string de conexão: 
**mongodb+srv://trapp:trapp@cluster0-fcbso.mongodb.net/test** ou linha de comando no mongoShell: **mongo "mongodb+srv://cluster0-fcbso.mongodb.net/test"  --username trapp** (informar senha: trapp)

Infelizmente ainda não estão se comunicando via message broker (rabbitMQ, etc), e sim diretamente, mas futuramente vou implementar essa parte.



