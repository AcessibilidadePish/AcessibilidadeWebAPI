# 🔧 Instalação Manual dos Git Hooks

## 📋 **Instruções de Configuração**

### **Passo 1: Configurar Hook Pre-Push**

Execute os comandos abaixo no **PowerShell** (na raiz do projeto):

```powershell
# 1. Criar diretório de hooks (se não existir)
if (-not (Test-Path ".git/hooks")) { 
    New-Item -ItemType Directory -Path ".git/hooks" -Force 
}

# 2. Configurar hook principal
@"
#!/bin/bash
powershell.exe -ExecutionPolicy Bypass -File ".git/hooks/pre-push.ps1"
"@ | Out-File -FilePath ".git/hooks/pre-push" -Encoding UTF8

# 3. Verificar se foi criado
Get-Content ".git/hooks/pre-push"
```

### **Passo 2: Testar a Configuração**

```powershell
# Testar hook manualmente
powershell .git/hooks/pre-push.ps1

# Ou testar build e testes diretamente
dotnet build AcessibilidadeWebAPI/AcessibilidadeWebAPI.csproj
dotnet test AcessibilidadeWebAPI.Tests
```

---

## 🎯 **Verificação Rápida**

Para verificar se os hooks estão funcionando:

### **1. Verificar Arquivos Existem**
```powershell
# Devem existir estes arquivos:
Test-Path ".git/hooks/pre-push"        # ✅ Deve retornar True
Test-Path ".git/hooks/pre-push.ps1"    # ✅ Deve retornar True
```

### **2. Testar Build**
```powershell
# Deve compilar sem erros
dotnet build --verbosity minimal
```

### **3. Testar Testes DataConclusao**
```powershell
# Deve passar todos os testes
dotnet test --filter "DataConclusao" --verbosity minimal
```

---

## 🚀 **Como Vai Funcionar**

### **Comportamento Esperado:**

1. **Commit normal**: `git commit -m "mensagem"`
2. **Push**: `git push origin main`  
3. **Hook automático executa**:
   - 📦 Build do projeto
   - 🧪 Build dos testes
   - 🔬 Execução dos testes
   - 🎯 Validação DataConclusao
4. **Push autorizado** se tudo OK ✅

### **Se algo falhar:**
```
❌ ERRO: Build/Testes falharam!
❌ Push cancelado.
```

---

## 📂 **Estrutura Final**

```
AcessibilidadeWebAPI/
├── .git/hooks/
│   ├── pre-push          # ✅ Hook principal
│   └── pre-push.ps1      # ✅ Implementação PowerShell
├── setup-git-hooks.ps1   # ✅ Script de configuração
├── GIT_HOOKS_README.md   # ✅ Documentação completa
└── INSTALACAO_GIT_HOOKS.md # ✅ Este guia
```

---

## ⚠️ **Troubleshooting**

### **Problema: Hook não executa**
```powershell
# Solução 1: Verificar configuração do Git
git config core.hooksPath .git/hooks

# Solução 2: Testar manualmente
powershell .git/hooks/pre-push.ps1
```

### **Problema: PowerShell não executa**
```powershell
# Solução: Ajustar política de execução
Set-ExecutionPolicy -ExecutionPolicy RemoteSigned -Scope CurrentUser
```

### **Problema: Erro de permissão**
```powershell
# Solução: Executar como administrador ou ajustar permissões
Get-ExecutionPolicy -List
```

---

## 🎉 **Confirmação de Sucesso**

Se tudo estiver configurado corretamente, você verá esta mensagem ao fazer push:

```
🪟 Executando pre-push validation...
🔍 Git Pre-Push Hook - AcessibilidadeWebAPI
📦 Executando build do projeto...
✅ Build do projeto principal: SUCESSO
🧪 Executando build do projeto de testes...
✅ Build do projeto de testes: SUCESSO
🔬 Executando testes...
✅ Todos os testes: SUCESSO
🎯 Validando correção DataConclusao...
✅ Correção DataConclusao: VALIDADA
🎉 PRE-PUSH VALIDATION: SUCESSO TOTAL!
🚀 Push autorizado! Código está pronto para produção.
```

---

## 🏆 **Benefícios Ativados**

Com os hooks configurados, o repositório agora tem:

- ✅ **Proteção automática** contra código quebrado
- ✅ **Validação da correção DataConclusao** a cada push
- ✅ **Build e testes obrigatórios** antes do push
- ✅ **Qualidade de código garantida**

**Só código 100% funcional será aceito no repositório!** 🚀✨ 