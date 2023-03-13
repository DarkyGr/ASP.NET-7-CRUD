create database db_aspnetCrud

use db_aspnetCrud

create table department(
	d_id int primary key identity(1, 1),
	d_name varchar(50)
)

create table employee(
	e_id int primary key identity(1, 1),
	e_name varchar(50),
	d_id int references department(d_id),
	salary int,
	contract_date date
)

insert into department(d_name) values
('Management'),
('Commerce'),
('Sales'),
('Marketing')

insert into employee(e_name, d_id, salary, contract_date) values
('John Smith', 1, 2500, getdate())

-->>>>>>>>>>>>>>>>>>>>>>>> Stored Procedures <<<<<<<<<<<<<<<<<<<<<<<

create procedure sp_listDepartments
as
begin
	select d_id, d_name from department
end

create procedure sp_listEmployees
as
begin
	set dateformat dmy
	select e.e_id, e.e_name,
	d.d_id, d.d_name, e.salary,
	convert(char(10), e.contract_date, 103) as 'contract_date'
	from employee as e
	inner join department as d on e.d_id = d.d_id
end

create procedure sp_saveEmployee(
@e_id int,
@e_name varchar(50),
@d_id int,
@salary int,
@contract_date varchar(10)
)
as
begin
	set dateformat dmy
	update employee set
	e_name = @e_name,
	d_id = @d_id,
	salary = @salary,
	contract_date = convert(date, @contract_date)
	where e_id = @e_id
end

create procedure sp_deleteEmployee(
@e_id int
)
as
begin
	delete from employee where e_id = @e_id
end