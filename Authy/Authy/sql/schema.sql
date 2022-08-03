create database authy;
use authy;

create table Users(
	Id int identity(1,1) not null,
	Username varchar(255) not null unique,
	Password varchar(max) not null,
	Salt varchar(max) not null default('00000000'),
	Name varchar(max),
	Role varchar(8) not null default('USER')
);

alter table Users add constraint PK_ID_USERS
primary key(Id);

alter table Users add constraint ROLE_CHECK_USERS
check(Role in ('USER', 'STAFF', 'ADMIN'));

create unique index IDX_USERNAME_USERS
on Users(Username);
