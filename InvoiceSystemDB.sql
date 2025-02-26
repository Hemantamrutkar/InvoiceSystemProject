create database InvoiceSystemDB
use InvoiceSystemDB
----------------------------------------------

create table tblcustomers
(
	customer_id int identity primary key,
	customer_name varchar(30),
	mobile_number varchar(10),
	city varchar(20)
)
-------------------------------------------
create table tblproducts
(
	product_id int identity primary key,
	product_name varchar(50),
	weight varchar(20),
	rate float,
	gst int,
	stock_quantity varchar(20),
	received_on varchar(20)
)
alter table tblproducts alter column stock_quantity int
alter table tblproducts alter column received_on date
------------------------------------------------
create table tblinvoice_details
(
	invoice_id int identity primary key,
	customer_id int constraint fkci references tblcustomers(customer_id),
	invoice_date date,
	total_amount float
)
--------------------------------------------
create table tblinvoice_products
(
	invoice_product_id int identity Primary key,
	invoice_id int constraint fkipi references tblinvoice_details (invoice_id),
	product_id int constraint fkipp references tblproducts (product_id),
	quentity varchar(20)
)
ALTER TABLE tblinvoice_products ALTER COLUMN quantity int;
EXEC sp_rename 'tblinvoice_products.quentity', 'quantity', 'COLUMN';
-------------------------------------------------------------------
create table tblinvoice_payments
(
	payment_id int identity primary key,
	invoice_id int constraint fkipii references tblinvoice_details (invoice_id),
	payment_date date,
	payment_amount float,
	payment_mode varchar(30),
	payment_description varchar (50)
)
--------------------------------------------
select * from tblcustomers
select * from tblproducts
select * from tblinvoice_details
select * from tblinvoice_products
select * from tblinvoice_payments
----------------------------------------------
create table tblweights
(
	weight_id int identity primary key,
	weight_title varchar(100)
)
insert into tblweights values ('Nos'),
('50 gram'),('100 gram'),('125 gram'),('150 gram'),('250 gram'),('500 gram'),
('1 kg'),('2 kg'),('5 kg'),('10 kg'),
('1 ltr'), ('2 ltr'), ('5 ltr'), ('10 ltr'), ('15 ltr')
--------------------------------------------------------------------------------
select * from tblweights