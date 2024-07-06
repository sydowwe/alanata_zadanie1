create table [User]
(
    Id       int identity
        constraint User_pk
            primary key,
    Name     nvarchar(50),
    Surname  nvarchar(50)  not null,
    Email    nvarchar(100),
    Login    nvarchar(50)  not null,
    Password nvarchar(max) not null
)
go

