# Jxtau
boilerplate for api .net core and client angular

Dotnet - new web api project
- dotnet –info
- dotnet new -h
- dotnet new sln
- dotnet new webapi -o Server
- dotnet sln add Server
- dotnet dev-certs https – trust
- cd Server
- dotnet watch run

Dotnet - EF Core
- Install-Package Microsoft.EntityFrameworkCore.SqlServer
- Install-Package Microsoft.EntityFrameworkCore.Design
- Install-Package Microsoft.EntityFrameworkCore.Tools
- dotnet ef dbcontext scaffold "Server=NEONSDI_N;Database=Appeal;Trusted_Connection=True;" Microsoft.EntityFrameworkCore.SqlServer -o Entities -t Appeal -t AppealContacts -t AppealStatusLog -t AppealStatusType -t AppellantType -t ContactType -t Department -t MeetingSchedule -t PlanType

Angular - new project
- npm install -g @angular/cli@latest
- ng --version
- ng new Client
- cd Client
- ng serve
- ng add @angular/material
- ng add @angular/cdk
- ng g s weather –-skip-tests
- ng g c _modules/page --module app
- ng g interceptor _interceptors/loading
- ng g guard _guards/auth

Angular - resources
- http://json2ts.com/

VS Code Settings
- exclude bin, obj
- compact folder
- private _, this off

Git
- git status
- git init
- dotnet new gitignore

WYSIWYG 
- Tiny MCE
- Angular Editor

PowerShell
- remove-item node_modules -Recurse -Force

NPM
- npm install
