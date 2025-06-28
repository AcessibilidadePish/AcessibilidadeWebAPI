@echo off
REM Configurar Git Hooks automaticamente - AcessibilidadeWebAPI
REM Este script é executado automaticamente pelo MSBuild

echo 🔧 Configurando Git Hooks automaticamente...

REM Verificar se .git existe
if not exist ".git" (
    echo ℹ️  Diretório .git não encontrado - pulando configuração de hooks
    exit /b 0
)

REM Verificar se hooks já estão configurados
if exist ".git\hooks\pre-push.ps1" (
    echo ✅ Git hooks já estão configurados
    exit /b 0
)

echo 📋 Instalando hooks pela primeira vez...

REM Criar diretório de hooks se não existir
if not exist ".git\hooks" mkdir ".git\hooks"

REM Copiar hooks da pasta versionada
if exist "hooks\pre-push" (
    copy "hooks\pre-push" ".git\hooks\pre-push" >nul 2>&1
    echo ✅ Hook pre-push instalado
)

if exist "hooks\pre-push.ps1" (
    copy "hooks\pre-push.ps1" ".git\hooks\pre-push.ps1" >nul 2>&1
    echo ✅ Hook PowerShell instalado
)

echo 🎉 Git hooks configurados com sucesso!
echo 🛡️  Seu repositório agora está protegido contra pushes com problemas!

exit /b 0 