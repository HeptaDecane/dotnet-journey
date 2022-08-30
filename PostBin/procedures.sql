create procedure sp_get_posts
as 
begin
	select 
		Id, 
		UserId, 
		Title, 
		Body 
	from Posts;
end;


create procedure sp_get_post
@Id int
as
begin
	select Id, UserId, Title, Body from Posts
	where Id=@Id;
end;


create procedure sp_create_post
@Title varchar(255), @Body varchar(max), @UserId int
as
begin
	insert into Posts(Title, Body, UserId)
	values (@Title, @Body, @UserId);
end;


create procedure sp_update_post
@Title varchar(255), @Body varchar(max), @Id int
as
begin 
	update Posts set
		Title = @Title,
		Body = @Body
	where Id = @Id;
end;


create procedure sp_delete_post
@Id int
as
begin
	delete from Posts
	where Id = @Id;
end;


create procedure sp_get_user
@Id int
as
begin
	select 
		Id, 
		Username, 
		Password,
		Salt,
		Name,
		Role
	from Users
	where Id=@Id;
end


create procedure sp_get_user_by_username
@Username varchar(255)
as
begin
	select 
		Id, 
		Username, 
		Password,
		Salt,
		Name,
		Role
	from Users
	where Username = @Username;
end;


create procedure sp_create_user
@Username varchar(255), @Password varchar(max), @Salt varchar(8), @Name varchar(255)
as
begin
	insert into Users(Username, Password, Salt, Name)
	values(@Username, @Password, @Salt, @Name);
end;


create procedure sp_update_user
@Username varchar(255), @Password varchar(max), @Name varchar(255), @Role varchar(8), @Id int
as
begin
	update Users set
		Username = @Username,
		Password = @Password,
		Name = @Name,
		Role = @Role
	where Id = @Id;
end;