CREATE TABLE CategoriaJogos
(
    Id INT PRIMARY KEY IDENTITY(1,1),
    Nome NVARCHAR(10) NOT NULL UNIQUE
);


INSERT INTO CategoriaJogos (Nome)
VALUES ('BRONZE'), ('PRATA'), ('OURO');

CREATE TABLE Jogos
(
    Identificador BIGINT PRIMARY KEY IDENTITY(1,1),
    Titulo NVARCHAR(200) NOT NULL,
    Descricao NVARCHAR(MAX) NULL,
    Disponivel BIT NOT NULL,
    CategoriaId INT NOT NULL FOREIGN KEY REFERENCES CategoriaJogos(Id),
    Responsavel NVARCHAR(100) NOT NULL,
    DataEntrega DATETIME NOT NULL
);


INSERT INTO Jogos (Titulo, Descricao, Disponivel, CategoriaId, Responsavel, DataEntrega)
VALUES 
-- OURO
('The Legend of Zelda: Breath of the Wild', 'Uma aventura épica em mundo aberto com mecânicas inovadoras.', 1, 3, 'Nintendo', '2017-03-03'),

('The Witcher 3: Wild Hunt', 'RPG com narrativa profunda e mundo vasto.', 1, 3, 'CD Projekt Red', '2015-05-19'),

('Red Dead Redemption 2', 'Faroeste cinematográfico com realismo impressionante.', 1, 3, 'Rockstar Games', '2018-10-26'),

-- PRATA
('Hollow Knight', 'Metroidvania desafiador com arte desenhada à mão.', 1, 2, 'Team Cherry', '2017-02-24'),

('Stardew Valley', 'Simulador de fazenda relaxante com elementos de RPG.', 1, 2, 'ConcernedApe', '2016-02-26'),

('Cuphead', 'Plataforma retrô com foco em chefões e estética dos anos 1930.', 1, 2, 'Studio MDHR', '2017-09-29'),

-- BRONZE
('Among Us', 'Jogo social de dedução e traição.', 1, 1, 'InnerSloth', '2018-06-15'),

('Slay the Spire', 'Mistura de roguelike com deck-building estratégico.', 1, 1, 'MegaCrit', '2019-01-23'),

('Undertale', 'RPG indie com narrativa única e múltiplos finais.', 1, 1, 'Toby Fox', '2015-09-15');

