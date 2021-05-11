# Chattoo

## Vygenerování GraphQL schema
get-graphql-schema http://localhost:5000/graphql > schema.graphql

## Vygenerování nové DB migrace
dotnet ef migrations add "MigrationName" --project Chattoo.Infrastructure --startup-project Chattoo.GraphQL --output-dir Persistence/Migrations




"DefaultConnection": "Server=.\\SQLExpress;Database=ChattooDBnew;Integrated Security=true"

"DefaultConnection": "Server=localhost;Database=Chattoo;User Id=sa;Password=Heslo123!"
