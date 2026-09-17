# BrazilBankHolidays

CLI em **.NET 10** que recebe um ano e retorna os feriados nacionais do Brasil para aquele ano, consumindo a [BrasilAPI](https://brasilapi.com.br/docs#tag/Feriados-Nacionais/paths/~1feriados~1v1~1%7Bano%7D/get).

## Escopo
- Aplicação de console simples, sem interface gráfica e sem persistência de dados.
- Consulta apenas feriados **nacionais** (não trata feriados estaduais/municipais).
- Saída em português (idioma fixo); código-fonte (classes, métodos, modelos, variáveis) em inglês.
- Sem cache de resultados e sem testes automatizados nesta versão.

## Requisitos técnicos
- **.NET 10** (SDK `10.0.400` ou compatível).
- `HttpClient` para chamar a BrasilAPI.
- `System.Text.Json` para desserialização da resposta.
- Tratamento de exceções de rede (`HttpRequestException`, timeout, 404) sem expor stack trace ao usuário.

## Implementação
Projeto localizado em `src/BrazilBankHolidays`:

| Arquivo | Responsabilidade |
|---|---|
| `Program.cs` | Leitura do ano (argumento ou prompt interativo), validação local, formatação e exibição do resultado, código de saída do processo. |
| `BrazilBankHolidaysApiClient.cs` | Chamada HTTP `GET /api/feriados/v1/{ano}` na BrasilAPI e tratamento de erros (`BrazilBankHolidaysApiException`). |
| `Models/Holiday.cs` | Modelo de desserialização do JSON retornado pela API (`date`, `name`, `type`). |

### Como executar
```bash
cd src/BrazilBankHolidays
dotnet run -- 2026
```
Sem argumento, o programa solicita o ano interativamente. Código de saída `0` em sucesso, diferente de `0` em erro (validação, ano inválido, falha de rede).

## Documentação (`docs/`)
| Arquivo | Conteúdo |
|---|---|
| `docs/SPEC.md` | Especificação funcional e técnica original do CLI: entrada, contrato da API, comportamento esperado, critérios de aceite e escopo/fora de escopo. |
| `docs/notes.md` | Histórico das alterações solicitadas após a primeira versão implementada (tradução dos identificadores para inglês, renomeações do projeto até o nome final `BrazilBankHolidays`). |
