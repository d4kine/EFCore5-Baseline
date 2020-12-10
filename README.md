# DotNet 5.0 / EFCore Basline

This is a sample baseline project to initiate a new C# DotNet 5.0 WebApi project with Entity Framework Core


# IMPORTANT: THIS IS WIP !

Currently the MySQL-Connection is not working and the general state is work in progress. Do not use it yet as your baseline.


## Secret-Configuration

For proper usage of the MySQL Database, the UserSecrets need to be managed:
```json
{
  "Database": {
    "User":     "admin",
    "Password": "admin"
  }
}
```