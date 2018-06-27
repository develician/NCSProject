# NCSProject
NCSProject


```sql
drop database if exists lib;
create database lib;

ALTER SCHEMA lib  DEFAULT CHARACTER SET utf8 ;

use lib;

drop table if exists admin;
create table admin(
	adminId varchar(20) primary key,
	adminPassword varchar(50)
);


insert into admin (adminId, adminPassword) values ('admin', 'admin1234');

drop table if exists books;
create table books( 
id int not null auto_increment primary key,
isbn varchar(12) not null,
name varchar(50) not null,
publisher varchar(50) not null,
page int not null,
userId int,
userName varchar(50),
isBorrowed int default 0,
borrowedAt date,
returnedAt date);

drop table if exists users;
create table users(
id int not null auto_increment primary key,
name varchar(50) not null,
phone varchar(11) not null,
borrowedNumber int default 0);

drop table if exists borrowed;
create table borrowed(
id int not null auto_increment primary key,
userid int,
username varchar(5),
isbn varchar(12),
bookname varchar(30),
borrowedat date,
returnat date);

drop table if exists history;
create table history (
	id int not null auto_increment primary key,
	userId int,
	userName varchar(500),
	borrowedAt date,
	returnedAt date,
	bookName varchar(500),
	bookIsbn varchar(12)	
);



ALTER TABLE admin convert to character set UTF8;
ALTER TABLE Books convert to character set UTF8;
ALTER TABLE Users convert to character set UTF8;
ALTER TABLE history convert to character set UTF8;

```
