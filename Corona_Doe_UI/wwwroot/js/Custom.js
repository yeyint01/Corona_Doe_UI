var cursubmenu = "start";
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
        //$('#' + input).focus();
        var id = document.getElementById(input);
        if (id) {
            document.getElementById(input).focus();
        }
        
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

    },
    ShowPassword: function (input, icon) {
        var type = $("#" + input).attr('type');
        if (type === 'password') {
            $("#" + input).attr('type', 'text');
            $('#' + icon).attr('class', 'eye-icon fa fa-eye-slash')
        } else {
            $("#" + input).attr('type', 'password');
            $('#' + icon).attr('class', 'eye-icon  fa fa-eye');
        }
    },

    autosuggest: function (id, arr) {
        /*the autocomplete function takes two arguments,
        the text field element and an array of possible autocompleted values:*/
        var currentFocus;
       
        var inp = document.getElementById(id);

        inp.addEventListener("input", function (e) {
            var a, b, i, val = this.value;
            /*close any already open lists of autocompleted values*/
            closeAllLists();
            if (!val) { return false; }
            currentFocus = -1;
            /*create a DIV element that will contain the items (values):*/
            a = document.createElement("DIV");
            a.setAttribute("id", this.id + "autocomplete-list");
            a.setAttribute("class", "autocomplete-items");
            /*append the DIV element as a child of the autocomplete container:*/
            this.parentNode.appendChild(a);
            /*for each item in the array...*/
            for (i = 0; i < arr.length; i++) {
                /*check if the item starts with the same letters as the text field value:*/
                if (arr[i].substr(0, val.length).toUpperCase() == val.toUpperCase()) {
                    /*create a DIV element for each matching element:*/
                    b = document.createElement("DIV");
                    /*make the matching letters bold:*/
                    b.innerHTML = "<strong>" + arr[i].substr(0, val.length) + "</strong>";
                    b.innerHTML += arr[i].substr(val.length);
                    /*insert a input field that will hold the current array item's value:*/
                    b.innerHTML += "<input type='hidden' value='" + arr[i] + "'>";
                    /*execute a function when someone clicks on the item value (DIV element):*/
                    b.addEventListener("click", function (e) {
                        /*insert the value for the autocomplete text field:*/
                        inp.value = this.getElementsByTagName("input")[0].value;
                        /*close the list of autocompleted values,
                        (or any other open lists of autocompleted values:*/
                        closeAllLists();
                    });
                    a.appendChild(b);
                }
            }
        });
        /*execute a function presses a key on the keyboard:*/
        inp.addEventListener("keydown", function (e) {
            var x = document.getElementById(this.id + "autocomplete-list");
            if (x) x = x.getElementsByTagName("div");
            if (e.keyCode == 40) {
                /*If the arrow DOWN key is pressed,
                increase the currentFocus variable:*/
                currentFocus++;
                /*and and make the current item more visible:*/
                addActive(x);
            } else if (e.keyCode == 38) { //up
                /*If the arrow UP key is pressed,
                decrease the currentFocus variable:*/
                currentFocus--;
                /*and and make the current item more visible:*/
                addActive(x);
            } else if (e.keyCode == 13) {
                /*If the ENTER key is pressed, prevent the form from being submitted,*/
                e.preventDefault();
                if (currentFocus > -1) {
                    /*and simulate a click on the "active" item:*/
                    if (x) x[currentFocus].click();
                }
            }
        });
        function addActive(x) {
            /*a function to classify an item as "active":*/
            if (!x) return false;
            /*start by removing the "active" class on all items:*/
            removeActive(x);
            if (currentFocus >= x.length) currentFocus = 0;
            if (currentFocus < 0) currentFocus = (x.length - 1);
            /*add class "autocomplete-active":*/
            x[currentFocus].classList.add("autocomplete-active");
        }
        function removeActive(x) {
            /*a function to remove the "active" class from all autocomplete items:*/
            for (var i = 0; i < x.length; i++) {
                x[i].classList.remove("autocomplete-active");
            }
        }
        function closeAllLists(elmnt) {
            /*close all autocomplete lists in the document,
            except the one passed as an argument:*/
            var x = document.getElementsByClassName("autocomplete-items");
            for (var i = 0; i < x.length; i++) {
                if (elmnt != x[i] && elmnt != inp) {
                    x[i].parentNode.removeChild(x[i]);
                }
            }
        }
        /*execute a function when someone clicks in the document:*/
        document.addEventListener("click", function (e) {
            closeAllLists(e.target);
        });
    },

    clearValue: function (id) {
        document.getElementById(id).value = '';
    },
    splitNrc: function (nrcValue) {
        var stateNo = nrcValue.slice(0, nrcValue.indexOf("/") + 1);
        var district = nrcValue.slice(nrcValue.indexOf("/") + 1, nrcValue.indexOf("("));
        var naing = nrcValue.slice(nrcValue.indexOf("("), nrcValue.indexOf(")") + 1);
        var registerNo = nrcValue.slice(nrcValue.indexOf(")") + 1, nrcValue.length);

        return { sno: stateNo, dis: district, na: naing, regno: registerNo };
    }
};