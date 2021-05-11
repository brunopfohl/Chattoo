# Chattoo

## Vygenerování GraphQL schema
get-graphql-schema http://localhost:5000/graphql > schema.graphql

## Vygenerování nové DB migrace
dotnet ef migrations add "MigrationName" --project Chattoo.Infrastructure --startup-project Chattoo.GraphQL --output-dir Persistence/Migrations
