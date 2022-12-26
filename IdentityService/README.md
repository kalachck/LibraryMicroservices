# AuthorizationService
This project is designed to optimize the process of authorization, registration and authentication of library users

| Method                       | Description                                                                  |
| ---------------------------- | ---------------------------------------------------------------------------- |
| RegisterAsync                          | Registers user                                                               |
| SignInAsync                       | Authorizes user                                                            |
| SignOutAsync                       | Allows user to sign out                                                             |

## Technologies
Microsoft SQL Server
OpenIddict
This technology was chosen because of its simplicity and ease of use.

##### Database structure:

| Users              |
| -------------------|
| Id                 |
| Name               |
| LastName           |
| MiddleName         |
| Login              |
| Password           |
| Role               |

| Books              |
| -------------------|
| Id                 |
| Title              |
| PublicationDate    |
| CategoryId         |
| Price              |

| Categories         |
| -------------------|
| Id                 |
| Title              |

| UsersBooks         |
| -------------------|
| UserId             |
| BookId             |

| BooksAuthors       |
| -------------------|
| BookId             |
| AuthorId           |


##### Logic:
This service implements a three-layer architecture. The DAL is intended for access to the storage of users, in particular for authorization and authentication. The BLL implements the user validation process for successful authorization or registration. The API Designed for convenient display of the authorization process in the browser and management of endpoints

# Cases

**Feature**: Register user and add into database  
**As a** user to register    
**I want to** register in library  

**Scenario** User was register successfully
**Given** a user who want to register in library  
**Then** the user was successfully register  
**And** successfull status was returned  

**Scenario** User failed validation  
**Given** a user who want to register in library  
**Then** the user wasn't register    
**And** error will be returned to user  

---

**Feature**: User sign in   
**As a** user to sign in    
**I want to** sign in to library  

**Scenario** User was sign in successfully  
**Given** a user who want to sign in to library  
**Then** the user signed in successfully     
**And** successfull status was returned  

**Scenario** User failed validation  
**Given** a user who want to sign in to library  
**Then** the user wasn't sign in  
**And** error will be returned to user  

---

**Feature**: User sign out   
**As a** user to sign out    
**I want to** sign out to library  

**Scenario** User was sign out successfully    
**Given** a user who want to sign out to library  
**Then** the user signed out successfully     
**And** successfull status was returned  

**Scenario** User wasn't sign out successfully  
**Given** a user who want to sign out from library  
**Then** the user wasn't sign out  
**And** error will be returned to an external service  