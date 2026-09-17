# SPEC — Console App de Feriados Nacionais (BrasilAPI)

## Objetivo
Aplicação de console em **.NET 10** que recebe um **ano** como entrada e retorna os **feriados nacionais** do Brasil para aquele ano, consumindo a API pública BrasilAPI:

- Documentação: https://brasilapi.com.br/docs#tag/Feriados-Nacionais/paths/~1feriados~1v1~1%7Bano%7D/get
- Endpoint: `GET https://brasilapi.com.br/api/feriados/v1/{ano}`

## Escopo
- Aplicação CLI (console) simples, sem interface gráfica.
- Não há persistência de dados; o app apenas consulta a API e exibe o resultado.
- Sem stack adicional definida além de .NET 10 (ver `CLAUDE.md`).
- O nome da CLI devera ser BrazilBankHolidays
- Todo e qualquer codigo devera ser em ingles. (Variaveis etc etc.)

## Entrada
- **Argumento de linha de comando**: ano com 4 dígitos (ex.: `dotnet run -- 2024`).
- Caso o argumento não seja informado, o programa deve solicitar o ano interativamente (prompt no console).
- Validação de entrada:
  - Deve ser um número inteiro.
  - Deve conter 4 dígitos (ano no formato `YYYY`).
  - A BrasilAPI aceita anos a partir de **1900** em diante; anos fora desse intervalo devem ser rejeitados antes da chamada à API, com mensagem de erro amigável.

## Integração com a API

### Requisição
```
GET https://brasilapi.com.br/api/feriados/v1/{ano}
```
- `{ano}`: inteiro, ano de referência para os feriados (parâmetro de path).

### Resposta de sucesso (HTTP 200)
Array JSON de feriados, cada item com a seguinte estrutura:

| Campo | Tipo   | Descrição                                              |
|-------|--------|---------------------------------------------------------|
| date  | string | Data do feriado no formato `YYYY-MM-DD`                 |
| name  | string | Nome do feriado (ex.: "Confraternização mundial")       |
| type  | string | Tipo do feriado (ex.: `"national"`)                     |

Exemplo:
```json
[
  {
    "date": "2024-01-01",
    "name": "Confraternização mundial",
    "type": "national"
  },
  {
    "date": "2024-04-21",
    "name": "Tiradentes",
    "type": "national"
  }
]
```

### Respostas de erro
- **HTTP 404**: ano inválido ou fora do intervalo suportado pela API. Corpo no formato:
```json
{
  "name": "string",
  "message": "string",
  "type": "string"
}
```
- **Erros de rede/timeout**: devem ser tratados e exibidos como mensagem de erro amigável no console, sem stack trace bruto.

## Comportamento esperado do CLI

1. Ler o ano (via argumento ou prompt interativo).
2. Validar o ano localmente (formato e intervalo mínimo).
3. Chamar a API `GET /api/feriados/v1/{ano}` via `HttpClient`.
4. Se sucesso:
   - Exibir a lista de feriados no console, ordenada por data, no formato:
     ```
     DD/MM/YYYY - Nome do feriado
     ```
   - Exibir também o total de feriados encontrados.
5. Se a API retornar 404 ou erro:
   - Exibir mensagem clara informando que não foi possível obter feriados para o ano informado.
6. Código de saída do processo:
   - `0` em caso de sucesso.
   - Diferente de `0` em caso de erro (validação, falha de rede, ano inválido).

## Requisitos técnicos
- **.NET 10**, projeto do tipo Console App.
- Uso de `HttpClient` (via `IHttpClientFactory` ou instância simples) para chamar a BrasilAPI.
- Desserialização do JSON com `System.Text.Json`.
- Tratamento de exceções de rede (`HttpRequestException`, timeout) sem expor detalhes técnicos ao usuário final.

## Critérios de aceite
- [ ] `dotnet run -- 2026` retorna a lista de feriados nacionais de 2026,
      formatada e legível.
- [ ] `dotnet run -- abc` exibe mensagem de erro de validação (não é número).
- [ ] `dotnet run` (sem argumento) exibe mensagem de uso.
- [ ] `dotnet run -- 1800` (ano fora do range suportado) trata o erro da API
      sem crashar, mostrando mensagem amigável.
- [ ] Nenhuma exception não tratada aparece no console em nenhum dos cenários
      acima.

## Fora de escopo
- Cache de resultados.
- Suporte a feriados estaduais/municipais (a API e este app tratam apenas feriados nacionais).
- Internacionalização (idioma fixo: português).
- Testes automatizados (não definidos nesta especificação inicial).
