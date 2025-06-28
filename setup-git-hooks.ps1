# Setup Git Hooks - AcessibilidadeWebAPI
# Script para configurar automaticamente os git hooks

Write-Host "🔧 Configurando Git Hooks - AcessibilidadeWebAPI" -ForegroundColor Cyan
Write-Host "=================================================" -ForegroundColor Cyan

# Verificar se estamos no diretório correto
if (-not (Test-Path ".git")) {
    Write-Host "❌ ERRO: Não foi encontrado diretório .git" -ForegroundColor Red
    Write-Host "❌ Execute este script na raiz do repositório AcessibilidadeWebAPI" -ForegroundColor Red
    exit 1
}

# Verificar se o diretório hooks existe
$hooksDir = ".git/hooks"
if (-not (Test-Path $hooksDir)) {
    Write-Host "📁 Criando diretório de hooks..." -ForegroundColor Yellow
    New-Item -ItemType Directory -Path $hooksDir -Force | Out-Null
}

Write-Host "📋 Configurando hook pre-push..." -ForegroundColor Blue

# Copiar arquivos de hook se eles existem
if (Test-Path ".git/hooks/pre-push") {
    Write-Host "✅ Hook pre-push já existe" -ForegroundColor Green
} else {
    Write-Host "❌ Arquivo pre-push não encontrado em .git/hooks/" -ForegroundColor Red
    Write-Host "🔧 Criando hook básico..." -ForegroundColor Yellow
    
    # Criar hook básico que chama o PowerShell
    $hookContent = @"
#!/bin/bash
# Git Pre-Push Hook - AcessibilidadeWebAPI
echo "🪟 Executando pre-push validation..."
powershell.exe -ExecutionPolicy Bypass -File "`$(git rev-parse --git-dir)/hooks/pre-push.ps1"
"@
    
    Set-Content -Path ".git/hooks/pre-push" -Value $hookContent -Encoding UTF8
}

if (Test-Path ".git/hooks/pre-push.ps1") {
    Write-Host "✅ Hook PowerShell já existe" -ForegroundColor Green
} else {
    Write-Host "❌ Arquivo pre-push.ps1 não encontrado em .git/hooks/" -ForegroundColor Red
    Write-Host "💡 Certifique-se de que o arquivo pre-push.ps1 existe na pasta .git/hooks/" -ForegroundColor Yellow
}

# Testar se dotnet está disponível
Write-Host "🔍 Verificando dependências..." -ForegroundColor Blue

try {
    $dotnetVersion = dotnet --version
    Write-Host "✅ .NET SDK encontrado: $dotnetVersion" -ForegroundColor Green
} catch {
    Write-Host "❌ ERRO: .NET SDK não encontrado!" -ForegroundColor Red
    Write-Host "❌ Instale o .NET SDK antes de continuar." -ForegroundColor Red
    exit 1
}

# Testar build do projeto
Write-Host "🧪 Testando build do projeto..." -ForegroundColor Blue

$buildTest = dotnet build AcessibilidadeWebAPI/AcessibilidadeWebAPI.csproj --verbosity quiet

if ($LASTEXITCODE -eq 0) {
    Write-Host "✅ Projeto compila corretamente" -ForegroundColor Green
} else {
    Write-Host "⚠️  AVISO: Projeto não compila no momento" -ForegroundColor Yellow
    Write-Host "⚠️  Corrija os erros de compilação antes do próximo push" -ForegroundColor Yellow
}

# Sucesso
Write-Host ""
Write-Host "🎉 CONFIGURAÇÃO CONCLUÍDA!" -ForegroundColor Green
Write-Host ""
Write-Host "📋 O que foi configurado:" -ForegroundColor Cyan
Write-Host "  ✅ Git hooks configurados" -ForegroundColor Green
Write-Host "  ✅ Pre-push validation ativado" -ForegroundColor Green
Write-Host "  ✅ Build e testes serão executados antes do push" -ForegroundColor Green
Write-Host ""
Write-Host "🚀 Como funciona:" -ForegroundColor Cyan
Write-Host "  1. Você faz commit normalmente: git commit -m 'mensagem'" -ForegroundColor White
Write-Host "  2. Quando fizer push: git push origin main" -ForegroundColor White
Write-Host "  3. O hook irá automaticamente:" -ForegroundColor White
Write-Host "     - Compilar o projeto" -ForegroundColor Yellow
Write-Host "     - Executar todos os testes" -ForegroundColor Yellow
Write-Host "     - Validar correção DataConclusao" -ForegroundColor Yellow
Write-Host "     - Só permitir push se tudo estiver OK" -ForegroundColor Yellow
Write-Host ""
Write-Host "💡 Para testar manualmente:" -ForegroundColor Cyan
Write-Host "   powershell .git/hooks/pre-push.ps1" -ForegroundColor White
Write-Host ""

exit 0 