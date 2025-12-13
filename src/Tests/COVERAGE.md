# Testes de Cobertura - StackFood

Este guia explica como executar os testes com cobertura de código usando o Coverlet.

## 🔧 Ferramentas Configuradas

- **Coverlet**: Ferramenta de cobertura de código para .NET
- **SonarCloud**: Análise de qualidade de código e cobertura

## 🚀 Executar Testes Localmente

### Opção 1: Usando Coverlet MSBuild (Recomendado)

```bash
dotnet test /p:CollectCoverage=true /p:CoverletOutputFormat=opencover /p:CoverletOutput=./coverage/
```

### Opção 2: Usando Coverlet Collector

```bash
dotnet test --collect:"XPlat Code Coverage" --results-directory ./TestResults/
```

### Opção 3: Com configuração customizada

```bash
dotnet test --settings src/Tests/StackFood.Tests/coverlet.runsettings
```

## 📊 Formatos de Saída

O Coverlet suporta múltiplos formatos:

- `opencover` - Formato usado pelo SonarCloud
- `lcov` - Formato usado por muitas ferramentas de CI/CD
- `cobertura` - Formato XML Cobertura
- `json` - Formato JSON
- `teamcity` - Formato TeamCity

### Gerar múltiplos formatos:

```bash
dotnet test /p:CollectCoverage=true /p:CoverletOutputFormat=\"opencover,lcov,json\" /p:CoverletOutput=./coverage/
```

## 🔍 Visualizar Relatório Localmente

### Instalar ReportGenerator

```bash
dotnet tool install -g dotnet-reportgenerator-globaltool
```

### Gerar relatório HTML

```bash
# Executar testes e gerar cobertura
dotnet test /p:CollectCoverage=true /p:CoverletOutputFormat=opencover /p:CoverletOutput=./coverage/

# Gerar relatório HTML
reportgenerator -reports:"**/coverage.opencover.xml" -targetdir:"coveragereport" -reporttypes:Html

# Abrir o relatório (Linux/WSL)
xdg-open coveragereport/index.html
```

## 🎯 Configuração do Projeto

### Pacotes NuGet Instalados

```xml
<PackageReference Include="coverlet.collector" Version="6.0.2" />
<PackageReference Include="coverlet.msbuild" Version="6.0.2">
  <PrivateAssets>all</PrivateAssets>
  <IncludeAssets>runtime; build; native; contentfiles; analyzers; buildtransitive</IncludeAssets>
</PackageReference>
```

### Exclusões Configuradas

O arquivo `coverlet.runsettings` exclui:

- Projetos de teste (`[*Tests]*`)
- Classes Program e Startup
- Código gerado automaticamente
- Código obsoleto

## 🔄 CI/CD - GitHub Actions

O workflow `.github/workflows/sonar.yml` executa automaticamente:

1. ✅ Build do projeto
2. ✅ Execução dos testes com cobertura
3. ✅ Envio dos relatórios para o SonarCloud
4. ✅ Comentários automáticos nas Pull Requests

### Triggers Configurados

- **Push**: branches `main`, `develop`, `master`
- **Pull Request**: branches `main`, `develop`, `master`
- **Manual**: via `workflow_dispatch`

## 📈 Métricas no SonarCloud

Após cada execução, você verá:

- **Coverage**: Percentual de código coberto por testes
- **Quality Gate**: Passou/Falhou
- **Bugs**: Bugs detectados
- **Vulnerabilities**: Vulnerabilidades de segurança
- **Code Smells**: Problemas de qualidade de código
- **Duplications**: Código duplicado

## 🔐 Secrets Necessários

Configure no GitHub (Settings → Secrets and variables → Actions):

- `SONAR_TOKEN`: Token de autenticação do SonarCloud
- `SONAR_PROJECT_KEY`: Chave do projeto no SonarCloud

## 📖 Referências

- [Coverlet Documentation](https://github.com/coverlet-coverage/coverlet)
- [SonarCloud .NET Coverage](https://docs.sonarsource.com/sonarqube-cloud/enriching/test-coverage/dotnet-test-coverage/)
- [ReportGenerator](https://github.com/danielpalme/ReportGenerator)
