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

create table Books( 
isbn varchar(12) primary key,
name varchar(50),
publisher varchar(50),
page int,
userid int,
username varchar(50),
isBorrowed int,
Borrowedat date,
returnat date);

create table Users(
id int primary key auto_increment,
name varchar(50),
phone varchar(12),
borrowednumber int,
delayedcnt int);

create table borrowed(
id int primary key auto_increment,
userid int,
username varchar(5),
isbn varchar(12),
bookname varchar(30),
borrowedat date,
returnat date);

create table history (
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
