Incident Management System – README
  This project is a Mini Incident Management System that allows internal teams to:
o	Creating an incident
o	Listing incidents
o	Updating incident status
The solution is built as a clean, maintainable MVP using ASP.NET Core (.NET 8), CQRS, Clean Architecture, and a React (TypeScript) frontend.
________________________________________Architecture Overview.
As it is internal service so we can follow Monolithic architecture. Separated React UI code , API and Notification part so that we can deploy each component separately. For each Component, will create separate GitHub Repo and CI/CD pipeline. This will enhance the loose coupling while doing deployment and release. 
   
Backend (ASP.NET Core)
•	Clean Architecture
o	Domain – Core entities and Enums
o	Application – CQRS commands/queries, handlers, interfaces
o	Infrastructure – EF Core, Azure Blob Storage, repositories
o	API – Controllers, DI, Swagger, configuration
•	CQRS with MediatR
o	Commands → Create / Update operations
o	Queries → Read operations
•	Entity Framework Core
o	SQL Server / Azure SQL
•	Thin Controllers
o	Controllers delegate logic to MediatR
o	No business logic in API layer
Frontend (React + TypeScript)
•	Component-based architecture
•	Separation of concerns:
o	pages – Screens
o	components – UI components
o	services – API calls (Axios)
o	models – TypeScript interfaces
•	REST integration with backend API
________________________________________
 Azure Services Used
1)	Azure Key Vault
•	Stores sensitive configuration:
o	SQL connection string
o	Blob Storage connection string
•	Accessed using DefaultAzureCredential
2)	 Azure Blob Storage
•	Stores incident attachments (screenshots, files)
•	Files accessed via secure Blob URLs
3)	 Azure App Service 
•	Backend API hosted on Azure App Service
•	Environment variables used for configuration
✉️4)  Azure Function (Notification)
•	Sends notifications (Email / Log)

 Frontend Setup Instructions
A)	Install dependencies
                                        npm install
B)	 Configure API URL
Create .env file:
VITE_API_URL=http://localhost:5000/api
C)	 Run UI
                                       npm run dev
 As it is internal service. for Authentication, we can use Azure Active Directory and Azure Easy Auth. This solution does not provide authentication.		

