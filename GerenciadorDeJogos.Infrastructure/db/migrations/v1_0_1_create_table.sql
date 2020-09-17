use gerenciarjogos;
create table tb_amigo(
amigo_id int auto_increment not null PRIMARY KEY,
nome_amigo varchar(40) not null,
telefone_amigo varchar(20) not null
);

create table tb_jogo(
jogo_id int auto_increment not null PRIMARY KEY,
nome_jogo varchar(40) not null,
amigo_id int not null,
FOREIGN KEY (amigo_id)
        REFERENCES tb_amigo (amigo_id)
        ON UPDATE RESTRICT ON DELETE CASCADE
);

create table tb_emprestimo(
emprestimo_id int auto_increment not null PRIMARY KEY,
amigo_id int not null,
data_emprestimo datetime not null,
qtd_dia INTEGER not null,
data_prevista_devolucao datetime,
FOREIGN KEY (amigo_id)
        REFERENCES tb_amigo (amigo_id)
        ON UPDATE RESTRICT ON DELETE CASCADE
);

create table tb_item_emprestado(
item_emprestado_id int auto_increment not null PRIMARY KEY,
jogo_id int not null,
emprestimo_id int not null,
data_devolucao datetime,
devolvido BOOLEAN ,
FOREIGN KEY (jogo_id)
        REFERENCES tb_jogo (jogo_id)
        ON UPDATE RESTRICT ON DELETE CASCADE,
		
FOREIGN KEY (emprestimo_id)
        REFERENCES tb_emprestimo (emprestimo_id)
        ON UPDATE RESTRICT ON DELETE CASCADE

);

create table tb_usuario(
usuario_id int auto_increment not null PRIMARY KEY,
nome varchar(70) not null,
login varchar(40) not null,
senha varchar(30) not null
);

SET character_set_client = utf8;
SET character_set_connection = utf8;
SET character_set_results = utf8;
SET collation_connection = utf8_general_ci;