# 🚀 Git Hooks Automáticos - AcessibilidadeWebAPI

## 📋 **Configuração Automática Integrada**

Este repositório possui **configuração automática de Git Hooks** integrada ao processo de build! Qualquer pessoa que clonar o repositório e compilar pela primeira vez terá os hooks configurados automaticamente.

---

## ⚡ **Como Funciona**

### **🎯 Zero Configuração Manual!**

1. **Clone o repositório**:
   ```bash
   git clone <url-do-repositorio>
   cd AcessibilidadeWebAPI
   ```

2. **Compile o projeto** (primeira vez):
   ```bash
   dotnet build
   ```

3. **✅ PRONTO!** Os hooks são configurados automaticamente durante o build!

### **🔧 Integração MSBuild**

No arquivo `AcessibilidadeWebAPI.csproj`:

```xml
<!-- 🔧 Configuração automática de Git Hooks -->
<Target Name="SetupGitHooks" BeforeTargets="PreBuildEvent" Condition="'$(Configuration)' == 'Debug'">
  <Message Text="🔧 Verificando configuração dos Git Hooks..." Importance="high" />
  
  <!-- Windows -->
  <Exec Command="setup-hooks.bat" 
        Condition="'$(OS)' == 'Windows_NT'" 
        ContinueOnError="true" 
        WorkingDirectory="$(ProjectDir)..\" />
  
  <!-- Linux/Mac -->
  <Exec Command="chmod +x setup-hooks.sh &amp;&amp; ./setup-hooks.sh" 
        Condition="'$(OS)' != 'Windows_NT'" 
        ContinueOnError="true" 
        WorkingDirectory="$(ProjectDir)..\" />
</Target>
```

### **📂 Estrutura dos Arquivos**

```
AcessibilidadeWebAPI/
├── hooks/                    # 📁 Hooks versionados
│   ├── pre-push             # Hook universal (bash)
│   └── pre-push.ps1         # Hook PowerShell (Windows)
├── setup-hooks.bat          # 🪟 Script Windows
├── setup-hooks.sh           # 🐧 Script Linux/Mac
└── AcessibilidadeWebAPI/
    └── AcessibilidadeWebAPI.csproj  # 🔧 Target MSBuild
```

---

## 🎪 **Comportamento Detalhado**

### **Durante o Build (Debug)**

```
dotnet build
🔧 Verificando configuração dos Git Hooks...
🔧 Configurando Git Hooks automaticamente...
📋 Instalando hooks pela primeira vez...
✅ Hook pre-push instalado
✅ Hook PowerShell instalado
🎉 Git hooks configurados com sucesso!
🛡️ Seu repositório agora está protegido contra pushes com problemas!
```

### **Builds Subsequentes**

```
dotnet build
🔧 Verificando configuração dos Git Hooks...
✅ Git hooks já estão configurados
```

### **Durante o Push (Automático)**

```
git push origin main
🔍 Git Pre-Push Hook - AcessibilidadeWebAPI
📦 Build do projeto... ✅
🧪 Build dos testes... ✅
🔬 Executando testes... ✅
🎯 Validando DataConclusao... ✅
🎉 PUSH AUTORIZADO! 🚀
```

---

## ⚙️ **Configurações e Condições**

### **Quando os Hooks São Instalados**

- ✅ **Primeira compilação** em modo Debug
- ✅ **Se o diretório `.git` existir**
- ✅ **Se os hooks ainda não estiverem configurados**

### **Quando NÃO São Instalados**

- ❌ **Builds em Release** (para evitar interferência em CI/CD)
- ❌ **Se não for um repositório Git**
- ❌ **Se os hooks já estiverem configurados**

### **Plataformas Suportadas**

- 🪟 **Windows**: Usa `setup-hooks.bat`
- 🐧 **Linux**: Usa `setup-hooks.sh`
- 🍎 **macOS**: Usa `setup-hooks.sh`

---

## 🎯 **Validações Automáticas**

Os hooks configurados executam **automaticamente** a cada push:

### **1. Build do Projeto Principal** 📦
```bash
dotnet build AcessibilidadeWebAPI/AcessibilidadeWebAPI.csproj --configuration Release
```

### **2. Build do Projeto de Testes** 🧪
```bash
dotnet build AcessibilidadeWebAPI.Tests/AcessibilidadeWebAPI.Tests.csproj --configuration Release
```

### **3. Execução de Todos os Testes** 🔬
```bash
dotnet test AcessibilidadeWebAPI.Tests --configuration Release
```

### **4. Validação Específica DataConclusao** 🎯
```bash
dotnet test --filter "DataConclusao" --configuration Release
```

---

## 🛡️ **Proteção Garantida**

### **❌ Pushes Bloqueados Se:**
- Código não compila
- Testes não compilam
- Qualquer teste falha
- Correção DataConclusao tem problemas

### **✅ Pushes Permitidos Apenas Se:**
- Build passa 100%
- Todos os testes passam
- Correção DataConclusao validada
- Código está pronto para produção

---

## 🔧 **Comandos Úteis**

### **Forçar Reconfiguração**
```bash
# Deletar hooks existentes
rm -rf .git/hooks/pre-push*

# Recompilar (vai reinstalar automaticamente)
dotnet build
```

### **Testar Hooks Manualmente**
```bash
# Windows
powershell .git/hooks/pre-push.ps1

# Linux/Mac
.git/hooks/pre-push
```

### **Bypass de Emergência**
```bash
# CUIDADO: Pula todas as validações!
git push --no-verify origin main
```

### **Ver Status dos Hooks**
```bash
# Verificar se existem
ls -la .git/hooks/pre-push*

# Ver conteúdo
cat .git/hooks/pre-push.ps1
```

---

## 🎉 **Benefícios da Integração MSBuild**

### **Para Novos Desenvolvedores** 👨‍💻
- ✅ **Zero configuração manual** necessária
- ✅ **Hooks instalados automaticamente** no primeiro build
- ✅ **Experiência consistente** em qualquer máquina
- ✅ **Não precisa lembrar** de executar scripts

### **Para a Equipe** 👥
- ✅ **Garantia de qualidade** desde o primeiro commit
- ✅ **Padronização automática** do ambiente
- ✅ **Redução de erros** por configuração manual
- ✅ **CI/CD mais estável** (Release builds não executam hooks)

### **Para o Projeto** 🏗️
- ✅ **Proteção total** do repositório
- ✅ **Validação da correção DataConclusao** sempre ativa
- ✅ **Qualidade de código** garantida automaticamente
- ✅ **Manutenção simplificada** dos hooks

---

## 🏆 **Resultado Final**

Com esta configuração, **100% dos desenvolvedores** que trabalharem no projeto terão:

- 🛡️ **Proteção automática** contra pushes problemáticos
- 🎯 **Validação contínua** da correção DataConclusao
- 📦 **Build e testes obrigatórios** antes de cada push
- ✅ **Qualidade de código garantida** sem esforço manual

**O repositório se torna automaticamente à prova de erros!** 🚀✨

---

*Configuração automática implementada com MSBuild Targets por Claude Sonnet 4 🇧🇷* 