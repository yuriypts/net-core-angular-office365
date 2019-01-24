# DocFlow Application

## Preparing for build application

	1. Switch to “dev” bruch (use command line or other programs).
	2. In "DefaultConnection" property (appsettings.json file) you should set your instance MS SQL server. (Server={your instance}).
	3. When you put correct instance you have to call command in Package-Console Line Visual Studio "update-database" (set Default Project "DocFlow.Data" in command line).
	4. Go inside directory “DocFlow.App” and run the command “npm install” (use command line windows or powershell) for install node packages.

## Preparing for start application
### *Fort start Application you have to run front end and back end parts

	1. Run front end part - Go inside directory “DocFlow.App” and run the command “npm start”.
	2. Run back end part - Set as Start Up Project “DocFlow” and run project (click to IIS Express)

	
## Connect Application for other Office 365

	For connect application you other Office 365 you need change properties in AzureADService object (appsettings.json):
	 - ClientId, 
	 - AppKey
	 - TokenEndpoint (https://login.windows.net/{tenatID}/oauth2/token)
