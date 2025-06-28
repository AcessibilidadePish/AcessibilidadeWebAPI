# Git Pre-Push Hook para Windows (PowerShell)
# AcessibilidadeWebAPI

Write-Host "🔍 Git Pre-Push Hook - AcessibilidadeWebAPI" -ForegroundColor Cyan
Write-Host "==========================================" -ForegroundColor Cyan

# Função para exibir mensagens coloridas
function Write-ColorOutput($ForegroundColor) {
    $fc = $host.UI.RawUI.ForegroundColor
    $host.UI.RawUI.ForegroundColor = $ForegroundColor
    if ($args) {
        Write-Output $args
    }
    else {
        $input | Write-Output
    }
    $host.UI.RawUI.ForegroundColor = $fc
}

Write-Host "📦 Executando build do projeto..." -ForegroundColor Blue

# Executar build do projeto principal
Write-Host "Compilando projeto principal..." -ForegroundColor Yellow
$buildResult = dotnet build AcessibilidadeWebAPI/AcessibilidadeWebAPI.csproj --configuration Release --no-restore --verbosity minimal

if ($LASTEXITCODE -ne 0) {
    Write-Host "❌ ERRO: Build do projeto principal falhou!" -ForegroundColor Red
    Write-Host "❌ Push cancelado. Corrija os erros de compilação antes de continuar." -ForegroundColor Red
    exit 1
}

Write-Host "✅ Build do projeto principal: SUCESSO" -ForegroundColor Green

# Executar build do projeto de testes
Write-Host "🧪 Executando build do projeto de testes..." -ForegroundColor Blue

$testBuildResult = dotnet build AcessibilidadeWebAPI.Tests/AcessibilidadeWebAPI.Tests.csproj --configuration Release --no-restore --verbosity minimal

if ($LASTEXITCODE -ne 0) {
    Write-Host "❌ ERRO: Build do projeto de testes falhou!" -ForegroundColor Red
    Write-Host "❌ Push cancelado. Corrija os erros de compilação nos testes." -ForegroundColor Red
    exit 1
}

Write-Host "✅ Build do projeto de testes: SUCESSO" -ForegroundColor Green

# Executar testes
Write-Host "🔬 Executando testes..." -ForegroundColor Blue

$testResult = dotnet test AcessibilidadeWebAPI.Tests --configuration Release --no-build --verbosity minimal

if ($LASTEXITCODE -ne 0) {
    Write-Host "❌ ERRO: Alguns testes falharam!" -ForegroundColor Red
    Write-Host "❌ Push cancelado. Corrija os testes que estão falhando." -ForegroundColor Red
    Write-Host "💡 Dica: Execute 'dotnet test --verbosity normal' para ver detalhes dos erros" -ForegroundColor Yellow
    exit 1
}

Write-Host "✅ Todos os testes: SUCESSO" -ForegroundColor Green

# Validar especificamente os testes da correção DataConclusao
Write-Host "🎯 Validando correção DataConclusao..." -ForegroundColor Blue

$dataConclusaoTestResult = dotnet test AcessibilidadeWebAPI.Tests --filter "DataConclusao" --configuration Release --no-build --verbosity minimal

if ($LASTEXITCODE -ne 0) {
    Write-Host "❌ ERRO: Testes da correção DataConclusao falharam!" -ForegroundColor Red
    Write-Host "❌ Push cancelado. A correção principal está com problemas." -ForegroundColor Red
    exit 1
}

Write-Host "✅ Correção DataConclusao: VALIDADA" -ForegroundColor Green

# Sucesso final
Write-Host ""
Write-Host "🎉 PRE-PUSH VALIDATION: SUCESSO TOTAL!" -ForegroundColor Green
Write-Host "✅ Build: OK" -ForegroundColor Green
Write-Host "✅ Testes: OK" -ForegroundColor Green
Write-Host "✅ Correção DataConclusao: OK" -ForegroundColor Green
Write-Host ""
Write-Host "🚀 Push autorizado! Código está pronto para produção." -ForegroundColor Cyan
Write-Host ""

exit 0 