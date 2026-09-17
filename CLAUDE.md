# CLAUDE.md

## Visão Geral
Projeto de estudos da Formação **AI Native Engineer**, aplicando a prática de **SDD (Spec Driven Development)**: as funcionalidades são especificadas primeiro em `docs/` (ex.: `docs/SPEC.md`) e só então implementadas, com as alterações solicitadas ao longo do processo registradas em `docs/notes.md`.

Implementação atual: CLI **BrazilBankHolidays** (.NET 10), em `src/BrazilBankHolidays`, que consulta feriados nacionais do Brasil via BrasilAPI. Ver `README.md` para detalhes de escopo e execução.

## Convenções
- Fluxo SDD: especificar em `docs/SPEC.md` antes de implementar; registrar alterações pós-implementação em `docs/notes.md`.
- Código-fonte (classes, métodos, modelos, variáveis) em **inglês**; mensagens exibidas ao usuário final no console em **português**.

## Comandos
```bash
cd src/BrazilBankHolidays
dotnet build
dotnet run -- <ano>
```
