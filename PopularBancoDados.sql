-- =============================================================================
-- SCRIPT PARA POPULAR BANCO DE DADOS - ACESSIBILIDADE WEB API
-- Dados fictícios para teste - Região de Belo Horizonte/MG
-- =============================================================================

-- =============================================================================
-- LIMPAR DADOS EXISTENTES (opcional - descomente se necessário)
-- =============================================================================
-- PRINT 'Limpando dados existentes...'

-- Deletar dados respeitando as foreign keys (ordem inversa da criação)
-- DELETE FROM assistencia;
-- DELETE FROM avaliacaoLocal;
-- DELETE FROM historicoStatusSolicitacao;
-- DELETE FROM solicitacaoAjuda;
-- DELETE FROM dispositivo;
-- DELETE FROM deficiente;
-- DELETE FROM voluntario;
-- DELETE FROM usuario;
-- DELETE FROM [local];

-- Resetar contadores IDENTITY para começar do 1
-- DBCC CHECKIDENT ('assistencia', RESEED, 0);
-- DBCC CHECKIDENT ('avaliacaoLocal', RESEED, 0);
-- DBCC CHECKIDENT ('historicoStatusSolicitacao', RESEED, 0);
-- DBCC CHECKIDENT ('solicitacaoAjuda', RESEED, 0);
-- DBCC CHECKIDENT ('dispositivo', RESEED, 0);
-- DBCC CHECKIDENT ('usuario', RESEED, 0);
-- DBCC CHECKIDENT ('[local]', RESEED, 0);

-- PRINT 'Dados limpos e contadores resetados!'

-- =============================================================================
-- 1. INSERIR USUÁRIOS (BASE)
-- =============================================================================
PRINT 'Inserindo usuários...'

INSERT INTO usuario (nome, email, telefone, senha) VALUES
-- Voluntários (5)
('Maria Silva Santos', 'maria.silva@email.com', '(31) 98765-4321', 'senha123'),
('João Pedro Oliveira', 'joao.oliveira@email.com', '(31) 99876-5432', 'senha123'),
('Ana Carolina Lima', 'ana.lima@email.com', '(31) 97654-3210', 'senha123'),
('Carlos Eduardo Souza', 'carlos.souza@email.com', '(31) 96543-2109', 'senha123'),
('Patricia Costa Almeida', 'patricia.almeida@email.com', '(31) 95432-1098', 'senha123'),

-- Deficientes (10)
('Roberto Ferreira', 'roberto.ferreira@email.com', '(31) 94321-0987', 'senha123'),
('Luciana Barbosa', 'luciana.barbosa@email.com', '(31) 93210-9876', 'senha123'),
('Fernando Rodrigues', 'fernando.rodrigues@email.com', '(31) 92109-8765', 'senha123'),
('Camila Santos Cruz', 'camila.cruz@email.com', '(31) 91098-7654', 'senha123'),
('Diego Alves Pereira', 'diego.pereira@email.com', '(31) 90987-6543', 'senha123'),
('Juliana Mendes', 'juliana.mendes@email.com', '(31) 89876-5432', 'senha123'),
('Marcos Vinicius Silva', 'marcos.vinicius@email.com', '(31) 88765-4321', 'senha123'),
('Rafaela Gomes Dias', 'rafaela.dias@email.com', '(31) 87654-3210', 'senha123'),
('André Luiz Costa', 'andre.costa@email.com', '(31) 86543-2109', 'senha123'),
('Beatriz Nascimento', 'beatriz.nascimento@email.com', '(31) 85432-1098', 'senha123');

-- =============================================================================
-- 2. INSERIR VOLUNTÁRIOS
-- =============================================================================
PRINT 'Inserindo voluntários...'

INSERT INTO voluntario (idUsuario, disponivel, avaliacao) VALUES
(1, 1, 4.85),  -- Maria Silva Santos - Disponível
(2, 1, 4.92),  -- João Pedro Oliveira - Disponível
(3, 0, 4.67),  -- Ana Carolina Lima - Indisponível
(4, 1, 4.78),  -- Carlos Eduardo Souza - Disponível
(5, 1, 4.94);  -- Patricia Costa Almeida - Disponível

-- =============================================================================
-- 3. INSERIR DEFICIENTES
-- =============================================================================
PRINT 'Inserindo deficientes...'

INSERT INTO deficiente (idUsuario, tipoDeficiencia) VALUES
(6, 1),   -- Roberto Ferreira - Deficiência Visual
(7, 2),   -- Luciana Barbosa - Deficiência Auditiva
(8, 3),   -- Fernando Rodrigues - Deficiência Física
(9, 1),   -- Camila Santos Cruz - Deficiência Visual
(10, 4),  -- Diego Alves Pereira - Deficiência Intelectual
(11, 2),  -- Juliana Mendes - Deficiência Auditiva
(12, 3),  -- Marcos Vinicius Silva - Deficiência Física
(13, 1),  -- Rafaela Gomes Dias - Deficiência Visual
(14, 5),  -- André Luiz Costa - Deficiência Múltipla
(15, 3);  -- Beatriz Nascimento - Deficiência Física

-- =============================================================================
-- 4. INSERIR LOCAIS (Região de Belo Horizonte)
-- Coordenadas de BH convertidas para INT (multiplicadas por 1000000)
-- =============================================================================
PRINT 'Inserindo locais...'

INSERT INTO [local] (latitude, longitude, descricao, avaliacaoAcessibilidade) VALUES
-- Centro de BH
(-19916681, -43934493, 'Praça da Liberdade - Centro Cultural', 4),
(-19920458, -43937779, 'Shopping Diamond Mall', 5),
(-19932234, -43938473, 'Estação Central do Metrô', 3),

-- Savassi
(-19936912, -43934567, 'Shopping Pátio Savassi', 5),
(-19937845, -43935123, 'Praça da Savassi', 2),

-- Funcionários
(-19948123, -43934890, 'Hospital das Clínicas UFMG', 4),
(-19945678, -43932456, 'Centro de Saúde Funcionários', 3),

-- Barro Preto
(-19954321, -43931789, 'Mercado Central de BH', 2),

-- Lourdes
(-19961234, -43935678, 'Shopping Boulevard', 5),

-- Santa Efigênia
(-19925432, -43928765, 'Rodoviária de Belo Horizonte', 3),

-- Zona Norte
(-19912345, -43945678, 'Shopping Cidade', 4),

-- Contagem (região metropolitana)
(-19891234, -44054321, 'Centro de Contagem', 2);

-- =============================================================================
-- 5. INSERIR DISPOSITIVOS
-- =============================================================================
PRINT 'Inserindo dispositivos...'

INSERT INTO dispositivo (numeroSerie, dataRegistro, usuarioProprietarioId) VALUES
-- Dispositivos dos Deficientes
('DEV001-ABC123', '2024-01-15T10:30:00', 6),   -- Roberto Ferreira
('DEV002-XYZ789', '2024-01-20T14:15:00', 7),   -- Luciana Barbosa
('DEV003-QWE456', '2024-02-05T09:45:00', 8),   -- Fernando Rodrigues
('DEV004-RTY321', '2024-02-10T16:20:00', 9),   -- Camila Santos Cruz
('DEV005-UIO654', '2024-02-15T11:10:00', 10),  -- Diego Alves Pereira
('DEV006-ASD987', '2024-03-01T13:30:00', 11),  -- Juliana Mendes
('DEV007-FGH258', '2024-03-05T08:45:00', 12),  -- Marcos Vinicius Silva
('DEV008-JKL741', '2024-03-10T15:25:00', 13),  -- Rafaela Gomes Dias
('DEV009-ZXC963', '2024-03-15T10:55:00', 14),  -- André Luiz Costa
('DEV010-VBN852', '2024-03-20T12:40:00', 15),  -- Beatriz Nascimento

-- Dispositivos dos Voluntários
('DEV011-POI147', '2024-01-25T09:15:00', 1),   -- Maria Silva Santos
('DEV012-LKJ258', '2024-02-01T14:30:00', 2),   -- João Pedro Oliveira
('DEV013-MNB369', '2024-02-12T11:45:00', 3),   -- Ana Carolina Lima
('DEV014-QAZ741', '2024-02-20T16:10:00', 4),   -- Carlos Eduardo Souza
('DEV015-WSX852', '2024-03-01T08:20:00', 5);   -- Patricia Costa Almeida

-- =============================================================================
-- 6. INSERIR AVALIAÇÕES DE LOCAIS
-- =============================================================================
PRINT 'Inserindo avaliações de locais...'

-- Declarar variáveis para IDs dinâmicos
DECLARE @LocalPracaLiberdade INT = (SELECT idLocal FROM [local] WHERE descricao LIKE '%Praça da Liberdade%')
DECLARE @LocalDiamondMall INT = (SELECT idLocal FROM [local] WHERE descricao LIKE '%Diamond Mall%')
DECLARE @LocalMetro INT = (SELECT idLocal FROM [local] WHERE descricao LIKE '%Metrô%')
DECLARE @LocalPatioSavassi INT = (SELECT idLocal FROM [local] WHERE descricao LIKE '%Pátio Savassi%')
DECLARE @LocalPracaSavassi INT = (SELECT idLocal FROM [local] WHERE descricao LIKE '%Praça da Savassi%')
DECLARE @LocalHospital INT = (SELECT idLocal FROM [local] WHERE descricao LIKE '%Hospital%')
DECLARE @LocalCentroSaude INT = (SELECT idLocal FROM [local] WHERE descricao LIKE '%Centro de Saúde%')
DECLARE @LocalMercado INT = (SELECT idLocal FROM [local] WHERE descricao LIKE '%Mercado Central%')
DECLARE @LocalBoulevard INT = (SELECT idLocal FROM [local] WHERE descricao LIKE '%Boulevard%')
DECLARE @LocalRodoviaria INT = (SELECT idLocal FROM [local] WHERE descricao LIKE '%Rodoviária%')

-- Dispositivos por usuário
DECLARE @DispRoberto INT = (SELECT d.id FROM dispositivo d INNER JOIN usuario u ON d.usuarioProprietarioId = u.idUsuario WHERE u.nome = 'Roberto Ferreira')
DECLARE @DispLuciana INT = (SELECT d.id FROM dispositivo d INNER JOIN usuario u ON d.usuarioProprietarioId = u.idUsuario WHERE u.nome = 'Luciana Barbosa')
DECLARE @DispFernando INT = (SELECT d.id FROM dispositivo d INNER JOIN usuario u ON d.usuarioProprietarioId = u.idUsuario WHERE u.nome = 'Fernando Rodrigues')
DECLARE @DispCamila INT = (SELECT d.id FROM dispositivo d INNER JOIN usuario u ON d.usuarioProprietarioId = u.idUsuario WHERE u.nome = 'Camila Santos Cruz')
DECLARE @DispDiego INT = (SELECT d.id FROM dispositivo d INNER JOIN usuario u ON d.usuarioProprietarioId = u.idUsuario WHERE u.nome = 'Diego Alves Pereira')
DECLARE @DispJuliana INT = (SELECT d.id FROM dispositivo d INNER JOIN usuario u ON d.usuarioProprietarioId = u.idUsuario WHERE u.nome = 'Juliana Mendes')
DECLARE @DispMarcos INT = (SELECT d.id FROM dispositivo d INNER JOIN usuario u ON d.usuarioProprietarioId = u.idUsuario WHERE u.nome = 'Marcos Vinicius Silva')
DECLARE @DispRafaela INT = (SELECT d.id FROM dispositivo d INNER JOIN usuario u ON d.usuarioProprietarioId = u.idUsuario WHERE u.nome = 'Rafaela Gomes Dias')
DECLARE @DispAndre INT = (SELECT d.id FROM dispositivo d INNER JOIN usuario u ON d.usuarioProprietarioId = u.idUsuario WHERE u.nome = 'André Luiz Costa')
DECLARE @DispBeatriz INT = (SELECT d.id FROM dispositivo d INNER JOIN usuario u ON d.usuarioProprietarioId = u.idUsuario WHERE u.nome = 'Beatriz Nascimento')
DECLARE @DispMaria INT = (SELECT d.id FROM dispositivo d INNER JOIN usuario u ON d.usuarioProprietarioId = u.idUsuario WHERE u.nome = 'Maria Silva Santos')
DECLARE @DispJoao INT = (SELECT d.id FROM dispositivo d INNER JOIN usuario u ON d.usuarioProprietarioId = u.idUsuario WHERE u.nome = 'João Pedro Oliveira')
DECLARE @DispAna INT = (SELECT d.id FROM dispositivo d INNER JOIN usuario u ON d.usuarioProprietarioId = u.idUsuario WHERE u.nome = 'Ana Carolina Lima')
DECLARE @DispCarlos INT = (SELECT d.id FROM dispositivo d INNER JOIN usuario u ON d.usuarioProprietarioId = u.idUsuario WHERE u.nome = 'Carlos Eduardo Souza')
DECLARE @DispPatricia INT = (SELECT d.id FROM dispositivo d INNER JOIN usuario u ON d.usuarioProprietarioId = u.idUsuario WHERE u.nome = 'Patricia Costa Almeida')

INSERT INTO avaliacaoLocal (idLocal, idDispositivo, acessivel, observacao, timestamp) VALUES
-- Avaliações da Praça da Liberdade
(@LocalPracaLiberdade, @DispRoberto, 1, 'Local bem acessível, com rampas e sinalização adequada para deficientes visuais.', '2024-06-01T10:30:00'),
(@LocalPracaLiberdade, @DispFernando, 1, 'Boa acessibilidade para cadeirantes, piso tátil presente.', '2024-06-02T14:15:00'),
(@LocalPracaLiberdade, @DispRafaela, 1, 'Estacionamento com vagas especiais disponíveis.', '2024-06-03T09:45:00'),

-- Avaliações do Shopping Diamond Mall
(@LocalDiamondMall, @DispLuciana, 1, 'Excelente acessibilidade, elevadores funcionando perfeitamente.', '2024-06-05T11:20:00'),
(@LocalDiamondMall, @DispCamila, 1, 'Banheiros adaptados limpos e bem conservados.', '2024-06-06T16:30:00'),
(@LocalDiamondMall, @DispJuliana, 1, 'Atendimento especializado no balcão de informações.', '2024-06-07T13:45:00'),

-- Avaliações da Estação do Metrô
(@LocalMetro, @DispDiego, 0, 'Elevador quebrado há dias, dificultando acesso de cadeirantes.', '2024-06-10T08:15:00'),
(@LocalMetro, @DispMarcos, 1, 'Piso tátil bem conservado e sinalização sonora funcionando.', '2024-06-11T12:30:00'),
(@LocalMetro, @DispAndre, 0, 'Falta de funcionários treinados para auxiliar deficientes.', '2024-06-12T15:20:00'),

-- Avaliações do Shopping Pátio Savassi
(@LocalPatioSavassi, @DispBeatriz, 1, 'Infraestrutura completa para acessibilidade.', '2024-06-15T10:45:00'),
(@LocalPatioSavassi, @DispMaria, 1, 'Estacionamento amplo com vagas preferenciais.', '2024-06-16T14:10:00'),

-- Avaliações da Praça da Savassi
(@LocalPracaSavassi, @DispJoao, 0, 'Calçadas irregulares e falta de rampas de acesso.', '2024-06-18T09:30:00'),
(@LocalPracaSavassi, @DispAna, 0, 'Bancos sem encosto adequado para idosos e deficientes.', '2024-06-19T16:45:00'),

-- Avaliações do Hospital das Clínicas
(@LocalHospital, @DispCarlos, 1, 'Hospital bem preparado para receber pessoas com deficiência.', '2024-06-20T11:15:00'),
(@LocalHospital, @DispPatricia, 1, 'Equipe médica capacitada no atendimento especializado.', '2024-06-21T13:30:00'),

-- Avaliações do Centro de Saúde
(@LocalCentroSaude, @DispRoberto, 1, 'Acesso facilitado e atendimento prioritário.', '2024-06-22T08:45:00'),
(@LocalCentroSaude, @DispFernando, 0, 'Fila única dificulta organização para atendimento preferencial.', '2024-06-23T15:10:00'),

-- Avaliações do Mercado Central
(@LocalMercado, @DispDiego, 0, 'Corredores estreitos dificultam circulação de cadeirantes.', '2024-06-25T10:20:00'),
(@LocalMercado, @DispMarcos, 0, 'Piso irregular e muitos obstáculos no caminho.', '2024-06-26T14:35:00'),

-- Avaliações do Shopping Boulevard
(@LocalBoulevard, @DispAndre, 1, 'Shopping moderno com excelente acessibilidade.', '2024-06-28T12:00:00'),
(@LocalBoulevard, @DispLuciana, 1, 'Todas as lojas têm acesso facilitado.', '2024-06-29T16:20:00'),

-- Avaliações da Rodoviária
(@LocalRodoviaria, @DispCamila, 0, 'Instalações antigas precisam de reforma para melhor acessibilidade.', '2024-07-01T09:10:00'),
(@LocalRodoviaria, @DispJuliana, 1, 'Guichês adaptados para cadeirantes disponíveis.', '2024-07-02T13:25:00');

-- =============================================================================
-- 7. INSERIR SOLICITAÇÕES DE AJUDA
-- =============================================================================
PRINT 'Inserindo solicitações de ajuda...'

-- IDs dos usuários deficientes
DECLARE @IdRoberto INT = (SELECT idUsuario FROM usuario WHERE nome = 'Roberto Ferreira')
DECLARE @IdLuciana INT = (SELECT idUsuario FROM usuario WHERE nome = 'Luciana Barbosa')
DECLARE @IdFernando INT = (SELECT idUsuario FROM usuario WHERE nome = 'Fernando Rodrigues')
DECLARE @IdCamila INT = (SELECT idUsuario FROM usuario WHERE nome = 'Camila Santos Cruz')
DECLARE @IdDiego INT = (SELECT idUsuario FROM usuario WHERE nome = 'Diego Alves Pereira')
DECLARE @IdJuliana INT = (SELECT idUsuario FROM usuario WHERE nome = 'Juliana Mendes')
DECLARE @IdMarcos INT = (SELECT idUsuario FROM usuario WHERE nome = 'Marcos Vinicius Silva')
DECLARE @IdRafaela INT = (SELECT idUsuario FROM usuario WHERE nome = 'Rafaela Gomes Dias')
DECLARE @IdAndre INT = (SELECT idUsuario FROM usuario WHERE nome = 'André Luiz Costa')
DECLARE @IdBeatriz INT = (SELECT idUsuario FROM usuario WHERE nome = 'Beatriz Nascimento')

INSERT INTO solicitacaoAjuda (idUsuario, descricao, status, dataSolicitacao, dataResposta, latitude, longitude, enderecoReferencia) VALUES
-- Status: 1=Pendente, 2=Em Andamento, 3=Concluída, 4=Cancelada

-- Solicitações Concluídas
(@IdRoberto, 'Preciso de ajuda para atravessar a Av. Afonso Pena próximo ao centro', 3, '2024-06-01T08:30:00', '2024-06-01T08:45:00', -19.916681, -43.934493, 'Av. Afonso Pena, próximo à Praça da Liberdade'),
(@IdLuciana, 'Auxílio para localizar entrada acessível no Shopping Diamond Mall', 3, '2024-06-02T14:20:00', '2024-06-02T14:30:00', -19.920458, -43.937779, 'Shopping Diamond Mall - Entrada Principal'),
(@IdFernando, 'Ajuda para usar o elevador na estação do metrô', 3, '2024-06-05T09:15:00', '2024-06-05T09:25:00', -19.932234, -43.938473, 'Estação Central do Metrô - Plataforma'),

-- Solicitações Em Andamento
(@IdCamila, 'Preciso de orientação para chegar até a recepção do hospital', 2, '2024-06-28T10:45:00', '2024-06-28T11:00:00', -19.948123, -43.934890, 'Hospital das Clínicas UFMG - Entrada Principal'),
(@IdDiego, 'Auxílio para encontrar o balcão de informações no shopping', 2, '2024-06-29T15:30:00', '2024-06-29T15:35:00', -19.936912, -43.934567, 'Shopping Pátio Savassi - Piso Térreo'),

-- Solicitações Pendentes
(@IdJuliana, 'Preciso de ajuda para navegar pelo Mercado Central', 1, '2024-06-30T12:00:00', NULL, -19.954321, -43.931789, 'Mercado Central - Entrada da Rua Curitiba'),
(@IdMarcos, 'Auxílio para localizar plataforma de ônibus na rodoviária', 1, '2024-06-30T16:20:00', NULL, -19.925432, -43.928765, 'Rodoviária de BH - Terminal Rodoviário'),
(@IdRafaela, 'Orientação para usar caixa eletrônico adaptado no banco', 1, '2024-07-01T09:30:00', NULL, -19.937845, -43.935123, 'Praça da Savassi - Agência Bancária'),

-- Solicitações Mais Antigas (Concluídas)
(@IdAndre, 'Preciso de companhia para consulta médica', 3, '2024-05-15T14:00:00', '2024-05-15T14:10:00', -19.945678, -43.932456, 'Centro de Saúde Funcionários'),
(@IdBeatriz, 'Ajuda para fazer compras no supermercado', 3, '2024-05-20T10:30:00', '2024-05-20T10:40:00', -19.961234, -43.935678, 'Shopping Boulevard - Supermercado');

-- =============================================================================
-- 8. INSERIR ASSISTÊNCIAS
-- =============================================================================
PRINT 'Inserindo assistências...'

-- IDs dos voluntários
DECLARE @IdMaria INT = (SELECT idUsuario FROM usuario WHERE nome = 'Maria Silva Santos')
DECLARE @IdJoao INT = (SELECT idUsuario FROM usuario WHERE nome = 'João Pedro Oliveira')
DECLARE @IdCarlos INT = (SELECT idUsuario FROM usuario WHERE nome = 'Carlos Eduardo Souza')
DECLARE @IdPatricia INT = (SELECT idUsuario FROM usuario WHERE nome = 'Patricia Costa Almeida')

-- IDs das solicitações de ajuda
DECLARE @SolRoberto INT = (SELECT s.idSolicitacaoAjuda FROM solicitacaoAjuda s INNER JOIN usuario u ON s.idUsuario = u.idUsuario WHERE u.nome = 'Roberto Ferreira')
DECLARE @SolLuciana INT = (SELECT s.idSolicitacaoAjuda FROM solicitacaoAjuda s INNER JOIN usuario u ON s.idUsuario = u.idUsuario WHERE u.nome = 'Luciana Barbosa')
DECLARE @SolFernando INT = (SELECT s.idSolicitacaoAjuda FROM solicitacaoAjuda s INNER JOIN usuario u ON s.idUsuario = u.idUsuario WHERE u.nome = 'Fernando Rodrigues')
DECLARE @SolCamila INT = (SELECT s.idSolicitacaoAjuda FROM solicitacaoAjuda s INNER JOIN usuario u ON s.idUsuario = u.idUsuario WHERE u.nome = 'Camila Santos Cruz')
DECLARE @SolDiego INT = (SELECT s.idSolicitacaoAjuda FROM solicitacaoAjuda s INNER JOIN usuario u ON s.idUsuario = u.idUsuario WHERE u.nome = 'Diego Alves Pereira')
DECLARE @SolAndre INT = (SELECT s.idSolicitacaoAjuda FROM solicitacaoAjuda s INNER JOIN usuario u ON s.idUsuario = u.idUsuario WHERE u.nome = 'André Luiz Costa')
DECLARE @SolBeatriz INT = (SELECT s.idSolicitacaoAjuda FROM solicitacaoAjuda s INNER JOIN usuario u ON s.idUsuario = u.idUsuario WHERE u.nome = 'Beatriz Nascimento')

INSERT INTO assistencia (idSolicitacaoAjuda, idUsuario, dataAceite, dataConclusao, DeficienteIdUsuario) VALUES
-- Assistências Concluídas
(@SolRoberto, @IdMaria, '2024-06-01T08:45:00', '2024-06-01T09:15:00', @IdRoberto),  -- Maria ajudou Roberto
(@SolLuciana, @IdJoao, '2024-06-02T14:30:00', '2024-06-02T15:00:00', @IdLuciana),  -- João ajudou Luciana
(@SolFernando, @IdCarlos, '2024-06-05T09:25:00', '2024-06-05T09:45:00', @IdFernando),  -- Carlos ajudou Fernando
(@SolAndre, @IdPatricia, '2024-05-15T14:10:00', '2024-05-15T15:30:00', @IdAndre), -- Patricia ajudou André
(@SolBeatriz, @IdMaria, '2024-05-20T10:40:00', '2024-05-20T12:00:00', @IdBeatriz), -- Maria ajudou Beatriz

-- Assistências Em Andamento
(@SolCamila, @IdJoao, '2024-06-28T11:00:00', NULL, @IdCamila),  -- João está ajudando Camila
(@SolDiego, @IdCarlos, '2024-06-29T15:35:00', NULL, @IdDiego); -- Carlos está ajudando Diego

-- =============================================================================
-- 9. INSERIR HISTÓRICO DE STATUS DAS SOLICITAÇÕES
-- =============================================================================
PRINT 'Inserindo histórico de status...'

INSERT INTO historicoStatusSolicitacao (solicitacaoAjudaId, statusAnterior, statusAtual, dataMudanca) VALUES
-- Histórico das solicitações concluídas
(@SolRoberto, 1, 2, '2024-06-01T08:45:00'),  -- Pendente -> Em Andamento
(@SolRoberto, 2, 3, '2024-06-01T09:15:00'),  -- Em Andamento -> Concluída

(@SolLuciana, 1, 2, '2024-06-02T14:30:00'),  -- Pendente -> Em Andamento
(@SolLuciana, 2, 3, '2024-06-02T15:00:00'),  -- Em Andamento -> Concluída

(@SolFernando, 1, 2, '2024-06-05T09:25:00'),  -- Pendente -> Em Andamento
(@SolFernando, 2, 3, '2024-06-05T09:45:00'),  -- Em Andamento -> Concluída

-- Histórico das solicitações em andamento
(@SolCamila, 1, 2, '2024-06-28T11:00:00'),  -- Pendente -> Em Andamento
(@SolDiego, 1, 2, '2024-06-29T15:35:00'),  -- Pendente -> Em Andamento

-- Histórico das solicitações antigas
(@SolAndre, 1, 2, '2024-05-15T14:10:00'),  -- Pendente -> Em Andamento
(@SolAndre, 2, 3, '2024-05-15T15:30:00'),  -- Em Andamento -> Concluída

(@SolBeatriz, 1, 2, '2024-05-20T10:40:00'), -- Pendente -> Em Andamento
(@SolBeatriz, 2, 3, '2024-05-20T12:00:00'); -- Em Andamento -> Concluída

-- =============================================================================
-- 10. VERIFICAR DADOS INSERIDOS
-- =============================================================================
PRINT 'Verificando dados inseridos...'

SELECT 'Usuários' as Tabela, COUNT(*) as Total FROM usuario
UNION ALL
SELECT 'Voluntários', COUNT(*) FROM voluntario
UNION ALL
SELECT 'Deficientes', COUNT(*) FROM deficiente
UNION ALL
SELECT 'Locais', COUNT(*) FROM [local]
UNION ALL
SELECT 'Dispositivos', COUNT(*) FROM dispositivo
UNION ALL
SELECT 'Avaliações de Locais', COUNT(*) FROM avaliacaoLocal
UNION ALL
SELECT 'Solicitações de Ajuda', COUNT(*) FROM solicitacaoAjuda
UNION ALL
SELECT 'Assistências', COUNT(*) FROM assistencia
UNION ALL
SELECT 'Histórico de Status', COUNT(*) FROM historicoStatusSolicitacao;

PRINT 'Script executado com sucesso!'
PRINT 'Dados fictícios inseridos para teste do sistema de acessibilidade.'
PRINT 'Região: Belo Horizonte e Região Metropolitana - Minas Gerais'

-- =============================================================================
-- RESUMO DOS DADOS INSERIDOS:
-- - 15 usuários (5 voluntários + 10 deficientes)
-- - 12 locais diferentes na região de BH
-- - 15 dispositivos
-- - 22 avaliações de locais
-- - 10 solicitações de ajuda
-- - 7 assistências (5 concluídas, 2 em andamento)
-- - Histórico completo de mudanças de status
-- ============================================================================= 