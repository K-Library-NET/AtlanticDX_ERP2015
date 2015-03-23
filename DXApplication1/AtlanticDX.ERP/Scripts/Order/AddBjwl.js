var Base = {
    formId: 'bgwl_form',//表单Id
    index: 1,//区分上一步0和下一步1(默认下一步)
    //返回到上一步(合同订单信息)
    toPrev: function () {
        parent.updateMainTab('采购交单', '修改采购合同', '/Orders/OrderContract/Add?=' + $("#").val());
    },
    //关闭当前选项卡,返回合同订单列表
    closeToList: function () {
        closeMainTab('采购交单');
        addMainTab('采购与销售合同', 'Orders/OrderContract/Index');
    },
    //弹出确认框提示保存
    conformSave: function (_index) {
        this.index = _index;
        $.messager.confirm('提示', '是否保存当前页面信息?', function (yes) {
            if (yes) {
                $("#" + formId).submit();
            }
            else {
                Base.jump();
            }
        });
    },
    //页面跳转
    jump: function () {
        if (index == 1) {
            this.closeToList();
        }
        else {
            this.toPrev();
        }
    }
}

$(function () {
    //初始化期货\现货数据        
    $("#lblOrderType").text('@Model.OrderType' == '1' ? '现货' : '期货');
    //绑定上一步事件
    $("#btnPrev").click(function () {
        Base.conformSave(0);
    });
    //绑定下一步事件
    $("#btnNext").click(function () {
        Base.conformSave(1);
    });
    //绑定香港物流公司数据验证(界面选择空值后则不可修改下面对应的表格)
    $("input.num_compute").focus(function () {
        var typename = $(this).attr("name"); //获取当前文本框名称
        //香港物流运费信息
        if (typename.indexOf("HongkongLogistics") >= 0) {
            var HongKongLogisticsCompanyId = $("#HongkongLogistics_HongKongLogisticsCompanyId").val().trim();
            if (HongKongLogisticsCompanyId == "" || HongKongLogisticsCompanyId == "0") {
                $.messager.alert('提示', '请选择香港物流公司!');
                $("#HongkongLogistics_HongKongLogisticsCompanyId").focus();
            }
        }
            //内地物流运费信息
        else {
            var MainlandLogisticsCompanyId = $("#MainlandLogistics_MainlandLogisticsCompanyId").val().trim();
            if (MainlandLogisticsCompanyId == "" || MainlandLogisticsCompanyId == "0") {
                $.messager.alert('提示', '请选择内地物流公司!');
                $("#MainlandLogistics_MainlandLogisticsCompanyId").focus();
            }
        }
    });
    //自动计算运费小计=“收单吨重” * “运费” + “保险”
    $('input.num_compute').on('keyup', function () {
        if (isNaN($(this).val())) {
            $(this).val('').focus();
        } else {
            var tr = $(this).parent().parent();
            var tds = tr.find("input:text");
            var dz = parseFloat(tds.eq(1).val());
            var yf = parseFloat(tds.eq(2).val());
            var bx = parseFloat(tds.eq(3).val());
            var total = 0;
            if (!isNaN(dz) && !isNaN(yf)) {
                total += dz * yf;
            }
            if (!isNaN(bx)) {
                total += bx;
            }
            tds.last().val(total.toFixed(2));
        }
    });

    //初始化表单验证项信息
    var validateOptions = get_validate_rules_messages('#' + formId);
    //表单提交            
    $('#' + formId).validate({
        rules: validateOptions.rules,
        messages: validateOptions.messages,
        submitHandler: function (form) {
            //发送请求
            $.ajax({
                url: $('#' + formId).prop('action'),
                data: $(form).serialize(),
                type: 'post',
                cache: false,
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
                        $.messager.alert('提示', '添加成功', function () {
                            Base.jump();
                        });
                    }
                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    $.messager.progress('close');
                    $.messager.alert('提示', '保存出错!');
                }
            });
        }
    });
});