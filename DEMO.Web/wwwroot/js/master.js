var loc = window.location.pathname;
var dir = loc.substring(0, loc.lastIndexOf('/'));
var DomainUrl = "";
var aaaaa = dir.split('/');
for (var i = 3; i < aaaaa.length; i++) {
    DomainUrl = DomainUrl + '../'
}

function fn_loadselect() {
    var url = window.location.pathname.split('/').pop().toLowerCase().replace('.aspx', '');

    $('input').attr('autocomplete', 'off');
    $('input[type="date"]').val(new Date().toDateInputValue());
    if (url != 'services') {
        $(".x_select2").select2();
    }
}
Date.prototype.toDateInputValue = (function () {
    var local = new Date(this);
    local.setMinutes(this.getMinutes() - this.getTimezoneOffset());
    return local.toJSON().slice(0, 10);
});
function GetdateTime(jsonWcfDate) {
    if (jsonWcfDate == undefined) {
        return;
    }
    var month_names_short = ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec'];
    var myDate = new Date(parseInt(jsonWcfDate.replace(/\/+Date\(([\d+-]+)\)\/+/, '$1')));
    var ddd = myDate.getDate() + '-' + month_names_short[myDate.getMonth()] + '-' + myDate.getFullYear() + ' ' + myDate.toLocaleTimeString();
    return ddd;
}
function _LoadData() {
    var url = window.location.pathname.split('/').pop().toLowerCase().replace('.aspx', '');
    var addon = ['dashboard', 'fundtransfer', 'cy_txnreport']
    var vv = addon.indexOf(url);
    if (vv >= 0) {
        var ddd = new Date();
        var script = document.createElement('script');
        script.src = "https://code.jquery.com/ui/1.13.1/jquery-ui.js";
        script.async = false;
        document.body.appendChild(script);
        var script = document.createElement('script');
        script.src = (addon[vv] + '.js?v=' + ddd.getDay());
        script.async = false;
        document.body.appendChild(script);
    }
}


function GetMenus() {
    var SPageUrl = window.location.href;
    var l = '<li class=""><a class="has-arrow ai-icon" href="#" aria-expanded="false"><i class="flaticon-050-info"></i><span class="nav-text">Apps</span></a><ul aria-expanded="false" class="mm-collapse" style="height: 16px;"><li><a href="app-profile.html">Profile</a></li><li><a href="post-details.html">Post Details</a></li<li><a class="has-arrow" href="javascript:void()" aria-expanded="false">Email</a><ul aria-expanded="false" class="mm-collapse left"><li><a href="email-compose.html">Compose</a></li><li><a href="email-inbox.html">Inbox</a></li><li><a href="email-read.html">Read</a></li></ul></li><li><a href="app-calender.html">Calendar</a></li><li><a class="has-arrow" href="javascript:void()" aria-expanded="false">Shop</a><ul aria-expanded="false" class="mm-collapse left"><li><a href="ecom-product-grid.html">Product Grid</a></li><li><a href="ecom-product-list.html">Product List</a></li><li><a href="ecom-product-detail.html">Product Details</a></li><li><a href="ecom-product-order.html">Order</a></li><li><a href="ecom-checkout.html">Checkout</a></li><li><a href="ecom-invoice.html">Invoice</a></li><li><a href="ecom-customers.html">Customers</a></li></ul></li></ul></li>';
    $("#menu>li:gt(1)").remove();
    $.ajax({
        type: "POST",
        url: DomainUrl + 'Dashboard.aspx/GetMenus',
        data: JSON.stringify({}),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            response = JSON.parse(response.d);
            if (response.length > 0) {
                $("#menu").append(response[0].menu)
                $('#menu').metisMenu('dispose').metisMenu()
            }
        }
    });
}
function isDeciamlKey(evt, element) {
    var charCode = (evt.which) ? evt.which : event.keyCode
    if (charCode > 31 && (charCode < 48 || charCode > 57) && !(charCode == 46 || charCode == 8))
        return false;
    else {
        var len = $(element).val().length;
        var index = $(element).val().indexOf('.');
        if (index > 0 && charCode == 46) {
            return false;
        }
        if (index > 0) {
            var CharAfterdot = (len + 1) - index;
            if (CharAfterdot > 6) {
                return false;
            }
        }
    }
    return true;
}
function validateEmail(email) {
    var re = /\S+@\S+\.\S+/;
    return re.test(email);
}
function onchange_export(evnt) {
    debugger;
    var href = document.location.href;
    var pageName = href.substr(href.lastIndexOf('/') + 1);
    pageName = pageName.replace('.aspx','')
    if (evnt == "")
        tableToExcel('example', 'Sheet1');
    else
        tableToExcel(evnt, pageName);

}
var tableToExcel = (function () {
    var uri = 'data:application/vnd.ms-excel;base64,'
        , template = '<html xmlns:o="urn:schemas-microsoft-com:office:office" xmlns:x="urn:schemas-microsoft-com:office:excel" xmlns="http://www.w3.org/TR/REC-html40"><head><!--[if gte mso 9]><xml><x:ExcelWorkbook><x:ExcelWorksheets><x:ExcelWorksheet><x:Name>{worksheet}</x:Name><x:WorksheetOptions><x:DisplayGridlines/></x:WorksheetOptions></x:ExcelWorksheet></x:ExcelWorksheets></x:ExcelWorkbook></xml><![endif]--></head><body><table>{table}</table></body></html>'
        , base64 = function (s) { return window.btoa(unescape(encodeURIComponent(s))) }
        , format = function (s, c) { return s.replace(/{(\w+)}/g, function (m, p) { return c[p]; }) }
    return function (table, name) {
        if (!table.nodeType) table = document.getElementById(table)
        var ctx = { worksheet: "abc" || 'Worksheet', table: CheckhyperLink(table.innerHTML) }
        var a = document.createElement('a');
        a.href = uri + base64(format(template, ctx))
        a.download = name + '.xls';
        //triggering the function
        a.click();
        //window.location.href = uri + base64(format(template, ctx))
    }
})();
function CheckhyperLink(str) {
    var a = ''
    $.each($(str), function (index, value) {
        a = a + $($(value).find('a').remove().end()).html();
    });
    return a;
}
function LoadTables() {

    $.each($('.tablesorter'), function (i, v) {
        var ObjData = v;
        if ($(ObjData).is('table') && $(ObjData).find('thead').length == 0) {
            var id = $(v).attr('id');
            $(ObjData).prepend("<thead></thead>");
            $(ObjData).find('tbody tr:first').remove().appendTo($(ObjData).find('thead'));
            var table = $(v).DataTable({
               /* aLengthMenu: [[10, 100, 1000, 5000, 10000, 50000, -1], [10, 100, 1000, 5000, 10000, 50000, "All"]],*/
                aLengthMenu: [[10, 25, 50, 100, 200,250, 500, 1000, 2000, 2500, 5000, -1], [10, 25, 50, 100, 200,250, 500, 1000, 2000, 2500, 5000, "All"]],
                iDisplayLength: 10, initComplete: function (settings, json) {
                    var a = '<a class="exportbtn" onclick="onchange_export(\'' + id + '\')" title="Export"><i class="fas fa-file-excel"></i></a>';
                    $(document).find(".dataTables_filter").append(a);
                }, language: {
                    paginate: {
                        next: '&#8594;', // or '→'
                        previous: '&#8592;' // or '←' 
                    }
                }
            });
            table.draw('page');
        }
    })
}
var req = Sys.WebForms.PageRequestManager.getInstance();
req.add_beginRequest(function () {
    LoadTables();
});
req.add_endRequest(function () {
    LoadTables();
});
window.addEventListener('load', function () {
    LoadTables();
});
document.onreadystatechange = function () {
    if (document.readyState === "complete") {

    }
    else {
        window.onload = function () {
            LoadTables();
        }
    };
};
function isNumber(evt) {
    evt = (evt) ? evt : window.event;
    var charCode = (evt.which) ? evt.which : evt.keyCode;
    if (charCode > 31 && (charCode < 48 || charCode > 57)) {
        return false;
    }
    return true;
};

function GETNOTIFICATION() {
    $("#_Notification").empty();
    $.ajax({
        type: "POST",
        url: DomainUrl + 'Dashboard.aspx/GetNotification',
        data: JSON.stringify({ "METHOD": "GET50" }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            response = JSON.parse(response.d);
            if (response.length > 0) {
                var cnt = response.length;;
                $("#lnk_notificationCount").text((cnt > 10 ? '9+' : cnt));
                $.each(response, function (i, v) {
                    var x = '<li><div class="d-flex bd-highlight"><div class="user_info" onclick="updateNotificationCount(' + v.id + ');"><span></span><span class="notification_date">' + GetdateTime(v.adddate) + '</span><p>' + v.message + '</p></div></div></li>';
                    $("#_Notification").append(x);
                });
                
            }
        }
    });
}
function updateNotificationCount(ID) {
    debugger;
    $.ajax({
        type: "POST",
        url: DomainUrl + 'Dashboard.aspx/updateNotificationCount',
        data: JSON.stringify({ "METHOD": "seenupdate", "ID": ID }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            response = JSON.parse(response.d);
            if (response.length > 0) {
                var cnt = response.length;;
                $("#lnk_notificationCount").text((cnt > 10 ? '0' : cnt));
            
            }
        
        }
    });
}

function fn_AddFavIcon() {
    var path = window.location.pathname;
    var x = path.split("/").pop();
    $.ajax({
        type: "POST",
        url: DomainUrl + 'Dashboard.aspx/AddFavLink',
        data: JSON.stringify({ "PAGENAME": x, "Method": "INSERT", "Id": "1" }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            response = JSON.parse(response.d);
            if (response.length > 0) {
                alert('Success');
            }
        }
    });
};
function LogOut() {
    var SPageUrl = window.location.href;
    $.ajax({
        type: "POST",
        url: '../../b2c.aspx/LogOut',
        data: JSON.stringify({}),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            response = response.d;
            if (response == 'Success') {
                localStorage.removeItem('UserInfo');
                localStorage.removeItem('LoginInfo');
                window.location.href = '../memberlogin.aspx?Message=Logout Successfully';
            }
        }
    });
}
function Dialog_Alert(msg) {
    show_alert(msg);
};
function ShowAlert(msg, msgtype) {
    Dialog_Alert(msg);
};
function validateFloatKeyPress(el, evt) {

    var charCode = (evt.which) ? evt.which : event.keyCode;
    if (charCode != 46 && charCode > 31
        && (charCode < 48 || charCode > 57)) {
        return false;
    }

    if (charCode == 46 && el.value.indexOf(".") !== -1) {
        return false;
    }

    return true;
}
