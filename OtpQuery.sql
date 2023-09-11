use master
go


create database TestOTP
go

use TestOTP
go

create table OTPSettings (
Id uniqueidentifier not null primary key,
checklsitAllWa bit,
checklistAllEmail bit,
withdrawlWa bit,
withdrawlEmail bit,
ForgotPasswordWa bit,
ForgotPasswordEmail bit,
ResetPasswordWa bit,
ResetPassword bit
)
go

Create Procedure UpdateOTP
@id Uniqueidentifier,
@checklsitAllWa bit,
@checklistAllEmail bit,
@withdrawlWa bit,
@withdrawlEmail bit,
@ForgotPasswordWa bit,
@ForgotPasswordEmail bit,
@ResetPasswordWa bit,
@ResetPassword bit
as
begin 
	if exists (select 1 from OTPSettings where id = @id)
	begin
		update OTPSettings set 
		checklsitAllWa=@checklsitAllWa ,
		checklistAllEmail=@checklistAllEmail ,
		withdrawlWa=@withdrawlWa ,
		withdrawlEmail=@withdrawlEmail ,
		ForgotPasswordWa=@ForgotPasswordWa ,
		ForgotPasswordEmail=@ForgotPasswordEmail ,
		ResetPasswordWa=@ResetPasswordWa ,
		ResetPassword=@ResetPassword 
	end
	else 
	begin
	insert into OTPSettings values(NEWID(),@checklsitAllWa ,@checklistAllEmail,@withdrawlWa,@withdrawlEmail,@ForgotPasswordWa,@ForgotPasswordEmail,@ResetPasswordWa,@ResetPassword)
	end
end
go
