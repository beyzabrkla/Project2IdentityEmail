using Microsoft.AspNetCore.Identity;

namespace Project2IdentityEmail.Models
{
    public class CustomIdentityValidator: IdentityErrorDescriber
    {
        #region password errors
        public override IdentityError PasswordTooShort(int length)
        {
            return new IdentityError()
            {
                Code = "PasswordTooShort",
                Description = $"Parola en az {length} karakter olmalıdır."
            };
        }

        public override IdentityError PasswordRequiresLower()
        {
            return new IdentityError()
            {
                Code = "PasswordRequiresLower",
                Description = $"Lütfen en az 1 tane küçük harf girişi yapınız!"
            };
        }

        public override IdentityError PasswordRequiresUpper()
        {
            return new IdentityError()
            {
                Code = "PasswordRequiresUpper",
                Description = $"Lütfen en az 1 tane büyük harf girişi yapınız!"
            };
        }

        public override IdentityError PasswordRequiresDigit()
        {
            return new IdentityError()
            {
                Code = "PasswordRequiresDigit",
                Description = $"Lütfen en az 1 tane sayı girişi yapınız!"
            };
        }

        public override IdentityError PasswordRequiresNonAlphanumeric()
        {
            return new IdentityError()
            {
                Code = "PasswordRequiresNonAlphanumeric",
                Description = $"Lütfen en az 1 tane özel karakter girişi yapınız!"
            };
        }

        public override IdentityError PasswordRequiresUniqueChars(int uniqueChars)
        {
            return new IdentityError()
            {
                Code = "PasswordRequiresUniqueChars",
                Description = $"Parola en az {uniqueChars} benzersiz karakter içermelidir."
            };
        }

        public override IdentityError PasswordMismatch()
        {
            return new IdentityError()
            {
                Code = "PasswordMismatch",
                Description = $"Girilen parola hatalıdır, lütfen tekrar deneyiniz!"
            };
        }
        #endregion


        #region username errors
        public override IdentityError DuplicateUserName(string userName)
        {
            return new IdentityError()
            {
                Code = "DuplicateUserName",
                Description = $"{userName} kullanıcı adı sistemde kayıtlıdır, lütfen farklı bir kullanıcı adı deneyiniz!"
            };
        }

        public override IdentityError InvalidUserName(string userName)
        {
            return new IdentityError()
            {
                Code = "InvalidUserName",
                Description = $"{userName} geçerli bir kullanıcı adı formatında değildir, lütfen geçerli bir kullanıcı adı giriniz!"
            };
        }
        #endregion


        #region email errors
        public override IdentityError DuplicateEmail(string email)
        {
            return new IdentityError()
            {
                Code = "DuplicateEmail",
                Description = $"{email} e-posta adresi sistemde kayıtlıdır, lütfen farklı bir e-posta adresi deneyiniz!"
            };
        }

        public override IdentityError InvalidEmail(string email)
        {
            return new IdentityError()
            {
                Code = "InvalidEmail",
                Description = $"{email} geçerli bir e-posta adresi formatında değildir, lütfen geçerli bir e-posta adresi giriniz!"
            };
        }
        #endregion


        #region role errors
        public override IdentityError UserAlreadyInRole(string role)
        {
            return new IdentityError()
            {
                Code = "UserAlreadyInRole",
                Description = $"Kullanıcı zaten {role} rolünde bulunmaktadır!"
            };
        }

        public override IdentityError UserNotInRole(string role)
        {
            return new IdentityError()
            {
                Code = "UserNotInRole",
                Description = $"Kullanıcı {role} rolünde bulunmamaktadır!"
            };
        }

        public override IdentityError InvalidRoleName(string role)
        {
            return new IdentityError()
            {
                Code = "InvalidRoleName",
                Description = $"{role} geçerli bir rol adı formatında değildir, lütfen geçerli bir rol adı giriniz!"
            };
        }

        #endregion


        public override IdentityError ConcurrencyFailure()
        {
            return new IdentityError()
            {
                Code = "ConcurrencyFailure",
                Description = $"Veritabanı işlemi sırasında bir hata oluştu, lütfen tekrar deneyiniz!"
            };
        }

        public override IdentityError DefaultError()
        {
            return new IdentityError()
            {
                Code = "DefaultError",
                Description = $"Kimlik doğrulama işlemi sırasında bir hata oluştu, lütfen tekrar deneyiniz!"
            };
        }
    }
}
