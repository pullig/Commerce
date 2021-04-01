@echo off
echo Execute tests...
dotnet test Commerce.Test
echo Building and creating containers...
docker-compose down
docker-compose up --build -d
echo Waiting for SQL Server to be ready...
powershell.exe -Command "Start-Sleep -Seconds 30"
echo Creating database...
sqlcmd -S localhost -U sa -P "teleMedicina@test" -d "master" -i setup.sql

start chrome https://localhost:49155/swagger/index.html

echo Finished.