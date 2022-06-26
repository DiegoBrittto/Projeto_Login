create database dbAUTORIZACAO;

use dbAUTORIZACAO;

create table tbUSUARIO(
UsuarioID int primary key auto_increment,
UsuNome varchar(100) not null unique,
Login varchar(50) not null unique,
Senha varchar(100) not null
);


delimiter $$
create procedure spInsertUsuario(vUsuNome varchar(100), vLogin varchar(50), vSenha varchar(100))
begin
	insert into tbUSUARIO(UsuNome,Login, Senha)
			values(vUsuNome,vLogin, vSenha);
end $$ 

call spInsertUsuario('Molina','Molina','123456');

delimiter $$
create procedure spSelectLogin(vLogin varchar(50))
begin

	Select Login from tbUsuario where Login = vLogin;

end $$ 

delimiter $$
create procedure spSelectUsuario(vUsuario varchar(100))
begin

	Select Usuario from tbUsuario where Usuario = vUsuario;

end $$ 

delimiter $$
create procedure spUpdateSenha(vLogin varchar(50), vSenha varchar(100))
begin

	Update  tbUsuario set Senha= vSenha where Login = vLogin;

end $$ 

call spInsertUsuario('Molina','Molina','123456');
call spSelectLogin('Zezinho');
call spSelectUsuario('Mateus');
call spUpdateSenha('Molina','12345678');


select * from tbUsuario;

