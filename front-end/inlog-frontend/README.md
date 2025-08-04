# Projeto Frontend - Documentação

## 📌 Visão Geral

Este documento descreve as tecnologias, arquitetura, padrões utilizados, versões das dependências e instruções de como iniciar o projeto frontend fornecido. O projeto utiliza o FluentUI para tentar proporcionar um visual mais agradável e alinhado às ferramentas da Microsoft.

## 🚀 Tecnologias e Versões

### Plataforma
- **Angular 20.1.4**
- **TypeScript 5.8.2**

### Bibliotecas Principais
- **FluentUI Web Components** (2.6.1)
- **Angular Material e CDK** (20.1.4)
- **Leaflet** (1.9.4)
- **ngx-toastr** (19.0.0)
- **RxJS** (7.8.0)

### Ferramentas Auxiliares
- **Express** (5.1.0) para suporte ao SSR (Server-Side Rendering)

## 🎨 Interface e Design

- **FluentUI**: Utilizado para alinhar a interface com o padrão visual das ferramentas da Microsoft, buscando maior clareza, elegância e profissionalismo na interface.

## 📚 Estrutura e Organização

O projeto segue o padrão recomendado pelo Angular, com componentes standalone e organização clara por feature:

```
src/
├── app/
│   ├── vehicles/
│   │   ├── vehicle-list/
│   │   ├── vehicle-create/
│   │   ├── vehicle-update/
│   │   └── vehicle-form/
│   ├── app.routes.ts
│   └── app.component.ts
├── custom-theme.scss
└── main.ts
└── styles.css
└── index.html
```

## 🔄 Padrões e Arquitetura

- **Componentização**: Uso extensivo de componentes standalone, promovendo modularidade e reuso.
- **Service Layer**: Centralização das requisições HTTP utilizando Angular Services.
- **Routing**: Implementado com Angular Router para navegação simplificada entre as views.

## ⚙️ Como Executar o Projeto

### Pré-requisitos

- [Node.js](https://nodejs.org) (recomendado v20 ou superior)
- [Angular CLI](https://angular.io/cli)

### Passos para execução

1. Clone o repositório e acesse a pasta do projeto:

```bash
git clone https://github.com/MathSzoke/Desafio-Inlog---Vaga-FullStack-Developer-Angular.git
cd front-end/inlog-frontend
```

2. Instale as dependências do projeto:

```bash
npm install
```

3. Execute o servidor de desenvolvimento:

```bash
npm start
```

Acesse a aplicação através da URL:
```
http://localhost:4200
```

## 🚧 Build do Projeto

Para realizar o build do projeto para produção:

```bash
npm run build
```

Os arquivos buildados estarão localizados em:
```
dist/inlog-frontend
```

## 🌍 SSR (Server-Side Rendering)

Para utilizar SSR:

```bash
npm run build:ssr
npm run serve:ssr:inlog-frontend
```

## 🧪 Execução dos Testes

Para executar os testes:

```bash
npm run test
```

## 📖 Rotas da Aplicação

- `/vehicles`: Lista todos os veículos cadastrados.
- `/vehicles/create`: Formulário para cadastro de novos veículos.

## 🌟 FluentUI

Utilizado para implementar componentes visuais, como botões, inputs e dropdowns, proporcionando uma experiência mais agradável, similar aos aplicativos Microsoft.

## 📝 Observações Finais

Esta documentação permite rápida assimilação do projeto frontend, possibilitando que desenvolvedores possam executar, testar e expandir o projeto com facilidade, mantendo o padrão visual agradável proporcionado pelo FluentUI.

*OBS: Infelizmente a utilização do FluentUI para esse projeto foi de fato um experimento, da qual não obtive grandes sucessos, pois depois de ter tentado utilizar muito da ferramenta do FluentUI,
me dei conta de que o FluentUI possui mais suporte á React do que ao Angular... Mas podemos ter uma breve ideia de como é a UI da biblioteca FluentUI da Microsoft.*
