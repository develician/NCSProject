# NCSProject
NCSProject


```sql
create database lib;
use lib;

create table admin(
	adminId varchar(20) primary key,
	adminPassword varchar(50)
);


insert into admin (adminId, adminPassword) values ('admin', 'admin1234');

create table books( 
id int not null auto_increment primary key,
isbn varchar(12) not null,
name varchar(50) not null,
publisher varchar(50) not null,
page int not null,
userId int,
userName varchar(50),
isBorrowed int,
borrowedAt date,
returnedAt date);

create table users(
id int not null auto_increment primary key,
name varchar(50),
phone varchar(11),
borrowedNumber int,
delayeCcnt int);

create table borrowed(
id int not null auto_increment primary key,
userid int,
username varchar(5),
isbn varchar(12),
bookname varchar(30),
borrowedat date,
returnat date);

create table history (
	id int not null auto_increment primary key,
	userId int,
	userName varchar(500),
	borrowedAt date,
	returnedAt date,
	bookName varchar(500),
	bookIsbn varchar(12)	
);

ALTER SCHEMA lib  DEFAULT CHARACTER SET utf8 ;

ALTER TABLE admin convert to character set UTF8;
ALTER TABLE Books convert to character set UTF8;
ALTER TABLE Users convert to character set UTF8;
ALTER TABLE history convert to character set UTF8;

```
