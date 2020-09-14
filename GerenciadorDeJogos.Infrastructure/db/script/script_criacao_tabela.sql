
create table tb_amigo(
amigo_id varchar(36) not null PRIMARY KEY,
nome_amigo varchar(40) not null,
telefone_amigo varchar(20) not null

);

create table tb_jogo(
jogo_id varchar(36) not null PRIMARY KEY,
nome_jogo varchar(40) not null,
amigo_id varchar(36) not null,
FOREIGN KEY (amigo_id)
        REFERENCES tb_amigo (amigo_id)
        ON UPDATE RESTRICT ON DELETE CASCADE
);

create table tb_emprestimo(
emprestimo_id varchar(36) not null PRIMARY KEY,
amigo_id varchar(36) not null,
data_emprestimo datetime not null,
qtd_dia INTEGER not null,
data_prevista_devolucao datetime,
FOREIGN KEY (amigo_id)
        REFERENCES tb_amigo (amigo_id)
        ON UPDATE RESTRICT ON DELETE CASCADE
);

create table tb_item_emprestado(
item_emprestado_id varchar(36) not null PRIMARY KEY,
jogo_id varchar(36) not null,
emprestimo_id varchar(36) not null,
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
usuario_id varchar(36) not null PRIMARY KEY,
login varchar(40) not null,
senha varchar(30) not null
)