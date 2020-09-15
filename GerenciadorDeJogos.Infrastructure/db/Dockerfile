FROM mysql:5.7.22
EXPOSE 3306
COPY ./GerenciadorDeJogos.Infrastructure/db/migrations/ /home/database/
COPY ./GerenciadorDeJogos.Infrastructure/db/dataset/ /home/database/
COPY ./GerenciadorDeJogos.Infrastructure/Ci/init_database.sh /docker-entrypoint-initdb.d/init_database.sh