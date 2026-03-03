# ECommerce-API
Ecommerce API


# create table - users
CREATE TABLE Users (
    Id BIGINT PRIMARY KEY IDENTITY(1,1),

    FirstName NVARCHAR(100) NOT NULL,
    LastName NVARCHAR(100) NOT NULL,
    Email NVARCHAR(255) NOT NULL,
    Password NVARCHAR(255) NOT NULL,
    PhoneNumber NVARCHAR(20) NOT NULL,

    CreatedBy BIGINT NOT NULL,
    CreatedDate DATETIME2(0) NOT NULL 
        CONSTRAINT DF_Users_CreatedDate DEFAULT GETDATE(),

    UpdatedBy BIGINT NULL,
    UpdatedDate DATETIME2(0) NULL,

    CONSTRAINT UQ_Users_Email UNIQUE (Email),

    CONSTRAINT FK_Users_CreatedBy 
        FOREIGN KEY (CreatedBy) REFERENCES Users(Id),

    CONSTRAINT FK_Users_UpdatedBy 
        FOREIGN KEY (UpdatedBy) REFERENCES Users(Id)
);

# create table - countries
CREATE TABLE Countries (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(150) NOT NULL
);


# create table - States
CREATE TABLE States (
    Id INT PRIMARY KEY IDENTITY(1,1),

    Name NVARCHAR(150) NOT NULL,

    CountryId INT NOT NULL,

    CONSTRAINT FK_States_Country
        FOREIGN KEY (CountryId) REFERENCES Countries(Id)
)



# create table - Districts
CREATE TABLE Districts (
    Id INT PRIMARY KEY IDENTITY(1,1),

    Name NVARCHAR(150) NOT NULL,

    StateId INT NOT NULL,

    CONSTRAINT FK_Districts_State
        FOREIGN KEY (StateId) REFERENCES States(Id)
);


# create table - Taluk
CREATE TABLE Taluks (
    Id INT PRIMARY KEY IDENTITY(1,1),

    Name NVARCHAR(150) NOT NULL,

    DistrictId INT NOT NULL,

    CONSTRAINT FK_Taluks_District
        FOREIGN KEY (DistrictId) REFERENCES Districts(Id)
);


# create table - UserAddress
CREATE TABLE UserAddresses (
    Id BIGINT PRIMARY KEY IDENTITY(1,1),

    UserId BIGINT NOT NULL,

    StreetName NVARCHAR(255) NOT NULL,
    Village NVARCHAR(150) NOT NULL,

    TalukId INT NOT NULL,

    Pincode NVARCHAR(10) NOT NULL,

    CreatedBy BIGINT NOT NULL,
    CreatedDate DATETIME2(0) NOT NULL DEFAULT GETDATE(),

    UpdatedBy BIGINT NULL,
    UpdatedDate DATETIME2(0) NULL,

    CONSTRAINT FK_UserAddresses_User
        FOREIGN KEY (UserId) REFERENCES Users(Id),

    CONSTRAINT FK_UserAddresses_Taluk
        FOREIGN KEY (TalukId) REFERENCES Taluks(Id),

    CONSTRAINT FK_UserAddresses_CreatedBy
        FOREIGN KEY (CreatedBy) REFERENCES Users(Id),

    CONSTRAINT FK_UserAddresses_UpdatedBy
        FOREIGN KEY (UpdatedBy) REFERENCES Users(Id)
);


# create table Categories
CREATE TABLE Categories (
    Id INT PRIMARY KEY IDENTITY(1,1),

    Name NVARCHAR(150) NOT NULL,

    Description NVARCHAR(500) NULL,

    CreatedBy BIGINT NOT NULL,
    CreatedDate DATETIME2(0) NOT NULL DEFAULT GETDATE(),

    UpdatedBy BIGINT NULL,
    UpdatedDate DATETIME2(0) NULL,

    CONSTRAINT UQ_Categories_Name UNIQUE (Name),

    CONSTRAINT FK_Categories_CreatedBy
        FOREIGN KEY (CreatedBy) REFERENCES Users(Id),

    CONSTRAINT FK_Categories_UpdatedBy
        FOREIGN KEY (UpdatedBy) REFERENCES Users(Id)
        ON DELETE SET NULL
);


# create table Products
CREATE TABLE Products (
    Id BIGINT PRIMARY KEY IDENTITY(1,1),

    Name NVARCHAR(200) NOT NULL,
    Description NVARCHAR(1000) NULL,

    CreatedBy BIGINT NOT NULL,
    CreatedDate DATETIME2(0) NOT NULL DEFAULT GETDATE(),

    UpdatedBy BIGINT NULL,
    UpdatedDate DATETIME2(0) NULL,

    CONSTRAINT UQ_Products_Name UNIQUE (Name),

    CONSTRAINT FK_Products_CreatedBy
        FOREIGN KEY (CreatedBy) REFERENCES Users(Id),

    CONSTRAINT FK_Products_UpdatedBy
        FOREIGN KEY (UpdatedBy) REFERENCES Users(Id)
        ON DELETE SET NULL
);


# create table Orders
CREATE TABLE Orders (
    Id BIGINT PRIMARY KEY IDENTITY(1,1),

    OrderRef NVARCHAR(50) NOT NULL,

    OrderDate DATETIME2(0) NOT NULL,

    CreatedBy BIGINT NOT NULL,
    CreatedDate DATETIME2(0) NOT NULL DEFAULT GETDATE(),

    UpdatedBy BIGINT NULL,
    UpdatedDate DATETIME2(0) NULL,

    CONSTRAINT UQ_Orders_OrderRef UNIQUE (OrderRef),

    CONSTRAINT FK_Orders_CreatedBy
        FOREIGN KEY (CreatedBy) REFERENCES Users(Id),

    CONSTRAINT FK_Orders_UpdatedBy
        FOREIGN KEY (UpdatedBy) REFERENCES Users(Id)
        ON DELETE SET NULL
);

# create table OrderItems
CREATE TABLE OrderItems (
    Id BIGINT PRIMARY KEY IDENTITY(1,1),

    OrderId BIGINT NOT NULL,
    ProductId BIGINT NOT NULL,

    CreatedBy BIGINT NOT NULL,
    CreatedDate DATETIME2(0) NOT NULL DEFAULT GETDATE(),

    UpdatedBy BIGINT NULL,
    UpdatedDate DATETIME2(0) NULL,

    CONSTRAINT FK_OrderItems_Order
        FOREIGN KEY (OrderId) REFERENCES Orders(Id)
        ON DELETE CASCADE,

    CONSTRAINT FK_OrderItems_Product
        FOREIGN KEY (ProductId) REFERENCES Products(Id),

    CONSTRAINT FK_OrderItems_CreatedBy
        FOREIGN KEY (CreatedBy) REFERENCES Users(Id),

    CONSTRAINT FK_OrderItems_UpdatedBy
        FOREIGN KEY (UpdatedBy) REFERENCES Users(Id)
        ON DELETE SET NULL
);

# create table Invoices
CREATE TABLE Invoices (
    Id INT PRIMARY KEY IDENTITY(1,1),

    OrderId BIGINT NOT NULL,

    InvoiceDate DATETIME2(0) NOT NULL,

    CreatedBy BIGINT NOT NULL,
    CreatedDate DATETIME2(0) NOT NULL DEFAULT GETDATE(),

    UpdatedBy BIGINT NULL,
    UpdatedDate DATETIME2(0) NULL,

    CONSTRAINT UQ_Invoices_Order UNIQUE (OrderId),

    CONSTRAINT FK_Invoices_Order
        FOREIGN KEY (OrderId) REFERENCES Orders(Id)
        ON DELETE CASCADE,

    CONSTRAINT FK_Invoices_CreatedBy
        FOREIGN KEY (CreatedBy) REFERENCES Users(Id),

    CONSTRAINT FK_Invoices_UpdatedBy
        FOREIGN KEY (UpdatedBy) REFERENCES Users(Id)
        ON DELETE SET NULL
);
