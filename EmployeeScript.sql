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


Exec EmployeeUpdate 
@ID = 0,
@EmployeeFirstName='Max',
@EmployeeLastName='Connor',
@EmployeeEmail='max.connor@lifeshop.com',
@EmployeePhone='8888888888',
@EmployeeAddress='123 Business Lane',
@EmployeeCity='Harrisburg',
@EmployeeState='Pennsylvania',
@EmployeeZip='17000',
@EmployeeUsername='CEO',
@EmployeePassword='123',
@EmployeeRole='CEO',
@NewID=0;
GO
Exec EmployeeUpdate 
@ID = 0,
@EmployeeFirstName='Marge',
@EmployeeLastName='Smith',
@EmployeeEmail='marge.smith@lifeshop.com',
@EmployeePhone='8888888888',
@EmployeeAddress='123 Business Lane',
@EmployeeCity='Harrisburg',
@EmployeeState='Pennsylvania',
@EmployeeZip='17000',
@EmployeeUsername='Director',
@EmployeePassword='123',
@EmployeeRole='Product Director',
@NewID=0;
GO
Exec EmployeeUpdate 
@ID = 0,
@EmployeeFirstName='John',
@EmployeeLastName='Doe',
@EmployeeEmail='john.doe@lifeshop.com',
@EmployeePhone='8888888888',
@EmployeeAddress='123 Business Lane',
@EmployeeCity='Harrisburg',
@EmployeeState='Pennsylvania',
@EmployeeZip='17000',
@EmployeeUsername='Manager',
@EmployeePassword='123',
@EmployeeRole='Sales Manager',
@NewID=0;
GO
Select * From Employee;
GO

Select CustomerUsername From Customer; 
go
/*Life Blanket*/
Exec ProductUpdate
@ID=0,
@ItemName="Life Blanket",
@ItemDesc="Signature Company Blanket",
@Price=35,
@Discount = 10,
@Picture="\Image\LifePictures.png",
@NewID=0;
GO
/*Life Shop Blanket Bot*/
Exec ProductUpdate
@ID=0,
@ItemName="LifeShop BlanketBot",
@ItemDesc="New Retro BlanketBot",
@Price=45,
@Discount = 0,
@Picture="\Image\BlanketBot.png",
@NewID=0;
GO
/*Chester Magnet*/
Exec ProductUpdate
@ID=0,
@ItemName="Chester Magnet",
@ItemDesc="Magnet of Chester, the Cat!",
@Price=7.95,
@Discount = 0,
@Picture="\Image\Chester.png",
@NewID=0;
GO
/*LifeShop Magnet*/
Exec ProductUpdate
@ID=0,
@ItemName="LifeShop Magnet",
@ItemDesc="Magnet of the LifeShop Company Logo",
@Price=5.75,
@Discount = 5,
@Picture="\Image\LifeShopMagnet.png",
@NewID=0;
GO
/*Tree Poster*/
Exec ProductUpdate
@ID=0,
@ItemName="Tree Poster",
@ItemDesc="Lovely poster of a summer night.",
@Price=9.99,
@Discount = 25,
@Picture="\Image\TreePoster.png",
@NewID=0;
GO
Select * From Product;
GO
