To run migration
#Open a command line, go to the project folder, and run
dotnet restore

#If everything is fine, you should be able to run
dotnet ef

#After that you can run commands like:
dotnet ef migrations add initial
dotnet ef migrations add migrateion1 --project DataAccessEF --startup-project ConsoleProgram

dotnet ef database update --project DataAccessEF --startup-project ConsoleProgram
