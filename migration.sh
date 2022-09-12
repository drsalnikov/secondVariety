#!/bin/sh

dotnet aspnet-codegenerator identity --dbContext dbIdentityContext -fi 'Account._StatusMessage;Account.AccessDenied;Account.ConfirmEmail;Account.ConfirmEmailChange;Account.ExternalLogin;Account.ForgotPassword;Account.ForgotPasswordConfirmation;Account.Lockout;Account.Login;Account.LoginWith2fa;Account.LoginWithRecoveryCode;Account.Logout;Account.Manage._Layout;Account.Manage._ManageNav;Account.Manage._StatusMessage;Account.Manage.ChangePassword;Account.Manage.DeletePersonalData;Account.Manage.Disable2fa;Account.Manage.DownloadPersonalData;Account.Manage.Email;Account.Manage.EnableAuthenticator;Account.Manage.ExternalLogins;Account.Manage.GenerateRecoveryCodes;Account.Manage.Index;Account.Manage.PersonalData;Account.Manage.ResetAuthenticator;Account.Manage.SetPassword;Account.Manage.ShowRecoveryCodes;Account.Manage.TwoFactorAuthentication;Account.Register;Account.RegisterConfirmation;Account.ResendEmailConfirmation;Account.ResetPassword;Account.ResetPasswordConfirmation' 
dotnet ef migrations add CreateIdentitySchema --context SecondVariety.Areas.Identity.Data.dbIdentityContext
dotnet ef database update --context SecondVariety.Areas.Identity.Data.dbIdentityContext
