# Chattoo

## Vygenerování GraphQL schema
get-graphql-schema http://localhost:5000/graphql > schema.graphql

## Vygenerování nové DB migrace
dotnet ef migrations add "MigrationName" --project Chattoo.Infrastructure --startup-project Chattoo.GraphQL --output-dir Persistence/Migrations




"DefaultConnection": "Server=.\\SQLExpress;Database=ChattooDBnew;Integrated Security=true"

"DefaultConnection": "Server=localhost;Database=Chattoo;User Id=sa;Password=Heslo123!"


Uživatel si přeje vytvoření události
- od do
- určitého typu
- s určitým okruhem lidí

Server aktivně prochází neobsloužená přání
- pokud nalezne shodu navrhne uživatelům vytvoření události
- - shoda může být vuči jinému přání
- - nebo vůči jiné události

Pokud uživatel otevře panel pro nalezení shody, uvidí návrhy

Jakmile dojde k propojení přání, zaniknou
- - stačí entitě nastavit nadřazenou událost, která ji obsluhuje

Server bude aktivně procházet již obsloužená přání u již dokončených událostí a bude je mazat

Pokud si jiný uživatel přeje vytvoření události, která vyhovuje přání jiného uživatele
- dojde k návrhu o vytvoření události, která odpovídá min. parametrům
- dojde k vytvoření události, pokud jsou splněny již všechny parametry (např. min kapacita)
