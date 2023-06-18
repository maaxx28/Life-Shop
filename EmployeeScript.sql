USE LifeShopData;
go
--Template for adding Customers
/*Exec CustomerUpdate 
@ID=0, 
@CustomerFirstName='',
@CustomerLastName='',
@CustomerEmail='',
@CustomerPhone='',
@CustomerAddress='',
@CustomerCity='',
@CustomerState='',
@CustomerZip='',
@CustomerUsername='',
@CustomerPassword='',
@NewID=0
GO*/
---Template for adding Employee
/*
Exec EmployeeUpdate 
@ID = 0,
@EmployeeFirstName='',
@EmployeeLastName='',
@EmployeeEmail='',
@EmployeePhone='88888888888',
@EmployeeAddress='123 Business Lane',
@EmployeeCity='Harrisburg',
@EmployeeState='Pennsylvania',
@EmployeeZip='17000',
@EmployeeUsername='',
@EmployeePassword='',
@EmployeeRole='',
@NewID=0;
GO
*/
Select * From Employee;
GO

