docker-build:
	docker-compose build

docker-up:
	docker-compose up -d

docker-down: 
	docker-compose down

api-test:
	dotnet test ./EventsApi/Events.Tests

all: docker-down docker-build docker-up