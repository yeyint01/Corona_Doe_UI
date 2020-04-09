using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using static System.Net.Mime.MediaTypeNames;
using e = Entity.shared;

namespace Corona_Doe_UI.Services
{
    public class Functions
    {
        private (string OrderBy, e.SortOrder Order) CurSortInfo;
        public bool IsValidASCII(string data)
        {
            var vaildASCII = true;
            vaildASCII = data.ToCharArray().All(a => a >= 32 && a <= 126);

            return vaildASCII;
        }

        public bool IsEmailValid(string email)
        {
            Regex regex = new Regex(@"^(?("")("".+?(?<!\\)""@)|(([0-9a-zA-Z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-zA-Z])@))" +
                @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-zA-Z][-0-9a-zA-Z]*[0-9a-zA-Z]*\.)+[A-Za-z0-9][\-A-Z-a-z0-9]{0,22}[A-Za-z0-9]))$");

            return regex.IsMatch(email.ToString().Trim());
        }
        public (string OrderBy, e.SortOrder Order) CurrentSortInfo(string orderBy)
        {
            if (CurSortInfo.OrderBy != null)
            {
                if (CurSortInfo.OrderBy != orderBy) // col changed
                    CurSortInfo = (orderBy, e.SortOrder.Ascending);
                else // order changed
                {
                    if (CurSortInfo.Order == e.SortOrder.Ascending)
                        CurSortInfo = (orderBy, e.SortOrder.Descending);
                    else if (CurSortInfo.Order == e.SortOrder.Descending)
                        CurSortInfo = (null, e.SortOrder.Ascending);
                }
            }
            else
                CurSortInfo = (orderBy, e.SortOrder.Ascending); // e.SortOder.Ascending does not use in backend

            return CurSortInfo;
        }

        public string GetBase64StringFromServer(string img)
        {
            var bytes = File.ReadAllBytes(img);
            return "data:image/jpeg;base64," + Convert.ToBase64String(bytes);
        }

        public int PageNo(int curPage, int rowCount, int index)
        {
            if (curPage == 1)
            {
                return index;
            }
            else
            {
                return rowCount * (curPage - 1) + index;
            }
        }
        public bool Validation(EditContext editContext, object propValue, string propName)
        {
            var vcontext = new ValidationContext(editContext.Model)
            {
                MemberName = propName
            };
            var result = new List<ValidationResult>();
            bool isvalid = Validator.TryValidateProperty(propValue, vcontext, result);
            editContext.NotifyFieldChanged(new FieldIdentifier(editContext.Model, propName));
            return isvalid;
        }

        public IEnumerable<string> NRCStateNumbers()
        {
            IEnumerable<string> nrcchar = new List<string> { "1/", "2/","3/",
                        "4/", "5/","6/","7/","8/","9/","10/","11/","12/","13/","14/" };
            return nrcchar;

        }
        public IEnumerable<string> NRCNaing()
        {
            IEnumerable<string> nrcchar = new List<string> { "(N)", "(E)",
                        "(P)", "(A)","F","TH","G" };
            return nrcchar;
        }

        public IEnumerable<string> NRCDistricts()
        {
            IEnumerable<string> nrcchar = new List<string> { "AHGAYA", "BAMANA", "DAPHAYA","HAPANA"
            ,"KAMANA","KAMATA","KAPATA","KHABADA","KHAPHANA","LAGANA","MAKANA","MAKATA","MAKHABA","MALANA"
            ,"MANYAMA","MASANA","NAMANA","PANADA","PATAAH","PAWANA","SABATA","SADANA","SALANA","SAPABA","TANANA","WAMANA","YABAYA"
            ,"YAKANA","MALANA","MAMANA","MANYANA","NAMANA","PANADA","PATAAH","PAWANA","SABATA","SADANA","SALANA","SAPABA","TANANA","WAMANA"};
            return nrcchar;
        }

        public string ValidatePassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
            {
                return "Please fill password.";
            }
            else if (!IsValidASCII(password))
            {
                return "Please fill password only english characters.";
            }
            else if (password.Length < 6)
            {
                return "Password must be at least 6 characters long.";
            }
            else if (!password.Any(c => char.IsLetter(c)))
            {
                return "Password must have at least one non alphanumeric character.";
            }
            else if (!password.Any(c => char.IsDigit(c)))
            {
                return "Password must have at least one digit ('0'-'9')";
            }
            else
            {
                return null;
            }
        }
        public IEnumerable<string> Township()
        {
            IEnumerable<string> townships = new List<string>{
                     "Yangon","Mandalay","Naypyidaw", "Taunggyi", "Mawlamyine",
                     "Bago","Myitkyina", "Monywa","Pathein","Pyay","Myeik",
                     "Meiktila", "Pakokku", "Taungoo","Sittwe", "Magway","Myingyan",
                     "Thanlyin","Hinthada","Sagaing","Dawei", "Mogok","Shwebo","Nyaunglebin",
                     "Bhamo", "Aunglan","Yenangyaung","Bogale" ,"Hlegu" ,"Minbu" ,"Tharrawaddy",
                     "Hakha" , "Thayet"};
            return townships;
        }

    }
}

