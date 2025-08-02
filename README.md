## Rodando o projeto

Basta executar na **raiz do projeto** (exatamente onde se encontra o arquivo docker-compose.yml), os comandos abaixo:

```shell
docker-compose build --no-cache
docker-compose up
```
> [!IMPORTANT]
> É necessário ter o Docker e o Docker Compose instalados.
> 
> ## No Linux:
> ```shell
> sudo apt-get install docker-compose-plugin
> ```
> 
> ## No Windows:
> Basta instalar o [Docker desktop](https://www.docker.com/products/docker-desktop/)
>
> ou, instalar via Chocolatey:
> ```shell
> choco install docker-desktop
> ```

Após a compilação com sucesso via docker, você deve conseguir visualizar a tela a partir da URL [localhost](http://localhost:4200).

<hr/>

# Desafio Inlog - Vaga FullStack Developer 🚀

Objetivo Geral:

O objetivo deste desafio é avaliar e demonstrar suas habilidades no desenvolvimento de back-end, seguido pela integração e desenvolvimento do front-end, utilizando as APIs que você criou na primeira etapa.

# Back - End

## Introdução 📜
O objetivo é completar a aplicação WebApi presente na pasta back-end.

## Instruções 📝
A solução esta em dotnet 6.0 e pode ser utilizado a IDE de sua preferência mas
deve ser feito como o dotnet 6.0 ou superior.

Dentro da Solução tem algumas sugestões de estrutura de projeto, para
que possa colocar as classes e interfaces necessárias.

## Iniciando o Projeto 🚀
Dentro da pasta Controllers tem uma controller de veículos 
com dois métdos para servir de ponto de partida, um POST e 
um GET, não é necessário fazer os demais endpoints.

As classes para representar o veiculo já existem.

Poderá ser feito um armazenamento de dados em memória ou com 
um banco de dados que não precisar ser disponibilizado na entrega
(por meio de docker-compose por exemplo).

Deve ser feito pelo menos um teste unitário para qualquer camada.

#### Observações:🌟


## Dicas 💡
Fique a vontade para adicionar nugets para ajudar no desenvolvimento.

# Front - End

## Introdução 📜
O objetivo deste desafio é criar um projeto Angular 12+ que contenha duas páginas: uma para listar veículos e outra para cadastrar novos veículos. 

Na página de listagem de veículos, será necessário utilizar a biblioteca do mapa, como o Leaflet ou Google Maps API, para exibir um mapa com a localização de cada veículo da lista.

Além disso, a lista de veículos deve estar ordenada pela localização mais próxima do usuário. Na página de cadastro de veículos, será necessário criar um formulário para inserir as informações básicas e a localização do veículo. 

O design do projeto fica à escolha do desenvolvedor do teste. Use sua criatividade e mostre suas habilidades em Angular, API e testes automatizados neste desafio!

## Instruções 📝
1. Crie 2 páginas em Angular: Uma para listagem de veículos e outra página de cadastro de veículos. 🚗📝

2. Na página de listagem de veículos, utilize a biblioteca do mapa, como o Leaflet ou Google Maps API para exibir um mapa e colocar um pin em cada localização de veículos na lista. 🗺️
    - A tela de listagem deve conter uma lista de veículos deve estar ordenada pela localização mais próxima do usuário (web).
    - Além da listagem, deve conter um mapa com as informações solicitadas.

3. Na página de cadastro de veículos, crie um formulário que permita ao usuário inserir as informações básicas e a localização do veículo.
   - Exemplo:
```json
{
    identifier: 'Vehicle 1',
    license_plate: 'AAA-9A99',
    tracker_serial_number: 'A0000000',
    coordinates: {
        latitude: -25.43247,
        longitude: -49.27845
    } 
}
```

4. Crie teste usando sua biblioteca favorita para garantir que:
   - A listagem de veículos seja renderizada corretamente com o mapa. 🧭
   - O formulário de cadastro de veículos esteja funcionando perfeitamente. ✅

#### Observações:🌟
- Você pode utilizar bibliotecas externas para ajudar no desenvolvimento do projeto, tais como:
    - Validadores,
    - Componentes,
    - Rotas.

- Caso deseje adicionar mais campos para o veículo como uma imagem entre outros, fique à vontade. Isso será visto como bônus. 🏎️💻

## Dicas 💡

- Se quiser adicionar algum bônus, como uma busca de veículos ou um filtro de veículos, fique à vontade.

---

## Como entregar 📨

- Crie um fork deste repositório e desenvolva nele.
- Após finalizar, enviar para o email beinlog@inlog.com.br o link do repositorio do github com seu projeto, além de seus dados de contato.

## Boa sorte!

