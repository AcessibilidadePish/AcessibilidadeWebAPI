# 🔧 Correções para Deploy no Azure App Service

## 🐛 **Problema Identificado**

O Azure App Service estava encontrando **2 arquivos `.runtimeconfig.json`**:
- `AcessibilidadeWebAPI.runtimeconfig.json` ✅ (correto)
- `AcessibilidadeWebAPI.Tests.runtimeconfig.json` ❌ (não deveria estar lá)

**Erro no log:**
```
WARNING: Expected to find only one file with extension '.runtimeconfig.json' but found 2
WARNING: Found files: 'AcessibilidadeWebAPI.runtimeconfig.json, AcessibilidadeWebAPI.Tests.runtimeconfig.json'
```

## ✅ **Correções Implementadas**

### **1. GitHub Actions Workflow Atualizado**

#### **📁 `.github/workflows/azure-webapps-dotnet-core.yml`**

**Mudanças principais:**

- ✅ **Especificação explícita do projeto** a ser publicado
- ✅ **Exclusão dos testes** do pacote de deploy
- ✅ **Adição de testes** no pipeline (mas sem incluí-los no deploy)
- ✅ **Configuração do startup command** explicitamente
- ✅ **Debug de arquivos publicados** para verificação

```yaml
# Publica APENAS o projeto principal
dotnet publish AcessibilidadeWebAPI/AcessibilidadeWebAPI.csproj \
  -c Release \
  -o published \
  --no-restore \
  --self-contained false \
  --runtime linux-x64
```

### **2. Arquivos de Configuração Criados**

#### **📁 `.deployment`**
```ini
[config]
project = AcessibilidadeWebAPI/AcessibilidadeWebAPI.csproj
```
- Indica ao Azure qual projeto deve ser usado

#### **📁 `web.config`**
```xml
<aspNetCore processPath="dotnet"
            arguments=".\AcessibilidadeWebAPI.dll"
            stdoutLogEnabled="false"
            stdoutLogFile=".\logs\stdout"
            hostingModel="inprocess" />
```
- Especifica explicitamente qual DLL executar

#### **📁 `appsettings.Production.json`**
- Configurações específicas para produção
- Connection strings para Azure SQL
- Configurações JWT para produção

### **3. Melhorias no Pipeline**

- ✅ **Variáveis de ambiente** centralizadas
- ✅ **Execução de testes** antes do deploy
- ✅ **Listagem de arquivos** para debug
- ✅ **Startup command** configurado: `dotnet AcessibilidadeWebAPI.dll`

---

## 🚀 **Próximos Passos**

### **1. Configurar App Settings no Azure Portal**

No Azure Portal, vá em **Configuration** → **Application settings** e adicione:

```json
{
  "name": "ASPNETCORE_ENVIRONMENT",
  "value": "Production"
}
```

### **2. Configurar Connection String**

Em **Configuration** → **Connection strings**, adicione sua string de conexão do Azure SQL Database.

### **3. Configurar JWT Key**

Em **Configuration** → **Application settings**, adicione:

```json
{
  "name": "Jwt__Key",
  "value": "SUA_CHAVE_SEGURA_COM_PELO_MENOS_32_CARACTERES"
}
```

### **4. Fazer um novo Deploy**

Depois de fazer commit dessas mudanças:

```bash
git add .
git commit -m "fix: corrigir deploy Azure App Service - remover projeto de testes"
git push origin master
```

O GitHub Actions vai executar automaticamente e fazer o deploy correto.

---

## 🔍 **Verificação**

Após o deploy, verifique:

1. **Logs do GitHub Actions** - Deve mostrar apenas 1 arquivo `.runtimeconfig.json`
2. **Log Stream no Azure** - Não deve mais mostrar o warning sobre múltiplos arquivos
3. **Swagger UI** - Acesse `https://as-acessibilidadewebapi.azurewebsites.net/swagger`

---

## 📊 **Resultado Esperado**

✅ **Deploy bem-sucedido** sem warnings  
✅ **API rodando corretamente** no Azure  
✅ **Apenas arquivos necessários** no App Service  
✅ **Startup correto** com a DLL especificada  

---

*Correções implementadas para resolver problema de múltiplos arquivos runtimeconfig.json no Azure App Service* 🇧🇷 