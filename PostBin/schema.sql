create table Posts(
	Id int not null identity(1,1),
	UserId int not null default 0,
	Title varchar(255) not null,
	Body varchar(max)
);

create table Users(
	Id int identity(0,1) not null,
	Username varchar(255) not null unique,
	Password varchar(max) not null,
	Salt varchar(8) not null default('00000000'),
	Name varchar(255),
	Role varchar(8) not null default('USER')
);

alter table Posts add constraint PK_ID_POSTS
primary key(Id);

alter table Users add constraint PK_ID_USERS
primary key(Id);

alter table Users add constraint ROLE_CHECK_USERS
check(Role in ('USER', 'STAFF', 'ADMIN'));

create unique index IDX_USERNAME_USERS
on Users(Username);

alter table Posts add constraint FK_USER_ID_POSTS
foreign key(UserId) references Users(Id)
on delete cascade;

insert into Users(Username, Password, Name)
values ('anonymous', '00000000', 'Anonymous');
