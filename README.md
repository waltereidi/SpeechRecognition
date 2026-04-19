 
## 📑 Sumário

- [Introdução](#introdução)  
- [Conhecimento Obtido](#conhecimento-obtido-durante-o-desenvolvimento)  
- [Dificuldades](#dificuldades)  
- [Desenvolvimento](#desenvolvimento)  
- [Resultados](#resultados)  
- [Conclusão](#conclusão)  
- [Melhorias Futuras](#melhorias-futuras-para-o-sistema)  

---

## 🚀 Introdução

A arquitetura do sistema foi projetada para ser escalável tanto na regra de negócio quanto nos serviços da aplicação.

O sistema utiliza:

- Domínio rico para desacoplamento da regra de negócio  
- Camada de aplicação como intermediadora  
- Abstração da infraestrutura  
- Mensageria para comunicação entre microserviços  

### 🧩 Arquitetura de Microserviços

- Cada microserviço é responsável por interpretar suas próprias regras  
- Os dados são isolados por serviço  
- Comunicação desacoplada via mensageria  

Os serviços de tradução e conversão de áudio utilizam padrões de projeto do GoF (Gang of Four).

---

## 📚 Conhecimento Obtido Durante o Desenvolvimento

- Banco de dados NoSQL  
- Firestore  
- Domain Driven Design (DDD)  
- CQRS (Command Query Responsibility Segregation)  
- Domínio rico  
- Mensageria  
- Containerização  

---

## ⚠️ Dificuldades

- Aplicação do conceito de Separation of Concerns (SoC)  
- Desacoplamento entre domínio e infraestrutura  
- Organização da arquitetura em camadas  

---

## 🏗️ Desenvolvimento

### Benefícios do Banco NoSQL

- Facilidade de uso  
- Ecossistema rico  
- Flexibilidade de estrutura  

### Diferenças entre Firestore e Bancos Relacionais

O Firestore é uma opção viável para aplicações:

- Pequenas  
- Altamente escaláveis  
- Distribuídas  

Muito utilizado em:

- Aplicações mobile  
- Sistemas distribuídos  

---

## 📊 Resultados

O sistema desenvolvido foi capaz de:

- Realizar tradução de áudio  
- Converter formatos de áudio  
- Escalar conforme demanda  
- Permitir expansão futura com facilidade  

---

## 🧠 Conclusão

A utilização de arquitetura baseada em microserviços, aliada a boas práticas de engenharia de software, permitiu o desenvolvimento de uma aplicação moderna, escalável e desacoplada.

O uso de Firestore e mensageria contribuiu para a construção de um sistema distribuído eficiente.

---

## 🔮 Melhorias Futuras para o Sistema

- Deploy automatizado para múltiplos hosts  
- Novos templates de tradução de áudio  
- Mais formatos de conversão de áudio  
- Reconhecimento de texto com LLM  
- Implementação de interface de usuário  

---
