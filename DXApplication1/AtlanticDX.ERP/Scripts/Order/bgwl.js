$(function () {   
    //绑定香港物流公司数据验证(界面选择空值后则不可修改下面对应的表格)
    $(".gkdl").on('focus', 'input.num_compute', function () {
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
    $(".gkdl").on('keyup', 'input.num_compute', function () {
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

    isOrder = true; //采购订单
    $(function () {
        //绑定自动计算
        $('#PaymentTotal').on('change', compute_ImportBalancedPayment);
        $('#ImportDeposite').on('change', compute_ImportBalancedPayment);

        //计算采购尾款默认值
        function compute_ImportBalancedPayment() {
            var balancedPayment = 0;
            var total = parseFloat($('#PaymentTotal').val());
            var deposite = parseFloat($('#ImportDeposite').val());
            if ($.isNumeric(total) && $.isNumeric(deposite) && (total - deposite) > 0) {
                balancedPayment = (total - deposite).toFixed(2);
            }
            $('#ImportBalancedPayment').val(balancedPayment);
        }

        //绑定合同编号变化事件
        $('#ContractKey').textbox({
            validType: 'isKey',
            //转换为大写
            onChange: function (newValue, oldValue) {
                $('#ContractKey').textbox('setValue', newValue.toUpperCase());
            }
        });
        //提交
        var validateOptions = get_validate_rules_messages('#add_order_form');
        $('#add_order_form').validate({
            rules: validateOptions.rules,
            messages: validateOptions.messages,
            submitHandler: function (form) {
                var isPass = false;
                if ($('#ContractKey').val().trim() == '') {
                    $.messager.alert('提示', '合同编号不能为空!');
                    return isPass;
                }
                //汇率验证
                //if ($('#ContractKey').val().trim() == '') {
                //    $.messager.alert('提示', '合同编号不能为空!');
                //    return isPass;
                //}
                //检查已选商品列
                if (picked_productkeys_ids.length == 0) {
                    $.messager.alert('提示', '未选择商品');
                }
                else {
                    $('#picked_products_table tbody tr').each(function (index, obj) {
                        if ($(obj).find('td').eq(1).text() == '') {
                            $.messager.alert('提示', '已选商品列表存在空行');
                        } else {
                            $inputs = $(obj).find(':text');
                            for (i = 0; i < $inputs.length; i++) {
                                var isHkLogistics = $inputs.eq(i).prop('name').indexOf('HongkongLogistics') >= 0;
                                var isMlLogistics = $inputs.eq(i).prop('name').indexOf('MainlandLogistics') >= 0;
                                var needHkLogistics = $('#add_order_form select[name="HongkongLogistics.HongKongLogisticsCompanyId"]').val() != '';
                                var needMlLogistics = $('#add_order_form select[name="MainlandLogistics.MainlandLogisticsCompanyId"]').val() != '';
                                if (isHkLogistics && !needHkLogistics) {

                                } else if (isMlLogistics && !needMlLogistics) {

                                }
                                else if ($inputs.eq(i).val() == '' || isNaN($inputs.eq(i).val())) {
                                    var labelObj = $('#picked_products_table thead th:nth-child(' + ($inputs.eq(i).parent().index() + 1) + ')');
                                    var labelText = labelObj.find('label').size() == 1 ? $.trim(labelObj.find('label').text()) : labelObj.text()
                                    $.messager.alert('提示', '请正确填写' + labelText);
                                    $inputs.eq(i).focus();
                                    return false;
                                }
                            }
                            isPass = true;
                        }
                    });
                    if (isPass && $("#Payment").val().trim() == '') {
                        isPass = false;
                        $.messager.alert('提示', '请正确填写付款方式');
                    }
                    if (!isPass) {
                        return isPass;
                    }
                    //发送请求
                    $.ajax({
                        url: $('#add_order_form').prop('action'),
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
                                $.messager.alert('提示', '保存成功', 'info', function () {
                                    parent.closeMainTab();
                                    parent.updateMainTab('采购合同', '采购合同', '/Orders/OrderContract/Index');
                                });

                            }
                        }
                    });
                }
            }
        });
    });

    $(function () {
        $('select[name="HarborAgent.HarborAgentId"]').change(function () {

        });
        $('select[name="HongkongLogistics.HongKongLogisticsCompanyId"]').change(function () {

        });
        $('select[name="MainlandLogistics.MainlandLogisticsCompanyId"]').change(function () {

        });
    });
});