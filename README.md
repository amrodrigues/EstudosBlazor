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

 # Roteamento Parametrizado (Blazor Components)

Neste projeto, utilizamos **rotas parametrizadas** (ou *route templates*) para carregar componentes com base em dados específicos fornecidos na URL.

Essa abordagem é crucial para exibir a página de detalhes de um usuário, garantindo que o `Id` (Identificador) do usuário seja capturado diretamente da rota e seja do tipo correto.

## 1. Definição da Rota (`@page`)

A rota para a página de detalhes do usuário é definida no topo do componente `UserDetalhe.razor` através da diretiva `@page`.

### Estrutura da Rota:

```razor
@page "/users/{Id:int}"
```
Parte	Explicação
- "/users"	É o caminho base da URL.
- {Id:int}	Define um parâmetro de rota chamado Id.
- :int	É uma restrição de tipo. Ela informa ao sistema de roteamento do Blazor que o valor capturado para Id deve ser um número inteiro.

- ## Roteamento Parametrizado para Tratamento de Erros

O componente `Erro.razor` demonstra o uso de rotas parametrizadas para criar uma página dinâmica de exibição de erros, onde a mensagem exibida ao usuário é determinada pelo código de status HTTP passado na URL.

### Componente: `Erro.razor`

#### 1. Definição da Rota

A rota utiliza o parâmetro `{code}` com a restrição de tipo `:int` para garantir que apenas códigos numéricos válidos sejam processados.

```razor
@page "/erro/{code:int}"
```
#### 2. Parametrização
Temos que parametrizar o erro na Program.cs
```razor
app.UseStatusCodePagesWithRedirects("/erro/{0}");
```
#### 3. Estrutura da class erro (componente)
Estrutura do componemte de erro:

```
@code {
    string Message = string.Empty;

    [Parameter]
    public int code { get; set; }

    protected override void OnInitialized()
    {
        switch (code)
        {
            case 401:
                Message = "Não te conheço"; // Não Autorizado
                break;
            case 403:
                Message = "Te conheço mas você não tem acesso!"; // Proibido
                break;
            case 404:
                Message = "Página não encontrada"; // Not Found
                break;
            case 500:
                Message = "Erro no servidor"; // Erro Interno do Servidor
                break;
            default:
                Message = "Erro desconhecido";
                break;
        }
    }
}
```

## Restrições de Rotas Avançadas (Route Constraints)

Neste projeto, exploramos o uso de **restrições de tipo** avançadas para garantir que os parâmetros de rota correspondam ao tipo de dado esperado. Isso previne o `InvalidCastException` e melhora a segurança e a robustez da aplicação.

### 1. Rota com Múltiplas Restrições e Parâmetro Opcional

A rota `/restricao-rota1/{id:int}/{option:bool?}` ilustra o uso de duas restrições e um parâmetro opcional:

| Parâmetro | Restrição | Explicação | Exemplo de URL |
| :--- | :--- | :--- | :--- |
| `{id:int}` | `:int` | Obriga o valor a ser um número inteiro (`System.Int32`). | `/restricao-rota1/123` |
| `{option:bool?}` | `:bool` | Obriga o valor a ser um booleano (`true` ou `false`). O `?` torna o parâmetro **opcional** na URL. | `/restricao-rota1/123/true` |

### 2. Rota com GUID (Identificador Global Único)

A rota `/restricao-rota2/{uid:guid}` demonstra a restrição para identificadores globais:

| Parâmetro | Restrição | Explicação | Exemplo de URL |
| :--- | :--- | :--- | :--- |
| `{uid:guid}` | `:guid` | Obriga o valor a ser formatado como um GUID válido (`System.Guid`), como `xxxxxxxx-xxxx-...`. | `/restricao-rota2/a1b2c3d4-...` |

Essas restrições garantem que o Blazor Router só resolva a navegação se o formato dos dados na URL for compatível com o tipo de dado C# esperado na propriedade `[Parameter]`.
