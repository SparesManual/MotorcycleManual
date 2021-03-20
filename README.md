![build](https://github.com/DicksHalfwayInnTeam/MotorcycleManual/workflows/.NET/badge.svg)

# MotorcycleManual
Interactive software that allows a user to search and identify motorcycle parts from a Motorcycle Parts Manual

## Setting everything up

### The database

1. Open a command prompt or terminal
2. Navigate to the repository folder
4. If the `src/Libraries/Db/Db.Infrastructure/Migrations` sub-folder are not empty and the database structure is up to date, skip the following sub-steps:
   1. Check if `dotnet ef --version` returns the version 5.0.0, if not follow the troubleshooting steps.
   2. If the database needs to be reset, delete the folders inside the `src/Libraries/Db/Db.Infrastructure/Migrations` folder and the `src/Libraries/Db/Db.API/motor.db` and `src/Libraries/Db/Db.API/auth.db` files
   3. Ensure that you are at the root of the repository   
   4. Run the following commands:
      - `dotnet ef migrations add ManualInitial -p Libraries/Db/Db.Infrastructure -s Libraries/Db/Db.API -o Migrations/Manual -c ManualContext`
      - `dotnet ef migrations add IdentityInitial -p Libraries/Db/Db.Infrastructure -s Libraries/Db/Db.API -o Migrations/Identity -c IdentityContext`
   5. Verify that the `src/Libraries/Db/Db.Infrastructure/Migrations` folder now has two subfolders with content
   6. Run the following commands:
      - `dotnet ef database update -p Libraries/Db/Db.API -c ManualContext`
      - `dotnet ef database update -p Libraries/Db/Db.API -c IdentityContext`
   7. Run the Db.API project to execute the initial set-up data-seeding process
   
### SSL Certificates

1. Open a command prompt or terminal
2. Navigate to the repository folder
3. Navigate to `src/Libraries/Db/Db.API`
4. Enter the following commnand into the command prompt: `dotnet dev-certs https -t`
5. Confirm any popping up prompts
6. Navigate to `src/Application/MRI.Application`
7. Repeat steps 4 and 5

### Troubleshooting

#### Missing DOTNET EF tool

To download the EF tool, run the following command in a terminal: `dotnet tool install --global dotnet-ef --version 5.0.0`

Run `dotnet ef --version` afterwards to make sure the correct tool version is installed.

## Running the application

When working on/testing the front-end application, the database server must be running in the background.

To do so, follow these steps:

1. In Visual Studio open View -> Terminal
2. Navigate to `src/Libraries/Db/Db.API`
3. Enter the following command into the terminal: `dotnet run`

To terminate the running application, press `Ctrl+C` in the terminal.

With the database application running in the background, the MRI.Application can be run using Visual Studio directly.
