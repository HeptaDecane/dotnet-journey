use warehouse_management;

create table Stores(
	Id int not null identity(1,1),
	Name varchar(64) not null,
	Address varchar(128) not null,
	Pin varchar(6) not null,
	Phone varchar(16) not null,
	Email varchar(32) not null
);

create table Products(
	Id int not null identity(1,1),
    Name varchar(64) not null,
    Description varchar(256),
    Category varchar(32) not null,
	Image varchar(256)
);

create table Ref_Products_Stores(
    Id int not null identity(1,1),
    ProductId int not null,
    StoreId int not null,
	Quantity int not null
);

alter table Stores add constraint PK_ID_STORES
primary key(Id);

alter table Products add constraint PK_ID_PRODUCTS
primary key(Id);

alter table Ref_Products_Stores add constraint PK_ID_REF_PRODUCTS_STORES
primary key(Id);


alter table Ref_Products_Stores add constraint FK_STORE_ID_REF_PRODUCTS_STORES
foreign key(StoreId) references Stores(Id)
on delete cascade;

alter table Ref_Products_Stores add constraint FK_PRODUCT_ID_REF_PRODUCTS_STORES
foreign key(ProductId) references Products(Id)
on delete cascade;
