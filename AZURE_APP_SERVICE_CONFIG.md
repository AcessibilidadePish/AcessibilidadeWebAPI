# 🚀 Configuração Azure App Service para AcessibilidadeWebAPI

## ✅ **Configurações Necessárias no Azure Portal**

### **1. Configurações da Aplicação (Application Settings)**

No Azure Portal, navegue para:
**App Service** → **Configuration** → **Application settings**

Adicione as seguintes configurações:

#### **🔧 Configurações Básicas**

| Nome | Valor | Descrição |
|------|-------|-----------|
| `ASPNETCORE_ENVIRONMENT` | `Production` | Define o ambiente como produção |
| `WEBSITE_RUN_FROM_PACKAGE` | `1` | Otimiza execução a partir do pacote |
| `DOTNET_ROOT` | `/home/site/wwwroot` | Caminho do .NET no App Service Linux |

#### **🔐 Configurações JWT**

| Nome | Valor | 
|------|-------|
| `JwtSettings__SecretKey` | `MinhaChaveSecretaSuperSeguraParaJWT123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz` |
| `JwtSettings__Issuer` | `AcessibilidadeWebAPI` |
| `JwtSettings__Audience` | `AcessibilidadeWebAPI-Users` |
| `JwtSettings__ExpiryInHours` | `720` |

### **2. Connection String**

Em **Configuration** → **Connection strings**, adicione:

- **Nome**: `DefaultConnection`
- **Valor**: `Server=tcp:pishadm.database.windows.net,1433;Initial Catalog=bancopish_dev;Persist Security Info=False;User ID=adminpish;Password=asdQWE123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;`
- **Tipo**: `SQLAzure`

### **3. Configurações Gerais (General Settings)**

Em **Configuration** → **General settings**:

- **Stack**: `.NET`
- **Major version**: `.NET 8 (LTS)`
- **Minor version**: `.NET 8`
- **Platform**: `64 Bit`
- **Always On**: `On` (se disponível no seu plano)
- **ARR affinity**: `On`
- **HTTP version**: `2.0`
- **HTTPS Only**: `On`

### **4. Comando de Inicialização (Startup Command)**

⚠️ **IMPORTANTE**: Para App Service Linux com .NET 8, **NÃO configure** um startup command manual. O Azure detecta automaticamente a aplicação .NET.

Se precisar configurar manualmente (geralmente não necessário):
- **Startup Command**: `dotnet AcessibilidadeWebAPI.dll`

---

## 🔍 **Verificação e Diagnóstico**

### **1. Log Stream**

Acesse **App Service** → **Log stream** para ver os logs em tempo real.

Você deve ver algo como:
```
Info: Microsoft.Hosting.Lifetime[14]
      Now listening on: http://[::]:8080
Info: Microsoft.Hosting.Lifetime[0]
      Application started. Press Ctrl+C to shut down.
```

### **2. Diagnose and Solve Problems**

Use a ferramenta de diagnóstico integrada:
**App Service** → **Diagnose and solve problems**

Verifique especialmente:
- **Availability and Performance**
- **Configuration and Management**

### **3. Verificar Aplicação**

Acesse os seguintes endpoints:

- **Swagger UI**: `https://as-acessibilidadewebapi.azurewebsites.net/`
- **Health Check**: `https://as-acessibilidadewebapi.azurewebsites.net/api/teste`
- **API Base**: `https://as-acessibilidadewebapi.azurewebsites.net/api`

---

## 🐛 **Troubleshooting**

### **Problema: "Expected to find only one file with extension '.runtimeconfig.json'"**

✅ **Solução**: Já corrigido no workflow - publica apenas o projeto principal.

### **Problema: "startup-command is not a valid input"**

✅ **Solução**: Removido do workflow - Azure detecta automaticamente.

### **Problema: API não inicia**

Verificar:
1. **Connection String** está correta
2. **JWT Key** está configurada
3. **Logs** para erros específicos

### **Problema: Erro 500**

1. Ative logs detalhados:
   ```
   ASPNETCORE_DETAILEDERRORS = true
   ASPNETCORE_ENVIRONMENT = Development
   ```
2. Verifique Log Stream
3. Corrija o problema
4. Volte para `Production`

---

## 📋 **Checklist de Deploy**

- [x] **GitHub Actions** configurado corretamente
- [x] **Publish Profile** salvo como secret `AZURE_WEBAPP_PUBLISH_PROFILE`
- [ ] **Application Settings** configuradas no Azure
- [ ] **Connection String** configurada no Azure
- [ ] **JWT Settings** configuradas no Azure
- [ ] **SSL/HTTPS** habilitado
- [ ] **Always On** habilitado (se disponível)
- [ ] **Swagger** acessível
- [ ] **API** respondendo corretamente

---

## 🎯 **Resultado Final**

Com essas configurações, sua API estará:

✅ **Rodando corretamente** no Azure App Service Linux  
✅ **Sem warnings** de múltiplos arquivos .runtimeconfig.json  
✅ **Com detecção automática** do .NET 8  
✅ **Swagger acessível** na raiz  
✅ **Pronta para produção** 🚀

---

*Configuração completa para Azure App Service Linux com .NET 8* 🇧🇷 