INTRODUÇÃO
A arquitetura do sistema foi projetada para ser escalável tanto em regra de negócio como em serviços de sua aplicação através de um domínio rico que desacopla a regra de negócio das demais camadas, a camada de aplicação é responsável por intermediar todas as dependências de baixo nível e utiliza uma abstração da camada de infraestrutura para garantir que o banco de dados seja totalmente desacoplado da regra de negócio e um serviço de mensageria que fornece uma maneira de se comunicar com micro serviços sem acoplar diretamente com a regra de negócio e funcionar como um sistema distribuído.

Detalhes de arquitetura de micro serviços : 


As normas de funcionamento devem ser interpretadas apenas pelo micro serviço desta forma os dados armazenados no banco de dados podem ser interpretados apenas pelo próprio micro serviço.
O serviço de tradução de aúdio e conversão de aúdio utilizam padrões de projeto GoF( Gang of Four ).

CONHECIMENTO OBTIDO DURANTE O DESENVOLVIMENTO
Fundamentos de banco de dados NoSQL e prática em desenvolvimento com FireStore e DDD(Domain Driven Design), CQRS(Command Query Responsability Segregation), Domínio Rico , Mensageria e Conteinerização

DIFICULDADES
Aplicação de conceitos de SoC(Separation of concerns) entre domínio e aplicação para implementação de infraestrutura com desacoplada de sua regra de negócio 

BENEFÍCIOS DO BANCO NOSQL
Facilidade de uso e ecossistema rico em funcionalidades
DIFERENÇAS ENTRE FIRESTORE E BANCOS RELACIONAIS
FireStore é uma opção viável para aplicações pequenas e altamente escaláveis devido a sua facilidade de uso e estruturas de tabelas maleáveis, uma opção muito utilizada para sistemas mobile e sistemas distribuídos
MELHORIAS FUTURAS PARA O SISTEMA

Adicionar deploy automatizado para múltiplos Hosts.
Adicionais mais templates de tradução de aúdio
Adicionar mais opções de conversão para formatos de aúdio
Adicionar reconhecimento de texto por LLM 
Implementar interface de usuário
