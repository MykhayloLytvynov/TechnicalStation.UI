use orders;

DROP TABLE IF EXISTS `orders`;

CREATE TABLE IF NOT EXISTS `orders`(
id int PRIMARY KEY AUTO_INCREMENT NOT NULL,
start_date DATETIME NOT NULL,
end_date DATETIME NOT NULL,
car_produser TEXT  ,
car_model TEXT,
car_year_of_creation DATETIME NOT NULL,
firstname_of_client TEXT  ,
secondname_of_client TEXT,
patronymic_of_client TEXT 
)ENGINE=INNODB; 

drop procedure if exists AddOrder;
delimiter $$
create procedure AddOrder(IN Order_start_date datetime  ,
		IN Order_end_date datetime,
		IN Car_produser TEXT ,
		IN Car_model TEXT ,
		IN Car_year_of_creation DATETIME,
        IN Firstname_of_client TEXT  ,
		IN Secondname_of_client TEXT,
		IN Patronymic_of_client TEXT,
		OUT OrderId int )

begin

	INSERT INTO `orders`
	(start_date ,
	end_date ,
	car_produser,
	car_model ,
	car_year_of_creation,
	firstname_of_client,
    secondname_of_client,
    patronymic_of_client)
	VALUES
	( 
	Order_start_date ,
	Order_end_date,
	Car_produser,
	Car_model,
	Car_year_of_creation,
    Firstname_of_client,
	Secondname_of_client,
	Patronymic_of_client);

set OrderId = last_insert_id();
end$$
delimiter ;

drop procedure if exists GetOrderById;
delimiter $$
create procedure GetOrderById(IN OrderId int)
begin
select * from `orders` where `orders`.id = OrderId;
end$$
delimiter ;
