SOLUTION = FinDoc.sln
API_PROJECT = src/FinDoc.Api/FinDoc.Api.csproj

.PHONY: all restore build run clean test watch

all: restore build

restore:
	dotnet restore $(SOLUTION)

build:
	dotnet build $(SOLUTION)

run:
	dotnet run --project $(API_PROJECT)

test:
	dotnet test

watch:
	dotnet watch --project $(API_PROJECT) run

clean:
	dotnet clean $(SOLUTION)
