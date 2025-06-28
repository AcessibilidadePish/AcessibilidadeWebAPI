@echo off
echo #!/bin/sh > ../.git/hooks/pre-push
echo echo Executando build do projeto... >> ../.git/hooks/pre-push
echo dotnet build >> ../.git/hooks/pre-push
echo if [ ^$? -ne 0 ]; then >> ../.git/hooks/pre-push
echo   echo Build falhou! >> ../.git/hooks/pre-push
echo   exit 1 >> ../.git/hooks/pre-push
echo fi >> ../.git/hooks/pre-push
echo echo Executando testes do projeto... >> ../.git/hooks/pre-push
echo dotnet test >> ../.git/hooks/pre-push
echo if [ ^$? -ne 0 ]; then >> ../.git/hooks/pre-push
echo   echo Testes falharam! >> ../.git/hooks/pre-push
echo   exit 1 >> ../.git/hooks/pre-push
echo fi >> ../.git/hooks/pre-push
