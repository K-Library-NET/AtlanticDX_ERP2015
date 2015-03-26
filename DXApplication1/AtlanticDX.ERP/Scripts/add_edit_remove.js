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

$(function () {
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
    $('#add_dialog form').submit(function () {
        if (!$(this).valid()) return false;
        $.ajax({
            url: '/' + Area + '/' + Controller + '/Add',
            type: 'post',
            data: $(this).serialize(),
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
                    $.messager.alert('提示', messages.join('<br/>'));
                } else {
                    $.messager.alert('提示', '添加成功');
                    $('#my_datagrid').datagrid('reload');
                    $('#add_dialog').dialog('close');
                }
            }
        });
        return false;
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
    $('#edit_dialog form').submit(function () {
        //if (!$(this).valid()) return false;
        $.ajax({
            url: '/' + Area + '/' + Controller + '/Edit',
            type: 'post',
            data: $(this).serialize(),
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
    });

    //fiter form
    $('#filter_form').submit(function () {
        $('#my_datagrid').datagrid('load',$(this).serialize());
    });

});

/*工具*/

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
function GetValidateRulesAndMessages(form) {
    var REQUIRED_ATTR = 'data-val-required';
    var DATE_ATTR = 'data-val-date';
    var NUMBER_ATTR = 'data-val-number';
    var rules = {};
    var messages = {};
    $('input[' + REQUIRED_ATTR + '],input[' + DATE_ATTR + ']', $(form)).each(function (j, obj) {
        var name = $(obj).hasClass('easyui-datebox') ? $(obj).attr('textboxname') : $(obj).prop('name');
        if (name != undefined) {
            var requiredMsg = $(obj).attr(REQUIRED_ATTR);
            var dateMsg = $(obj).attr(DATE_ATTR);
            var numberMsg = $(obj).attr(NUMBER_ATTR);
            rules[name] = {};
            messages[name] = {};
            if (requiredMsg != undefined && requiredMsg != '') {
                rules[name]['required'] = true;
                messages[name]['required'] = requiredMsg;
            }
            if (dateMsg != undefined && dateMsg != '') {
                rules[name]['date'] = true;
                messages[name]['date'] = dateMsg;
            }
            if (numberMsg != undefined && numberMsg != '') {
                rules[name]['number'] = true;
                messages[name]['number'] = numberMsg;
            }
        }
    });
    return { rules: rules, messages: messages };
    
};