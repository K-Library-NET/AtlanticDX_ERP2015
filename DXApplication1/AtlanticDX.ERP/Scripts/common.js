//datagrid增删改操作
var add_action = function () {
    $('#add_dialog').dialog('open').dialog('setTitle', '添加');
};

var add_row = function () {
    add_action();
};

var add_handler = function () {
    add_action();
};

var edit_action = function (row) {
    $('#edit_dialog').dialog('open').dialog('setTitle', '编辑');
    $('#edit_dialog form').form('load', row);
}

var edit_row = function (index) {
    var row = $('#my_datagrid').datagrid('getRows')[index];
    edit_action(row);
}


var edit_handler = function () {
    var row = $('#my_datagrid').datagrid('getSelected');
    if (row) {
        edit_action(row);
    } else {
        $.messager.alert('提示', '请选择一行数据');
    }
};


var remove_action = function (row) {
    $.messager.confirm('确认', '确定删除吗?', function (r) {
        if (r) {
            var queryString = '1=1';
            for (var key in row) {
                if (!/^\/Date\([0-9]+\)\/$/.test(row[key])) {
                    queryString += '&' + key + '=' + row[key];
                }
            }
            $.ajax({
                url: '/' + Area + '/' + Controller + '/Remove',
                type: 'post',
                data: queryString,
                dataType: 'json',
                beforeSend: function () {
                    $.messager.progress({
                        title: '',
                        msg: '处理中...'
                    });
                },
                success: function (resp) {
                    $.messager.progress('close');
                    var messages = [];
                    for (i = 0; i < resp.length; i++) {
                        for (j = 0; j < resp[i]['Messages'].length; j++) {
                            messages.push(resp[i]['Messages'][j]);
                        }
                    }
                    if (messages.length > 0) {
                        $.messager.alert('提示', messages.join('\n'));
                    } else {
                        $.messager.alert('提示', '删除成功');
                        $('#my_datagrid').datagrid('reload');
                    }
                }
            });
        }
    });
}

var remove_row = function (index) {
    var row = $('#my_datagrid').datagrid('getRows')[index];
    remove_action(row);
}

var remove_handler = function () {
    var row = $('#my_datagrid').datagrid('getSelected');
    if (row) {
        remove_action(row);
    } else {
        $.messager.alert('提示', '请选择一行数据');
    }
};

var datagrid_toolbar = [{
    text: '添加',
    iconCls: 'icon-add',
    handler: add_handler
}, {
    text: '编辑',
    //iconCls: 'icon-edit',
    handler: edit_handler
}, {
    text: '删除',
    //iconCls: 'icon-remove',
    handler: remove_handler
}];

var get_datagrid_row_by_index = function (index) {
    var row = $('#my_datagrid').datagrid('getRows')[index];
    return row;
}

//close dialog
var closeDialog = function (selector) {
    $(selector).dialog('close')
}

//submit form
function submitForm(selector) {
    if ($(selector).form('validate')) {
        $(selector).submit();
    }
}

$.fn.serializeJson = function () {
    var arr = this.serializeArray();
    var json = {};
    for (i = 0; i < arr.length; i++) {
        if (json[arr[i].name] != undefined) {
            json[arr[i].name] = json[arr[i].name] + ',' + arr[i].value;
        } else {
            json[arr[i].name] = arr[i].value;
        }
    }
    return json;
}

function get_current_date() {
    var date = new Date();
    return date.getFullYear() + "-" + (date.getMonth() + 1) + "-" + date.getDate();
}

$(function () {
    jQuery.validator.setDefaults({
        debug: false,
        ignore: '',
        onkeyup: false,
        //onfocusout: false,
        showErrors: function (errorMap, errorList) {
            if (errorList != null && errorList.length > 0) {
                $.messager.alert('提示',errorList[0].message);
            }
        }
    });

    //datagrid选择行事件
    $('#my_datagrid').datagrid({
        onSelect: function (index, row) {
            $('#my_datagrid').datagrid('unselectRow', index);
        }
    });

    //add dialog
    $('#add_dialog').dialog({
        modal: true,
        onClose: function () {
            $('#my_datagrid').datagrid('unselectAll');
            $('#add_dialog form').form('reset');
        }
    });

    //submit add form
    var addValidateOptions = get_validate_rules_messages('#add_dialog form');
    $('#add_dialog form').validate({
        rules: addValidateOptions.rules,
        messages: addValidateOptions.messages,
        submitHandler: function (form) {
            $.ajax({
                url: '/' + Area + '/' + Controller + '/Add',
                type: 'post',
                data: $('#add_dialog form').serializeJson(),
                dataType: 'json',
                beforeSend: function () {
                    $.messager.progress({
                        title: '',
                        msg: '处理中...'
                    });
                },
                success: function (resp) {
                    $.messager.progress('close');
                    var messages = GetModelStateErrors(resp);
                    if (messages.length > 0) {
                        $.messager.alert('提示', messages.join('<br/>'));
                    } else {
                        $.messager.alert('提示', '操作成功');
                        $('#my_datagrid').datagrid('reload');
                        $('#add_dialog').dialog('close');
                    }
                }
            });
            return false;
        }
    });

    //edit dialog
    $('#edit_dialog').dialog({
        modal: true,
        onClose: function () {
            $('#my_datagrid').datagrid('unselectAll');
            $('#edit_dialog form').form('reset');
        }
    });

    //submit edit form
    var editValidateOptions = get_validate_rules_messages('#edit_dialog form');
    $('#edit_dialog form').validate({
        rules: editValidateOptions.rules,
        messages: editValidateOptions.messages,
        submitHandler: function (form) {
            $.ajax({
                url: '/' + Area + '/' + Controller + '/Edit',
                type: 'post',
                data: $('#edit_dialog form').serializeJson(),
                dataType: 'json',
                beforeSend: function () {
                    $.messager.progress({
                        title: '',
                        msg: '处理中...'
                    });
                },
                success: function (resp) {
                    $.messager.progress('close');
                    var messages = GetModelStateErrors(resp);
                    if (messages.length > 0) {
                        $.messager.alert('提示', messages.join('<br/>'));
                    } else {
                        $.messager.alert('提示', '编辑成功');
                        $('#my_datagrid').datagrid('reload');
                        $('#edit_dialog').dialog('close');
                    }
                }
            });
            return false;
        }
    });

    //fiter form
    var filterValidateOptions = get_validate_rules_messages('#filter_form');
    $('#filter_form').validate({
        rules: filterValidateOptions.rules,
        messages: filterValidateOptions.messages,
        submitHandler: function (form) {
            $('#filter_dialog').hide();
            var filters = {};
            var arr = $('#filter_form').serializeArray();
            for (i = 0; i < arr.length; i++) {
                filters[arr[i]['name']] = arr[i]['value'];
            }
            $('#my_datagrid').datagrid('load', filters);
            return false;
        }
    });

});

/*检查一个array是否包含某个值
@return bool
*/
Array.prototype.contains = function (obj) {
    var i = this.length;
    while (i--) {
        if (this[i] === obj) {
            return true;
        }
    }
    return false;
}

/*获取服务端输出的ModelState错误
@return array
*/
function GetModelStateErrors(resp) {
    var messages = [];
    for (i = 0; i < resp.length; i++) {
        for (j = 0; j < resp[i]['Messages'].length; j++) {
            messages.push(resp[i]['Messages'][j]);
        }
    }
    return messages;
}

/*将MVC框架生成的验证信息字段转换成jquery.validate验证*/
function get_validate_rules_messages(form) {
    var rule_names = ['required', 'date', 'number', 'min', 'max'];
    var rules = {};
    var messages = {};
    $('input[data-val-required],input[data-val-date],input[data-val-number],input[data-val-min],input[data-val-max]', $(form)).each(function (j, obj) {
        var name = $(obj).hasClass('easyui-datebox') ? $(obj).attr('textboxname') : $(obj).prop('name');
        if (name != undefined) {
            rules[name] = {};
            messages[name] = {};
            for (i = 0; i < rule_names.length; i++) {
                var msg = $(obj).attr('data-val-' + rule_names[i]);
                if (msg != undefined) {
                    if (rule_names[i] == 'min' || rule_names[i] == 'max') {
                        if ($.isNumeric(msg)) {
                            rules[name][rule_names[i]] = parseInt(msg);
                        }
                    } else {
                        rules[name][rule_names[i]] = true;
                        if (msg != '') {
                            messages[name][rule_names[i]] = msg;
                        }
                    }
                }
            }
        }
    });
    return { rules: rules, messages: messages };

};

// 匹配合同编号，以字母开头，只能包含字符、数字和下划线。
$.extend($.fn.validatebox.defaults.rules, {
    isKey: {
        validator: function (value, param) {
            value = value.toUpperCase();
            return /^[a-zA-Z][a-zA-Z0-9]*$/.test(value);
        },
        message: '以字母开头，只能包含字符、数字和下划线。'
    }
});