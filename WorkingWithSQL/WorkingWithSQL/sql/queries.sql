use warehouse_management;

insert into Stores(Name, Address, Pin, Phone, Email) 
values
('Apt. 911','Rice Streets, 4400 Kassulke Village','619342','+235723467821','apt911@bwm.com'),
('Apt. 361','Jakubowski Junctions, 2973 Rudy Rue','876298','+982734627193','apt361@bwm.com'),
('Apt. 111','Daugherty Loaf, 79920 Gerhold Gateway','782671','+098371647836','apt111@bwm.com'),
('Suite 548','Rice Streets, 4400 Kassulke Village','981124','+782736127465','suite548@bwm.com'),
('Suite 570','Jessyca Centers,9396 Frederic Club','893657','+893478290417','suite570@bwm.com');

insert into Products(Name, Description, Category, Image)
values
('Fjallraven - Foldsack No. 1 Backpack', 'Your perfect pack for everyday use and walks in the forest.','men`s clothing', 'https://fakestoreapi.com/img/81fPKd-2AYL._AC_SL1500_.jpg'),
('Mens Cotton Jacket', 'Good gift choice for you or your family member.', 'men`s clothing', 'https://fakestoreapi.com/img/71li-ujtlUL._AC_UX679_.jpg'),
('iPhone 9', 'An apple mobile which is nothing like apple', 'smartphones', 'https://dummyjson.com/image/i/products/1/thumbnail.jpg'),
('MacBook Pro', 'MacBook Pro 2021 with mini-LED display may launch between September, November', 'laptops', 'https://dummyjson.com/image/i/products/6/thumbnail.png'),
('Key Holder', 'Attractive DesignMetallic materialFour key hooksReliable & DurablePremium Quality', 'home decor', 'https://dummyjson.com/image/i/products/30/thumbnail.jpg');

insert into Ref_Products_Stores(StoreId, ProductId, Quantity)
values
(1, 2, 23),
(2, 1, 70),
(4, 1, 32),
(3, 5, 50),
(5, 4, 76),
(2, 3, 40);
