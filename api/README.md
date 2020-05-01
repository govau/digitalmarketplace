
Scaffolding the database
`dotnet ef dbcontext scaffold "Host=localhost;Port=15432;Database=digitalmarketplace;Username=postgres;Password=password" Npgsql.EntityFrameworkCore.PostgreSQL -o ./Entities --data-annotations --force -c DigitalMarketplaceContext`
