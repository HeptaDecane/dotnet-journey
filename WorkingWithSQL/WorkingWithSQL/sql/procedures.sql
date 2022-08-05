use  warehouse_management;


create Procedure CreateProduct
@Name varchar(64), @Description varchar(256), @Category varchar(32), @Image varchar(256)
as
begin
	insert into Products (Name, Description, Category, Image)
	values (@Name, @Description, @Category, @Image);
end


create procedure GetProducts 
as
begin
	select * from Products;
end


create procedure GetProductById 
@Id int 
as
begin
	select * from Products where Id=@Id;
end


create procedure UpdateProductById
@Id int, @Name varchar(64), @Description varchar(256), @Category varchar(32), @Image varchar(256)
as
begin
	update Products set
	Name = @Name,
	Description = @Description,
	Category = @Category,
	Image = @Image
	where Id = @Id;
end

create procedure DeleteProductById
@Id int
as
begin
	delete from Products
	where Id=@Id;
end


create procedure CreateStore
@Name varchar(64), @Address varchar(128), @Pin varchar(6), @Phone varchar(16), @Email varchar(32)
as
begin
	insert into Stores (Name, Address, Pin, Phone, Email)
	values (@Name, @Address, @Pin, @Phone, @Email);
end


create procedure GetStores
as
begin
	select * from Stores;
end


create procedure GetStoreById
@Id int
as
begin
	select * from Stores
	where Id=@Id;
end


create procedure UpdateStoreById
@Id int, @Name varchar(64), @Address varchar(128), @Pin varchar(6), @Phone varchar(16), @Email varchar(32)
as
begin
	update Stores set
	Name = @Name,
	Address = @Address,
	Pin = @Pin,
	Phone = @Phone,
	Email = @Email
	where Id = @Id;
end

create procedure DeleteStoreById
@Id int
as
begin
	delete from Stores
	where Id=@Id;
end