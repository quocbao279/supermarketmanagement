

create table users
(
userID int primary key identity,
uName Nvarchar(50),
uUsername varchar(50),
uPass varchar(50),
uPhone varchar(50),
uImage image
)

create table Product
(
proID int primary key identity,
pName Nvarchar(50) not null,
pCatID int,
pBarcode varchar(50),
pCost float,
pPrice float,
pExp varchar(50),
pImage image 
)

Create table Customer
(
cusID int primary key identity,
cusName Nvarchar(50) not null,
cusPhone varchar(50),
cusEmail varchar(50)
)

Create table Supplier
(
supID int primary key identity,
supName Nvarchar(50) not null,
supPhone varchar(50),
supEmail varchar(50),
supAddress varchar(50),
)

Create table Category
(
catID int primary key identity,
catName Nvarchar(150) not null
)



Create table tblMian
(
MainID int primary key identity,
mdate date,
mType varchar(10),
mSupCusID int
)

Create table tblDetails
(
detailID int primary key identity,
dMainID int,
productID int,
qty int,
price float,
amount float,
cost float
)

create table Discount(
disID int primary key identity,
disName Nvarchar(50) not null,
disDetail int,
disOndate date,
disOutdate date
)

Alter table users add uRole nvarchar(50);

UPDATE users SET uRole = 'Admin' WHERE uUsername = 'admin';
UPDATE users SET uRole = 'Manager' WHERE uUsername = 'manager';
UPDATE users SET uRole = 'Employee' WHERE uUsername = 'employee';