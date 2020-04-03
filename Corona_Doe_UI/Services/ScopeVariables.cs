using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Corona_Doe_UI.Services
{
    public class ScopeVariables
    {
        public bool IsMobile;
        public bool IsTablet;

        private bool isLoading; //for lodaing
        public bool IsLoading
        {
            get { return isLoading; }
            set
            {
                isLoading = value;
                LoadingEvent?.Invoke();
            }
        }

        public Action LoadingEvent;

        #region UI Icon

        public string FaSave = "fa fa-save";
        public string FaEdit = "fa fa-pencil";
        public string FaClose = "fa fa-close";
        public string FaSignOut = "fa fa-sign-out";
        public string FaTrash = "fa fa-trash"; // For Delete btn
        public string FaBan = "fa fa-ban";
        public string dateformat = "dd-MMM-yyyy";
        public string datetimeformat = "yyyy-MM-ddTHH:mm";

        #endregion UI Icon
    }
}
