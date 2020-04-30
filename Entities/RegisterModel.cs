using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BlogApp.Entities
{
    public class RegisterModel
    {

        [Required(ErrorMessage = "Ad  giriniz.")]
        [DisplayName("Ad")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Soyad  giriniz.")]
        [DisplayName("Soyad")]
        public string Surname { get; set; }

        [Required(ErrorMessage = "Kullanıcı adı giriniz.")]
        [DisplayName("Kullanıcı Adı")]
        public string Username { get; set; }

        [DisplayName("Fotoğraf")]
        public string UserImage { get; set; }

        [Required(ErrorMessage = "Eposta giriniz.")]
        [DisplayName("E-posta")]
        [EmailAddress(ErrorMessage ="Eposta adresi doğru değil.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Şifre  giriniz.")]
        [DisplayName("Şifre")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Şifre onay giriniz.")]
        [DisplayName("Şifre Tekrar")]
        [Compare("Password",ErrorMessage ="Şifreler uyuşmuyor!")]
        public string RePassword { get; set; }

    }
}