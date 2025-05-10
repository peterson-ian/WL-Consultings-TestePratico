CREATE TABLE Usuario (
    Id UUID PRIMARY KEY,
    Nome VARCHAR(100) NOT NULL,
    Email VARCHAR(100) NOT NULL,
    Senha VARCHAR(255) NOT NULL,
    Bloqueado BOOLEAN NOT NULL,
    Ativo BOOLEAN NOT NULL,
    Tentativas_Login INTEGER DEFAULT 0
);

INSERT INTO usuario (
    id, nome, email, senha, bloqueado, ativo, tentativas_login
) VALUES (
    '01e76595-c268-482b-8c58-0e6e5b10abcc',
    'admin',
    'admin@gmail.com',
    '$2a$12$yHVibJdcjRTKSNyfSwYr8OAnd3pcpo8PbSpLlOSTh6v0BmMIsIy8m',
    false,
    true,
    0
);

CREATE TABLE Carteira (
    Id UUID PRIMARY KEY,
    Usuario_Id UUID NOT NULL,
    Moeda VARCHAR(10) NOT NULL DEFAULT 'BRL',
    Saldo DECIMAL(19, 4) NOT NULL DEFAULT 0.0000,
    Data_Criacao TIMESTAMPTZ  NOT NULL DEFAULT CURRENT_TIMESTAMP,
    Data_Atualizacao TIMESTAMPTZ  NOT NULL DEFAULT CURRENT_TIMESTAMP,
    status VARCHAR(20) NOT NULL,
    CONSTRAINT fk_usuario FOREIGN KEY (Usuario_Id) REFERENCES USuario(Id) ON DELETE RESTRICT
);

INSERT INTO Carteira (
    Id,
    Usuario_Id,
    Moeda,
    Saldo,
    Data_Criacao,
    Data_Atualizacao,
    Status
) VALUES (
    '7c9e6679-7425-40de-944b-e07fc1f90ae7',
    '01e76595-c268-482b-8c58-0e6e5b10abcc',
    'BRL',
    0,
    CURRENT_TIMESTAMP,
    CURRENT_TIMESTAMP,
    'ATIVA'
);


CREATE TABLE Transacao (
    Id UUID PRIMARY KEY,
    Codigo_Transacao VARCHAR(50) UNIQUE NOT NULL,
    Carteira_Origem_Id UUID,
    Carteira_Destino_Id UUID NOT NULL,
    Valor DECIMAL(19, 4) NOT NULL,
    Tipo  VARCHAR(20) NOT NULl,
    Data_Solicitacao TIMESTAMPTZ  NOT NULL DEFAULT CURRENT_TIMESTAMP,
    Data_Processamento TIMESTAMPTZ ,
    Data_Conclusao TIMESTAMP,
    Descricao TEXT,
    Status VARCHAR(20) NOT NULL,
    Motivo_Falha TEXT,
    Hash_Verificacao VARCHAR(128) NOT NULL, 
    CONSTRAINT fk_carteira_origem FOREIGN KEY (Carteira_Origem_Id) REFERENCES Carteira(Id) ON DELETE RESTRICT,
    CONSTRAINT fk_carteira_destino FOREIGN KEY (Carteira_Destino_Id) REFERENCES Carteira(Id) ON DELETE RESTRICT
);