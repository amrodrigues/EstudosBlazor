# 🚀 MinhaPrimeiraApp - Blazor Web App

Este projeto foi criado como um Blazor Web App usando o template do .NET 8, configurado para suportar interatividade nos modos **Server** e **WebAssembly (WASM)**.

---

## 🛠️ Tecnologias Utilizadas

* **Framework:** ASP.NET Core
* **Modelo de Aplicação:** Blazor Web App (.NET 8+)
* **Linguagem:** C#
* **Interatividade:**
    * **Blazor Server:** Para interatividade baseada em conexão SignalR (renderização inicial rápida).
    * **Blazor WebAssembly:** Para interatividade rodando diretamente no navegador do cliente (modo de produção preferencial para Auto).

---

## ⚙️ Configuração Essencial (`Program.cs`)

Para garantir que o modo de renderização `InteractiveAuto` funcione corretamente, o arquivo `Program.cs` deve conter as seguintes duas etapas de configuração:

### 1. Registro de Serviços (Services)

A aplicação deve registrar os serviços para ambos os modelos interativos:

```csharp
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents(); // ESSENCIAL
```

### 2. Mapeamento de Endpoints (Endpoints)

A aplicação deve mapear os endpoints e configurar os modos de renderização no pipeline HTTP:

```csharp
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode(); // ESSENCIAL
```

##  📚 Estrutura do Projeto
* Program.cs: O ponto de entrada da aplicação onde são configurados os serviços e o pipeline de requisição.

* Components/: Contém os componentes Razor, layouts e a estrutura principal do Blazor.

* MinhaPRimeiraApp.Client/: (Se for uma solução com múltiplos projetos) O projeto dedicado que contém o código a ser compilado para WebAssembly.
