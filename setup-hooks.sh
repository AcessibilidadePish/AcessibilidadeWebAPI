#!/bin/bash
# Configurar Git Hooks automaticamente - AcessibilidadeWebAPI
# Este script é executado automaticamente pelo MSBuild

echo "🔧 Configurando Git Hooks automaticamente..."

# Verificar se .git existe
if [ ! -d ".git" ]; then
    echo "ℹ️  Diretório .git não encontrado - pulando configuração de hooks"
    exit 0
fi

# Verificar se hooks já estão configurados
if [ -f ".git/hooks/pre-push.ps1" ]; then
    echo "✅ Git hooks já estão configurados"
    exit 0
fi

echo "📋 Instalando hooks pela primeira vez..."

# Criar diretório de hooks se não existir
mkdir -p ".git/hooks"

# Copiar hooks da pasta versionada
if [ -f "hooks/pre-push" ]; then
    cp "hooks/pre-push" ".git/hooks/pre-push"
    chmod +x ".git/hooks/pre-push"
    echo "✅ Hook pre-push instalado"
fi

if [ -f "hooks/pre-push.ps1" ]; then
    cp "hooks/pre-push.ps1" ".git/hooks/pre-push.ps1"
    echo "✅ Hook PowerShell instalado"
fi

echo "🎉 Git hooks configurados com sucesso!"
echo "🛡️  Seu repositório agora está protegido contra pushes com problemas!"

exit 0 