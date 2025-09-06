SOLUTION = FinDoc.sln
API_PROJECT = src/FinDoc.Api/FinDoc.Api.csproj

.PHONY: all restore build run clean test watch

all: restore build

restore:
	dotnet restore $(SOLUTION)

build:
	dotnet build $(SOLUTION) --no-restore

run:
	dotnet run --project $(API_PROJECT)

test:
	dotnet test $(SOLUTION) --no-build

watch:
	dotnet watch --project $(API_PROJECT) run

clean:
	dotnet clean $(SOLUTION)

lint:
	dotnet format --verify-no-changes

coverage:
	dotnet test /p:CollectCoverage=true /p:CoverletOutput=coverage/ /p:CoverletOutputFormat=opencover
	reportgenerator -reports:coverage/coverage.opencover.xml -targetdir:coveragereport


## opening report generator in your browser
xdg-open coveragereport/index.html
