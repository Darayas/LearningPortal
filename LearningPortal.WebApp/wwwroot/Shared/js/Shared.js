function SendForm(_Url, _FormId, _Callback = function (data) { }) {
    var _Form = $('#' + _FormId)[0];
    var _FormData = new FormData(_Form);

    $.ajax({
        type: 'Post',
        enctype: 'multipart/form-data',
        url: _Url,
        data: _FormData,
        processData: false,
        contentType: false,
        cache: false,
        timeout: 60000,
        beforeSend: function (xhr) {
            var securityToken = $('[name=__RequestVerificationToken]').val();
            xhr.setRequestHeader('XSRF-TOKEN', securityToken);
        },
        success: function (data) {
            _Callback(data);
        },
        complete: function (data) {

        },
        error: function (err) {

        }
    });
}

function SendData(_Url, _Data, _Callback = function (data) { }) {
    $.ajax({
        type: 'Post',
        enctype: 'multipart/form-data',
        url: _Url,
        data: _Data,
        timeout: 60000,
        beforeSend: function (xhr) {
            var securityToken = $('[name=__RequestVerificationToken]').val();
            xhr.setRequestHeader('XSRF-TOKEN', securityToken);
        },
        success: function (data) {
            _Callback(data);
        },
        complete: function (data) {

        },
        error: function (err) {

        }
    });
}


function LoadComponent(_Url, _Data, _CallbackFuncs = function (data) { }, _EnableLoading = true) {
    $.ajax({
        url: _Url,
        type: 'get',
        data: _Data,
        beforeSend: function (xhr) {
            if (_EnableLoading)
                $('.loading').show();
        },
        complete: function (data) {
            if (_EnableLoading)
                $('.loading').hide(100);
        },
        error: function (data) {
            if (data.status == 429) {
                Alert429();
            }
        }
    }).done(function (data) {
        _CallbackFuncs(data);
    });
}

function Alert429() {
    return swal.fire({
        title: '429',
        html: $.parseHTML(Err429Msg)[0].data,
        icon: 'warning',
        confirmButtonText: OkText
    });
}

function Logout() {
    SendData('/fa/Logout', {});
}

function ReloadPage() {
    location.reload();
}
function forgeryToken() {
    return kendo.antiForgeryTokens()
}