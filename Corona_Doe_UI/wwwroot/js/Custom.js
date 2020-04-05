var cursubmenu ="start";
window.CustomJsFunctions = {
    openModal: function (el) {
        modal = document.getElementById(el);

        if (modal) {
            modal.style.display = 'block';
            mainBody = document.getElementById('main-body');
            if (mainBody) {
                mainBody.style.overflow = 'hidden';
            }
        }
    },
    closeModal: function (el) {
        modal = document.getElementById(el);

        if (modal) {
            modal.style.display = 'none';
        }
    },
    checkModal: function () {
        allmodal = document.getElementsByClassName('modal-body');

        if (allmodal.length <= 0) {
            mainBody = document.getElementById('main-body');
            if (mainBody) {
                mainBody.style.overflow = 'auto';
            }
            return false;
        }
        else {
            return true;
        }
    },
    autofocus: function (input) {
        $('#' + input).focus();
    },
    toast: function (id, text) {
        $('#' + id).toast('show');
        var span = document.createElement('span');
        span.innerText = text;
        if ($('.toast-body').find('span')) $('.toast-body').find('span').remove();
        $('.toast-body').append(span);
    },

    toastarray: function (id, arr) {
        $('#' + id).toast('show');
        var span = document.createElement('span');
        for (var i = 0; i < arr.length; i++) {
            var iDiv = document.createElement('div');
            iDiv.innerText = arr[i];
            span.appendChild(iDiv);
        }
        if ($('.toast-body').find('span')) $('.toast-body').find('span').remove();
        $('.toast-body').append(span);
    },
    getWindowScreen: function () {
        return { height: window.innerHeight, width: window.innerWidth };
    },
    registerResizeCallback: function () {
        window.addEventListener('resize', CustomJsFunctions.resized);
    },
    resized: function () {
        DotNet.invokeMethodAsync('Corona_Doe_UI', 'OnBrowserResize').then(data => data);
    },
    tableWidth: function (el) {
        tblwidth = document.getElementById(el);
        if (tblwidth) {
            wth = tblwidth.clientWidth;
            return wth;
        } else return window.innerWidth - 30;
    },
    setGridHeight: function (apiName) {
        modalHeight = window.innerHeight - 220;
        header = document.getElementById('app-header');
        toolbar = document.getElementById(apiName + '-my-toolbar');
        if (toolbar && header) {
            return window.innerHeight - (30 + document.getElementById('app-header').clientHeight + toolbar.clientHeight);
        } else {
            if (header) {
                return window.innerHeight - (30 + document.getElementById('app-header').clientHeight);
            }
        }
        return 500; //for temporariy
    },

    openSubMenu: function (id) {
        var x = document.getElementById(id);
        //if (cursubmenu != "start" && id == "covid19") {
        //    document.getElementById(cursubmenu).style.display = "none";
        //}
        //else
            if (cursubmenu != "start" && cursubmenu != id) {
                document.getElementById(cursubmenu).style.display = "none";
        }
        if (x) {
            if (x.style.display === "none") {
                x.style.display = "block";
                cursubmenu = id;
            } else {
                x.style.display = "none";
            }
        }
        
        
    }
};